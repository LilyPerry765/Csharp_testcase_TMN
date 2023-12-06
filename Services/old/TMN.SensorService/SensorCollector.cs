using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Timers;
using Thread = System.Threading.Thread;
using TMN;

namespace TMN
{
    class SensorCollector
    {

        private const short SECONDS = 1000;
        System.IO.Ports.SerialPort port;

        // SerialPortEmulator port;
        public event Action<KeyValuePair<byte, double>> ModuleDataReceived;
        private static object portAccessLock = new object();
        private States state;
        private byte leftDigit, rightDigits, currentModuleIndex;
        private static byte[] modules;
        private bool IsCurrentDataInvalid;
        public bool IsEnabled;
        private Timer watchDogTimer = new Timer(1 * SECONDS);
        private DateTime lastAliveTime;
        private int validDataCount = 0;
        private static int instanceCount = 0;
        private bool stopSignalReceived;

        public SensorCollector()
        {
            try
            {
                Logger.WriteDebug("SensoreCollector Instances: {0}", ++instanceCount);
                CreatePort();
                modules = DB.Instance.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID).Select(s => (byte)s.ModulNumber.Value).OrderBy(m => m).ToArray();
                Logger.WriteInfo("Loaded modules: {0}", string.Join(",", modules));
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            watchDogTimer.Elapsed -= new ElapsedEventHandler(watchDogTimer_Elapsed);
            watchDogTimer.Elapsed += new ElapsedEventHandler(watchDogTimer_Elapsed);
        }

        private void CreatePort()
        {
            port = port = new System.IO.Ports.SerialPort();
            // port = new SerialPortEmulator();
        }

        void watchDogTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread.CurrentThread.Name = "Watchdog Timer";
            WatchDog();
        }

        private void WatchDog()
        {
            try
            {
                watchDogTimer.Stop();
                Logger.WriteIf(StartArgs.ShowWatchDogLogs, LogType.Debug, "WatchDog State: {0}", IsAlive ? "Alive" : "Dead");
                //  Logger.WriteInfo("WatchDog: IsAlive:{0}, CurrentState:{1}, CurrentModule: {2}", isAlive, state, modules[currentModuleIndex]);
                if (!IsAlive)
                {
                    Logger.WriteWarning("Failed on module {0}. Restarting from next module...", modules[currentModuleIndex]);
                    LoadNextModule();
                    port.Dispose();
                    CreatePort();
                    Start(currentModuleIndex);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                if (!stopSignalReceived)
                    watchDogTimer.Start();
            }
        }

        private bool IsAlive
        {
            get
            {
                return (DateTime.Now - lastAliveTime).TotalSeconds < Math.Max(StartArgs.ResetTimeout, StartArgs.RequestInterval);
            }
            set
            {
                if (value)
                {
                    lastAliveTime = DateTime.Now;
                    // Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Last Alive time updated.");
                }
            }
        }

        public void Start(byte startingModuleIndex = 0)
        {
            try
            {
                IsEnabled = true;
                IsAlive = true;
                if (port.IsOpen)
                    port.Close();
                InitPort();
                port.Open();
                currentModuleIndex = startingModuleIndex;
                state = States.Ready;
                IsCurrentDataInvalid = false;
                SendRequest();
                Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Sensor monitoring started with module {0}.", modules[currentModuleIndex]);
                if (!stopSignalReceived)
                    watchDogTimer.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void SendRequest()
        {
            const byte firstByte = 0xAA;
            const byte ignoredByte = 0;
            const byte lastByte = 0x55;
            const byte anyNumberFrom20To100 = 20;

            Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Sending requested for Module{0}.", modules[currentModuleIndex]);
            try
            {
                System.Threading.Thread.Sleep(StartArgs.RequestInterval * SECONDS);
                IsAlive = true;
                Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Thread {0}({1}) waits for lock to access port.", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                lock (portAccessLock)
                {
                    Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Thread {0}({1}) is accessing port.", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                    if (!port.IsOpen)
                    {
                        port.Open();
                        Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Thread {0}({1}) openned port.", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                    }
                }
                byte[] request = new byte[] { firstByte, modules[currentModuleIndex], ignoredByte, anyNumberFrom20To100, ignoredByte, ignoredByte, ignoredByte, lastByte };
                port.Write(request, 0, 8);
                Logger.WriteIf(StartArgs.ShowPortDataLogs, LogType.Debug, "Request sent: {0}({1})", string.Join(" ", request));
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Logger.Write(ex, "Thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name);
                port.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                byte b = (byte)port.ReadByte();

                Logger.WriteIf(StartArgs.ShowPortDataLogs, LogType.Debug, "[{0:X}] Received in state [{1}].", b, state);

                switch (state)
                {
                    case States.Ready:
                        if (b == 0xAA)
                            state = States.WaiForModule;
                        else
                            LogInvalidVal(b);
                        break;
                    case States.WaiForModule:
                        if (b == modules[currentModuleIndex])
                            state = States.WaitForRightDigits;
                        else
                        {
                            LogInvalidVal(b);
                            IsCurrentDataInvalid = true;
                            state = States.WaitForEnd;
                        }
                        break;
                    case States.WaitForRightDigits:
                        rightDigits = b;
                        state = States.WaitForLeftDigit;
                        break;
                    case States.WaitForLeftDigit:
                        leftDigit = b;
                        state = States.WaitForEnd;
                        break;
                    case States.WaitForEnd:
                        if (b == 0x55)
                        {
                            state = States.Ready;
                            ModuleFinished();
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            //    Logger.WriteDebug("State changed to [{0}]. CM:{1}", state.ToString(), modules[currentModuleIndex]);
        }

        private void ModuleFinished()
        {
            try
            {
                Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Module finished");
                if (!IsEnabled)
                    return;

                if (IsCurrentDataInvalid)
                {
                    IsCurrentDataInvalid = false;
                    LoadNextModule();
                    SendRequest();
                    return;
                }

                var moduleValuePair = new KeyValuePair<byte, double>(modules[currentModuleIndex], CalculateValue());
                OnModuleDataReceived(moduleValuePair);
                Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Loading next module...");
                LoadNextModule();
                Logger.WriteIf(StartArgs.ShowDebugLogs, LogType.Debug, "Sending request...");
                SendRequest();


            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void LoadNextModule()
        {
            byte lastModuleIndex = (byte)(modules.Length - 1);
            if (currentModuleIndex == lastModuleIndex)
            {
                Logger.WriteInfo("Module cycle finished with {0} valid sensor data.", validDataCount);
                if (validDataCount == 0)
                {
                    //"Sensor power may be off.\nSensor wire may be cut.\nPc COM may be disconnected.\nSettings may be incorrect in sensor configuration manager.\nSensors may be damaged."
                    ServiceState.ReportActivity(ServiceTypes.SensorService, "عدم دسترسی به سنسورها");
                }
                else
                {
                    ServiceState.ReportActivity(ServiceTypes.SensorService);
                }
                currentModuleIndex = 0;
                validDataCount = 0;
            }
            else
                currentModuleIndex++;
        }

        private void OnModuleDataReceived(KeyValuePair<byte, double> moduleValuePair)
        {
            validDataCount++;
            //   Logger.WriteDebug("Module{0} = {1}{2}", moduleValuePair.Key, moduleValuePair.Value, moduleValuePair.Key == modules.Length ? "\n" : "");
            if (ModuleDataReceived != null)
            {
                ModuleDataReceived(moduleValuePair);
            }
        }

        private double CalculateValue()
        {
            return double.Parse(string.Format("{0:X}{1:X}.{2:X}", leftDigit, rightDigits >> 4, rightDigits & 0x0F));
        }

        private void LogInvalidVal(byte val)
        {
            if (StartArgs.ShowDebugLogs)
                Logger.WriteWarning("\"{0}\" is invalid in state {1}", val, state);
        }

        void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            if (StartArgs.ShowDebugLogs)
                Logger.WriteDebug(e.EventType.ToString());
        }

        public void Stop()
        {
            try
            {
                stopSignalReceived = true;
                watchDogTimer.Stop();
                IsEnabled = false;
                port.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }


        private void InitPort()
        {
            try
            {
                port.BaudRate = 4800;
                port.Parity = System.IO.Ports.Parity.Even;
                port.DataBits = 8;
                port.PortName = RegSettings.Get("PortName", "COM1") as string;
                port.StopBits = System.IO.Ports.StopBits.One;

                port.DataReceived -= port_DataReceived;
                port.DataReceived += port_DataReceived;

                port.ErrorReceived -= port_ErrorReceived;
                port.ErrorReceived += port_ErrorReceived;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Thread ID: " + Thread.CurrentThread.ManagedThreadId);
            }
        }

        ~SensorCollector()
        {
            Logger.WriteInfo("Sensor Collector destroyed.");
            instanceCount--;
        }
    }

    enum States
    {
        Ready,
        WaiForModule,
        WaitForRightDigits,
        WaitForLeftDigit,
        WaitForEnd
    }
}
