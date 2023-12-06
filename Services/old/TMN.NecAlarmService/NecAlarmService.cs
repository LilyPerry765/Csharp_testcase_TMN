using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Enterprise;
using System.Text.RegularExpressions;
using System.Timers;
using TMN;
using TMN.Helpers;

namespace NecAlarmService
{
    partial class NecAlarmService : ServiceBase
    {
        TMNModelDataContext db;
        Client c;
        Timer timer = new Timer(5000);

        public NecAlarmService()
        {
            InitializeComponent();
            Logger.WriteStart("Starting service...");
            c = new Client();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

        }

        protected override void OnStart(string[] args)
        {
            db = new TMNModelDataContext();
            timer.Start();
            Logger.WriteInfo("Service started.");
        }

        protected override void OnStop()
        {
            timer.Stop();
            c.Disconnect();
            Logger.WriteEnd("Service stoped.");
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            string result;
            if (c.SendCommand("view alm cls_alm=all type=dtl  st=unack ", ">", out result))
            {
                //  result=result.Replace(" ","");
                Logger.WriteDebug(result);
                ParseResult(result);
            }
            else
                Logger.WriteError("Command failed.");

            timer.Start();
        }

        private void ParseResult(string result)
        {
            Regex regex = new Regex(@"# alarm state #\s*\nalmsg_no=(?<number>\d+) cls_alm=(?<severity>\S+).+\ndate=(?<DateTime>\S+) inf=(?<Title>.+)(?:(?s)(?<other>.+?)\n\n(?-s))?", RegexOptions.Compiled);
            var matches = regex.Matches(result);
            foreach (Match match in matches)
            {
                long number = long.Parse(match.Groups["number"].Value);
                AlarmSeverities severity = SeverityConverter(match.Groups["severity"].Value);
                DateTime dateTime = DateTime.ParseExact(match.Groups["DateTime"].Value, "MM/dd/yy_HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string title = match.Groups["Title"].Value.Trim();
                string other = null;
                if (match.Groups["other"].Success)
                    other = match.Groups["other"].Value.Trim();

                SaveAlarm(number, severity, dateTime, title, match.Value.Trim());
            }
        }

        private Guid GetDefaultCenterID()
        {
            string defaultCEnterID = RegSettings.Get("DefaultCenter") as string;
            Guid result = Guid.Empty;
            Guid.TryParse(defaultCEnterID, out result);
            return result;
        }

        private void SaveAlarm(long number, AlarmSeverities severity, DateTime time, string title, string data)
        {
            try
            {
                if (!db.LogAlarms.Any(a => a.NecNumber == number))
                {
                    db.LogAlarms.InsertOnSubmit(new LogAlarm()
                    {
                        Data = data,
                        ID = Guid.NewGuid(),
                        IsRead = false,
                        NecNumber = number,
                        Time = time,
                        Severity = (byte)severity,
                        Location = "",
                        Title = title,
                        CenterID = GetDefaultCenterID()
                    });
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private static AlarmSeverities SeverityConverter(string sev)
        {
            switch (sev)
            {
                case "mn":
                    return AlarmSeverities.Minor;
                case "mj":
                    return AlarmSeverities.Major;
                case "cr":
                    return AlarmSeverities.Critical;
                default:
                    return AlarmSeverities.Information;
            }
        }

    }
}

