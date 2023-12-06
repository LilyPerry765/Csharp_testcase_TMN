using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TMN
{
     class KaraLogParser
    {
         public List<LogAlarm> GetLogBlocks(string logBlock, IEnumerable<AlarmSeverityOverride> severityOverrides)
        {
            List<LogAlarm> alarms = new List<LogAlarm>();
            try
            {
                var maches = Regex.Matches(logBlock, @"(?<yea>\d{4})/(?<mon>\d{1,2})/(?<day>\d{1,2})\s:\s(?<hou>\d{2}):(?<min>\d{2}):(?<sec>\d{2})\s*((?<Severity>\w+)\s*\->)?\s*(?<Title>.+)");
                Match old = null;
                foreach (Match item in maches)
                {
                    if (old != null)
                    {
                        LogAlarm alarm = ParseBlock(old, item, logBlock);
                        alarms.Add(alarm);
                    }
                    old = item;
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return alarms;
        }

         private LogAlarm ParseBlock(Match match, Match next, string logblock)
         {
             AlarmSeverities sevirity = DetermineSeverity(match.Groups["Severity"].Value);
             string title = GetTitle(match.Groups["Title"].Value);
             DateTime time = GetDateTime(match.Groups["yea"].Value, match.Groups["mon"].Value, match.Groups["day"].Value, match.Groups["hou"].Value, match.Groups["min"].Value, match.Groups["sec"].Value);

             var newLogAlarm = new LogAlarm()
             {
                 ID = Guid.NewGuid(),
                 Data = logblock.Substring(match.Index, next.Index - match.Index),
                 IsRead = (sevirity == AlarmSeverities.Information) ? true : false,
                 Severity = (byte)sevirity,
                 Time = time,
                 Title = title,
                 CenterID = Center.CurrentCenterID,
                 Location = null,
                 MessageID = null
             };
             return newLogAlarm;
         }


        private  DateTime GetDateTime(string yea, string mon, string day, string hou, string min, string sec)
        {
            //var g = Regex.Match(datetime, @"(?<yea>\d{4})/(?<mon>\d{1,2})/(?<day>\d{1,2})\s:\s(?<hou>\d{2}):(?<min>\d{2}):(?<sec>\d{2})").Groups;
            PersianCalendar calendar = new PersianCalendar();
            DateTime d;
            try
            {
                d = calendar.ToDateTime(int.Parse(yea), int.Parse(mon), int.Parse(day), int.Parse(hou), int.Parse(min), int.Parse(sec), 0);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "error parsing time of alarm : \n {0}/{1}/{2} {3}:{4}:{5}", yea, mon,day,hou, min, sec);
                d = DateTime.Now;
            }
            return d;
        }

        private  string GetTitle(string title)
        {
            try
            {
                int dotindex = title.IndexOf('.');
                if (dotindex != -1)
                    title = title.Substring(0, dotindex);
                if (title.Length > 50)
                    title = title.Substring(0, 50);
                return title;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return string.Empty;
            }
        }



        private  AlarmSeverities DetermineSeverity(string severity)
        {
            AlarmSeverities sevirity = AlarmSeverities.Information;

            if (severity == "Critical")
                sevirity = AlarmSeverities.Critical;
            if (severity == "Major")
                sevirity = AlarmSeverities.Major;
            if (severity == "Minor")
                sevirity = AlarmSeverities.Minor;

            return sevirity;
        }
    }
}
