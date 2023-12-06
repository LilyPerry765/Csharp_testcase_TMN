using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;

namespace TMN
{
    public class EricssonParser
    {
        internal LogAlarm Parse(Match alarmMatch)
        {
            try
            {
                LogAlarm alarm = new LogAlarm();
                alarm.CenterID = Center.CurrentCenterID;
                alarm.Data = alarmMatch.Value.Trim();
                alarm.ID = Guid.NewGuid();
                alarm.IsRead = false;
                alarm.Location = null;
                alarm.MessageID = int.Parse(alarmMatch.Groups["MessageID"].Value);
                alarm.Severity = (byte)ParseSeverity(alarmMatch.Groups["Severity"].Value);
                alarm.Time = DateTime.ParseExact(alarmMatch.Groups["DateTime"].Value, "yyMMdd   HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string title = alarmMatch.Groups["Title"].Value.Trim();
                alarm.Title = title.Length <= 50 ? title : title.Substring(0, 47) + "...";
                return alarm;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        private AlarmSeverities ParseSeverity(string severity)
        {
            switch (severity)
            {
                case "A1":
                    return AlarmSeverities.Critical;
                case "A2":
                    return AlarmSeverities.Major;
                case "A3":
                    return AlarmSeverities.Minor;
                case "O1":
                case "O2":
                    return AlarmSeverities.Information;
                default:
                    return AlarmSeverities.Information;
            }
        }
    }
}
