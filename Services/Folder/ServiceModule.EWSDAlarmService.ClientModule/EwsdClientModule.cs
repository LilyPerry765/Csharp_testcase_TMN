using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Enterprise;
using System.Timers;
using Folder.EMQ;
namespace TMN
{
    [ServiceModule("TMN.Ewsd", ServiceModuleSide.Client)]
    public class EwsdClientModule : ServiceModuleClient
    {
        private System.IO.FileInfo logFile;
        private string processorName = "";
        private DateTime lastWriteTime;
        private Timer timer;

        public override bool Start()
        {
            Logger.WriteInfo("Starting EwsdClientModule...");
            try
            {
                processorName = RegSettings.Get("ewsdAlarmLogFixString", "") as string;
                timer = new Timer(5000); //TODO: replace 5000 with setting parameter
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                InitializeService();
                timer.Start();
        
                //severityOverrides = DB.Instance.AlarmSeverityOverrides.Where(s => s.SwitchTypeID == Center.Current.Switch).ToArray();
                //Call("GetSeverityOverride", Center.CurrentCenterID);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return true;
        }

        //[RemoteMethod]
        //public void ResponseSeverityOverride(CallInfo callInfo, List<AlarmSeverityOverride> severityOverrides)
        //{
        //    try
        //    {
        //        this.severityOverrides = severityOverrides;
        //        InitializeService();
        //        timer.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex);
        //    }
        //}


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

            Logger.WriteEnd("TMN EWSD Alarm Service stopped.");
        }

        private int LastFilePos
        {
            get
            {
                int result = 0;
                int.TryParse(RegSettings.Get("lastPosition", "0").ToString(), out result);
                return result;
            }
            set
            {
                RegSettings.Save("lastPosition", value);
            }
        }


        private System.IO.FileInfo FindFile()
        {
            //Impersonate if needed
            string userName = RegSettings.Get("ewsdUserName") as string;
            string password = Cryptographer.Decode(RegSettings.Get("ewsdPassword", "") as string);
            Impersonation.TryImpersonateByPath(userName, password, RegSettings.Get("ewsdAlarmLogPath") as string);

            Logger.WriteInfo("Finding alarm log file...");
            string pattern = RegSettings.Get("ewsdAlarmLogPatern") as string;
            string path = RegSettings.Get("ewsdAlarmLogPath") as string;


            if (!System.IO.Directory.Exists(path))
            {
                Logger.WriteCritical("Directory \"{0}\" could not be found!", path);
                return null;
            }
            else if (pattern == null)
            {
                Logger.WriteCritical("File pattern is not set!");
                return null;
            }

            foreach (var f in new System.IO.DirectoryInfo(path).GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly))
            {
                if (Regex.IsMatch(f.Name, pattern, RegexOptions.IgnoreCase))
                {
                    Logger.WriteInfo("\"{0}\" is selected as alarm log file in path \"{1}\" accourding to patern \"{2}\".", f.Name, path, pattern);
                    return f;
                }
            }
            Logger.WriteCritical("Directory \"{0}\" was found, but no file with pattern \"{1}\" was found!", path, pattern);
            return null;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                string txt = GetAddedText();
                if (txt != string.Empty)
                {
                    List<string> data = GetLogBlocks(txt);
                    List<LogAlarm> alarms = GetLogAlarms(data);
                    Call("ProcessData", alarms);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                //do
                //{
                //    Call("SetServiceState", string.Format("{0} \n {1}", "ارتباط با OMT", ex.Message));
                //    Logger.WriteInfo("Restarting service in 10 seconds...");
                //    System.Threading.Thread.Sleep(10000);
                //} while (!InitializeService());
                try
                {
                    if (!InitializeService())
                    {
                        Call("SetServiceState", string.Format("{0} \n {1}", "ارتباط با OMT", ex.Message));
                        //ServiceState.ReportActivity(ServiceTypes.AlarmService, "ارتباط با OMT", ex.Message);
                        //Log(LogType.Info, "Restarting service in few seconds...");
                        Logger.WriteInfo("Restarting service in few seconds...");
                    }
                }
                catch (Exception ex2)
                {
                    Logger.Write(ex2);
                }
            }
            finally
            {
                timer.Start();
            }
        }

        private List<LogAlarm> GetLogAlarms(List<string> blocks)
        {
            List<LogAlarm> results = new List<LogAlarm>();
            foreach (string block in blocks)
            {
                var message = EwsdLogParser.ParseLog(block); //, severityOverrides);
                results.Add(message);
            }
            return results;
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

        private List<string> GetLogBlocks(string inputText)
        {
            List<string> result = new List<string>();
            try
            {
                //shahab 900613
                //change line break before block start from 6 to 7, because alarm  07751 not in bound of that 
                var matches = Regex.Matches(inputText, processorName + @"(.|\s)*?((\n|\r){7,}|\z)", RegexOptions.Compiled);
                foreach (Match match in matches)
                {
                    string data = match.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(data))
                        result.Add(data);
                }
            }
            catch
            {
                Logger.WriteInfo("");
            }
            return result;
        }
    }
}
