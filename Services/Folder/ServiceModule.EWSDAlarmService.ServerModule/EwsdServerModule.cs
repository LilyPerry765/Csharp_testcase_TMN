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
    [ServiceModule("TMN.Ewsd", ServiceModuleSide.Server)]
    public class EwsdServerModule : ServiceModuleServer
    {

        private List<AlarmSeverityOverride> severityOverrides;
        private DateTime deleteTimeout;

        public EwsdServerModule()
        {
            deleteTimeout = DateTime.Now;
        }

        public override bool Start()
        {
            Logger.WriteInfo("Starting EwsdServerModule ...");
            using (var db = new TMNModelDataContext())
            {
                severityOverrides = (from severity in db.AlarmSeverityOverrides
                                     join types in db.SwitchTypes on severity.SwitchTypeID equals types.ID
                                     where types.Name == "EWSD"
                                     select severity).ToList();
            }

            return true;
        }

        //[RemoteMethod]
        //public void GetSeverityOverride(CallInfo callInfo, Guid centerID)
        //{
        //    using (var db = new TMNModelDataContext())
        //    {
        //        var center = db.Centers.FirstOrDefault(c => c.ID == centerID);
        //        List<AlarmSeverityOverride> severityOverrides = db.AlarmSeverityOverrides.Where(s => s.SwitchTypeID == center.SwitchType.ID).ToList();
        //        Call(callInfo.SourceClientID, "ResponseSeverityOverride", severityOverrides);
        //        Logger.WriteInfo("Getting severity overrides  ...");
        //    }
        //}

        [RemoteMethod]
        public void ProcessData(CallInfo callInfo, List<LogAlarm> alarms)
        {
            try
            {
                var insertsCount = 0;
                var recoversCount = 0;
                var overridesCount = 0;
                var deletesCount = 0;
                // var acknowledgedAlarms = 0;
                //if (data != null)
                //{

                foreach (LogAlarm alarm in alarms)
                {
                    //var message = EwsdLogParser.ParseLog(block, severityOverrides);
                    overridesCount += OvrrideSeverity(alarm);
                    if (alarm != null && (AlarmSeverities)alarm.Severity != AlarmSeverities.None && !alarm.Data.Contains("EXEC'D"))
                    {
                        //shahab 900613 
                        //also find recovery alarm that determine termination of alarm with 'END OF ' statement in title of alarm block
                        if (alarm.Title.ToUpper().EndsWith(" END") || alarm.Title.ToUpper().StartsWith("END OF "))
                        {
                            // This is a recovery
                            recoversCount = AcknowledgeRecoveredAlarms(alarm);
                            //foreach (var recoveredAlarm in FindRecoveredAlarmID(message))
                            //{
                            //    recoveredAlarm.IsRead = true;
                            //    DB.SubmitChanges();
                            //    recoversCount++;
                            //}
                        }
                        else // this may be an alarm
                        {
                            // Informations are initially aknowledged
                            //if ((AlarmSeverities)message.Severity == AlarmSeverities.Information)
                            //    message.IsRead = true;

                            if (InsertAlarm(alarm))
                                insertsCount++;
                        }
                    }
                }
                //}
                //   acknowledgedAlarms = DB.ExecuteCommand("UPDATE LogAlarm SET IsRead = 1 WHERE [CenterID] = {0} AND IsRead=0 AND [Time] < DATEADD(HOUR,-1, GETDATE());", Center.CurrentCenterID);
                //TODO: comment because of redundancy . check it later

                int AlarmExpireSeconds = Setting.Get(Setting.ALARM_EXPIRE_SECONDS, Setting.DEFAULT_ALARM_EXPIRE_SECONDS);
                if (DateTime.Now.Subtract(deleteTimeout).TotalSeconds > AlarmExpireSeconds)
                {
                    deletesCount = DB.Instance.ExecuteCommand("DELETE FROM LogAlarm WHERE [CenterID]={0} AND [Time]<DATEADD(SECOND,-{2},GETDATE()) AND Severity={1};", callInfo.SourceClientID, AlarmSeverities.Information, AlarmExpireSeconds);
                    deleteTimeout = DateTime.Now;
                }
                Logger.WriteInfo("{0} alarm(s) inserted, {1} recovered, {2} overrided, and {3} deleted.", insertsCount, recoversCount, overridesCount, deletesCount);
                // Notifis oly if no error ocuures 
                ServiceState.ReportActivity(ServiceTypes.AlarmService, callInfo.SourceClientID);

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                //IsMonitoring = false;
            }
        }

        [RemoteMethod]
        public void SetServiceState(CallInfo callInfo, string message)
        {
            ServiceState.ReportActivity(ServiceTypes.AlarmService, message);
        }


        private static bool InsertAlarm(LogAlarm message)
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
                        if (match.Success)
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.Data.Contains(match.Value));
                    }
                    else if (recovery.MessageID == 7751)   //recovery for alarm like - LINE LOCKOUT -  recover with 'END OF COMMUNICATION ALARM'
                    {   //only Alarm Identification property in alarm data match pattern like 'DN=23451'
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
                                recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.Data.Contains(int.Parse(match.Groups["param"].Value).ToString())).ToArray();
                            }
                            catch
                            {
                                recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && la.Data.Contains(match.Groups["param"].Value)).ToArray();
                            }
                        }
                    }
                    else
                    {
                        if (recovery.Title.Contains('/'))
                        {
                            string recoveryTitle = recovery.Title.Substring(0, recovery.Title.IndexOf('/'));
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && (la.Location ?? "") == (recovery.Location ?? "") && la.Title.Contains(recoveryTitle)).OrderByDescending(la => la.Time);
                        }
                        else
                        {
                            recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && (la.Location ?? "") == (recovery.Location ?? "") && la.Title.Contains(recovery.Title)).OrderByDescending(la => la.Time);
                        }
                    }


                    foreach (var alarm in recoveredAlarms)
                    {
                        try
                        {
                            alarm.IsRead = true;
                            recoverCount++;
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex);
                        }
                    }
                    db.SubmitChanges();
                    return recoverCount;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return 0;
        }


    }
}
