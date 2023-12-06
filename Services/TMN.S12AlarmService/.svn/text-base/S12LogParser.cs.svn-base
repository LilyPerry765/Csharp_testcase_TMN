using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;
using System.Globalization;

namespace TMN
{
     class S12LogParser
    {
        //public  IEnumerable<LogAlarm> GetRecentAlarms(string text, string processorName)
        //{
        //    var result = GetAlarms(text, processorName).GroupBy(a => a.Time).OrderBy(g => g.Key).ToArray();
        //    if (result == null)
        //    {
        //        return Enumerable.Empty<LogAlarm>();
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}


        public  IEnumerable<LogAlarm> GetAlarms(string text, string processorName)
        {
            List<LogAlarm> alarms = new List<LogAlarm>();
            foreach (string block in GetLogBlocks(text, processorName))
            {
                AlarmSeverities severity = DetermineSeverity(block);
                string location = "" + GetLocation(block);
                LogAlarm newAlarm = new LogAlarm()
                {
                    ID = Guid.NewGuid(),
                    IsRead = severity == AlarmSeverities.Information ? true : false,
                    Title = GetTitle(block),
                    Data = block,
                    CenterID = Center.CurrentCenterID,
                    MessageID = GetMessageID(block),
                    Location =  location == "" ? null : location,
                    Time = GetDateTime(block),
                    Severity = (byte)DetermineSeverity(block)
                };

                alarms.Add(newAlarm);
            }
            return alarms;
        }

        //private  bool? CheckNeedVisibile(string block)
        //{
        //    return !block.Contains("ALARM STATE = ON"); 
        //}

        private  IEnumerable<string> GetLogBlocks(string inputText, string processorName)
        {
            var matches = Regex.Matches(inputText, processorName);
            int start = 0; int end = 0;
            List<string> blocks = new List<string>();
            for (int i = 0; i < matches.Count; i++ )
            {
                start = matches[i].Index;
                end = matches.Count == (i + 1) ? inputText.Length : matches[i + 1].Index;
                string data = inputText.Substring(start, end - start);
                if (!string.IsNullOrWhiteSpace(data))
                    blocks.Add(data);
            }
            return blocks;
        }

        private  DateTime GetDateTime(string block)
        {
            DateTime d;
            string dateString = "";
            var match = Regex.Match(block, @".+\s+(?<date>\d+\-\d+\-\d+\s\s\d+\:\d+\:\d+)");
            if(match.Success)
                dateString = match.Groups["date"].Value;
            try
            {
                d = DateTime.ParseExact(dateString, "yyyy-MM-dd  HH:mm:ss", CultureInfo.InvariantCulture);
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
                string title = "";
                string type = "";
                var match = Regex.Match(block, @"SWA-.*");
                if (match.Success)
                    title = match.Value.Trim();
                else
                {
                    match = Regex.Match(block, @".*(\r|\n).*(\r|\n).*(\r|\n)\s*(?<title>.+)");

                    if (match.Success)
                        title = match.Groups["title"].Value.Trim();
                    else
                        throw new FormatException(block); 
                }

                match = Regex.Match(block, @"ALARM TYPE\s+(?<type>\w+)");
                if(match.Success)
                    type = match.Groups["type"].Value;

                if ((title.Length + type.Length) > 50)
                {
                    title = title.Substring(0, 50 - type.Length -5);
                    title = string.Format("{0}({1})...", title, type);
                }

                return title;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Title dosen't match pattern");
            }
            return "UNDETERMINED TITLE";
        }

        private  int? GetMessageID(string block)
        {
            const string pattern = @"NO\s=\s(?<ID>\d+)";
            try
            {
                var match = Regex.Match(block, pattern);
                if (match.Success)
                {
                    return int.Parse(match.Groups["ID"].Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        private  string GetLocation(string block)
        {
            var match = Regex.Match(block, @"NA\s*=\s*H'(?<NA>\w+)\s*");
            if (match.Success)
                return match.Groups["NA"].Value.Trim();

            match = Regex.Match(block, @"NA\s*=\s*(?<NA>\w+)\s*");
            if (match.Success)
                return match.Groups["NA"].Value.Trim();

            match = Regex.Match(block, @"(?<NA>.+NA.+)(\r\n)?[ |-]+?\r\n(?<table>[\w|'|\s|\r|\n]+)[-]+");
            if (match.Success)
            {
                string locations = "";
                int NaIndex = 0;
                string head = match.Groups["NA"].Value;
                string []arrHeadItems = Regex.Replace(head, @"\s+", " ").Split(' ');
                for (int i = 0; i < arrHeadItems.Length; i++)
                {
                        if(arrHeadItems[i] == "NA")
                        {
                            NaIndex = i;
                            break;
                        }
                }
                string table = match.Groups["table"].Value;

                string[] row = table.Replace("\r\n", "\n").Split('\n');
                for (int i = 0; i < row.Length; i++)
                {
                    string[] column = Regex.Replace(row[i], @"\s+", " ").Split(' ');
                    if (column.Length > NaIndex)
                        locations += column[NaIndex] + "_";
                }

                return locations.Replace("H'","").TrimEnd('_');
            }


            match = Regex.Match(block, @"DN\s*(=|:)\s*(?<DN>\d+)\s*");

            if (match.Success)
                return match.Groups["DN"].Value.Trim();


            return null;
        }

        private  AlarmSeverities DetermineSeverity(string block)
        {
            var match = Regex.Match(block, @"^.*(\r|\n).*(\r|\n)\s*(->)?(?<severity>\*+)");
            AlarmSeverities sevirity;
            string result = "";
            if (match.Success)
                result = match.Groups["severity"].Value.Trim();

            if (result.Length == 3)
                sevirity = AlarmSeverities.Critical;
            else if (result.Length == 2)
                sevirity = AlarmSeverities.Major;
            else if (result.Length == 1)
                sevirity = AlarmSeverities.Minor;
            else
                sevirity = AlarmSeverities.Information;


            return sevirity;
        }

    }
}
