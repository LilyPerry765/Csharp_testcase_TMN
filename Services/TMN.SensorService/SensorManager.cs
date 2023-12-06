using System;
using Enterprise;
using System.Timers;
using Thread = System.Threading.Thread;
using System.IO.Ports;

namespace TMN
{
    static class SensorManager
    {
        #region Events

        public static event EventHandler<SensorStatusEventArgs> SensorStatusReceived;

        #endregion

        #region Constants

        //private const int DEVICE_NUMBER = 1;
        private const int RECEIVE_WAIT_TIME_OUT = 3000;
        private const int MAX_SEND_COMMAND_RETRYS_COUNT = 3;
        private const char SENSOR_COMMAND_STATUS_REQUEST = 'S';
        private const char SENSOR_COMMAND_MIN_MAX_SET = 'M';
        private const char SENSOR_COMMAND_CALIBRATE = 'C';
        private const char SENSOR_COMMAND_DIMENSION = 'D';
        private const char SENSOR_COMMAND_POWER_CONDUCTOR_ACTIVATE = 'A';
        private const char SENSOR_COMMAND_SWITCH_LINE_SET = 'L';

        #endregion

        #region Private Fields

        private static SerialPort port;
        private static Timer timer;
        private static string buffer;
        private static bool isPortInitialized;
        private static object portReaderLock = new object();
        private static object portWriterLock = new object();
        private static bool receiveCompleted = false;
        private static bool autoSensorStatusRequestIsEnabled = false;
        public static string[] _deviceNumber;
        public static string[] DeviceNumber
        {
            get
            {
                if (_deviceNumber == null)
                {
                    string deviceNumbers = RegSettings.Get(Program.DEVICE_NUMBER_KEY, "1") as string;
                    _deviceNumber = deviceNumbers.Split(',');
                }
                return _deviceNumber;
            }
        }
        private static int currentDeviceIndex = 0;

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
                Logger.WriteInfo("Sensor status is being requested about every {0} second(s).", timer.Interval / 1000);
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
                if (port != null && port.IsOpen)
                    port.Dispose();
                port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
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
                //RequestSensorStatusWithEvent();
                RequestSensorStatusWithEvent();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                if (autoSensorStatusRequestIsEnabled)
                    timer.Start();
            }
        }

        private static void RequestSensorStatusWithEvent()
        {
            var data = RequestSensorStatus();
            OnSensorDataReceived(data);
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
        private static void OnSensorDataReceived(SensorStatus sensorStatus)
        {
            if (SensorStatusReceived != null && sensorStatus != null)
            {
                SensorStatusReceived(null, new SensorStatusEventArgs(sensorStatus));
            }
            else
            {
                //if(sensorStatus.IsSensor)
                    ServiceState.ReportActivity(ServiceTypes.SensorService, "اشکال در ارتباط با سنسور");
                //else
                //    ServiceState.ReportActivity(ServiceTypes.CircuitService, "اشکال در ارتباط با کابل");
            }
        }

        #endregion

        #region Public Methods

        public static void StartRequestingSensorStatusPeriodically(int interval)
        {
            autoSensorStatusRequestIsEnabled = true;
            RequestSensorStatusWithEvent();
            InitializeTimer(interval);
        }

        public static void StopRequestingSensorStatusPeriodically()
        {
            try
            {
                autoSensorStatusRequestIsEnabled = false;
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

        //public static bool SensorDimension()
        //{
        //    string request = string.Format("[{0}{1}]", SENSOR_COMMAND_DIMENSION, DEVICE_NUMBER);
        //    string result = SendCommand(request);
        //    if (result != null)
        //    {
        //        Logger.WriteInfo("Sensor Dimension set to {0}.", DEVICE_NUMBER);
        //        return true;
        //    }
        //    return false;
        //}
        public static SensorStatus RequestSensorStatus()
        {
            currentDeviceIndex++;
            if (currentDeviceIndex >= DeviceNumber.Length)
                currentDeviceIndex = 0;
            string request = string.Format("[{0}{1}]", SENSOR_COMMAND_STATUS_REQUEST, DeviceNumber[currentDeviceIndex]);
            string result = SendCommand(request);
            if (result != null)
                return SensorStatus.Parse(result, currentDeviceIndex);
            else
                return null;
        }

        public static bool SetCriticalBounds(Bound temperature1, Bound temperature2, Bound temperature3, Bound humidity, bool activateAlarm)
        {
            string query = string.Format("[{0}{1}{2}{3}{4}{5}{6}]", SENSOR_COMMAND_MIN_MAX_SET, DeviceNumber[0], Convert.ToByte(activateAlarm), temperature1, temperature2, temperature3, humidity);
            if (SendCommand(query) != null)
            {
                Logger.WriteInfo("Critical bounds set.");
                return true;
            }
            return false;
        }

        public static bool SetPowerOpenCloseMode(string mode) //(byte p1, byte p2, byte p3, byte p4, byte p5, byte p6, byte p7, byte p8)
        {
            //string query = string.Format("[{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}]", SENSOR_COMMAND_POWER_CONDUCTOR_ACTIVATE, DEVICE_NUMBER, p1, p2, p3, p4, p5, p6, p7, p8);
            string query = string.Format("[{0}{1}{2}]", SENSOR_COMMAND_POWER_CONDUCTOR_ACTIVATE, DeviceNumber[0], mode);
            if (SendCommand(query) != null)
            {
                Logger.WriteInfo("Power circuit Mode set.");
                return true;
            }
            return false;
        }

        public static bool SetSwitchLine(string state) //(byte line1, byte line2)
        {
            string query = string.Format("[{0}{1}{2}]", SENSOR_COMMAND_SWITCH_LINE_SET, DeviceNumber[0], state ); //line1, line2);
            if (SendCommand(query) != null)
            {
                Logger.WriteInfo("Rele Mode changed.");
                return true;
            }
            return false;
        }

        public static void ActivateBuzzer(bool activate)
        {
            // SENSOR_COMMAND_MIN_MAX_SET ignoroes critical bounds with both min & max set to 0. so pass empty Bounds (Bound.Ignore) for sensors you dont want to change in this command.
            if (SetCriticalBounds(Bound.Ignore, Bound.Ignore, Bound.Ignore, Bound.Ignore, activate))
                Logger.WriteInfo("Buzzer {0}.", activate ? "activated." : "muted.");
        }

        public static void Calibrate(int sensorNumber, double newValue)
        {
            string query = string.Format("[{0}{1}{2}{3:000}]", SENSOR_COMMAND_CALIBRATE, DeviceNumber[0], sensorNumber, newValue * 10);
            if (SendCommand(query) != null)
                Logger.WriteInfo("Sensor {0} calibrated to {1}.", sensorNumber, newValue);
        }

        public static void ResetCalibration(int sensorNumber)
        {
            Calibrate(sensorNumber, 0);
        }



        #endregion


    }


}
