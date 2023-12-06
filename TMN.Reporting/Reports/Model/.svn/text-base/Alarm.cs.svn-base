using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    public class Alarm
    {
        private const string DateFormat = "yyyy/MM/dd";
        TMN.Alarm alarm;
        public Alarm(TMN.Alarm alarm)
        {
            this.alarm = alarm;
        }

        public string ReportDate
        {
            get
            {
                return alarm.ReportTime.Value.ToPersianDate().ToString(DateFormat) + "\n"
                    + alarm.ReportTime.Value.ToString(DateFormat);
            }
        }

        public string ReportTime
        {
            get
            {
                return alarm.ReportTime.Value.ToString("HH:mm");
            }
        }

        public string AnnounceDate
        {
            get
            {
                if (alarm.AnnounceDate.HasValue)
                    return alarm.AnnounceDate.Value.ToPersianDate().ToString(DateFormat) + "\n"
                    + alarm.AnnounceDate.Value.ToString(DateFormat); 
                return null;
            }
        }

        public string Dest
        {
            get
            {
                return alarm.Route.Destination.Name;
            }
        }

        public string LinkAddress
        {
            get
            {
                return alarm.Link.Address;
            }
        }

        public string Reporter
        {
            get
            {
                return alarm.User.FullName;
            }
        }

        public string Receiver
        {
            get
            {
                return alarm.ReportReceiver;
            }
        }

        public string AlarmType
        {
            get
            {
                return alarm.AlarmType.Name;
            }
        }

        public string Fixer
        {
            get
            {
                return alarm.Fixer;
            }
        }

        public string Assessor
        {
            get
            {
                return alarm.Assessor;
            }
        }

        public string ConnectDate
        {
            get
            {
                if (alarm.ConnectTime.HasValue)
                {
                    return alarm.ConnectTime.Value.ToPersianDate().ToString(DateFormat) + "\n"
                        + alarm.ConnectTime.Value.ToString(DateFormat);
                }
                return "-";
            }
        }

        public string DisconnectDate
        {
            get
            {
                if (alarm.DisconnectTime.HasValue)
                {
                    return alarm.DisconnectTime.Value.ToPersianDate().ToString(DateFormat) + "\n"
                        + alarm.DisconnectTime.Value.ToString(DateFormat);
                }
                return "-";
            }
        }

        public string ConnectT
        {
            get
            {
                if (alarm.ConnectTime.HasValue)
                {
                    return alarm.ConnectTime.Value.ToString("HH:mm");
                }
                return "-";
            }
        }

        public string DisconnectT
        {
            get
            {
                if (alarm.DisconnectTime.HasValue)
                {
                    return alarm.DisconnectTime.Value.ToString("HH:mm");
                }
                return "-";
            }
        }

        public string DamagePlace
        {
            get
            {
                return alarm.DamagePlace;
            }
        }

        public string Duration
        {
            get
            {
                return alarm.Duration;
            }
        }

        public string Description
        {
            get
            {
                return alarm.Description;
            }
        }

        public string CenterType
        {
            get
            {
                return alarm.Route.Destination == null ? null : ((CenterTypes)alarm.Route.Destination.CenterType).ToString();
            }
        }


    }
}
