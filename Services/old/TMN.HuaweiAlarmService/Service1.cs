using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Enterprise;
using System.Text.RegularExpressions;
using System.Timers;

namespace TMN.HuaweiAlarmService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(15 * 1000);
        private Huawei huaweiConnection;

        public Service1()
        {
            InitializeComponent();
            InitilizeService();
        }

        private void InitilizeService()
        {
            Logger.WriteInfo("Initializing service...");

            string switchIP = RegSettings.Get("Huawei_IP") as string;
            string userName = RegSettings.Get("Huawei_UserName") as string;
            string password = RegSettings.Get("Huawei_Password") as string;
            string switchVersion = RegSettings.Get("Huawei_Version") as string;

            huaweiConnection = new Huawei(System.Net.IPAddress.Parse(switchIP), userName, password, 60);
            huaweiConnection.Version = switchVersion;
            huaweiConnection.TrytoReconnect = true;
            huaweiConnection.Recieve += new MEventHandler(huaweiConnection_Recieve);
            huaweiConnection.ChangeState += new ChangeStateEventHandler(huaweiConnection_ChangeState);
            huaweiConnection.OnConnect += new NetEvents(huaweiConnection_OnConnect);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            Logger.WriteInfo("Service initialized.");
        }

        protected override void OnStart(string[] args)
        {
            Logger.WriteStart("Starting TMN Huawei Alarm Service {0} ...", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            try
            {
                huaweiConnection.TrytoReconnect = true;
                huaweiConnection.Connect();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            timer.Start();
            Logger.WriteInfo("Service started.");
        }

        protected override void OnStop()
        {
            timer.Stop();
            try
            {
                huaweiConnection.TrytoReconnect = false;
                huaweiConnection.Disconnect();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            Logger.WriteEnd("Service stopped.");
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                ServiceState.ReportActivity(ServiceTypes.AlarmService);
                //    Logger.WriteInfo(huaweiConnection.Switchstatus.ToString());
                if ((huaweiConnection.Switchstatus == SwitchState.Idle))
                {
                    Logger.WriteDebug("Requesting alarms...");
                    huaweiConnection.SendCommand("LST ALMLOG:;");
                }
                else if (huaweiConnection.Switchstatus == SwitchState.LoggedOut)
                {
                    huaweiConnection.Connect();
                }
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


        void huaweiConnection_OnConnect(object sender)
        {
            ((Huawei)sender).SendAuthentication();
        }

        void huaweiConnection_ChangeState(SwitchStatusEventArgs e)
        {
            if ((e.State == SwitchState.LoggedIn))
            {
                Logger.WriteInfo("State changed to loggedIn");
                ((Huawei)e.Sender).SetAlarmOFF();
            }
        }

        public AlarmMessage GetAlarmMessage(Match match, string version)
        {

            try
            {
                AlarmMessage alarmMsg = new AlarmMessage();


                if (match.Groups["ABI"].Value == "A")
                {
                    alarmMsg.ABIndication = ABIndication.A;
                }
                else
                {
                    if (match.Groups["ABI"].Value == "BAM")
                    {
                        alarmMsg.ABIndication = ABIndication.B;
                    }
                    else
                    {
                        alarmMsg.ABIndication = ABIndication.B;
                    }
                }
                alarmMsg.ABIndicationSpecified = true;
                alarmMsg.AlarmName = match.Groups["AlarmName"].Value;


                //alarmMsg.AlarmSt = AlarmState.OnAckOnClear;
                // alarmMsg.AlarmStateSpecified = true;
                switch (match.Groups["AlarmType"].Value.Trim())
                {
                    case "Software system":
                        {
                            alarmMsg.AlarmType = SwitchAlarmTypes.Software;
                            alarmMsg.AlarmTypeSpecified = true;
                            break;
                        }
                    case "Hardware system":
                        {
                            alarmMsg.AlarmType = SwitchAlarmTypes.Hardware;
                            alarmMsg.AlarmTypeSpecified = true;
                            break;
                        }
                    case "Operation system":
                        {
                            alarmMsg.AlarmType = SwitchAlarmTypes.Operation;
                            alarmMsg.AlarmTypeSpecified = true;
                            break;
                        }
                    ////////////////////
                    default:
                        {
                            alarmMsg.AlarmType = SwitchAlarmTypes.Power;
                            alarmMsg.AlarmTypeSpecified = true;
                            break;
                        }
                }

                alarmMsg.Code = match.Groups["AlarmID"].Value;
                switch (match.Groups["MessageType"].Value)
                {
                    case "Event":
                        {
                            alarmMsg.EventType = SwitchEventTypes.Event;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }
                    case "Alarm":
                        {
                            alarmMsg.EventType = SwitchEventTypes.Fault;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }
                    case "Fault":
                        {
                            alarmMsg.EventType = SwitchEventTypes.Fault;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }
                    case "Recovery":
                        {
                            alarmMsg.EventType = SwitchEventTypes.Recovery;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }
                    ////////////////
                    default:
                        {
                            alarmMsg.EventType = SwitchEventTypes.Recovery;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }

                }
                //alarmMsg.Exchange = "535";
                alarmMsg.LocationInformation = match.Groups["LocationINF"].Value;
                alarmMsg.ModuleNo = match.Groups["MoudleNumber"].Value;

                switch (match.Groups["AlarmSeverity"].Value)
                {
                    case "Warning alarm":
                        {
                            alarmMsg.Severity = AlarmSeverities.Information;
                            alarmMsg.EventTypeSpecified = true;
                            break;
                        }
                    case "Critical alarm":
                        {
                            alarmMsg.Severity = AlarmSeverities.Critical;
                            alarmMsg.SeveritySpecified = true;
                            break;
                        }
                    case "Major alarm":
                        {
                            alarmMsg.Severity = AlarmSeverities.Major;
                            alarmMsg.SeveritySpecified = true;
                            break;
                        }
                    case "Minor alarm":
                        {
                            alarmMsg.Severity = AlarmSeverities.Minor;
                            alarmMsg.SeveritySpecified = true;
                            break;
                        }
                    //////////////
                    default:
                        {
                            alarmMsg.Severity = AlarmSeverities.Information;
                            alarmMsg.SeveritySpecified = true;
                            break;
                        }
                }

                if (version == "2")
                {
                    alarmMsg.StartTime = DateTime.Parse(match.Groups["ATime"].Value);
                    alarmMsg.StartTimeSpecified = true;
                }


                switch (match.Groups["FunctionSubSystem"].Value)
                {
                    case "Signaling system":
                        {
                            alarmMsg.SubSystem = SubSystem.SignalingSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                    case "Call identification":
                        {
                            alarmMsg.SubSystem = SubSystem.CallIdentification;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                    case "Transmission system":
                        {
                            alarmMsg.SubSystem = SubSystem.TransmissionSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                    case "Trunk system":
                        {
                            alarmMsg.SubSystem = SubSystem.TrunkSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                    case "Control system":
                        {
                            alarmMsg.SubSystem = SubSystem.ControlSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }

                    case "Module communication system":
                        {
                            alarmMsg.SubSystem = SubSystem.ModuleCommunicationSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                    /////////////////
                    default:
                        {
                            alarmMsg.SubSystem = SubSystem.ModuleCommunicationSystem;
                            alarmMsg.SubSystemSpecified = true;
                            break;
                        }
                }
                // it must be set from begining the request
                alarmMsg.SwitchId = "1";

                alarmMsg.SwitchingCenterID = 1;
                alarmMsg.SwitchingCenterIDSpecified = true;

                alarmMsg.OtherInformation = match.Groups["OtherInfo"].Value;
                alarmMsg.Data = match.Value;
                alarmMsg.RecoveryAdvice = match.Groups["RecoveryAdvice"].Value;

                return alarmMsg;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Parsing alarm data failed.");
                return null;
            }
        }

        void huaweiConnection_Recieve(SwitchEventArgs e)
        {
            Logger.WriteDebug("Data received. Parsing data...");
            AcknowledgeAll();

            string regEx = "";
            if (((Huawei)e.Sender).Version == "2")
            {
                regEx = @"ALARM\s*(?<AlarmID>[\d]+)\s*(?<MessageType>[\w]+)\s*(?<AlarmSeverity>\w+\s\w+)\s*\w*\s*\w*\s*(?<AlarmType>[\s\w]+)\s*\r\n?\s*Alarm occurrence time\s*=\s*(?<ATime>[\d-\s:]+)\s*\n\n?\s*Function sub-system\s*=\s*(?<FunctionSubSystem>[\d\w\s]+)\s*\r\n?\s*A/B indication\s*=\s*(?<ABI>[\w]*)\s*\r\n?\s*Module number\s*=\s*(?<MoudleNumber>[\w\d-\s:]+)\s*\r\n?\s*Alarm name\s*=\s*(?<AlarmName>[\W\w]+?)\s*\r\n?\s*Location information\s*=\s*(?<LocationINF>[\W\w]+?)\s*\r\n?\s*Other information\s*=\s*(?<OtherInfo>[\W\w]+?)\s*\r\n?\s*Recovery advice\s*=\s*(?<RecoveryAdvice>[\W\w]+?)\r\n\r\n";
            }
            else
            {
                regEx = @"ALARM\s*(?<AlarmID>[\d]+)\s*(?<MessageType>[\w]+)\s*(?<AlarmSeverity>\w+\s\w+)\s*\w*\s*\w*\s*(?<AlarmType>[\s\w]+)\s*\r\n?\s*Function sub-system\s*=\s*(?<FunctionSubSystem>[\d\w\s]+)\s*\r\n?\s*A/B indication\s*=\s*(?<ABI>[\w]*)\s*\r\n?\s*Module number\s*=\s*(?<MoudleNumber>[\w\d-\s:]+)\s*\r\n?\s*Alarm name\s*=\s*(?<AlarmName>[\W\w]+?)\s*\r\n?\s*Location information\s*=\s*(?<LocationINF>[\W\w]+?)\s*\r\n?\s*Other information\s*=\s*(?<OtherInfo>[\W\w]+?)\s*\r\n?\s*Recovery advice\s*=\s*(?<RecoveryAdvice>[\W\w]+?)\r\n\r\n";
            }

            var matches = Regex.Matches(e.Response, regEx, RegexOptions.Compiled);
            int count = 0;
            foreach (Match match in matches)
            {
                if (SaveAlarm(GetAlarmMessage(match, ((Huawei)e.Sender).Version)))
                    count++;
            }

            Logger.WriteDebug("Parsing data finished. {0} new alarm(s) detected.", count);
        }

        private void AcknowledgeAll()
        {
            try
            {
                Logger.WriteInfo("Marking all alarms as read...");
                var cnt = DB.Instance.ExecuteCommand("UPDATE LogAlarm SET IsRead=1 WHERE CenterID={0} AND IsRead=0", Center.CurrentCenterID);
                Logger.WriteInfo("{0} alarm(s) marked as read.", cnt);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private static bool SaveAlarm(AlarmMessage alarmMessage)
        {
            if (alarmMessage != null)
            {
                try
                {
                    var db = new TMNModelDataContext();
                    LogAlarm existingAlarm = db.LogAlarms.SingleOrDefault(la => (la.CenterID == Center.CurrentCenterID) && (la.Time == alarmMessage.StartTime) && (la.Title == alarmMessage.AlarmName.Trim()));
                    if (existingAlarm == null)
                    {
                        LogAlarm logAlarm = new LogAlarm();
                        logAlarm.ID = Guid.NewGuid();
                        logAlarm.CenterID = Center.CurrentCenterID;
                        logAlarm.Time = alarmMessage.StartTime;
                        logAlarm.Data = alarmMessage.Data;
                        logAlarm.Title = alarmMessage.AlarmName.Trim();
                        logAlarm.Severity = (byte)alarmMessage.Severity;
                        logAlarm.IsRead = false;
                        db.LogAlarms.InsertOnSubmit(logAlarm);
                    }
                    else
                    {
                        existingAlarm.IsRead = false;
                    }
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
            return false;
        }
    }
}
