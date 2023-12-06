using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Runtime.Serialization;

namespace TMN
{
    public partial class ServiceState
    {
        //private TMNModelDataContext db = new TMNModelDataContext();
        private static int? sensorTimeout, alarmTimeout;

        static ServiceState()
        {
            RefreshTimeouts();
        }

        public static void RefreshTimeouts()
        {
            sensorTimeout = Setting.Get(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT);
            alarmTimeout = Setting.Get(Setting.ALARM_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_ALARM_SERVICE_ACTIVITY_TIMEOUT);
        }

        private int MaxInactiveSeconds
        {
            get
            {
                return (ServiceTypes)ServiceType == ServiceTypes.AlarmService ? alarmTimeout.Value  : sensorTimeout.Value;
            }
        }

        public bool IsConnected
        {
            get
            {

                return InActiveSeconds < MaxInactiveSeconds; // && Description == null;
            }
        }

        public string DisconnectReason
        {
            get
            {
                return InActiveSeconds < MaxInactiveSeconds ? Description : "عدم ارتباط با سرويس";
            }
        }

        private System.ServiceProcess.ServiceController controller;
        public System.ServiceProcess.ServiceController Controller
        {
            get
            {
                if (controller == null)
                {
                    try
                    {
                        string serviceName = null;
                        switch ((ServiceTypes)ServiceType)
                        {
                            case ServiceTypes.SensorService:
                                serviceName = "SensorService";
                                break;
                            case ServiceTypes.AlarmService:
                                serviceName = Center.SwitchType.Name + "AlarmService";
                                break;
                            case ServiceTypes.CircuitService:
                                serviceName = "CircuitService";
                                break;
                        }
                        controller = new System.ServiceProcess.ServiceController(serviceName, Center.IPAddress);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex);
                    }
                }
                Impersonation.TryImpersonate(Center.UserName, Center.Password, Center.IPAddress);
                return controller;
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                SendPropertyChanged("Status");
            }
        }

        public string ServiceName
        {
            get
            {
                return ((ServiceTypes)ServiceType).ToString();
            }
        }

        public static void ReportActivity(ServiceTypes serviceType, Guid currentCenterID, string description = null, string technicalReport = null)
        {
            try
            {
               // TMNModelDataContext db = new TMNModelDataContext();
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    var state = db.ServiceStates.SingleOrDefault(ss => ss.CenterID == currentCenterID && (TMN.ServiceTypes)ss.ServiceType == serviceType);
                    DateTime lastAcviteTime = db.GetDate().Value;

                    if (state == null)
                    {
                        state = new ServiceState()
                        {
                            CenterID = currentCenterID,
                            ServiceType = (int)serviceType,
                            LastActiveTime = lastAcviteTime
                        };
                        db.ServiceStates.InsertOnSubmit(state);
                    }
                    state.Description = description;
                    state.TechnicalReport = technicalReport;
                    if (description == null)
                        state.LastActiveTime = lastAcviteTime;
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public static void ReportActivity(ServiceTypes serviceType, string description = null, string technicalReport = null)
        {
            ReportActivity(serviceType, Center.CurrentCenterID, description, technicalReport);
        }

        partial void OnUpgradeTimeChanged()
        {
            SendPropertyChanged("UpgradeTimeSpan");
        }

        public string UpgradeTimeSpan
        {
            get
            {
                var ts = DateTime.Now - UpgradeTime;
                if (ts.HasValue)
                {
                    if (ts.Value.Days > 0)
                    {
                        return ts.Value.Days + " روز پيش";
                    }
                    else if (ts.Value.Hours > 0)
                    {
                        return ts.Value.Hours + " ساعت پيش";
                    }
                    else
                    {
                        return " تازگی";
                    }
                }
                return "هرگز";
            }
        }

        public static void UpgareVersion(System.Reflection.Assembly serviceAssembly, ServiceTypes serviceType)
        {
            Logger.WriteInfo("Checking service state...");
            try
            {
                string newVersion = serviceAssembly.GetName().Version.ToString();
                var db = new TMNModelDataContext();
                var service = db.ServiceStates.SingleOrDefault(s => s.CenterID == Center.CurrentCenterID && s.ServiceType == (int)serviceType);
                var dbNow = db.GetDate().Value;
                if (service == null)
                {
                    service = new ServiceState()
                    {
                        CenterID = Center.CurrentCenterID,
                        ServiceType = (int)ServiceTypes.SensorService,
                        LastActiveTime = dbNow
                    };
                    db.ServiceStates.InsertOnSubmit(service);
                    Logger.WriteInfo("No service state record found; New state record created.");
                }
                if (service.Version == null || service.Version != newVersion)
                {
                    service.UpgradeTime = dbNow;
                    service.Version = newVersion;
                    db.SubmitChanges();
                    Logger.WriteInfo("Service version upgraded to {0}.", newVersion);
                    return;
                }
                else
                {
                    Logger.WriteInfo("Service state checked. No upgrading needed.");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteCritical("Service state check failed!");
            }
        }


    }
}

