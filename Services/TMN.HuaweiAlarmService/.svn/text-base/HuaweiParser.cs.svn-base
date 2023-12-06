using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;

namespace TMN
{
    enum HuaweiAlarmTypeEnum
    {
        EVENT = 0,
        FAULT = 1,
        RECOVERY = 2,
        HISTORICAL = 3
    }
    class HuaweiParser
    {
        public HuaweiParser(bool insertInfoLog = true)
        {
            this.InsertInfoLog = insertInfoLog;
        }
        private bool InsertInfoLog = true;

        private HuaweiAlarmTypeEnum ParseAlarmType(string type)
        {
            switch (type)
            {
                case "event":
                    return HuaweiAlarmTypeEnum.EVENT;
                case "fault":
                    return HuaweiAlarmTypeEnum.FAULT;
                case "recovery":
                    return HuaweiAlarmTypeEnum.RECOVERY;
                case "historical":
                    return HuaweiAlarmTypeEnum.HISTORICAL;
                default:
                    return HuaweiAlarmTypeEnum.EVENT;
            }
        }

        private AlarmSeverities ParseSeverity(string severity)
        {
            switch (severity)
            {
                case "Warning alarm":
                case "Warning":
                    return AlarmSeverities.Information;
                case "Critical alarm":
                case "Critical":
                    return AlarmSeverities.Critical;
                case "Major alarm":
                case "Major":
                    return AlarmSeverities.Major;
                case "Minor alarm":
                case "Minor":
                    return AlarmSeverities.Minor;
                default:
                    return AlarmSeverities.Information;
            }
        }

        public List<LogAlarm> Parse(string receivedData)
        {
            List<LogAlarm> result = new List<LogAlarm>();
            string regEx;
            regEx = @"ALARM\s*(?<AlarmID>[\d]+)\s*(?<MessageType>[\w]+)\s*(?<AlarmSeverity>\w+)\s*\w*\s*(?<AlarmType>[\s\w]+?)\s*\r\n\s*(.+\r\n)+";

            var matches = Regex.Matches(receivedData, regEx);

            foreach (Match match in matches)
            {
                string type = match.Groups["MessageType"].Value;
                AlarmSeverities severity = ParseSeverity(match.Groups["AlarmSeverity"].Value);

                if (type.ToLower() == "fault")
                {
                    if (InsertInfoLog == true || severity != AlarmSeverities.Information)
                    {
                        LogAlarm logAlarm = new LogAlarm();

                        logAlarm.Time = GetTime(match.Value);
                        logAlarm.Data = match.Value;
                        logAlarm.Title = GetTitle(match.Value);
                        logAlarm.ID = Guid.NewGuid();
                        logAlarm.CenterID = Center.CurrentCenterID;
                        logAlarm.Location = GetLocation(match.Value);
                        logAlarm.MessageID = int.Parse(match.Groups["AlarmID"].Value);
                        logAlarm.Severity = (byte)severity; // (byte)ParseSeverity(match.Groups["AlarmSeverity"].Value);
                        logAlarm.IsRead = logAlarm.Severity != (byte)AlarmSeverities.Information ? false : true; //logAlarm.IsRead = false; // GetIsRead(match.Value);

                        result.Add(logAlarm);
                    }
                }
            }
            return result;
        }

        private static bool GetIsRead(string data)
        {

            if (Regex.IsMatch(data, @"Clear(ed)? time\s+=\s+\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}"))
                return true;
            return false;
        }

        private DateTime GetTime(string data)
        {
            try
            {
                Regex reg = new Regex(@"Alarm\s+\w+\s+time\s+=\s+(?<time>.+)");
                Match match = reg.Match(data);
                return DateTime.Parse(match.Groups["time"].Value);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Can't Parse Time of Alarm. \n alarm data: \n {0} \n exception: \n {1}", data, ex.ToString());
                return DateTime.Now; // switchVersion == "2" ? DateTime.Parse(match.Groups["Time"].Value) : DateTime.Now;
            }
        }

        private string GetLocation(string data)
        {
            try
            {
                Regex reg = new Regex(@"Location\s+\w+\s+=\s+(?<location>.+)");
                Match match = reg.Match(data);
                string loc = match.Groups["location"].Value;
                return loc.Length < 19 ? loc: loc.Substring(0, 19) ;
            }
            catch (Exception ex)
            {
                Logger.WriteError("Can't find location of Alarm. \n alarm data: \n {0} \n exception: \n {1}", data, ex.ToString());
                return ""; 
            }
        }

        private string GetTitle(string data)
        {
            try
            {
                Regex reg = new Regex(@"Alarm name\s+=\s+(?<name>.+)");
                Match match = reg.Match(data);
                string name = match.Groups["name"].Value;
                return name.Length < 50 ? name : name.Substring(0, 50);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Can't find name of Alarm. \n alarm data: \n {0} \n exception: \n {1}", data, ex.ToString());
                return "UNKOWN";
            }
        }

    }
}
