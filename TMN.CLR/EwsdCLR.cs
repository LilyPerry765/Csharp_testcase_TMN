using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Text.RegularExpressions;
using Enterprise;
using Microsoft.SqlServer.Server;

namespace TMN
{
    class EwsdCLR
    {
        [SqlFunction]
        private int AcknowledgeRecoveredAlarms(LogAlarm recovery)
        {
            try
            {
                
                IEnumerable<LogAlarm> recoveredAlarms = null;
                using (var db = new TMNModelDataContext())
                {

                    recoveredAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.Time <= recovery.Time && (la.Location ?? "") == (recovery.Location ?? "")).ToArray();


                    //shahab 900613
                    //find recovery for external alarm such as dc and main power or such that
                    if (recovery.Title.ToUpper().Contains("EXTERNAL ALARM EXCHANGE"))
                    {
                        recoveredAlarms = recoveredAlarms.Where(r => Regex.Match(r.Data, @"EAL\s+\d+(\r|\n)").Value == Regex.Match(recovery.Data, @"EAL\s+\d+(\r|\n)").Value);
                    }
                    else
                    {
                        if (recovery.MessageID == 7751)   //recovery for alarm like - LINE LOCKOUT -  recover with 'END OF COMMUNICATION ALARM'
                        { //only Alarm Identification property in alarm data match pattern like 'DN=23451'
                            //if all alarm identification match recovery so ACK it
                            var matchs = Regex.Matches(recovery.Data, @"\DN=(?<param>\w+)");  //@"\w+=(?<param>\w+)");
                            foreach (Match match in matchs)
                            {
                                try
                                {
                                    recoveredAlarms = recoveredAlarms.Where(r => r.Data.Contains(int.Parse(match.Groups["param"].Value).ToString())).ToArray();
                                }
                                catch
                                {
                                    recoveredAlarms = recoveredAlarms.Where(r => r.Data.Contains(match.Groups["param"].Value)).ToArray();
                                }
                            }
                            if (matchs.Count > 0)
                                goto Acknowledge;
                        }

                        recoveredAlarms = recoveredAlarms.Where(la => recovery.Title.Trim().Contains(Regex.Replace(la.Title, @"/.+", "").Trim())).OrderByDescending(la => la.Time);
                    }
                Acknowledge:
                    int recoverCount = 0;

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
