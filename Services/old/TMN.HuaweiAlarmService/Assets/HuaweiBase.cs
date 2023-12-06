using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Net;

namespace TMN
{
    public class HuaweiBase
    {
        #region Properties
        public object TopologyOwner;
        public int CommandTimeout = 60000;
        private System.Timers.Timer commandTimer;
        public int Port = 6000;
        public Guid ID;
        public string Version;
        public IPAddress IP;
        private string UN;
        private string PW;
        private string LastCommand = "Nothing";
        public SwitchState Switchstatus;
        private TCPConnection tcpc;
        public bool Conncted = false;
        public string switchResult;
        bool Autologin = false;
        public bool OnlineMode = false;

        public HuaweiBase(IPAddress ip, string username, string password, int CommandTimeout)
        {
            this.IP = ip;
            this.Switchstatus = SwitchState.LoggedOut;
            this.UN = username;
            this.PW = password;
            tcpc = new TCPConnection();
            tcpc.Recieve += new MHandler(tcpc_Recieve);
            tcpc.ConnectError += new NetEvents(tcpc_ConnectError);
            tcpc.SendError += new NetEvents(tcpc_SendError);
            tcpc.RecieveError += new NetEvents(tcpc_RecieveError);
            tcpc.evConnected += new NetEvents(tcpc_evConnected);

            commandTimer = new System.Timers.Timer();
            commandTimer.Interval = CommandTimeout;
            commandTimer.Elapsed += new System.Timers.ElapsedEventHandler(commandTimer_Elapsed);
            commandTimer.Enabled = false;
        }


        public HuaweiBase()
        {
            this.Switchstatus = SwitchState.LoggedOut;
            tcpc = new TCPConnection();
            tcpc.Recieve += new MHandler(tcpc_Recieve);
            tcpc.ConnectError += new NetEvents(tcpc_ConnectError);
            tcpc.SendError += new NetEvents(tcpc_SendError);
            tcpc.RecieveError += new NetEvents(tcpc_RecieveError);
            tcpc.evConnected += new NetEvents(tcpc_evConnected);
            commandTimer = new System.Timers.Timer();
            commandTimer.Interval = CommandTimeout;
            commandTimer.Elapsed += new System.Timers.ElapsedEventHandler(commandTimer_Elapsed);
            commandTimer.Enabled = false;
        }

        void commandTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            commandTimer.Enabled = false;
            tcpc.Disconnect();
            OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Disconnected));
            OnCommandTimeOut();
        }

        void tcpc_evConnected(object sender)
        {
            Conncted = true;
            if (OnConnect != null)
            {
                OnConnect(this);
            }
            if (Autologin)
            {
                SendAuthentication();
            }
        }

        public string Username
        {
            get
            {
                return UN;
            }
            set
            {
                UN = value;
            }
        }

        public string Password
        {
            get
            {
                return PW;
            }
            set
            {
                PW = value;
            }
        }
        public bool TrytoReconnect
        {
            get
            {
                return tcpc.Trytoreconnect;
            }
            set
            {
                tcpc.Trytoreconnect = value;
            }
        }


        void tcpc_RecieveError(object sender)
        {
            if (ErrorOnRecieve != null)
            {

                OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Disconnected));

                ErrorOnRecieve(this);
            }
        }

        void tcpc_SendError(object sender)
        {
            if (ErrorOnSend != null)
            {

                OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Disconnected));

                if (tcpc.Connected)
                {
                    tcpc.Disconnect();
                }
                ErrorOnSend(this);
            }
        }

        void tcpc_ConnectError(object sender)
        {
            if (ErrorOnConnect != null)
            {

                OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Disconnected));

                if (tcpc.Connected)
                {
                    tcpc.Disconnect();
                }
                ErrorOnConnect(this);
            }
        }



        #endregion

        #region Events
        public event MEventHandler Recieve;
        public event MEventHandler Alarm;
        public event ChangeStateEventHandler ChangeState;
        public event EventHandler Timeout;
        public event NetEvents ErrorOnConnect;
        public event NetEvents ErrorOnSend;
        public event NetEvents ErrorOnRecieve;
        public event NetEvents OnConnect;



        protected virtual void OnAlarm(SwitchEventArgs e)
        {
            if (this.Alarm != null)
            {
                this.Alarm(e);
            }
        }

        protected virtual void OnRecieve(SwitchEventArgs e)
        {
            if (this.Recieve != null)
            {
                this.Recieve(e);
            }
        }

        protected virtual void OnChanegeState(SwitchStatusEventArgs e)
        {
            if (this.ChangeState != null)
            {
                this.ChangeState(e);
                Logger.WriteInfo("State changed to {0}.", e.State);
            }
        }

        protected virtual void OnCommandTimeOut()
        {
            if (this.Timeout != null)
            {
                this.Timeout(this, new EventArgs());
            }
        }

        void tcpc_onDisconnect(object sender, EventArgs e)
        {
            if (this.ChangeState != null)
            {
                this.OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Disconnected));
            }
        }

        void tcpc_Recieve(string Result)
        {
            //  Logger.WriteDebug(Result);

            #region LoggedIN Section

            if (Result.Contains("login successfully"))
            {
                Logger.WriteInfo("Login succeeded.");
                Switchstatus = SwitchState.LoggedIn;
                OnChanegeState(new SwitchStatusEventArgs(this, Switchstatus));
                if (Autologin)
                {
                    SetAlarmOFF();
                }
                return;
            }
            else if (Result.Contains("Login failed"))
            {
                string errorString = "Login failed.";
                if (Result.Contains("invalid account"))
                {
                    errorString += " Invalid user name.";
                }
                if (Result.Contains("password is incorrect"))
                {
                    errorString += " Invalid password.";
                }
                Logger.WriteCritical(errorString);
                return;
            }
            #endregion

            #region AlarmOFF
            //+++    HW-CC08        2010-11-08 19:03:16 O&M    #2333782 %%SET CWSON:SWT=OFF;%%
            //%%SET CWSON: SWT=OFF;%%
            if (Result.Contains("%%SET CWSON:"))
            {
                Switchstatus = SwitchState.Idle;
                OnChanegeState(new SwitchStatusEventArgs(this, Switchstatus));
                return;
            }
            #endregion

            #region Alarm Section
            if (Result.Contains("ALARM ") & OnlineMode)
            {
                OnAlarm(new SwitchEventArgs(this, Result));
                return;
            }
            #endregion

            #region CommandResult
            string lastcommandresult = string.Format("%%{0}%%\r\nRETCODE = 0", LastCommand);

            switchResult += Result;
            if (Result.Contains("Result number ") | Result.Contains("The relevant result is not found"))
            {
                commandTimer.Enabled = false;
                Switchstatus = SwitchState.Idle;
                OnRecieve(new SwitchEventArgs(this, switchResult));
                OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Idle));
                switchResult = "";
            }
            #endregion
        }
        #endregion

        #region Switch Face Mathods
        //public void GetReady()
        //{
        //    Autologin = true;
        //    Connect();
        //}
        public void Disconnect()
        {
            tcpc.Disconnect();
        }
        public void Connect()
        {
            Logger.WriteInfo("Connecting to switch..");
            tcpc.Connect(IP, Port);
        }
        public void SendCommand(string Command)
        {
            if (this.Switchstatus == SwitchState.Idle)
            {
                Switchstatus = SwitchState.Busy;
                this.OnChanegeState(new SwitchStatusEventArgs(this, SwitchState.Busy));
                LastCommand = Command;
                switchResult = "";
                commandTimer.Enabled = true;
                tcpc.Send(Command);

            }
        }
        public void SendAuthentication()
        {
            Logger.WriteDebug("Sending Login command (LGI)...");
            Switchstatus = SwitchState.Initializing;
            string loginstr = "LGI:OP=" + UN + ",PWD=" + PW + ";";
            //LGI:OP=testuser1,PWD=123;
            tcpc.Send(loginstr);
        }
        public void SetAlarmOFF()
        {
            Logger.WriteInfo("Setting alarm off (SET CWSON)...");
            string commandstr = "SET CWSON: SWT=OFF;";
            tcpc.Send(commandstr);
        }
        #endregion

        #region Destructor
        ~HuaweiBase()
        {
            if (tcpc.Connected)
                tcpc.Disconnect();
        }
        #endregion

    }
}
