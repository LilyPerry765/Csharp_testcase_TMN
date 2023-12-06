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
            SensorManager.SensorStatusReceived -= new EventHandler<SensorStatusEventArgs>(sensor_SensorDataReceived);
            SensorManager.SensorStatusReceived += new EventHandler<SensorStatusEventArgs>(sensor_SensorDataReceived);
            //new Thread(() =>
            //    {

                    SetCriticalBounds();
                //}).Start();
            SensorManager.StartRequestingSensorStatusPeriodically(Setting.Get(Setting.SENSOR_QUERY_INTERVAL, Setting.DEFAULT_SENSOR_QUERY_INTERVAL));
        }

        private void SetCriticalBounds()
        {
            Logger.WriteInfo("Setting critical bounds...");
            try
            {
                Bound temperature1Bound = new Bound(), temperature2Bound = new Bound(), temperature3Bound = new Bound(), humidityBound = new Bound();
                int checksum = 0;

                IEnumerable<Sensor> sensors = null;
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    
                    //if (e.Status.IsSensor && sensors == null)
                        sensors = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID != (int)SensorTypes.Circuit).ToArray();
                    //else if (!e.Status.IsSensor && circuits1 == null && e.Status.circuit.Keys.Min() >= 101 && e.Status.circuit.Keys.Max() <= 130)
                    //    circuits1 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 101 && s.ModulNumber <= 130).ToArray();
                    //else if (!e.Status.IsSensor && circuits2 == null && e.Status.circuit.Keys.Min() >= 131 && e.Status.circuit.Keys.Max() <= 160)
                    //    circuits2 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 131 && s.ModulNumber <= 160).ToArray();
                            var wrongCables = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.IsRead == false && (la.Severity == (byte)AlarmSeverities.CircuitOpen || la.Severity == (byte)AlarmSeverities.CircuitShort));
                            foreach (LogAlarm alarm in wrongCables)
                                if (Activate.ContainsKey(alarm.MessageID.Value - 10000) == false)
                                    Activate.Add(alarm.MessageID.Value - 10000, alarm.ID);
                }

                saveInterval = Setting.Get(Setting.SENSOR_SAVE_INTERVAL, Setting.DEFAULT_SENSOR_SAVE_INTERVAL);


                foreach (var sensor in sensors)
                {
                    switch (sensor.ModulNumber.Value)
                    {
                        case 1:
                            temperature1Bound = new Bound(sensor.Min ?? 0, sensor.Max ?? 0);
                            checksum++;
                            break;
                        case 2:
                            temperature2Bound = new Bound(sensor.Min ?? 0, sensor.Max ?? 0);
                            checksum++;
                            break;
                        case 3:
                            temperature3Bound = new Bound(sensor.Min ?? 0, sensor.Max ?? 0);
                            checksum++;
                            break;
                        case 4:
                            humidityBound = new Bound(sensor.Min ?? 0, sensor.Max ?? 0);
                            checksum++;
                            break;
                        //case 11:
                        //case 12:
                        //case 13:
                        //case 14:
                        //case 15:
                        //case 16:
                        //case 17:
                        //case 18:
                        //    break;
                        //default:
                        //    if(SensorManager.DeviceNumber.Length == 1)
                        //        throw new NotSupportedException(string.Format("Module No. {0} is not supported. A valid mudule No. is 1 to 4.", sensor.ModulNumber.Value));
                    }
                    
                    
                }
                if (checksum == 4)
                {
                    bool isBuzzerActivated = Convert.ToBoolean(RegSettings.Get(Program.BUZZER_ACTIVATION_KEY, true));
                    
                    SensorManager.SetCriticalBounds(temperature1Bound, temperature2Bound, temperature3Bound, humidityBound, isBuzzerActivated);
                }
                //else
                //{
                //    Logger.WriteWarning("There is only {0} sensors instead of 4. Setting critical bounds aborted.", checksum);
                //}
                string powerNOCMode = "" + RegSettings.Get(Program.POWER_CONDUCTOR_NOC, "00000000");
                string switchLine = "" + RegSettings.Get(Program.SWITCH_LINE_STATE, "10"); //openfirstline closeSecondLine
                //SensorManager.SensorDimension();
                SensorManager.SetPowerOpenCloseMode(powerNOCMode);
                //SensorManager.SetSwitchLine(switchLine);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public override void Stop()
        {
            Logger.WriteInfo("Stopping service...");
            SensorManager.StopRequestingSensorStatusPeriodically();
            Logger.WriteEnd("TMN Sensor Service stopped.");
        }


        //Note: This method can be accessed from different threads that raise "SensorCollector.SensorDataReceived" event.
        Dictionary<int, DateTime> timerCircuit = new Dictionary<int, DateTime>();
        IEnumerable<Sensor> sensors = null;
        IEnumerable<Sensor> circuits1 = null;
        IEnumerable<Sensor> circuits2 = null;
        private void sensor_SensorDataReceived(object sender, SensorStatusEventArgs e)
        {
            try
            {
                IEnumerable<Sensor> items = null;
                var dbDate = DateTime.Now;

                bool sensorChanged = bool.Parse(Setting.Get(Setting.SENSOR_CHANGED, "false"));
                if (sensorChanged)
                {
                    circuits1 = null;
                    circuits2 = null;
                    sensors = null;

                }
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    dbDate = db.GetDate().Value;
                    if (e.Status.IsSensor && sensors == null)
                        sensors = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.ModulNumber <= 100).ToArray();
                    else if (!e.Status.IsSensor && circuits1 == null && e.Status.circuit.Keys.Min() >= 101 && e.Status.circuit.Keys.Max() <= 130)
                        circuits1 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 101 && s.ModulNumber <= 130).ToArray();
                    else if (!e.Status.IsSensor && circuits2 == null && e.Status.circuit.Keys.Min() >= 131 && e.Status.circuit.Keys.Max() <= 160)
                        circuits2 = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.TypeID == (byte)SensorTypes.Circuit && s.ModulNumber >= 131 && s.ModulNumber <= 160).ToArray();
                }
                bool shouldInsertNewRecord = (dbDate - lastSaveTime).TotalSeconds >= saveInterval;

                if (e.Status.IsSensor)
                    items = sensors.ToList();
                else if (!e.Status.IsSensor && e.Status.circuit.Keys.Min() >= 101 && e.Status.circuit.Keys.Max() <= 130)
                    items = circuits1.ToList();
                else if (!e.Status.IsSensor && e.Status.circuit.Keys.Min() >= 131 && e.Status.circuit.Keys.Max() <= 160)
                    items = circuits2.ToList(); 

                foreach (var sensor in items)
                {
                        double value;
                        switch (sensor.ModulNumber.Value)
                        {
                            case 1:
                                value = e.Status.Temperature1;
                                InsertSensorData(dbDate, shouldInsertNewRecord, sensor, value);
                                break;
                            case 2:
                                value = e.Status.Temperature2;
                                InsertSensorData(dbDate, shouldInsertNewRecord, sensor, value);
                                break;
                            case 3:
                                value = e.Status.Temperature3;
                                InsertSensorData(dbDate, shouldInsertNewRecord, sensor, value);
                                break;
                            case 4:
                                value = e.Status.Humidity;
                                InsertSensorData(dbDate, shouldInsertNewRecord, sensor, value);
                                break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:

                                int i = sensor.ModulNumber.Value;
                                value = e.Status.Power(i) ? 1 : 0;
                                CheckAlarm(i, e.Status.Power(i).ToString(), sensor.Title, "POWER");
                                InsertSensorDataV2(dbDate, sensor, value);
                                break;
                            default:
                                //if (sensor.ModulNumber.Value > 100)
                                {
                                    //CircuitEnum value;
                                    int j = sensor.ModulNumber.Value;
                                    CircuitEnum cvalue = e.Status.Circuit(j);

                                    if (sensor.Min != null && sensor.Min > 0)
                                    {
                                        if (cvalue != CircuitEnum.NORMAL)
                                        {
                                            if (timerCircuit.ContainsKey(j))
                                            {
                                                if (timerCircuit[j].AddSeconds(sensor.Min.Value) < DateTime.Now)
                                                    cvalue = CircuitEnum.NORMAL;
                                                else
                                                    timerCircuit.Remove(j);
                                            }
                                            else
                                            {
                                                timerCircuit.Add(j, DateTime.Now);
                                                cvalue = CircuitEnum.NORMAL;
                                            }
                                        }
                                        else
                                        {
                                            if (timerCircuit.ContainsKey(j))
                                                timerCircuit.Remove(j);

                                        }
                                    }

                                    CheckAlarm(j, cvalue.ToString(), sensor.Title, "CIRCUIT");
                                    InsertSensorDataV2(dbDate, sensor, (double)cvalue);
                                }
                                break;
                                //throw new NotSupportedException(string.Format("Module No. {0} is not supported. A valid mudule No. is 1 to 4 or 11 to 18.", sensor.ModulNumber.Value));
                        }

                        

                }
                SetCenterNewAlarms();
                if(e.Status.IsSensor)
                    ServiceState.ReportActivity(ServiceTypes.SensorService);
                else
                    ServiceState.ReportActivity(ServiceTypes.CircuitService);
                Logger.WriteDebug("Sensor data updated successfully: {0}", e.Status);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                if (e.Status.IsSensor)
                    ServiceState.ReportActivity(ServiceTypes.SensorService, "مشکل در ذخيره اطلاعات", ex.Message);
                else
                    ServiceState.ReportActivity(ServiceTypes.CircuitService, "مشکل در ذخيره اطلاعات", ex.Message);
            }
        }

        private Dictionary<int, double> lastSensorsDatas = new Dictionary<int, double>();
        private void InsertSensorData(DateTime dbDate, bool shouldInsertNewRecord, Sensor sensor, double value)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                
                if (shouldInsertNewRecord)
                {
                    db.SensorDatas.InsertOnSubmit(new SensorData()
                    {
                        SensorID = sensor.ID,
                        ID = Guid.NewGuid(),
                        Date = dbDate,
                        Value = value
                    });
                    lastSaveTime = dbDate;
                    Logger.WriteInfo("New sensor data inserted.");
                }
                else
                {
                    if (!lastSensorsDatas.ContainsKey(sensor.ModulNumber.Value))
                        lastSensorsDatas.Add(sensor.ModulNumber.Value, value);
                    if (lastSensorsDatas[sensor.ModulNumber.Value] + 1 < value && lastSensorsDatas[sensor.ModulNumber.Value] - 1 > value)
                    {
                        SensorData lastSensorData = db.SensorDatas.Where(sd => sd.SensorID == sensor.ID).OrderByDescending(sd => sd.Date).FirstOrDefault();
                        lastSensorData.Date = dbDate;
                        lastSensorData.Value = value;
                    }
                }

                db.SubmitChanges();
            }
        }

        private Dictionary<Guid, double> lastSensorsData = new Dictionary<Guid,double>();
        private Dictionary<Guid, DateTime> lastSensorsSaveTime = new Dictionary<Guid, DateTime>();
        private void InsertSensorDataV2(DateTime dbDate, Sensor sensor, double value)
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                
                bool mustInsert = false;
                if (!lastSensorsData.Keys.Contains(sensor.ID))
                {
                    //if (sensor.ModulNumber > 100 && value != (byte)CircuitEnum.NORMAL)
                        mustInsert = true;
                    //else if (sensor.ModulNumber < 100)
                    //    mustInsert = true;
                    lastSensorsData.Add(sensor.ID, value);
                    lastSensorsSaveTime.Add(sensor.ID, dbDate);
                }
                else if(lastSensorsData[sensor.ID] != value)
                {
                    mustInsert = true;
                    //SensorData lastSensorData = db.SensorDatas.Where(sd=> sd.SensorID == sensor.ID).OrderByDescending(sd => sd.Date).FirstOrDefault();
                    db.SensorDatas.InsertOnSubmit(new SensorData()
                    {
                        SensorID = sensor.ID,
                        ID = Guid.NewGuid(),
                        Date = dbDate.Subtract(new TimeSpan(0, 0, saveInterval)),
                        Value = lastSensorsData[sensor.ID],
                    });
                }
                else if (DateTime.Now.Subtract(lastSensorsSaveTime[sensor.ID]).TotalMinutes > 120)
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
                        Value = value
                    });
                    lastSensorsSaveTime[sensor.ID] = dbDate;
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
                AlarmSeverities severity = value == CircuitEnum.OpenCircuit.ToString() ? AlarmSeverities.CircuitOpen : AlarmSeverities.CircuitShort;
                insertAlarm(moduleNumber, title.ToUpper(), alarmType, value, severity);
            }
            else if (alarmType == "POWER" && value == "True")
            {
                insertAlarm(moduleNumber, title.ToUpper(), alarmType, value, AlarmSeverities.Power);
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
                    //LogAlarm existingAlarm = db.LogAlarms.FirstOrDefault(la => la.ID == Activate[moduleNumber]);
                    //if (existingAlarm != null)
                    List<LogAlarm> existingAlarms = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID && la.MessageID == (10000 + moduleNumber) && la.IsRead == false ).ToList();
                    foreach (LogAlarm existingAlarm in existingAlarms) //if (existingAlarm != null)
                    {
                        StringBuilder rs = new StringBuilder();
                        rs.AppendLine();
                        rs.AppendFormat("RECOVERY {0} ALARM  ", alarmType);
                        rs.AppendLine();
                        rs.AppendFormat("TIME = {0}", existingAlarm.Time > DateTime.Now ? existingAlarm.Time.ToString("yyyy-MM-dd  HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss"));
                        existingAlarm.IsRead = true;
                        existingAlarm.Data = existingAlarm.Data + rs.ToString();
                        Activate.Remove(moduleNumber);
                    }
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void insertAlarm(int moduleNumber, string title, string alarmType, string value, AlarmSeverities severity)
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

       UNSOLICITED REPORT   NO = {1}

♂", alarm.Time.ToString("yyyy-MM-dd   HH:mm:ss"), alarm.MessageID, title, moduleNumber, alarmType);
                   
                    alarm.Severity = (byte)severity;
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
