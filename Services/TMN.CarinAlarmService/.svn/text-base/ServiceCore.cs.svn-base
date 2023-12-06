using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Enterprise;
using System.IO;
using System.Timers;
using System.Reflection;

namespace TMN
{
    /// <summary>
    /// Carin Service Core
    /// </summary>
    class ServiceCore : TmnService
    {
        private  Timer timer;
        private FileInfo alarmLogFile;
        private DateTime lastWriteTime;
        //private TMNModelDataContext db = new TMNModelDataContext();

        public ServiceCore()
        {
            timer = new Timer(AlarmQueryInterval);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        public override void Stop()
        {
            Logger.WriteInfo("Stopping TMN Carin Alarm Service...");
            timer.Stop();
            Logger.WriteEnd("TMN Carin Alarm Service stopped.");
        }

        public override void Start()
        {
            InitializeService();
            timer.Start();
            Logger.WriteInfo("File Pos: {0}", LastFilePos);
        }

        private bool InitializeService()
        {
            try
            {
                alarmLogFile = FindFile();
                if (alarmLogFile != null)
                {
                    if ((string)RegSettings.Get(Constants.LastFileName) != alarmLogFile.FullName)
                    {
                        RegSettings.Save(Constants.LastFileName, alarmLogFile.FullName);
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

        private int LastFilePos
        {
            get
            {
                int result = 0;
                int.TryParse(RegSettings.Get(Constants.LastPosition, "0").ToString(), out result);
                return result;
            }
            set
            {
                RegSettings.Save(Constants.LastPosition, value);
            }
        }

        private System.IO.FileInfo FindFile()
        {
            //Impersonate if needed
            string userName = RegSettings.Get(Constants.UserName) as string;
            string password = Cryptographer.Decode(RegSettings.Get(Constants.Password, "") as string);
            Impersonation.TryImpersonateByPath(userName, password, RegSettings.Get(Constants.FltFolderPath) as string);

            Logger.WriteInfo("Finding alarm log file...");
            string fileName = DateTime.Now.ToString(@"yyyyMMdd\.\f\l\t");
            currentDay = DateTime.Now.Day;
            string directory = RegSettings.Get(Constants.FltFolderPath) as string;
            string path = Path.Combine(directory, fileName);

            if (!System.IO.Directory.Exists(directory))
            {
                Logger.WriteCritical("Directory \"{0}\" could not be found or access denied!", directory);
                return null;
            }
            else if (!System.IO.File.Exists(path))
            {
                Logger.WriteCritical("File \"{0}\" was not found in directory \"{1}\"!", fileName, directory);
                return null;
            }
            else
            {
                Logger.WriteInfo("File {0} is selected.", fileName);
                return new FileInfo(path);
            }
        }

        private int currentDay = 0;
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                if (DateTime.Now.Day != currentDay)
                    InitializeService();
                CheckForAlarms();
                ServiceState.ReportActivity(ServiceTypes.AlarmService);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "ارتباط با SMAT", ex.Message);
                Logger.WriteInfo("Restarting service in few seconds...");
                System.Threading.Thread.Sleep(10 * SECOND);

                //reinitialize service in next timer elapsed
                currentDay = 0;
            }
            finally
            {
                timer.Start();
            }
        }

        private void CheckForAlarms()
        {
            CarinLogParser parser = new CarinLogParser();
            int recoverAlarmCounter = 0;
            int newAlarmCounter = 0;
            int alarmsCounter = 0;
            string text = GetAddedText();
            if (text != null)
            {
                //using (TMNModelDataContext db = new TMNModelDataContext())
                {

                    //var previouseActiveAlarms = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false).ToList();
                    foreach (var alarm in parser.GetAlarms(text))
                    {
                        //var existingAlarm = previouseActiveAlarms.SingleOrDefault(a => a.Title == alarm.Title && a.Location == alarm.Location);
                        //if (existingAlarm != null)
                        if(alarm.Data.Contains("FLT") == true)
                        {
                            // any existing detected in previouseActiveAlarms must be removed from this list, because any remaining active alarm will be marked as Read.
                            InsertAlarm(alarm);
                            newAlarmCounter++;
                            //previouseActiveAlarms.Remove(existingAlarm);
                        }
                        else // if (alarm.Data.Contains("SYS") == true)
                        {
                            recoverAlarmCounter += RecoverAlarm(alarm);
                        }
                        alarmsCounter++;
                    }
                    //previouseActiveAlarms.ForEach(a => { a.IsRead = true; a.Severity = (byte)(a.Severity % 10); });
                    //db.SubmitChanges();
                    Logger.WriteInfo("{0} active alarm(s) detected. {1} of them are new. {2} alarm(s) recovered.", alarmsCounter, newAlarmCounter, recoverAlarmCounter);
                }
                SetCenterNewAlarms();
            }
            else
            {
                Logger.WriteInfo("No new data available yet.");
            }
        }

        private int RecoverAlarm(LogAlarm alarm)
        {
            int recoverd = 0;
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    List<LogAlarm> existingAlarms = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && la.MessageID == alarm.MessageID && la.Location == alarm.Location).ToList();
                    foreach (LogAlarm existingAlarm in existingAlarms)
                    {
                        existingAlarm.IsRead = true;
                        existingAlarm.Data += "\n  RECOVERY ALARM  \n" + alarm.Data;
                        if (existingAlarm.Severity >= 11 && existingAlarm.Severity <= 13)
                            existingAlarm.Severity = (byte)(existingAlarm.Severity % 10);
                        db.SubmitChanges();
                    }
                    recoverd = existingAlarms.Count;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCritical("error in recover alam:\n{0}\nException:\n{1}", alarm.Data, ex);
            }
            return recoverd;
        }

        private void InsertAlarm(LogAlarm alarm)
        {
            //if (alarm != null)
            {
                try
                {
                    using (var db = new TMNModelDataContext())
                    {
                        db.LogAlarms.InsertOnSubmit(alarm);
                        db.SubmitChanges();
                        CountNewAlarms(alarm);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteCritical("error in insert alam:\n{0}\nException:\n{1}", alarm.Data, ex);
                }
            }
        }

        private string GetAddedText()
        {
            const string mirrorFile = @"C:\Windows\Tmn_Alarm_Log.txt";
            alarmLogFile.Refresh();


            if (alarmLogFile == null)
                throw new FileNotFoundException("فایل لاگ انتخاب نشده است");

            if (!alarmLogFile.Exists)
                throw new FileNotFoundException(alarmLogFile.FullName);

            int hours = 1;
            int.TryParse("" + RegSettings.Get(Constants.DeactiveHours, "1"), out hours);
            if (alarmLogFile.LastWriteTime < DateTime.Now.AddHours(-hours))
                throw new TimeoutException(string.Format("در فایل {0} مدت {1} ساعت لاگ ثبت نشده است", alarmLogFile.Name, hours));

            if (alarmLogFile != null && alarmLogFile.LastWriteTime != lastWriteTime)
            {
                lastWriteTime = alarmLogFile.LastWriteTime;
                int startPos = LastFilePos;
                File.Copy(alarmLogFile.FullName, mirrorFile, true);
                using (var reader = new StreamReader(mirrorFile))
                {
                    string text = reader.ReadToEnd();
                    LastFilePos = text.Length;
                    return text.Substring(Math.Min(startPos, LastFilePos));
                }
            }
            return null;
        }

        //private string GetAddedText()
        //{
        //    alarmLogFile.Refresh();
        //    if (!alarmLogFile.Exists)
        //        throw new FileNotFoundException(alarmLogFile.FullName);
        //    if (alarmLogFile != null && alarmLogFile.LastWriteTime != lastWriteTime)
        //    {
        //        lastWriteTime = alarmLogFile.LastWriteTime;
        //        int startPos = LastFilePos;
        //        using (FileStream fsSource = new FileStream(alarmLogFile.FullName,
        //            FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //        {
        //            using (var reader = new StreamReader(fsSource))
        //            {
        //                string text = reader.ReadToEnd();
        //                LastFilePos = text.Length;
        //                return text.Substring(Math.Min(startPos, LastFilePos));
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}
