using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TMN
{
     class EwsdLogParser
    {
        public  LogAlarm ParseLog(string logBlock, IEnumerable<AlarmSeverityOverride> severityOverrides, DateTime? lastAlarmDate)
        {
            try
            {

                DateTime time = GetDateTime(logBlock);
                if (lastAlarmDate == null || time >= lastAlarmDate.Value)
                {
                    AlarmSeverities sevirity = DetermineSeverity(logBlock);
                    string title = GetTitle(logBlock);
                    string ltg = GetLtg(logBlock);


                    if (title.Length > 50)
                        title = title.Substring(0, 50);

                    var newLogAlarm = new LogAlarm()
                    {
                        ID = Guid.NewGuid(),
                        Data = logBlock,
                        IsRead = (sevirity == AlarmSeverities.Information) ? true : false,
                        Severity = (byte)sevirity,
                        Time = time,
                        Title = title,
                        CenterID = Center.CurrentCenterID,
                        Location = ltg,
                        MessageID = GetMaskNumber(logBlock)
                    };
                    return newLogAlarm;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }


        private  DateTime GetDateTime(string alarmBlock)
        {
            DateTime d;
            var g = Regex.Match(alarmBlock, @".+\s+(?<date>\d+\-\d+\-\d+\s\s\d+\:\d+:\d+)").Groups;
            string dateString = g["date"].Value;
            try
            {
                d = DateTime.ParseExact(dateString, "yy-MM-dd  HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, dateString);
                d = DateTime.Now;
            }
            return d;
        }

        private  string GetTitle(string block)
        {
            try
            {
                const string pattern = @"\r\n\r\n(\s*MASKNO\:\d+(\r\n)*)?(?:DATA\:\r\n)?\**\s*(?<title>.+?)\s{2,}.*";
                var matches = Regex.Matches(block, pattern);
                if (matches.Count > 0)
                {
                    string title = Regex.Matches(block, pattern)[0].Groups["title"].Value;
                    return title.Trim();
                }
                else
                {
                    Logger.WriteWarning("Unable to detect title with the given pattern in this block.\nPattern:\n{0}\nBlock:\n{1}", pattern, block);
                    return "<Unknown Title>";
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return string.Empty;
            }
        }

        private  int? GetMaskNumber(string block)
        {
            const string pattern = @"\s+\d+\/(?<Mask>\d+)\s+";
            try
            {
                var match = Regex.Match(block, pattern);
                if (match.Success)
                {
                    return int.Parse(match.Groups["Mask"].Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        private  string GetLtg(string block)
        {
            var match = Regex.Match(block, @"LTG =\s+(?<ltg>.*),\s*DIU = (?<diu>\d+)");
            if (match.Success)
            {
                var g = match.Groups;
                string ltg = g["ltg"].Value.Trim();
                string dui = g["diu"].Value.Trim();
                return string.Format("{0}-{1}", ltg, dui);
            }
            return null;
        }

        private  AlarmSeverities DetermineSeverity(string alarmBlock)
        {
            AlarmSeverities sevirity = AlarmSeverities.Information;
            //Match match = Regex.Match(alarmBlock, @".*\r\n.*\r\n.*\r\n(?<severity>\*+) ");
            //if (match.Success)
            //{
            //    if (match.Groups["severity"].Value.Length == 3)
            //        sevirity = AlarmSeverities.Critical;
            //    else if (match.Groups["severity"].Value.Length == 2)
            //        sevirity = AlarmSeverities.Major;
            //    else 
            //        sevirity = AlarmSeverities.Minor;
            //}
            //return sevirity;


            if (!alarmBlock.Contains("EDTS8"))
            {
                if (Regex.IsMatch(alarmBlock, @"\n\*{3} "))
                    sevirity = AlarmSeverities.Critical;
                else if (Regex.IsMatch(alarmBlock, @"\n\*{2} "))
                    sevirity = AlarmSeverities.Major;
                else if (Regex.IsMatch(alarmBlock, @"\n\* "))
                    sevirity = AlarmSeverities.Minor;
            }

            return sevirity;
        }
    }
}
