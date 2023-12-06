using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace TMN
{
    class ServiceCore : TmnService
    {
        SensorCollector sensor;
        //TMNModelDataContext DB = new TMNModelDataContext();

        public override void Start(string[] args)
        {
            ParseStartArgs(args);
            base.Start(args);
        }

        public override void Start()
        {
            if (sensor == null)
            {
                sensor = new SensorCollector();
                sensor.ModuleDataReceived -= sensor_ModuleDataReceived;
                sensor.ModuleDataReceived += sensor_ModuleDataReceived;
            }
            sensor.Start();
        }


        private static void ParseStartArgs(string[] args)
        {
            try
            {
                const string HelpString = "Supported Start Args: /? /debug /port /watchdog /interval [seconds] /timeout [seconds]";
                if (args.Any())
                {
                    Logger.WriteInfo("Privided Start Args: {0}", string.Join(" ", args));
                    if (args.Contains("/debug"))
                    {
                        StartArgs.ShowDebugLogs = true;
                    }
                    if (args.Contains("/port"))
                    {
                        StartArgs.ShowPortDataLogs = true;
                    }
                    if (args.Contains("/interval"))
                    {
                        StartArgs.RequestInterval = int.Parse(FindValueOf(args, "/interval"));
                        Logger.WriteInfo("Request Interval set to {0}", StartArgs.RequestInterval);
                    }
                    if (args.Contains("/timeout"))
                    {
                        StartArgs.ResetTimeout = int.Parse(FindValueOf(args, "/timeout"));
                        Logger.WriteInfo("Reset Timeout set to {0}", StartArgs.ResetTimeout);
                    }
                    if (args.Contains("/watchdog"))
                    {
                        StartArgs.ShowWatchDogLogs = true;
                    }
                    if (args.Contains("/?"))
                    {
                        Logger.WriteInfo(HelpString);
                    }
                }
                else
                {
                    Logger.WriteInfo(HelpString);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private static string FindValueOf(string[] args, string argName)
        {
            return args[Array.IndexOf(args, argName) + 1];
        }

        public override void Stop()
        {
            Logger.WriteInfo("Stopping service...");
            if (sensor != null)
                sensor.Stop();
            Logger.WriteEnd("TMN Sensor Service stopped.");
        }

        void sensor_ModuleDataReceived(KeyValuePair<byte, double> data)
        {
                var moduleNo = data.Key;
                var val = data.Value;
                try
                {
                    using (var db = new TMNModelDataContext())
                    {
                        Sensor sensor = db.Sensors.SingleOrDefault(s => s.Room.CenterID == Center.CurrentCenterID && s.ModulNumber == (int)moduleNo);
                        if (sensor != null)
                        {
                            var dbDate = db.GetDate().Value;
                            sensor.SensorDatas.Add(new SensorData()
                            {
                                ID = Guid.NewGuid(),
                                Value = val,
                                Date = dbDate
                            });
                            // Maintenance Service will delete old data
                            // DB.SensorDatas.DeleteAllOnSubmit(sensor.SensorDatas.Where(d => d.Date < dbDate.AddDays(-2)));
                            db.SubmitChanges();
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Logger.Write(ex);
                    IEnumerable<Sensor> invalidSensors;
                    using (var db = new TMNModelDataContext())
                    {
                        invalidSensors = db.Sensors.Where(s => s.Room.CenterID == Center.CurrentCenterID && s.ModulNumber == (int)moduleNo);
                    }
                    if (invalidSensors.Count() > 1)
                    {
                        foreach (var sensor in invalidSensors)
                        {
                            Logger.WriteDebug("Repeated Sensor with module:{0} and title:'{1}'!", sensor.ModulNumber, sensor.Title);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            
        }

    }
}
