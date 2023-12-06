using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using Enterprise;
using System.Windows.Threading;

namespace TMN
{
    class TelnetAlarmReceiver
    {
        #region Constants

        private const string ERICSSON_ALARM_REQUEST_COMMAND = "ALLIP;";
        private const string ERICSSON_AUTHORIZATION_FAILURE_MESSAGE = "AUTHORIZATION FAILURE";
        private const string ERICSSON_COMMAND_RESTRICTED_MESSAGE = "COMMAND RESTRICTED";
        private const string ERICSSON_SYNTAX_FAULT_MESSAGE = "SYNTAX FAULT";
        private const string ERICSSON_USERCODE_MESSAGE = "USERCODE";
        private const string ERICSSON_PASSWORD_MESSAGE = "PASSWORD";
        private const char END_OF_TEXT = (char)0x03;

        #endregion

        private static Timer timer = null;
        private TelnetClient telnet;

        public event Action<string> SwitchDataReceived;

        public TelnetAlarmReceiver()
        {
            string ip = RegSettings.Get(Program.IP_KEY, "10.0.0.10").ToString();
            telnet = new TelnetClient(ip);
            telnet.Connected += new EventHandler(telnet_Connected);
            telnet.ConnectIfNeeded();
        }

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

        private void telnet_Connected(object sender, EventArgs e)
        {
            string username = RegSettings.Get(Program.USER_CODE_KEY, "charging").ToString();
            string password = Cryptographer.Decode(RegSettings.Get(Program.PASSWORD_KEY, "TCTiran1") as string);
            Login(username, password);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                RequestAlarms();
                ServiceState.ReportActivity(ServiceTypes.AlarmService);
            }
            catch (Exception ex)
            {
                telnet.ConnectIfNeeded();
                Logger.Write(ex);
            }
            timer.Start();
        }

        private bool Login(string username, string password)
        {
            telnet.WaitFor(ERICSSON_USERCODE_MESSAGE);
            telnet.Send(username);
            telnet.WaitFor(ERICSSON_PASSWORD_MESSAGE);
            telnet.Send(password);
            var result = telnet.WaitFor(ERICSSON_AUTHORIZATION_FAILURE_MESSAGE, END_OF_TEXT + "<");
            if (result.Contains(ERICSSON_AUTHORIZATION_FAILURE_MESSAGE))
            {
                Logger.WriteWarning("Login failed!");
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "اشکال در login");
                return false;
            }
            else
            {
                Logger.WriteInfo("Login succeeded.");
                return true;
            }
        }

        private void RequestAlarms()
        {
            try
            {
                Logger.WriteDebug("Requesting alarms...");
                telnet.Send(ERICSSON_ALARM_REQUEST_COMMAND);
                string result = telnet.WaitFor(END_OF_TEXT + "<");
                if (result == null)
                {
                    Logger.WriteWarning("No data received!");
                }
                else if (result.Contains("COMMAND RESTRICTED"))
                {
                    Logger.WriteWarning("Command is restricted.");
                }
                else
                {
                    Logger.WriteDebug("Alarm data received.");
                    OnSwitchDataReceived(result);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "خطا در سرويس", ex.Message);
            }
        }

        private void OnSwitchDataReceived(string data)
        {
            if (SwitchDataReceived != null)
            {
                SwitchDataReceived(data);
            }
        }

        ~TelnetAlarmReceiver()
        {
            try
            {
                if (telnet != null)
                    telnet.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        #endregion

        public void Start()
        {
            try
            {
                //RequestAlarms();
                InitializeTimer();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public void Stop()
        {
            try
            {
                if (timer != null)
                    timer.Stop();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }
    }
}
