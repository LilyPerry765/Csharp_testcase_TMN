using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Timers;
using Enterprise;
using Timer = System.Timers.Timer;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text;

namespace TMN
{
    class ServiceCore : TmnService
    {
        private Timer timer = null;
        private List<HuaweiAlarmReceiver> huaweiAlarmReceivers = new List<HuaweiAlarmReceiver>();
        private string switchVersion;
        private string switchType;
        private bool insertInfoLog = true;

        public override void Start()
        {
            InitializeHuaweiAlarmReceiver();
            InitializeTimer();
        }

        public override void Stop()
        {
            if (timer != null)
                timer.Stop();
            foreach (HuaweiAlarmReceiver item in huaweiAlarmReceivers)
                if (item != null)
                    item.Dispose();
            Logger.WriteEnd("Service stopped.");
        }

        private void InitializeTimer()
        {
            timer = new Timer(AlarmQueryInterval);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            Logger.WriteInfo("ALarms will be checked about every {0} seconds.", AlarmQueryInterval);
        }

        private void InitializeHuaweiAlarmReceiver()
        {
            switchVersion = RegSettings.Get("Huawei_Version", "2") as string;
            switchType = RegSettings.Get("Huawei_Type", "Huawei") as string;
            string switchIP = RegSettings.Get("Huawei_IP") as string;
            string userName = RegSettings.Get("Huawei_UserName") as string;
            insertInfoLog = bool.Parse(RegSettings.Get("Huawei_InsertLogInfo", "true").ToString());
            string password = Cryptographer.Decode(RegSettings.Get("Huawei_Password", "") as string);
            string []ips = switchIP.Split(';');
            //string[] types = switchTypes.Split(';');
            //if (types.Length == 0)
            //    types = new string[1] { switchTypes };
            for (int i = 0; i < ips.Length; i++)
            {

                //string type = types.Length < i ? types[i] : types[0];
                if (ips[i] != "NAN")
                {
                    HuaweiAlarmReceiver reciever = new HuaweiAlarmReceiver(ips[i], userName, password, switchType, 6000 + (i * 2));
                    huaweiAlarmReceivers.Add(reciever);
                    if (i == 1)
                        reciever.isUMG = true;
                    reciever.Login();
                }
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                foreach (HuaweiAlarmReceiver receiver in huaweiAlarmReceivers)
                {
                    string data = receiver.RequestActiveAlarms();
                    if (data != null)
                    {
                        ServiceState.ReportActivity(ServiceTypes.AlarmService);
                        ParseReceivedData(data, receiver);
                    }
                    else
                        Logger.WriteWarning("No data received from {0} !", receiver.IP);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
            }
            timer.Start();
        }

        private int updatecounter = 0;
        private List<LogAlarm> prevActiveAlarms = null;
        void ParseReceivedData(string receivedData, HuaweiAlarmReceiver reciever)
        {

            Logger.WriteDebug("Data received. Parsing data... \n {0}", receivedData);
            int count = 0; int recover = 0;

            HuaweiParser parser = new HuaweiParser(insertInfoLog);
            var alarms = parser.Parse(receivedData);

            //recover
            if (prevActiveAlarms == null)
            {
                using (var db = new TMNModelDataContext())
                {
                    prevActiveAlarms = db.LogAlarms.Where(la => la.IsRead == false && la.CenterID == Center.CurrentCenterID).ToList();
                }
            }
            LogAlarm[] recoverdAlarms = (from p in prevActiveAlarms
                                         where !alarms.Any(c => c.MessageID == p.MessageID)
                                         select p).ToArray();

            LogAlarm[] newAlarms = (from p in alarms
                                    where !prevActiveAlarms.Any(c => c.MessageID == p.MessageID)
                                    select p).ToArray();

            LogAlarm[] existingAlarms = (from p in prevActiveAlarms
                                         where alarms.Any(c => c.MessageID == p.MessageID)
                                         select p).ToArray();
            
            foreach (LogAlarm item in recoverdAlarms)
            {
                if (recoverAlarm(item, ref reciever))
                    recover++;
            }

            //prevActiveAlarms.Clear();
            foreach (LogAlarm alarm in newAlarms)
            {
                if (SaveAlarm(alarm))
                    count++;
            }

            updatecounter++;
            if (updatecounter > 10)
            {
                foreach (LogAlarm alarm in existingAlarms)
                {
                    if (UpdateAlarm(alarm))
                        count++;
                }
                updatecounter = 0;
            }

            Logger.WriteInfo("{0} new alarm(s) detected. {1} alarm(s) recoverd.", count, recover);
            SetCenterNewAlarms();
        }

        private bool UpdateAlarm(LogAlarm updated)
        {
            try
            {
                //if (existingAlarm.Severity != (byte)AlarmSeverities.Information)
                {
                    using (var db = new TMNModelDataContext())
                    {
                        //signleOrDefault chagned to FirstOrDefualt because throw exception in some cases
                        LogAlarm existingAlarm = db.LogAlarms.FirstOrDefault(la => la.ID == updated.ID);
                        //new LINQManager().GenericDetach(existingAlarm); 
                        //db.LogAlarms.Attach(existingAlarm, false);
                        if (existingAlarm != null && existingAlarm.IsRead == true)
                        {
                            existingAlarm.IsRead = false;
                            //prevActiveAlarms.Add(existingAlarm);
                        }
                        db.SubmitChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }


        private bool recoverAlarm(LogAlarm recoverd, ref HuaweiAlarmReceiver reciever)
        {
            try
            {
                HuaweiParser parser = new HuaweiParser(insertInfoLog);
                string result = null;
                if (recoverd.MessageID != null)
                {
                    result = reciever.RequestAlarmRecovery(recoverd.MessageID.Value);
                    List<LogAlarm> list = parser.Parse(result);
                    if (list.Count > 0)
                    {
                        result = list[0].Data;
                        using (var db = new TMNModelDataContext())
                        {
                            //signleOrDefault chagned to FirstOrDefualt because throw exception in some cases
                            LogAlarm existingAlarm = db.LogAlarms.FirstOrDefault(la => la.ID == recoverd.ID);
                            //recoverd.Center = default(Center);
                            //new LINQManager().GenericDetach(recoverd); 
                            //db.LogAlarms.Attach(recoverd, false);
                            if (existingAlarm != null)
                            {
                                if (result == null)
                                {
                                    StringBuilder rs = new StringBuilder();
                                    rs.AppendLine();
                                    rs.Append("RECOVERY ALARM  ");
                                    rs.AppendLine();
                                    rs.AppendFormat("TIME = {0}", recoverd.Time > DateTime.Now ? recoverd.Time.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                                    result = recoverd.Data + rs.ToString();
                                }
                                existingAlarm.IsRead = true;
                                if (existingAlarm.Severity >= 11 && existingAlarm.Severity <= 13)
                                    existingAlarm.Severity = (byte)(existingAlarm.Severity % 10);
                                existingAlarm.Data = result;
                                db.SubmitChanges();
                                prevActiveAlarms.Remove(recoverd);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        Logger.WriteError("recovery alarm recieved but can't parse it by parser! \n {0}", result);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }

        private bool SaveAlarm(LogAlarm logAlarm)
        {
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    //signleOrDefault chagned to FirstOrDefualt because throw exception in some cases
                    db.LogAlarms.InsertOnSubmit(logAlarm);
                    CountNewAlarms(logAlarm);
                    prevActiveAlarms.Add(logAlarm);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }

        internal void Start(string data)
        {

            try
            {
                if (data != null)
                {
                    ServiceState.ReportActivity(ServiceTypes.AlarmService);
                    foreach (HuaweiAlarmReceiver receiver in huaweiAlarmReceivers)
                    {
                        ParseReceivedData(data, receiver);
                    }
                }
                else
                    Logger.WriteWarning("No data received!");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
            }
        }

    }

}
