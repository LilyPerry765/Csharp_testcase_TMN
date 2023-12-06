using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Transactions;

namespace TMN
{
    class ServiceCore : TmnService
    {
        private ConnectionMethods ConnectionMethod = ConnectionMethods.Telnet;
        private TelnetAlarmReceiver telnetAlarmReceiver;
        private RS232AlarmReceiver rs232AlarmReceiver;

        public override void Start()
        {
            Enum.TryParse<ConnectionMethods>(RegSettings.Get(Program.CONNECTION_METHOD_KEY, ConnectionMethods.Telnet.ToString()) as string, out ConnectionMethod);
            if (rs232AlarmReceiver != null || telnetAlarmReceiver != null)
                this.Stop();
            if (ConnectionMethod == ConnectionMethods.SerialPort)
            {
                rs232AlarmReceiver = new RS232AlarmReceiver();
                rs232AlarmReceiver.SwitchDataReceived += new Action<string>(SwitchDataReceived);
                rs232AlarmReceiver.Start();
            }
            else
            {
                telnetAlarmReceiver = new TelnetAlarmReceiver();
                telnetAlarmReceiver.SwitchDataReceived += new Action<string>(SwitchDataReceived);
                telnetAlarmReceiver.Start();
            }
        }

        private object parseLock = new object();
        private void SwitchDataReceived(string data)
        {
            Logger.WriteDebug("Data received.{0}", data);
            //Logger.WriteDebug("Data received.");
            //lock (parseLock)
            {
                ParseData(data);
            }
        }



        Dictionary<Guid, int> recoverdAlarms = new Dictionary<Guid, int>();
        private void ParseData(string data)
        {
            try
            {
                EricssonParser parser = new EricssonParser();
                Logger.WriteInfo("Parsing data... ");
                //const string pattern = @"\s+(?<Severity>\D\d)/.+\s+"".+""\s+(?<MessageID>\d+)\s+(?<DateTime>\d{6}\s{3}\d{4}).*\s+(?<Title>.+)\s+((( *\S+)+ *\r\n)( +\r\n)?)*";
                //const string pattern = @"\s+(?<Severity>\D\d)/.+\s+"".+""\s+(?<MessageID>\d+)\s+(?<DateTime>\d{6}\s{3}\d{4}).*\s+(?<Title>.+)\s+((( *\S+)+ *(\r|\n){1,4})( +\r\n)?)*";
                //const string pattern = @"\s+(?<Severity>\D\d)/.+\s+"".+""\s+(?<MessageID>\d+)\s+(?<DateTime>\d{6}\s{3}\d{4}).*\s+(?<Title>.+)((\r?\n){0,2}[ \S]+)*";


                if (data.IndexOf("??") > 0)
                    throw new FormatException("لاگ سوئیچ درست دریافت نشده است.");
                
                const string pattern = @"(?<Severity>\D\d)/.+\s+"".+""\s+(?<MessageID>\d+)\s+(?<DateTime>\d{6}\s{3}\d{4}).*\s+(?<Title>.+)[\s|\S]*?(\r?\n\s*){3}";
                var matches = Regex.Matches(data, pattern);
                int inserts = 0;
                int recovereds = 0;
                using (var db = new TMNModelDataContext())
                {
                    List<LogAlarm> prevActiveAlarms = db.LogAlarms.Where(a => a.CenterID == Center.CurrentCenterID && a.IsRead == false && ((a.Severity <= 3 && a.Severity >= 1) || (a.Severity <= 13 && a.Severity >= 11))).ToList();
                    foreach (Match match in matches)
                    {
                        //Logger.WriteDebug("Check alarm in index : {0}", match.Index);
                        LogAlarm alarm = parser.Parse(match);
                        if (alarm != null && (AlarmSeverities)alarm.Severity != AlarmSeverities.None)
                        {
                            var existingAlarm = prevActiveAlarms.FirstOrDefault(a => a.MessageID == alarm.MessageID && a.Time == alarm.Time);
                            if (existingAlarm != null)
                            {
                                //Logger.WriteDebug("existing alarm ID : {0}", existingAlarm.ID);
                                if(recoverdAlarms.ContainsKey(existingAlarm.ID))
                                    if(recoverdAlarms[existingAlarm.ID] == 3)
                                        prevActiveAlarms.Remove(existingAlarm);
                                    else
                                        recoverdAlarms[existingAlarm.ID] += 1;
                                else
                                    recoverdAlarms.Add(existingAlarm.ID, 1);
                            }
                            else
                            {
                                try
                                {
                                    //Logger.WriteDebug("new alarm ID : {0}", alarm.ID);
                                    db.LogAlarms.InsertOnSubmit(alarm);
                                    db.SubmitChanges();
                                    inserts++;
                                    CountNewAlarms(alarm);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Write(ex);
                                }
                            }
                        }
                    }
                    recovereds = prevActiveAlarms.Count;
                    prevActiveAlarms.ForEach((a) =>
                    {
                        StringBuilder rs = new StringBuilder();
                        rs.AppendLine();
                        rs.Append("RECOVERY ALARM  ");
                        rs.AppendLine();
                        rs.AppendFormat("TIME = {0}", a.Time > DateTime.Now ? a.Time.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        a.IsRead = true;
                        if (a.Severity >= 11 && a.Severity <= 13) 
                            a.Severity = (byte)(a.Severity % 10);
                        a.Data = a.Data + rs.ToString();
                        recoverdAlarms.Remove(a.ID);
                    });
                    db.SubmitChanges();
                }
                Logger.WriteInfo("{0} alarm(s) inserted. {1} recovered.", inserts, recovereds);
                SetCenterNewAlarms();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }


        public override void Stop()
        {
            Logger.WriteInfo("Stopping service...");
            if ( rs232AlarmReceiver != null)
            {
                rs232AlarmReceiver.Stop();
                rs232AlarmReceiver = null;
            }
            if (telnetAlarmReceiver != null)
            {
                telnetAlarmReceiver.Stop();
                telnetAlarmReceiver = null;
            }
            else
            {
                Logger.WriteError("Unknown connection method! Could not stop service.");
                return;
            }
            Logger.WriteEnd("Service stopped.");
        }
    }

}
