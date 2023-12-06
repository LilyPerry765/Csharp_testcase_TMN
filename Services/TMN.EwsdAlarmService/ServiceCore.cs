using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Enterprise;
using System.Timers;

namespace TMN
{
    class ServiceCore : TmnService
    {
        private System.IO.FileInfo logFile;
        private DateTime lastWriteTime;
        private string processorName = "";
        private IEnumerable<AlarmSeverityOverride> severityOverrides;
        private Timer timer = null;
        private DateTime deleteTimeout;
        private DateTime? _lastAlarmDate = null;
        private DateTime? lastAlarmDate
        {
            get
            {
                if (_lastAlarmDate == null)
                {
                    DateTime temp;
                    DateTime.TryParse(RegSettings.Get("lastAlarmDate", "").ToString(), out temp);
                    _lastAlarmDate = temp;
                }
                return _lastAlarmDate;
            }
            set
            {
                _lastAlarmDate = value;
                if(_lastAlarmDate != null)
                    RegSettings.Save("lastAlarmDate", _lastAlarmDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public ServiceCore()
        {
            timer = new Timer(AlarmQueryInterval);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        public override void Start()
        {
            try
            {
                deleteTimeout = DateTime.Now;
                processorName = RegSettings.Get("ewsdAlarmLogFixString", "") as string;
                severityOverrides = DB.Instance.AlarmSeverityOverrides.Where(s => s.SwitchTypeID == Center.Current.Switch).ToArray();
                InitializeService();
                timer.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private bool InitializeService()
        {
            try
            {
                logFile = FindFile();
                if (logFile != null)
                {
                    if ((string)RegSettings.Get("lastFileName") != logFile.FullName)
                    {
                        RegSettings.Save("lastFileName", logFile.FullName);
                        LastFilePos = 0;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }

        public override void Stop()
        {
            Logger.WriteInfo("Stopping TMN EWSD Alarm Service...");
            timer.Stop();
            //IsMonitoring = false;
            Logger.WriteEnd("TMN EWSD Alarm Service stopped.");
        }

        private int lastFilePosIndex = 0;
        private int LastFilePos
        {
            get
            {
                int result = 0;
                int.TryParse(RegSettings.Get("lastPosition_" + lastFilePosIndex, "0").ToString(), out result);
                return result;
            }
            set
            {
                RegSettings.Save("lastPosition_" + lastFilePosIndex, value);
            }
        }


        private System.IO.FileInfo FindFile()
        {
            //Impersonate if needed
            FileInfo result = null;
            string userName = RegSettings.Get("ewsdUserName") as string;
            string password = Cryptographer.Decode(RegSettings.Get("ewsdPassword", "") as string);
            Impersonation.TryImpersonateByPath(userName, password, RegSettings.Get("ewsdAlarmLogPath") as string);

            Logger.WriteInfo("Finding alarm log file...");
            string pattern = RegSettings.Get("ewsdAlarmLogPatern") as string;
            string paths = RegSettings.Get("ewsdAlarmLogPath") as string;

            string[] pathsArray = paths.Split(';');
            DateTime datetime = DateTime.MinValue;
            for (int i =0; i < pathsArray.Length; i++) 
            {
                string path = pathsArray[i];
                if (!System.IO.Directory.Exists(path))
                {
                    Logger.WriteCritical("Directory \"{0}\" could not be found!", path);
                }
                else if (pattern == null)
                {
                    Logger.WriteCritical("File pattern is not set!");
                }
                else
                {
                    foreach (var f in new System.IO.DirectoryInfo(path).GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly))
                    {
                        if (Regex.IsMatch(f.Name, pattern, RegexOptions.IgnoreCase))
                        {
                            if (result != null && result.LastWriteTime > f.LastWriteTime)
                                Logger.WriteInfo("\"{0}\" is match pattern but older than current file  \"{1}\" .", f.FullName, result.FullName);
                            else
                            {
                                result = f;
                                lastFilePosIndex = i;
                            }
                        }
                    }
                    if(result == null)
                        Logger.WriteCritical("Directory \"{0}\" was found, but no file with pattern \"{1}\" was found!", path, pattern);
                }
            }
            if(result != null)
                Logger.WriteInfo("\"{0}\" is selected as alarm log file in path \"{1}\" accourding to pattern \"{2}\".", result.Name, result.DirectoryName, pattern);
            return result;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                EwsdLogParser parser = new EwsdLogParser();
                string txt = GetAddedText();
                var insertsCount = 0;
                var recoversCount = 0;
                var overrodesCount = 0;
                var deletesCount = 0;
                using (var db = new TMNModelDataContext())
                {
                    if (lastAlarmDate == null)
                    {
                        lastAlarmDate = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID).Max(la => la.Time);
                        if (lastAlarmDate < DateTime.Now.Date)
                            lastAlarmDate = DateTime.Now.Date;
                    }
                }
                int i = 0;
                if (txt != null)
                {
                    IEnumerable<string> alarms = GetLogBlocks(txt);
                    foreach (string block in alarms)
                    {
                        var message = parser.ParseLog(block, severityOverrides, lastAlarmDate);
                        if (message != null)
                        {
                            overrodesCount += OvrrideSeverity(message);
                            if (message != null && (AlarmSeverities)message.Severity != AlarmSeverities.None && !message.Data.Contains("EXEC'D"))
                            {
                                //shahab 900613 
                                //also find recovery alarm that determine termination of alarm with 'END OF ' statement in title of alarm block
                                if (message.Title.ToUpper().EndsWith(" END") || message.Title.ToUpper().StartsWith("END OF "))
                                {
                                    // This is a recovery
                                    recoversCount = AcknowledgeRecoveredAlarms(message);
                                }
                                else // this may be an alarm
                                {
                                    if (InsertAlarm(message))
                                        insertsCount++;
                                }
                            }

                        }
                        if (((recoversCount + insertsCount) % 110) == 0 || i++ % 660 == 0)
                            ServiceState.ReportActivity(ServiceTypes.AlarmService);
                    }
                }
                //acknowledgedAlarms = DB.ExecuteCommand("UPDATE LogAlarm SET IsRead = 1 WHERE [CenterID] = {0} AND IsRead=0 AND [Time] < DATEADD(HOUR,-1, GETDATE());", Center.CurrentCenterID);
                //@todo: comment because of redundancy . check it later
                if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
                {
                    deletesCount = DB.Instance.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information, AlarmExpireSeconds);
                    deleteTimeout = DateTime.Now;
                }
                Logger.WriteInfo("{0} alarm(s) inserted, {1} recovered, {2} overrided, and {3} deleted.", insertsCount, recoversCount, overrodesCount, deletesCount);
                SetCenterNewAlarms();
                //if (insertsCount > 0)
                //    Setting.Set(string.Format("SoundAlertCenter_{0}", Center.Current.PointCode), SoundAlertStatus.EnableSoundAlert.ToString());
                // Notifis oly if no error ocuures 
                ServiceState.ReportActivity(ServiceTypes.AlarmService);

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                //IsMonitoring = false;
                try
                {
                    if (!InitializeService())
                    {
                        ServiceState.ReportActivity(ServiceTypes.AlarmService, "ارتباط با OMT", ex.Message);
                        Logger.WriteInfo("Restarting service in few seconds...");
                    }
                }
                catch (Exception ex2)
                {
                    Logger.Write(ex2);
                }

            }
            timer.Start();
        }

        private  bool InsertAlarm(LogAlarm message)
        {
            int retries = 5;
            while (retries-- > 0)
            {
                try
                {
                    using (var db = new TMNModelDataContext())
                    {
                        db.LogAlarms.InsertOnSubmit(message);
                        db.SubmitChanges();
                        CountNewAlarms(message);
                        lastAlarmDate = message.Time;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
            return false;
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
                    return Regex.Match(msg.Title, s.Pattern, RegexOptions.IgnoreCase).Success;
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
                if (firstOverride.NewTitle != null)
                    msg.Title = firstOverride.NewTitle;
                return 1;
            }
            return 0;
        }

        private int AcknowledgeRecoveredAlarms(LogAlarm recovery)
        {
            try
            {
                IEnumerable<LogAlarm> recoveredAlarms = null;
                using (var db = new TMNModelDataContext())
                {

                    int recoverCount = 0;
                    //recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && (la.Location ?? "") == (recovery.Location ?? ""));

                    //shahab 900613
                    //find recovery for external alarm such as dc and main power or such that
                    if (recovery.Title.ToUpper().Contains("EXTERNAL ALARM EXCHANGE"))
                    {
                        Match match = Regex.Match(recovery.Data, @"EAL\s+\d+(\r|\n)");
                        if(match.Success)
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.CenterID == Center.CurrentCenterID && la.Data.Contains(match.Value));
                    }
                    else if (recovery.MessageID == 7751)   //recovery for alarm like - LINE LOCKOUT -  recover with 'END OF COMMUNICATION ALARM'
                    {   
                        //only Alarm Identification property in alarm data match pattern like 'DN=23451'
                        //if all alarm identification match recovery so ACK it
                        var matches = Regex.Matches(recovery.Data, @"\w+=(?<param>\w+)");
                        Match match = null;
                        foreach (Match item in matches)
                        {
                            if (item.Value.StartsWith("DN"))
                            {
                                match = item; break;
                            }
                            else if (item.Value.StartsWith("LTG"))
                            {
                                match = item; break;
                            }
                            else if (item.Value.StartsWith("CLASS"))
                            {
                                match = item;
                            }
                        }
                        if (match != null)
                        {
                            try
                            {
                                recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.CenterID == Center.CurrentCenterID && la.Data.Contains(int.Parse(match.Groups["param"].Value).ToString())).ToArray();
                            }
                            catch
                            {
                                recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.CenterID == Center.CurrentCenterID && la.Data.Contains(match.Groups["param"].Value)).ToArray();
                            }
                        }
                    }
                    else
                    {
                        if(recovery.Title.Contains('/'))
                        {
                            string recoveryTitle = recovery.Title.Substring(0, recovery.Title.IndexOf('/'));
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.CenterID == Center.CurrentCenterID && (la.Location ?? "") == (recovery.Location ?? "") && la.Title.Contains(recoveryTitle)).OrderByDescending(la => la.Time);
                        }
                        else
                        {
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.CenterID == Center.CurrentCenterID && (la.Location ?? "") == (recovery.Location ?? "") && la.Title.Contains(recovery.Title)).OrderByDescending(la => la.Time);
                        }
                    }

                    foreach (var alarm in recoveredAlarms)
                    {
                        try
                        {
                            alarm.IsRead = true;
                            if(alarm.Severity >= 11 && alarm.Severity <= 13)
                                alarm.Severity = (byte)(alarm.Severity % 10);
                            alarm.Data += "\n  RECOVERY ALARM  \n" + alarm.Data;
                            recoverCount++;
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex);
                        }
                    }
                    db.SubmitChanges();
                    lastAlarmDate = recovery.Time;
                    return recoverCount;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return 0;
        }


        private IEnumerable<string> GetLogBlocks(string inputText)
        {
            //shahab 900613
            //change line break before block start, from 6 to 7 because alarm  07751 not in bound of that 
            var matches = Regex.Matches(inputText, processorName + @"(.|\s)*?((\n|\r){7,}|\z)");
            foreach (Match match in matches)
            {
                string data = match.Value.Trim();
                if (!string.IsNullOrWhiteSpace(data))
                    yield return data;
            }
        }

        private string GetAddedText()
        {
            const string mirrorFile = @"C:\Windows\Tmn_Alarm_Log.txt";
            logFile.Refresh();


            if (logFile == null)
                throw new FileNotFoundException("فایل لاگ انتخاب نشده است");

            if (!logFile.Exists)
                throw new FileNotFoundException(logFile.FullName);

            if (logFile.LastWriteTime < DateTime.Now.AddHours(-6))
                throw new TimeoutException(string.Format("در فایل {0} مدت زمانی است که لاگ ثبت نشده است", logFile.FullName));

            if (logFile != null && logFile.LastWriteTime != lastWriteTime)
            {
                lastWriteTime = logFile.LastWriteTime;
                int startPos = LastFilePos;
                File.Copy(logFile.FullName, mirrorFile, true);
                using (var reader = new StreamReader(mirrorFile))
                {
                    string text = reader.ReadToEnd();
                    LastFilePos = text.Length;
                    return text.Substring(Math.Min(startPos, LastFilePos));
                }
            }
            return null;
        }
    }
}
