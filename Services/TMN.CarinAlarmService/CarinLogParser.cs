using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TMN
{
     class CarinLogParser
    {
         private const string AlarmPattern = @"(?<Date>\d+-\d+-\d+\s\d+:\d+:\d+).+(\r|\n)*\w+?(?<ID>\d+)\s+(?<Title>.+)(\r|\n|\s)*.+?\s+LOCATION\s+:\s+(?<Location>.+)(\r|\n)*(\s+CLASS\s+:\s+(?<Class>.+))?";

        public  IEnumerable<LogAlarm> GetAlarms(string text)
        {
            List<LogAlarm> result = new List<LogAlarm>();
            foreach (Match match in Regex.Matches(text, AlarmPattern))
            {
                LogAlarm newAlarm = new LogAlarm()
                {
                    ID = Guid.NewGuid(),
                    IsRead = false,
                    Title = match.Groups["Title"].Value,
                    Data = match.Value,
                    CenterID = Center.CurrentCenterID,
                    MessageID = int.Parse(match.Groups["ID"].Value),
                    Location = match.Groups["Location"].Value,
                    Time = DateTime.Parse(match.Groups["Date"].Value),
                    Severity = (byte)ParseSeverity(match.Groups["Class"].Value)
                };
                newAlarm.Title = newAlarm.Title.Length > 50 ? newAlarm.Title.Substring(0, 47) + "..." : newAlarm.Title;
                newAlarm.Location = newAlarm.Location.Length > 20 ? newAlarm.Location.Substring(0, 17) + "..." : newAlarm.Location;
                result.Add(newAlarm);
            }
            return result;
        }

        //public  IEnumerable<LogAlarm> GetRecentAlarms(string text)
        //{
        //    var result = GetAlarms(text).GroupBy(a => a.Time).OrderBy(g => g.Key).LastOrDefault();
        //    if (result == null)
        //    {
        //        return Enumerable.Empty<LogAlarm>();
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}

        private  AlarmSeverities ParseSeverity(string severity)
        {
            if (severity.ToUpper().Contains("MAJOR"))
            {
                return AlarmSeverities.Major;
            }
            else if (severity.ToUpper().Contains("MINOR"))
            {
                return AlarmSeverities.Minor;
            }
            else
            {
                return AlarmSeverities.Information;
            }
        }
    }
}
