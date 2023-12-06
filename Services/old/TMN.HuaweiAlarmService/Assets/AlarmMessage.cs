using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public partial class AlarmMessage
    {

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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime StartTime
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlarmName
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
    }

}
