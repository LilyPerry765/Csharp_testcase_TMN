using System;
using Enterprise;
using System.Timers;
using Thread = System.Threading.Thread;
using System.IO.Ports;
using System.Windows.Threading;
using System.Text;

namespace TMN
{
    class RS232AlarmReceiver
    {
        #region Events

        public event Action<string> SwitchDataReceived;

        #endregion

        #region Constants

        private const int RECEIVE_WAIT_TIME_OUT = 1000;
        private const int MAX_SEND_COMMAND_RETRYS_COUNT = 3;
        private const string ERICSSON_ALARM_REQUEST_COMMAND = "allip;";
        private const char END_OF_TEXT = (char)0x03;

        #endregion

        #region Private Fields

        private SerialPort port;
        private static Timer timer = null;
        private string portName;
        private int portBaudRate;
        private string buffer;
        private bool isPortInitialized;
        private object portReaderLock = new object();
        private object portWriterLock = new object();
        private bool receiveCompleted = false;
        private DateTime commandTimeoutStartTime;

        #endregion

        #region Private Methods

        private void InitializeTimer()
        {
            try
            {
                int interval = 0;
                if (timer == null)
                {
                    interval = Setting.Get(Setting.ALARM_QUERY_INTERVAL, Setting.DEFAULT_ALARM_QUERY_INTERVAL);
                    timer = new Timer(interval * 1000);
                    timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                }
                timer.Start();
                Logger.WriteInfo("Alarms will be requested about every {0} second(s).", interval);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private bool InitializePort()
        {
            try
            {
                Logger.WriteInfo("Initializing port {0}...", portName);

                portName = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
                portBaudRate = int.Parse(RegSettings.Get(Program.PORT_BAUD_RATE_KEY, Program.DEFAULT_PORT_BAUD_RATE) as string);
                if (port != null && port.IsOpen)
                    port.Dispose();
                port = new SerialPort(portName, portBaudRate, Parity.Even, 7, StopBits.One);
                port.Handshake = Handshake.XOnXOff;
                port.DtrEnable = true;
                port.RtsEnable = true;
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Encoding = Encoding.UTF8;
                //port.NewLine = "\u00C1"; // or "/r/n"

                OpenPort();
                isPortInitialized = true;
                return true;

            }
            catch (System.ArgumentException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "نام پورت مورد نظر مجاز نیست", ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "نام پورت مورد نظر مجاز نیست", ex.Message);
            }
            catch (System.InvalidOperationException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در برقراری ارتباط با پورت مورد نظر", ex.Message);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم دسترسی به پورت مورد نظر", ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "اشکال در اتصال به سوييچ", ex.Message);
            }
            return false;
        }

        private void OpenPort()
        {
            port.Open();
            Logger.WriteDebug("Port {0} opened.", port.PortName);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                ServiceState.ReportActivity(ServiceTypes.AlarmService);
                RequestAlarms();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                timer.Start();
            }
        }

        private string SendCommand(string command, int retryCounter = 0)
        {
            Logger.WriteDebug("Command {0} queued.", command);
            //lock (portWriterLock)
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
                    port.Write(command + "\r");
                    Logger.WriteDebug("Command {0} sent. waiting for result...", command);
                    commandTimeoutStartTime = DateTime.Now;
                    bool isTimedOut = false;
                    while (!(receiveCompleted || isTimedOut))
                    {
                        Thread.Sleep(30);
                        isTimedOut = (DateTime.Now - commandTimeoutStartTime).TotalMilliseconds > RECEIVE_WAIT_TIME_OUT;
                    }
                    if (receiveCompleted)
                    {
                        //  Logger.WriteDebug("Command result: {0}", buffer);
                        Logger.WriteDebug("command completed.");
                        return buffer;
                    }
                    else // TimedOut
                    {
                        if (retryCounter < MAX_SEND_COMMAND_RETRYS_COUNT)
                        {
                            retryCounter++;
                            Logger.WriteWarning("Command {0} timed out. Retrying # {1} ...", command, retryCounter);
                            return SendCommand(command, retryCounter);
                        }
                        else
                        {
                            Logger.WriteWarning("Command {0} timed out after {1} retrys. Command aborted.", command, MAX_SEND_COMMAND_RETRYS_COUNT);
                            ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم دريافت پاسخ از سوييچ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, command);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "اشکال در ارتباط با سوييچ", ex.Message);
                }
                return null;
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Reset timeout
            commandTimeoutStartTime = DateTime.Now;
            lock (portReaderLock)
            {
                string data;
                try
                {
                    while (port.BytesToRead > 0)
                    {
                        data = port.ReadExisting();
                        if (data != "") //.Trim()
                        {
                            buffer += data;
                            commandTimeoutStartTime = DateTime.Now;
                        }
                    }
                    if (buffer.Contains(END_OF_TEXT + "<"))
                    {
                        receiveCompleted = true;
                    }
                }
                catch (System.ArgumentException ex)
                {
                    Logger.Write(ex);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در خواندن و نوشتن اطلاعات", ex.Message);
                }
                catch (System.TimeoutException ex)
                {
                    Logger.Write(ex);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم دريافت پاسخ از سوييچ", ex.Message);
                }
                catch (System.InvalidOperationException ex)
                {
                    Logger.Write(ex);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در عدم دسترسی به پورت مورد نظر", ex.Message);
                }
                catch (System.ServiceProcess.TimeoutException ex)
                {
                    Logger.Write(ex);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم دريافت پاسخ از سوييچ", ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
        }

        private void RequestAlarms()
        {
            string data = SendCommand(ERICSSON_ALARM_REQUEST_COMMAND);
            if (data != null)
            {
                if (SwitchDataReceived != null)
                {
                    SwitchDataReceived(data);
                }
            }
            else
            {
                Logger.WriteDebug("No data received!");
            }
        }


        #endregion

        #region Public Methods

        public void Start()
        {
            RequestAlarms();
            InitializeTimer();
        }

        public void Stop()
        {
            try
            {
                if (timer != null) timer.Stop();
                if (port != null && port.IsOpen)
                {
                    Logger.WriteDebug("Closing port...");
                    port.Close();
                    Logger.WriteDebug("Port closed.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در برقراری ارتباط با پورت مورد نظر", ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }


        #endregion

    }
}
