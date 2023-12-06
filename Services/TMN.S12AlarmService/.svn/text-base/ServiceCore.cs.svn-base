using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Enterprise;
using System.IO;
using System.Timers;
using System.Reflection;
using System.Xml.Linq;
using System.Windows.Threading;
//using System.Threading;

namespace TMN
{
    /// <summary>
    /// S12 Service Core
    /// </summary>
    class ServiceCore : TmnService
    {

        #region properties
        private Timer timer;
        public Dictionary<string, DateTime> changedFiles = new Dictionary<string, DateTime>();
        //private List<string> changedLogFiles = new List<string>();
        private string processorName = "";
        private bool isRepviewInitialized = false;
        private DateTime changeTime = DateTime.Now;
        private DateTime deleteTimeout = DateTime.Now;
        //private Watcher watcher = null;
        private List<AlarmSeverityOverride> severityOverrides;
        private string[] LogFolders;
        #endregion

        #region accessors


        public int DayOfAlarmLogs
        {
            get
            {
                try
                {
                    return (int)RegSettings.Get(Constants.LogFieDay);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                RegSettings.Save(Constants.LogFieDay, value);
            }
        }




        //private static object lockObject = new object();


        
        private List<string> getChangedFiles()
        {
            List<string> result = new List<string>();
            string[] files = Directory.GetFiles(currentfoldername);
            foreach (string filename in files)
            {
                FileInfo info = new FileInfo(filename);
                if (changedFiles.ContainsKey(info.Name))
                {
                    if (info.LastWriteTime > changedFiles[info.Name])
                    {
                        result.Add(info.FullName);
                        changedFiles[info.Name] = info.LastWriteTime;
                        if(changeTime < info.LastWriteTime)
                            changeTime = info.LastWriteTime;
                    }
                }
                else
                {
                    result.Add(info.FullName);
                    changedFiles.Add(info.Name, info.LastWriteTime);
                    if (changeTime < info.LastWriteTime)
                        changeTime = info.LastWriteTime;
                }
            }
            return result;
        }

        #endregion

        #region initialization

        public ServiceCore()
        {
            try
            {
                Logger.WriteDebug("ctor");
                timer = new Timer(AlarmQueryInterval);
                timer.Elapsed -= new ElapsedEventHandler(timer_Elapsed);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }   
        }

        public override void Stop()
        {
            try
            {
                Logger.WriteDebug("Stopping TMN S12 Alarm Service...");
                timer.Stop();
                Logger.WriteEnd("TMN S12 Alarm Service stopped.");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public override void Start()
        {
            try
            {
                processorName = RegSettings.Get(Constants.AlarmLogFixString, "") as string;
                using (var db = new TMNModelDataContext())
                {
                    severityOverrides = db.AlarmSeverityOverrides.Where(s => s.SwitchTypeID == Center.Current.Switch).ToList();
                }
                InitializeService();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void InitializeService()
        {
            Logger.WriteDebug("Initializing Service...");
            try
            {
                //Impersonate if needed
                string userName = RegSettings.Get(Constants.UserName) as string;
                string password = Cryptographer.Decode(RegSettings.Get(Constants.Password, "") as string);
                string temp =  "" + RegSettings.Get(Constants.LogFolderPath);
                LogFolders = temp.Split(';');
                foreach(string item in LogFolders)
                    Impersonation.TryImpersonate(userName, password, item);
                timer.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            Logger.WriteInfo("Service initialized.");
        }

        private string currentfoldername = "";
        private int repviewDeactiveMinutes = 10;
        private int folderIndex = 0;
        private void initializeRepview()
        {
            Logger.WriteDebug("Initializing watcher...");
            try
            {

                //find folder name
                string foldername = DateTime.Now.ToString("{0}yyyyMM{1}dd");
                foldername = string.Format(foldername, "\\M", "\\DAY");
                 repviewDeactiveMinutes = int.Parse("" + RegSettings.Get(Constants.RepviewDeactiveMinutes, "10"));

                 if (folderIndex >= LogFolders.Length)
                     folderIndex = 0;
                currentfoldername = "" + LogFolders[folderIndex] + foldername;
                folderIndex++;
                //set watcher
                Logger.WriteDebug("Setting to log folder {0} ...", currentfoldername);

                if (Directory.Exists(currentfoldername))
                {
                    changedFiles.Clear();
                    isRepviewInitialized = true;
                }
                else
                    throw new DirectoryNotFoundException("مقدور نیست دسترسی به مسیر فایل های لاگ .");

                Logger.WriteDebug("Set to log folder {0}", currentfoldername);
            }
            catch
            {
                isRepviewInitialized = false;
                throw;
            }
        }

        #endregion


        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Logger.WriteDebug("timer stopped");
            try
            {
                int day = DateTime.Now.Day;

                if (day != DayOfAlarmLogs || !isRepviewInitialized)
                {
                    TextSettings.Delete(DateTime.Now.ToString("yyyyMM"));
                    DayOfAlarmLogs = day;
                    initializeRepview();
                }

                Logger.WriteInfo("Checking new Alarms...");
                CheckForAlarms();
                Logger.WriteInfo("Check new alarm completed. ");


                if (repviewDeactiveMinutes > 0 && DateTime.Now.Subtract(changeTime).TotalMinutes > repviewDeactiveMinutes)
                    throw new TimeoutException("برنامه Repview مدت زمانی است که لاگ ثبت نکرده است.");

                ServiceState.ReportActivity(ServiceTypes.AlarmService);
            }
            catch (TimeoutException ex)
            {
                Logger.Write(ex);
                Logger.WriteInfo("Reinitialize watcher after 10 second...");
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم راه اندازی برنامه Repview", ex.ToString());
                isRepviewInitialized = false;
            }
            catch (FileNotFoundException ex)
            {
                Logger.Write(ex);
                Logger.WriteInfo("Reinitialize watcher after 10 second...");
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در خواندن لاگ فایل های Repview", ex.ToString());
                isRepviewInitialized = false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Logger.Write(ex);
                Logger.WriteInfo("Reinitialize watcher after 10 second...");
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در ارتباط با Repview", ex.ToString());
                isRepviewInitialized = false;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.ToString());
            }
            timer.Start();
            Logger.WriteDebug("timer started.");
        }

        private void CheckForAlarms()
        {
            var deletesCount = 0;
            var changed = getChangedFiles();
            S12LogParser parser = new S12LogParser();
            foreach (var filename in changed)
            {
                Logger.WriteDebug("checking file {0} ...", filename);
                int newAlarmCounter = 0; 
                int alarmsCounter = 0;
                int recoveredCounter = 0;
                int overrodesCount = 0;
                FileInfo alarmLogFile = new FileInfo(filename);
                string text = GetAddedText(alarmLogFile);
                if (text != null)
                {
                    Logger.WriteDebug("Find new data in file ");
                    IEnumerable<LogAlarm> alarms = parser.GetAlarms(text, processorName);
                    foreach (var alarm in alarms)
                    {
                        alarmsCounter++;
                        if (alarm.Data.ToUpper().Contains("ALARM STATE = ON"))
                        {
                            overrodesCount += OvrrideSeverity(alarm);
                            // this may be an alarm
                            InsertAlarm(alarm);
                            newAlarmCounter++;
                            CountNewAlarms(alarm);
                        }
                        else if (alarm.Data.ToUpper().Contains("ALARM STATE = OFF"))
                        {
                            // This is a recovery
                            recoveredCounter += RecoverAlarm(alarm);
                        }
                        else if (alarm.Data.ToUpper().Contains("SYSTEM REPORT") || alarm.Data.ToUpper().Contains("NOT SUCCESSFUL"))
                        {
                            overrodesCount += OvrrideSeverity(alarm);
                            // this may be an alarm
                            InsertAlarm(alarm);
                            newAlarmCounter++;
                            CountNewAlarms(alarm);
                        }
                        else if (alarm.Data.ToUpper().Contains("SUCCESSFUL"))
                        {
                            // This is a recovery
                            recoveredCounter += RecoverAlarm(alarm);
                        }
                    }
                    //var acknowledgedAlarms = db.ExecuteCommand("UPDATE LogAlarm SET IsRead = 1 WHERE [CenterID] = {0} AND IsRead=0 AND [Time] < DATEADD(HOUR,-1, GETDATE());", Center.CurrentCenterID);
                    //Logger.WriteInfo("{0} active alarm(s) detected. {1} of them are new. {2} alarm(s) recovered, and {3} marked as acknowledged.", alarmsCounter, newAlarmCounter, recoveredCounter, acknowledgedAlarms);
                    //int deletesCount = db.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(DAY,-1,GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information);

                    SetCenterNewAlarms(); 
                    Logger.WriteInfo("{0} active alarm(s) detected. {1} of them are new. {2} alarm(s) recovered and {3} alarm(s) overrided.", alarmsCounter, newAlarmCounter, recoveredCounter, overrodesCount);
                }
                else
                {
                    Logger.WriteDebug("No new data available yet.");
                }

                Logger.WriteDebug("Checked file {0}", filename);
            }
            

            if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
            {
                using (var db = new TMNModelDataContext())
                {
                    deletesCount = db.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information, AlarmExpireSeconds);
                }
                deleteTimeout = DateTime.Now;
                Logger.WriteInfo("{0} alarm(s) expired and deleted from logalarm.", deletesCount);
            }
        }



        private int RecoverAlarm(LogAlarm alarm)
        {
            int recoveredCounter = 0;
            using (var db = new TMNModelDataContext())
            {
                List<LogAlarm> alarms = FindRecoveredAlarmID(db, alarm);
                foreach (var recoveredAlarm in alarms)
                {
                    try
                    {
                        //alarm for more than one location
                        if (recoveredAlarm.Location.IsNull("").Contains('_'))
                        {
                            var locations = recoveredAlarm.Location.Split('_').Where(l => !alarm.Data.Contains(l));
                            recoveredAlarm.Location = "";
                            foreach (string item in locations)
                            {
                                recoveredAlarm.Location += item + '_';
                            }
                            recoveredAlarm.Location = recoveredAlarm.Location.TrimEnd('_');
                            recoveredAlarm.Data += "\n  RECOVERY ALARM  \n" + alarm.Data;
                            if (recoveredAlarm.Location == "")
                                recoveredAlarm.IsRead = true;
                        }
                        else
                        {
                            recoveredAlarm.IsRead = true;
                            recoveredAlarm.Data += "\n  RECOVERY ALARM  \n" + alarm.Data;
                        }
                        if (recoveredAlarm.Severity >= 11 && recoveredAlarm.Severity <= 13) 
                            recoveredAlarm.Severity = (byte)(recoveredAlarm.Severity % 10);
                        recoveredCounter++;

                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "Can't Ack alarm because can't update.\n alarm:\n{0} \n recovery: \n {1}\n exception detail:\n{2}", recoveredAlarm.Data, alarm.Data, ex.ToString());
                    }
                }
            }
            return recoveredCounter;
        }

        private List<LogAlarm> FindRecoveredAlarmID(TMNModelDataContext db, LogAlarm recovery)
        {
            //get all uread alarms for current center that raise before recovery.time and has ALARM STATE = ON
                //check recovery for determine is it for ack more than one alarm and then return all alarms recovery associate with them
                //if (recovery.Data.Contains("GLOBAL"))
                //    return result.Where(la => !la.Location.IsNullOrEmpty() && recovery.Data.Contains(la.Location)).ToList();
                //
                if (recovery.Data.Contains("GLOBAL"))
                {
                    var result = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && la.Time.Date <= recovery.Time.Date && la.Location != null).ToList(); 
                    return result.Where(la => la.Location.Split('_').Any(l => recovery.Data.Contains(l))).ToList();
                }
                if (recovery.Location != null && recovery.Location.Trim() != "")
                {
                    return db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && la.Time.Date <= recovery.Time.Date && la.Location != null && la.Location.Contains(recovery.Location)).ToList();  
                }
                //check alarms that determine with alarm number
                Match match = Regex.Match(recovery.Data, @"ALARM NUMBER\s*=\s*\d+");
                if (match.Success)
                    return db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && la.Time.Date <= recovery.Time.Date && la.Data.Contains(match.Value)).ToList();
                //at the end check result alarms title,location and messageid to find recoverd alarms
                return db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && la.Time.Date <= recovery.Time.Date && la.Title == recovery.Title && (la.Location ?? "") == (recovery.Location ?? "") && la.MessageID == recovery.MessageID).ToList();
        }

        private void InsertAlarm(LogAlarm alarm)
        {
            if (alarm != null)
            {
                try
                {
                    using (var db = new TMNModelDataContext())
                    {
                        db.LogAlarms.InsertOnSubmit(alarm);
                        db.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
        }

        private int OvrrideSeverity(LogAlarm msg)
        {
            var matchOverrides = severityOverrides.Where(s =>
            {
                //if (s.AlarmNumber != null)
                //{
                //    int alarmNumber;
                //    if (int.TryParse(s.AlarmNumber.Trim(), out alarmNumber))
                //    {
                //        return msg.MessageID == alarmNumber;
                //    }
                //    else
                //    {
                //        Logger.WriteError("\"{0}\" is an invalid AlarmNumber in AlarmSeverityOverride table.", s.AlarmNumber);
                //    }
                //}
                //else 
                if (s.Pattern != null)
                {
                    return Regex.Match(msg.Data, s.Pattern, RegexOptions.IgnoreCase).Success;
                }
                return false;
            });
            var firstOverride = matchOverrides.FirstOrDefault();
            if (matchOverrides.Count() > 1)
            {
                Logger.WriteWarning("{0} overrides are found for message with Mask No = \"{1}\" and Title = \"{2}\". The first one with ID=\"{3}\" is used.", matchOverrides.Count(), msg.MessageID, msg.Title, firstOverride.ID);
            }
            if (firstOverride != null)
            {
                msg.Severity = firstOverride.NewSeverity;
                if (msg.Severity.Value == (byte)AlarmSeverities.Information)
                    msg.IsRead = true;
                if (firstOverride.NewTitle != null)
                    msg.Title = firstOverride.NewTitle;
                return 1;
            }
            return 0;
        }

        private string GetAddedText(FileInfo alarmLogFile)
        {
            bool onlyNewContent = false;
            alarmLogFile.Refresh();
            if (!alarmLogFile.Exists)
                throw new FileNotFoundException(alarmLogFile.FullName);
            if (alarmLogFile != null)
            {
                string text = new S12Decompressor().Decompress(alarmLogFile.FullName, out onlyNewContent);
                if (onlyNewContent)
                    return text;
                
                int lastFilePos = int.Parse(TextSettings.Get(alarmLogFile.FullName.Replace('\\', '_'), "0"));
                if (lastFilePos == 0)
                    lastFilePos = int.Parse(TextSettings.Get(alarmLogFile.Name, "0"));

                int startPos = lastFilePos;
                lastFilePos = text.Length;
                TextSettings.Set(alarmLogFile.FullName.Replace('\\', '_'), lastFilePos.ToString());
                return text.Substring(Math.Min(startPos, lastFilePos));

            }
            return null;
        }

    }
}
