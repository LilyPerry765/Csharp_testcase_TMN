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
    class KaraServiceCore : TmnService
    {
        private string serverName = "";
        private string userName = "";
        private string passName = "";

        private Timer timer;
        private  DateTime deleteTimeout = DateTime.Now;

        public override void Start()
        {
            timer = new Timer(AlarmQueryInterval);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            Logger.WriteInfo("Alarms will be checked about every {0} seconds.", AlarmQueryInterval);
        }

        public override void Stop()
        {
            Logger.WriteEnd("Kara Alarm Service stopped.");
            timer.Stop();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                if (!hasDbConnectionResult)
                    CheckDBConnection();
                else
                   CollectAlarms();
                
                if(!hasDbConnectionResult)
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکلد در ارتباط با بانک اطلاعاتی", "Could not connect database!");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.ToString());
            }
            finally
            {
                timer.Start();
            }
        }

        private bool hasDbConnectionResult = false;
        private void CheckDBConnection()
        {
            
            serverName = (string)RegSettings.Get("KaraSqlServerAddress");
            userName = (string)RegSettings.Get("KaraSqlServerUser");
            passName = (string)RegSettings.Get("KaraSqlServerPass");
            using (var cnn = KaraEvent.GetConnection(serverName, userName, passName)) 
            {
                try
                {
                    cnn.Open();
                    Logger.WriteInfo("Found kara Database.");
                    hasDbConnectionResult = true;
                }
                catch (Exception ex)
                {

                    Logger.Write(ex);
                    Logger.WriteDebug("Connecting to server '{0}' failed. ", new System.Data.SqlClient.SqlConnectionStringBuilder(cnn.ConnectionString).DataSource);
                    return;
                }
            }

            using (var cnn = new TMNModelDataContext().Connection)
            {
                try
                {
                      cnn.Open();
                  Logger.WriteInfo("Found TMN Database.");
                    hasDbConnectionResult = true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                    Logger.WriteDebug("Connecting to server '{0}' failed. ", new System.Data.SqlClient.SqlConnectionStringBuilder(cnn.ConnectionString).DataSource);
                }
            }
        }

        private  DateTime lastAlarmDate ;
        private void CollectAlarms()
        {

            try
            {
                var insertsCount = 0;
                var deletesCount = 0;
                var recoveredCount = 0;
                lastAlarmDate = DateTime.Parse("" + RegSettings.Get("KaraLastAlarmDate", DateTime.Now));



                Logger.WriteInfo("Receiving alarms...");
                ServiceState.ReportActivity(ServiceTypes.AlarmService);
                IEnumerable<KaraEvent> karaEvent = null;


                karaEvent = GetKaraEventByDate(lastAlarmDate);
                if (karaEvent.Count() > 0)
                    lastAlarmDate = karaEvent.LastOrDefault().EventDateTime;

                Logger.WriteDebug("{0} messages received.", karaEvent.Count());

                foreach (var ke in karaEvent)
                {
                    insertsCount++;
                    var isRead = karaEvent.Any(rm => rm.HashCode == ke.HashCode && rm.EventStateEnum == StateEnum.Recovery);
                    if(ke.EventStateEnum == StateEnum.Failed)
                        SaveAlarm(ke, isRead);
                    else if (ke.EventStateEnum == StateEnum.Recovery )
                    {
                        var recoveredEvent = FindRecoveredEvent(ke);

                        if (recoveredEvent != null)
                        {
                            recoveredCount += MarkAlarmAsRecovered(recoveredEvent.MsgID, ke.Data);
                        }
                    }
                    //if (insertsCount % 100 == 0)
                    //{
                    //    ServiceState.ReportActivity(ServiceTypes.AlarmService);
                    //}
                }
                if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
                {
                    deletesCount = DB.Instance.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information, AlarmExpireSeconds);
                    deleteTimeout = DateTime.Now;
                }
                RegSettings.Save("KaraLastAlarmDate", lastAlarmDate);

                Logger.WriteInfo("{0} alarm(s) inserted, {1} recovered,  and {2} deleted.", insertsCount, recoveredCount, deletesCount);
                SetCenterNewAlarms(); 
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                hasDbConnectionResult = false;
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.ToString());
            }
        }

        private IEnumerable<KaraEvent> GetKaraEventByDate( DateTime startDate)
        {
            return new KaraEvent().GetEvents(string.Format("(EventDate + ' ' + EventTime) > '{0}'", startDate.ToString("yyyy/MM/dd hh:mm:ss")), serverName, userName, passName,true);
        }

        private KaraEvent FindRecoveredEvent(KaraEvent recoveryEvent)
        {
            return new KaraEvent().GetEvents(string.Format("HashCode = '{0}'", recoveryEvent.HashCode), serverName, userName, passName, false).FirstOrDefault();
        }

        private int MarkAlarmAsRecovered(int alarmID, string recoveryData)
        {
            var recoveredCount = 0;
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    var foundAlarms = db.LogAlarms.Where(a => a.CenterID == Center.CurrentCenterID && a.IsRead == false && a.MessageID == alarmID);
                    foreach (var alarm in foundAlarms)
                    {
                        alarm.Data += "\n  RECOVERY ALARM  \n" + recoveryData;
                        alarm.IsRead = true;
                        if (alarm.Severity >= 11 && alarm.Severity <= 13) 
                            alarm.Severity = (byte)(alarm.Severity % 10);
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

        private  void SaveAlarm(KaraEvent kara, bool isRead)
        {
            try
            {
                LogAlarm alarm = new LogAlarm()
                    {
                        Data = kara.Data,
                        ID = Guid.NewGuid(),
                        IsRead = isRead,
                        Time = kara.EventDateTime,
                        Severity = (byte)AlarmClassToSeverity(kara.ImportanceEnum),
                        Title = kara.FaultDescription.Length > 50 ? kara.FaultDescription.Substring(0, 47) + "..." : kara.FaultDescription,
                        CenterID = Center.CurrentCenterID,
                        MessageID = kara.MsgID,
                        Location = kara.Location
                    };

                if (alarm.Severity == (byte)AlarmSeverities.Information)
                    alarm.IsRead = true;

                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    db.LogAlarms.InsertOnSubmit(alarm);
                    db.SubmitChanges();
                    CountNewAlarms(alarm);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private  AlarmSeverities AlarmClassToSeverity(ImportanceEnum importance )
        {
            switch (importance)
            {
                case ImportanceEnum.CRITICAL:
                    return AlarmSeverities.Critical;
                case ImportanceEnum.MAJOR:
                    return AlarmSeverities.Major;
                case ImportanceEnum.MINOR:
                    return AlarmSeverities.Minor;
                case ImportanceEnum.INFO:     //Caution
                default:    //Information
                    return AlarmSeverities.Information;
            }
        }


    }
}
