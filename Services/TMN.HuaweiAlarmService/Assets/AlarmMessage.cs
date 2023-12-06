using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Text.RegularExpressions;

namespace TMN
{
    public  class AlarmMessage
    {
        #region Private Fields

        private string dataField;

        private string codeField;

        private AlarmSeverities severityField;

        private bool severityFieldSpecified;

        private System.DateTime startTimeField;

        private bool startTimeFieldSpecified;

        private SwitchAlarmTypes alarmTypeField;

        private bool alarmTypeFieldSpecified;

        private string switchIdField;

        private string locationInformationField;

        private string otherInformationField;

        private string recoveryAdviceField;

        private ABIndication aBIndicationField;

        private bool aBIndicationFieldSpecified;

        private string alarmNameField;

        private SubSystem subSystemField;

        private bool subSystemFieldSpecified;

        private string exchangeField;

        private string moduleNoField;

        private SwitchEventTypes eventTypeField;

        private bool eventTypeFieldSpecified;

        private int switchingCenterIDField;

        private bool switchingCenterIDFieldSpecified;

        private System.DateTime endTimeField;

        private bool endTimeFieldSpecified;
        #endregion

        #region Public Properties

        
        
        public string Data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }

        
        
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        
        
        public AlarmSeverities Severity
        {
            get
            {
                return this.severityField;
            }
            set
            {
                this.severityField = value;
            }
        }

        
       
        public bool SeveritySpecified
        {
            get
            {
                return this.severityFieldSpecified;
            }
            set
            {
                this.severityFieldSpecified = value;
            }
        }

        
        
        public System.DateTime Time
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        
       
        public bool StartTimeSpecified
        {
            get
            {
                return this.startTimeFieldSpecified;
            }
            set
            {
                this.startTimeFieldSpecified = value;
            }
        }

        
        
        public SwitchAlarmTypes AlarmType
        {
            get
            {
                return this.alarmTypeField;
            }
            set
            {
                this.alarmTypeField = value;
            }
        }

        
       
        public bool AlarmTypeSpecified
        {
            get
            {
                return this.alarmTypeFieldSpecified;
            }
            set
            {
                this.alarmTypeFieldSpecified = value;
            }
        }

        
        
        public string SwitchId
        {
            get
            {
                return this.switchIdField;
            }
            set
            {
                this.switchIdField = value;
            }
        }

        
        
        public string LocationInformation
        {
            get
            {
                return this.locationInformationField;
            }
            set
            {
                this.locationInformationField = value;
            }
        }

        
        
        public string OtherInformation
        {
            get
            {
                return this.otherInformationField;
            }
            set
            {
                this.otherInformationField = value;
            }
        }

        
        
        public string RecoveryAdvice
        {
            get
            {
                return this.recoveryAdviceField;
            }
            set
            {
                this.recoveryAdviceField = value;
            }
        }

        
        
        public ABIndication ABIndication
        {
            get
            {
                return this.aBIndicationField;
            }
            set
            {
                this.aBIndicationField = value;
            }
        }

        
       
        public bool ABIndicationSpecified
        {
            get
            {
                return this.aBIndicationFieldSpecified;
            }
            set
            {
                this.aBIndicationFieldSpecified = value;
            }
        }

        
        
        public string Title
        {
            get
            {
                return this.alarmNameField;
            }
            set
            {
                this.alarmNameField = value;
            }
        }

        
        
        public SubSystem SubSystem
        {
            get
            {
                return this.subSystemField;
            }
            set
            {
                this.subSystemField = value;
            }
        }

        
       
        public bool SubSystemSpecified
        {
            get
            {
                return this.subSystemFieldSpecified;
            }
            set
            {
                this.subSystemFieldSpecified = value;
            }
        }

        
        
        public string Exchange
        {
            get
            {
                return this.exchangeField;
            }
            set
            {
                this.exchangeField = value;
            }
        }

        
        
        public string ModuleNo
        {
            get
            {
                return this.moduleNoField;
            }
            set
            {
                this.moduleNoField = value;
            }
        }

        
        
        public SwitchEventTypes EventType
        {
            get
            {
                return this.eventTypeField;
            }
            set
            {
                this.eventTypeField = value;
            }
        }

        
       
        public bool EventTypeSpecified
        {
            get
            {
                return this.eventTypeFieldSpecified;
            }
            set
            {
                this.eventTypeFieldSpecified = value;
            }
        }

        
        
        public int SwitchingCenterID
        {
            get
            {
                return this.switchingCenterIDField;
            }
            set
            {
                this.switchingCenterIDField = value;
            }
        }

        
       
        public bool SwitchingCenterIDSpecified
        {
            get
            {
                return this.switchingCenterIDFieldSpecified;
            }
            set
            {
                this.switchingCenterIDFieldSpecified = value;
            }
        }

        
        
        public System.DateTime EndTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        
       
        public bool EndTimeSpecified
        {
            get
            {
                return this.endTimeFieldSpecified;
            }
            set
            {
                this.endTimeFieldSpecified = value;
            }
        }

        #endregion


        public static int cnt = 0;
        public AlarmMessage()
        {
            cnt++;
        }

        ~AlarmMessage()
        {
            cnt--;
        }

        public static AlarmMessage Create(Match match, string version)
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
                alarmMsg.Title = match.Groups["AlarmName"].Value;


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
                    alarmMsg.Time = DateTime.Parse(match.Groups["ATime"].Value);
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
    }

}
