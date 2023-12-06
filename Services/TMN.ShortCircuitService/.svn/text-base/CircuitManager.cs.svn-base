using System;
using Enterprise;
using System.Timers;
using Thread = System.Threading.Thread;
using System.IO.Ports;

namespace TMN
{
    static class CircuitManager
    {
        #region Events

        public static event EventHandler<CircuitStatusEventArgs> CircuitStatusReceived;

        #endregion

        #region Constants

        private const int RECEIVE_WAIT_TIME_OUT = 3000;
        private const int MAX_SEND_COMMAND_RETRYS_COUNT = 3;
        private const char CIRCUIT_COMMAND_STATUS_REQUEST = 'S';
        private const char CIRCUIT_COMMAND_MIN_MAX_SET = 'M';
        private const char CIRCUIT_COMMAND_CALIBRATE = 'C';
        private const char CIRCUIT_COMMAND_DIMENSION = 'D';
        private const char CIRCUIT_COMMAND_POWER_CONDUCTOR_ACTIVATE = 'A';
        private const char CIRCUIT_COMMAND_SWITCH_LINE_SET = 'L';

        #endregion

        #region Private Fields

        private static SerialPort port;
        private static Timer timer;
        public static int CurrentDeviceIndex = 0;
        private static string buffer;
        private static bool isPortInitialized;
        private static object portReaderLock = new object();
        private static object portWriterLock = new object();
        private static bool receiveCompleted = false;
        private static bool autoCircuitStatusRequestIsEnabled = false;
        public static string[] _deviceNumber;
        public static string[] DeviceNumber
        {
            get
            {
                if (_deviceNumber == null)
                {
                    string deviceNumbers = RegSettings.Get(Program.DEVICE_NUMBER_KEY_CIRCUIT, "NAN") as string;
                    if(deviceNumbers == "NAN")
                        deviceNumbers = RegSettings.Get(Program.DEVICE_NUMBER_KEY, "1") as string;
                    _deviceNumber = deviceNumbers.Split(',');
                }
                return _deviceNumber;
            }
        }

        public static int FaultDeviceNumber = -1;


        #endregion

        #region Private Methods

        private static void InitializeTimer(int interval)
        {
            try
            {
                if (timer == null)
                {
                    timer = new Timer(interval);
                    timer.AutoReset = false;
                    timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                }
                timer.Start();
                Logger.WriteInfo("Circuit status is being requested about every {0} second(s).", timer.Interval / 1000);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private static bool InitializePort()
        {
            try
            {
                string portName = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
                portName = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
                if (port != null && port.IsOpen)
                    port.Dispose();
                port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                //string deviceNumbers = RegSettings.Get(Program.DEVICE_NUMBER_KEY, "1") as string;
                //deviceNumber = deviceNumbers.Split(',');
                OpenPort();

                isPortInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }

        private static void OpenPort()
        {
            port.Open();
            Logger.WriteDebug("Port {0} opened.", port.PortName);
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //RequestCircuitStatusWithEvent();
                RequestCircuitStatusWithEvent();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                if (autoCircuitStatusRequestIsEnabled)
                    timer.Start();
            }
        }

        private static void RequestCircuitStatusWithEvent()
        {
            var data = RequestCircuitStatus();
            OnCircuitDataReceived(data);
        }

        private static string SendCommand(string query, int retryCounter = 0)
        {
            Logger.WriteDebug("Command {0} queued.", query);
            lock (portWriterLock)
            {
                //   Logger.WriteDebug("Lock passed.");
                try
                {
                    if (!isPortInitialized)
                        if (!InitializePort())
                            return null;

                    if (!port.IsOpen)
                        OpenPort();

                    Logger.WriteDebug("Port is ready.");

                    buffer = null;
                    receiveCompleted = false;
                    port.Write(query);
                    Logger.WriteDebug("Command {0} sent.", query);
                    DateTime startTime = DateTime.Now;
                    bool isTimedOut = false;
                    while (!(receiveCompleted || isTimedOut))
                    {
                        Thread.Sleep(30);
                        isTimedOut = (DateTime.Now - startTime).TotalMilliseconds > RECEIVE_WAIT_TIME_OUT;
                    }
                    if (receiveCompleted)
                    {
                        Logger.WriteDebug("Command result: {0}", buffer);
                        return buffer;
                    }
                    else // TimedOut
                    {
                        if (retryCounter < MAX_SEND_COMMAND_RETRYS_COUNT)
                        {
                            retryCounter++;
                            Logger.WriteWarning("Command {0} timed out. Retrying # {1} ...", query, retryCounter);
                            return SendCommand(query, retryCounter);
                        }
                        else
                        {
                            Logger.WriteWarning("Command {0} timed out after {1} retrys. Command aborted.", query, MAX_SEND_COMMAND_RETRYS_COUNT);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, query);
                }
                return null;
            }
        }

        private static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (portReaderLock)
            {
                try
                {
                    while (port.BytesToRead > 0)
                        buffer += port.ReadExisting();

                    if (buffer.EndsWith("]"))
                        receiveCompleted = true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
        }

        //This method can be accessed from different threads of the threadpool which "Port.DataReceived" is using.
        private static void OnCircuitDataReceived(CircuitStatus circuitStatus)
        {
            if (CircuitStatusReceived != null && circuitStatus != null)
            {
                CircuitStatusReceived(null, new CircuitStatusEventArgs(circuitStatus));
                if (FaultDeviceNumber == CircuitManager.CurrentDeviceIndex)
                    FaultDeviceNumber = -1;
            }
            else
            {
                FaultDeviceNumber = CurrentDeviceIndex;
                ServiceState.ReportActivity(ServiceTypes.CircuitService, string.Format("مشکل در ذخيره اطلاعات دستگاه کابل {0}", FaultDeviceNumber + 1)); 
            }
        }

        #endregion

        #region Public Methods

        public static void StartRequestingCircuitStatusPeriodically(int interval)
        {
            autoCircuitStatusRequestIsEnabled = true;
            RequestCircuitStatusWithEvent();
            InitializeTimer(interval);
        }

        public static void StopRequestingCircuitStatusPeriodically()
        {
            try
            {
                autoCircuitStatusRequestIsEnabled = false;
                if (timer != null) timer.Stop();
                if (port != null && port.IsOpen)
                {
                    Logger.WriteDebug("Closing port...");
                    port.Close();
                    Logger.WriteDebug("Port closed.");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public static void MarkForReInitialize()
        {
            isPortInitialized = false;
        }

        public static void ChangeInterval(int millisecond)
        {
            timer.Interval = millisecond;
        }

        //public static bool CircuitDimension()
        //{
        //    string request = string.Format("[{0}{1}]", CIRCUIT_COMMAND_DIMENSION, DEVICE_NUMBER);
        //    string result = SendCommand(request);
        //    if (result != null)
        //    {
        //        Logger.WriteInfo("Circuit Dimension set to {0}.", DEVICE_NUMBER);
        //        return true;
        //    }
        //    return false;
        //}
        public static CircuitStatus RequestCircuitStatus()
        {
            CurrentDeviceIndex++;
            if (CurrentDeviceIndex >= DeviceNumber.Length)
                CurrentDeviceIndex = 0;
            string request = string.Format("[{0}{1}]", CIRCUIT_COMMAND_STATUS_REQUEST, DeviceNumber[CurrentDeviceIndex]);
            string result = SendCommand(request);
            if (result != null)
                return CircuitStatus.Parse(result, CurrentDeviceIndex);
            else
                return null;
        }

        #endregion


    }


}
