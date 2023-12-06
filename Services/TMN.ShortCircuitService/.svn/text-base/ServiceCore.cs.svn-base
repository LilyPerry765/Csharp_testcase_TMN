using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace TMN
{
    class ServiceCore : TmnService
    {
        //private string processorName = "";
        private DateTime lastSaveTime;
        private int saveInterval = 15;

        public override void Start()
        {
            //processorName = RegSettings.Get(Constants.AlarmLogFixString, "") as string;
            CircuitManager.CircuitStatusReceived -= new EventHandler<CircuitStatusEventArgs>(circuit_CircuitDataReceived);
            CircuitManager.CircuitStatusReceived += new EventHandler<CircuitStatusEventArgs>(circuit_CircuitDataReceived);
            SetCriticalBounds();
            CircuitManager.StartRequestingCircuitStatusPeriodically(Setting.Get(Setting.SENSOR_QUERY_INTERVAL, Setting.DEFAULT_SENSOR_QUERY_INTERVAL));
        }

        private void SetCriticalBounds()
        {
            Logger.WriteInfo("Setting critical bounds...");
            try
            {
                //int checksum = 0;
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    var wrongCables = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && (la.Severity == (byte)AlarmSeverities.CircuitOpen || la.Severity == (byte)AlarmSeverities.CircuitShort) );
                    foreach(LogAlarm alarm in wrongCables)
                        if(Activate.ContainsKey(alarm.MessageID.Value - 10000) == false)
                            Activate.Add(alarm.MessageID.Value - 10000, alarm.ID);
                }

                //IEnumerable<Sensor> circuits = null;
                //using (TMNModelDataContext db = new TMNModelDataContext())
                //{
                //    circuits = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID  == (byte)SensorTypes.Circuit).ToArray();
                //}


                //foreach (var circuit in circuits)
                //{ 
                //    if(circuit.ModulNumber.Value <= 100)
                //        throw new NotSupportedException(string.Format("Module No. {0} is not supported. A valid mudule No. is greather than 100.", circuit.ModulNumber.Value));
                //    checksum++;
                //}

                //CircuitManager.CircuitDimension();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public override void Stop()
        {
            Logger.WriteInfo("Stopping service...");
            CircuitManager.StopRequestingCircuitStatusPeriodically();
            Logger.WriteEnd("TMN Circuit Service stopped.");
        }


        Dictionary<int, DateTime> timerCircuit = new Dictionary<int, DateTime>();
        //Note: This method can be accessed from different threads that raise "SensorCollector.SensorDataReceived" event.
        //IEnumerable<Sensor> sensors = null;
        IEnumerable<Sensor> circuits1 = null;
        IEnumerable<Sensor> circuits2 = null;        
        //Note: This method can be accessed from different threads that raise "CircuitCollector.CircuitDataReceived" event.

        private void circuit_CircuitDataReceived(object sender, CircuitStatusEventArgs e)
        {
            try
            {
                var dbDate = DateTime.Now;
                IEnumerable<Sensor> items = null;
                int saveInterval = Setting.Get(Setting.SENSOR_SAVE_INTERVAL, Setting.DEFAULT_SENSOR_SAVE_INTERVAL);
                bool sensorChanged = bool.Parse( Setting.Get(Setting.SENSOR_CHANGED, "false") );
                if (sensorChanged)
                {
                    circuits1 = null;
                    circuits2 = null;

                }
                //using (TMNModelDataContext db = new TMNModelDataContext())
                //{
                //    circuits = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= e.Status.Digits.Keys.Min() && s.ModulNumber <= e.Status.Digits.Keys.Max()).ToArray();
                //    dbDate = db.GetDate().Value;
                //}
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    dbDate = db.GetDate().Value;
                    if (circuits1 == null && e.Status.Digits.Keys.Min() >= 101 && e.Status.Digits.Keys.Max() <= 130)
                        circuits1 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 101 && s.ModulNumber <= 130).ToArray();
                    else if (circuits2 == null && e.Status.Digits.Keys.Min() >= 131 && e.Status.Digits.Keys.Max() <= 160)
                        circuits2 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 131 && s.ModulNumber <= 160).ToArray();
                }
                bool shouldInsertNewRecord = (dbDate - lastSaveTime).TotalSeconds >= saveInterval;

                if (e.Status.Digits.Keys.Min() >= 101 && e.Status.Digits.Keys.Max() <= 130)
                    items = circuits1.ToList();
                else if (e.Status.Digits.Keys.Min() >= 131 && e.Status.Digits.Keys.Max() <= 160)
                    items = circuits2.ToList(); 

                foreach (var circuit in items)
                {
                    int i = circuit.ModulNumber.Value;
                    CircuitEnum value = e.Status[i];
                    if (circuit.Min != null && circuit.Min > 0)
                    {
                        if (value != CircuitEnum.NORMAL)
                        {
                            if (timerCircuit.ContainsKey(i))
                            {
                                if (timerCircuit[i].AddSeconds(circuit.Min.Value) < DateTime.Now)
                                    value = CircuitEnum.NORMAL;
                                else
                                    timerCircuit.Remove(i);
                            }
                            else
                            {
                                timerCircuit.Add(i, DateTime.Now);
                                value = CircuitEnum.NORMAL;
                            }
                        }
                        else
                        {
                            if (timerCircuit.ContainsKey(i))
                                timerCircuit.Remove(i);

                        }
                    }
                    //CircuitEnum value;
                    CheckAlarm(i, value.ToString(), circuit.Title, "CIRCUIT");

                    InsertSensorData(dbDate, shouldInsertNewRecord, circuit, value);

                }

                SetCenterNewAlarms();


                if (CircuitManager.FaultDeviceNumber == -1)
                    ServiceState.ReportActivity(ServiceTypes.CircuitService);
                Logger.WriteDebug("Circuit data updated successfully: {0}", e.Status);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                CircuitManager.FaultDeviceNumber = CircuitManager.CurrentDeviceIndex;
                ServiceState.ReportActivity(ServiceTypes.CircuitService, string.Format("مشکل در ذخيره اطلاعات دستگاه کابل {0}", CircuitManager.FaultDeviceNumber + 1), ex.Message);
            }
        }

        //private void InsertCircuitData(DateTime dbDate, bool shouldInsertNewRecord, Sensor circuit, CircuitEnum value)
        //{
        //    using (TMNModelDataContext db = new TMNModelDataContext())
        //    {
        //        SensorData lastCircuitData = db.SensorDatas.Where(sd => sd.SensorID == circuit.ID).OrderByDescending(sd => sd.Date).FirstOrDefault();
        //        if (lastCircuitData == null || shouldInsertNewRecord)
        //        {
        //            db.SensorDatas.InsertOnSubmit(new SensorData()
        //            {
        //                SensorID = circuit.ID,
        //                ID = Guid.NewGuid(),
        //                Date = dbDate,
        //                Value = (byte)value
        //            });
        //            lastSaveTime = dbDate;
        //            Logger.WriteInfo("New circuit data inserted.");
        //        }
        //        else
        //        {
        //            lastCircuitData.Date = dbDate;
        //            lastCircuitData.Value = (byte)value;
        //        }

        //        db.SubmitChanges();
        //    }
        //}

        private Dictionary<Guid, CircuitEnum> lastSensorsData = new Dictionary<Guid, CircuitEnum>();
        private Dictionary<Guid, DateTime> lastSensorsSaveTime = new Dictionary<Guid, DateTime>();

        private void InsertSensorData(DateTime dbDate, bool shouldInsertNewRecord, Sensor sensor, CircuitEnum value)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {

                bool mustInsert = false;
                if (!lastSensorsData.Keys.Contains(sensor.ID))
                {
                    //if(value != CircuitEnum.NORMAL)
                    mustInsert = true;
                    lastSensorsData.Add(sensor.ID, value);
                    lastSensorsSaveTime.Add(sensor.ID, dbDate);
                }
                else if (lastSensorsData[sensor.ID] != value)
                {
                    mustInsert = true;
                    //SensorData lastSensorData = db.SensorDatas.Where(sd=> sd.SensorID == sensor.ID).OrderByDescending(sd => sd.Date).FirstOrDefault();
                    db.SensorDatas.InsertOnSubmit(new SensorData()
                    {
                        SensorID = sensor.ID,
                        ID = Guid.NewGuid(),
                        Date = dbDate.Subtract(new TimeSpan(0, 0, saveInterval)),
                        Value = (byte)lastSensorsData[sensor.ID],

                    });
                }
                else if (DateTime.Now.Subtract(lastSensorsSaveTime[sensor.ID]).TotalMinutes > 120) // (DateTime.Now.Subtract(lastSaveTime).TotalMinutes > 120)
                {
                    mustInsert = true;
                }
                if (mustInsert)
                {
                    db.SensorDatas.InsertOnSubmit(new SensorData()
                    {
                        SensorID = sensor.ID,
                        ID = Guid.NewGuid(),
                        Date = dbDate,
                        Value = (byte)value
                    });
                    lastSensorsSaveTime[sensor.ID] = dbDate;  //lastSaveTime = dbDate;
                    Logger.WriteInfo("New sensor data inserted.");
                }
                lastSensorsData[sensor.ID] = value;
                db.SubmitChanges();
            }
        }
        private Dictionary<int, Guid> Activate = new Dictionary<int, Guid>();

        private void CheckAlarm(int moduleNumber, string value, string title, string alarmType)
        {
            if (alarmType == "CIRCUIT" && value != CircuitEnum.NORMAL.ToString())
            {
                insertAlarm(moduleNumber, title.ToUpper(), alarmType, value);
            }
            else if (Activate.Keys.Contains(moduleNumber))
            {
                recoverAlarm(moduleNumber, alarmType);
            }
        }

        private void recoverAlarm(int moduleNumber, string alarmType)
        {
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    //signleOrDefault chagned to FirstOrDefualt because throw exception in some cases
                    List<LogAlarm> existingAlarms = db.LogAlarms.Where(la => la.MessageID == (10000 + moduleNumber) && la.IsRead == false ).ToList();
                    foreach (LogAlarm existingAlarm in existingAlarms) //if (existingAlarm != null)
                    {
                        StringBuilder rs = new StringBuilder();
                        rs.AppendLine();
                        rs.AppendFormat("RECOVERY {0} ALARM  ", alarmType);
                        rs.AppendLine();
                        rs.AppendFormat("TIME = {0}", existingAlarm.Time > DateTime.Now ? existingAlarm.Time.ToString("yyyy-MM-dd  HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                        existingAlarm.IsRead = true;
                        existingAlarm.Data = existingAlarm.Data + rs.ToString();
                        db.SubmitChanges();
                        Activate.Remove(moduleNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void insertAlarm(int moduleNumber, string title, string alarmType, string value)
        {
            using (var db = new TMNModelDataContext())
            {
                if (!Activate.Keys.Contains(moduleNumber))
                {
                    LogAlarm alarm = new LogAlarm();
                    alarm.ID = Guid.NewGuid();
                    alarm.MessageID = 10000 + moduleNumber;
                    alarm.Time = DateTime.Now;
                    alarm.Data = string.Format(@" {4}-ALARM              {0}  

***  SEA-MODULE ALARM {1}  

       ALARM     {2}			{4} REPORT
       -------------------------------------------------------------------------
       MODULE NUMBER = {3}
       {4} STATUS = {5}
       UNSOLICITED REPORT   NO = {1}

♂", alarm.Time.ToString("yyyy-MM-dd   HH:mm:ss"), alarm.MessageID, title, moduleNumber, alarmType, value);
                    alarm.Severity = value == CircuitEnum.OpenCircuit.ToString() ? (byte)AlarmSeverities.CircuitOpen : (byte)AlarmSeverities.CircuitShort;
                    alarm.CenterID = Center.CurrentCenterID;
                    alarm.Title = string.Format("SEA-{1} {0}", title, alarmType);
                    if (alarm.Title.Length > 50) alarm.Title = alarm.Title.Substring(0, 50);
                    alarm.Location = string.Format("SEM={0}", moduleNumber);
                    if (alarm.Location.Length > 20) alarm.Location = alarm.Location.Substring(0, 20);
                    alarm.IsRead = false;
                    db.LogAlarms.InsertOnSubmit(alarm);
                    db.SubmitChanges();
                    Activate.Add(moduleNumber, alarm.ID);
                    CountNewAlarms(alarm);
                }
                else
                {
                    LogAlarm existingAlarm = db.LogAlarms.FirstOrDefault(la => la.ID == Activate[moduleNumber]  && la.IsRead == true);
                    if (existingAlarm != null)
                    {
                        existingAlarm.IsRead = false;
                        db.SubmitChanges();
                        CountNewAlarms(existingAlarm);
                    }
                }
            }
        }



    }

}
