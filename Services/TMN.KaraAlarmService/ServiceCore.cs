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
        //private string processorName = "";
        private IEnumerable<AlarmSeverityOverride> severityOverrides;
        private Timer timer;
        private DateTime deleteTimeout;

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
                //processorName = RegSettings.Get("karaAlarmLogFixString", "") as string;
                using (var db = new TMNModelDataContext())
                {
                    severityOverrides = db.AlarmSeverityOverrides.Where(s => s.SwitchTypeID == Center.Current.Switch).ToArray();
                }
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
            Logger.WriteInfo("Stopping TMN KARA Alarm Service...");
            timer.Stop();
            //IsMonitoring = false;
            Logger.WriteEnd("TMN KARA Alarm Service stopped.");
        }

        private int LastFilePos
        {
            get
            {
                int result = 0;
                int.TryParse(RegSettings.Get("karaLastPosition", "0").ToString(), out result);
                return result;
            }
            set
            {
                RegSettings.Save("karaLastPosition", value);
            }
        }


        private System.IO.FileInfo FindFile()
        {
            //Impersonate if needed
            string userName = RegSettings.Get("karaUserName") as string;
            string password = Cryptographer.Decode(RegSettings.Get("karaPassword", "") as string);
            
            Impersonation.TryImpersonateByPath(userName, password, RegSettings.Get("karaAlarmLogPath") as string);

            Logger.WriteInfo("Finding alarm log file...");
            //string pattern = RegSettings.Get("karaAlarmLogPatern") as string;
            string path = RegSettings.Get("karaAlarmLogPath") as string;


            if (!System.IO.Directory.Exists(path))
            {
                Logger.WriteCritical("Directory \"{0}\" could not be found!", path);
                return null;
            }
            //else if (pattern == null)
            //{
            //    Logger.WriteCritical("File pattern is not set!");
            //    return null;
            //}

            string filename = DateTime.Now.ToString("{0}yyyyMMdd{1}");
            filename = string.Format(filename, "\\", ".glb");
            if (File.Exists(path + filename))
                return new FileInfo(path + filename);
            //foreach (var f in new DirectoryInfo(path).GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly))
            //{
            //    if (Regex.IsMatch(f.Name, pattern, RegexOptions.IgnoreCase))
            //    {
            //        Logger.WriteInfo("\"{0}\" is selected as alarm log file in path \"{1}\" accourding to patern \"{2}\".", f.Name, path, pattern);
            //        return f;
            //    }
            //}
            Logger.WriteCritical("Directory \"{0}\" was found, but no file with name \"{1}\" was found!", path, filename);
            return null;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                KaraLogParser parser = new KaraLogParser();
                string txt = GetAddedText();
                var insertsCount = 0;
                //var recoversCount = 0;
                var overrodesCount = 0;
                var deletesCount = 0;

                if (txt != null)
                {
                    foreach (LogAlarm message in parser.GetLogBlocks(txt, severityOverrides))
                    {
                        //var message = parser.ParseLog(block, severityOverrides);
                        overrodesCount += OvrrideSeverity(message);
                        if (message != null && (AlarmSeverities)message.Severity != AlarmSeverities.None && !message.Data.Contains("EXEC'D"))
                        {

                            //else // this may be an alarm
                            //{
                                if (InsertAlarm(message))
                                    insertsCount++;
                            //}
                        }
                    }
                }
                //   acknowledgedAlarms = DB.ExecuteCommand("UPDATE LogAlarm SET IsRead = 1 WHERE [CenterID] = {0} AND IsRead=0 AND [Time] < DATEADD(HOUR,-1, GETDATE());", Center.CurrentCenterID);
                //TODO: comment because of redundancy . check it later
                if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
                {
                    using (var db = new TMNModelDataContext())
                    {
                        deletesCount = db.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", Center.CurrentCenterID, AlarmSeverities.Information, AlarmExpireSeconds);
                        deleteTimeout = DateTime.Now;
                    }
                }
                Logger.WriteInfo("{0} alarm(s) inserted, {1} overrided, and {2} deleted.", insertsCount, overrodesCount, deletesCount);
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
                        ServiceState.ReportActivity(ServiceTypes.AlarmService, "ارتباط با سویدچ Kara", ex.Message);
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
                if (s.AlarmNumber != null)
                {
                    int alarmNumber;
                    if (int.TryParse(s.AlarmNumber.Trim(), out alarmNumber))
                    {
                        return msg.MessageID == alarmNumber;
                    }
                    else
                    {
                        Logger.WriteError("\"{0}\" is an invalid AlarmNumber in AlarmSeverityOverride table.", s.AlarmNumber);
                    }
                }
                else if (s.Pattern != null)
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




        private List<string> GetLogBlocks(string inputText)
        {
            //shahab 900613
            //change line break before block start, from 6 to 7 because alarm  07751 not in bound of that 
            return inputText.Replace("\\n", "♂").Split('♂').ToList();
            //var matches = Regex.Matches(inputText, processorName + @"(.|\s)*?((\n|\r){7,}|\z)", RegexOptions.Compiled);
            //foreach (Match match in matches)
            //{
            //    string data = match.Value.Trim();
            //    if (!string.IsNullOrWhiteSpace(data))
            //        yield return data;
            //}
        }

        private string GetAddedText()
        {
            const string mirrorFile = @"C:\Windows\Tmn_Alarm_Log.txt";
            logFile.Refresh();

            if (!logFile.Exists)
                throw new FileNotFoundException(logFile.FullName);

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
