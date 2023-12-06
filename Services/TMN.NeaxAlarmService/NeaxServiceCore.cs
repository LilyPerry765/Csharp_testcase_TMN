using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Timers;
using System.Reflection;
using System.Net.NetworkInformation;

namespace TMN
{
    class NeaxServiceCore : TmnService
    {
        private const string SwitchIP = "192.168.0.10";

        private Ping ping = new Ping();
        private Timer timer;
        private DateTime deleteTimeout = DateTime.Now;

        public override void Start()
        {
            if (CheckDBConnection())
            {
                timer = new Timer(AlarmQueryInterval);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Start();
                Logger.WriteInfo("Alarms will be checked about every {0} seconds.", AlarmQueryInterval);
            }
        }

        public override void Stop()
        {
            if(timer != null)
                timer.Stop();
            Logger.WriteEnd("Neax Alarm Service stopped.");
        }

       private static bool isPinged = false;
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                new System.Threading.Thread(() =>
                {
                    isPinged = PingSwitch();
                }).Start();
                if (isPinged)
                    CollectAlarms();
                else
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل درارتباط شبکه ای با سوییچ", "Could not ping switch!");

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
            }
            finally
            {
                timer.Start();
            }
        }

        private bool PingSwitch()
        {
            try
            {
                Logger.WriteInfo("Pinging switch...");
                var pingResult = ping.Send(SwitchIP);
                if (pingResult.Status == IPStatus.Success)
                {
                    Logger.WriteInfo("Ping succeeded.");
                    return true;
                }
                else
                {
                    Logger.WriteCritical("Pinging switch ({0}) failed: {1}", SwitchIP, pingResult.Status);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }

        private bool CheckDBConnection()
        {
            using (var cnn = new TMNModelDataContext().Connection)
            {
                try
                {
                    cnn.Open();
                    Logger.WriteInfo("Found TMN Database.");
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                    Logger.WriteDebug("Connecting to server '{0}' failed. ", new System.Data.SqlClient.SqlConnectionStringBuilder(cnn.ConnectionString).DataSource);
                }
            }
            return false;
        }

        private DateTime? lastAlarmDate = null;
        private void CollectAlarms()
        {

            try
            {
                string filePath = (string)RegSettings.Get("VitaOneDataFilePath");
                var insertsCount = 0;
                var deletesCount = 0;
                var recoveredCount = 0;

                if (!System.IO.File.Exists(filePath))
                {
                    Logger.WriteCritical("Path \"{0}\" is invalid or not found.", filePath);
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در یافتن فایل مقصد", filePath);
                }
                else
                {
                    Logger.WriteInfo("Receiving alarms...");
                    ServiceState.ReportActivity(ServiceTypes.AlarmService);
                    IEnumerable<NeaxMessage> neaxMessages = null;
                    using (var db = new TMN.TMNModelDataContext())
                    {
                        if (lastAlarmDate == null)
                        {
                            lastAlarmDate = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID).OrderBy(la => la.Time).Select(la => la.Time).FirstOrDefault();
                            
                            lastAlarmDate = lastAlarmDate.Value == DateTime.MinValue ? db.GetDate() : lastAlarmDate;
                        }
                    }
                    neaxMessages = GetNeaxMessagesByDate(filePath, lastAlarmDate.Value).SequencialSort(m => m.MSG_ID);
                    if (neaxMessages.Count() > 0)
                        lastAlarmDate = neaxMessages.Max(m => m.MESSAGE_DATE);

                    Logger.WriteDebug("{0} messages received.", neaxMessages.Count());

                    foreach (var msg in neaxMessages)
                    {
                        insertsCount++;
                        var isRead = neaxMessages.Any(rm => rm.RECOVER_NUMBER == msg.MESSAGE_NUMBER && rm.KEY_INFORMATION == msg.KEY_INFORMATION && rm.MESSAGE_DATE >= msg.MESSAGE_DATE);
                        SaveAlarm(msg.ALARM_CLASS, msg.MESSAGE_DATE, msg.ALARM_BODY, msg.DETAIL, msg.MSG_ID, isRead);
                        if (msg.RECOVER_NUMBER > 0)
                        {
                            var recoveredMessage = FindRecoveredMessage(filePath, msg);

                            if (recoveredMessage != null)
                            {
                                recoveredCount += MarkAlarmAsRecovered(recoveredMessage.MESSAGE_DATE, recoveredMessage.MSG_ID, recoveredMessage.DETAIL);
                            }
                        }
                        if (insertsCount % 100 == 0)
                        {
                            ServiceState.ReportActivity(ServiceTypes.AlarmService);
                        }

                    }
                    if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
                    {
                        deletesCount = DB.Instance.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information, AlarmExpireSeconds);
                        deleteTimeout = DateTime.Now;
                    }
                    SetCenterNewAlarms();
                    Logger.WriteInfo("{0} alarm(s) inserted, {1} recovered,  and {2} deleted.", insertsCount, recoveredCount, deletesCount);
                }
            }
            catch (Exception  ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
            }
        }

        private IEnumerable<NeaxMessage> GetNeaxMessagesByDate(string filePath, DateTime startDate)
        {
            return NeaxMessage.GetMessages(filePath, string.Format("SELECT * FROM MESSAGE WHERE MESSAGE_DATE > #{0}#", startDate.ToString("yyyy/MM/dd HH:mm:ss")));
            
        }

        private NeaxMessage FindRecoveredMessage(string filePath, NeaxMessage recoveryMessage)
        {
            return NeaxMessage.GetMessages(filePath, string.Format("SELECT TOP 1 * FROM MESSAGE WHERE MESSAGE_DATE <= #{0}# AND MESSAGE_NUMBER={1} AND KEY_INFORMATION='{2}' ORDER BY MESSAGE_DATE DESC", recoveryMessage.MESSAGE_DATE, recoveryMessage.RECOVER_NUMBER, recoveryMessage.KEY_INFORMATION)).FirstOrDefault();
        }

        private int MarkAlarmAsRecovered(DateTime alarmTime, int alarmID, string detail)
        {
            var recoveredCount = 0;
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    var foundAlarms = db.LogAlarms.Where(a => a.CenterID == Center.CurrentCenterID && a.IsRead == false && a.Time == alarmTime && a.MessageID == alarmID);
                    foreach (var alarm in foundAlarms)
                    {
                        alarm.IsRead = true;

                        if (alarm.Severity >= 11 && alarm.Severity <= 13)
                            alarm.Severity = (byte)(alarm.Severity % 10);

                        alarm.Data += "\n  RECOVERY ALARM  \n" + detail;
                        recoveredCount++;
                    }

                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return recoveredCount;
        }

        private void SaveAlarm(int alarmClass, DateTime time, string title, string data, int messageID, bool isRead)
        {
            // Logger.WriteDebug("Title Length={0}, messageID={1}", title.Length, messageID);

            try
            {
                LogAlarm alarm = new LogAlarm()
                    {
                        Data = data,
                        ID = Guid.NewGuid(),
                        IsRead = isRead,
                        Time = time,
                        Severity = (byte)AlarmClassToSeverity(alarmClass),
                        Title = title.Length > 50 ? title.Substring(0, 47) + "..." : title,
                        CenterID = Center.CurrentCenterID,
                        MessageID = messageID
                    };
                if (alarm.Severity == (byte)AlarmSeverities.Information)
                {
                    alarm.IsRead = true;
                    //if(time.AddSeconds(AlarmExpireSeconds).Ticks < dbDate.Ticks)
                    //    return false;
                }
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    db.LogAlarms.InsertOnSubmit(alarm);
                    db.SubmitChanges();
                    CountNewAlarms(alarm);

                }
                //return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            //return false;
        }

        private AlarmSeverities AlarmClassToSeverity(int alarmClass)
        {
            switch (alarmClass)
            {
                case 1:
                    return AlarmSeverities.Critical;
                case 2:
                    return AlarmSeverities.Major;
                case 3:
                    return AlarmSeverities.Minor;
                case 4:     //Caution
                default:    //Information
                    return AlarmSeverities.Information;
            }
        }


    }
}
