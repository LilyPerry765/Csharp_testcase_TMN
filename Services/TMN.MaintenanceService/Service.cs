using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Enterprise;
using System.Reflection;

namespace TMN
{
    partial class Service : ServiceBase
    {
        Timer timer = new Timer(1000 * 60 * 60);
        public Service()
        {
            InitializeComponent();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                DeleteOldData();
                // MarkOldAlarmsAsRead();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            timer.Start();
        }

        //private void MarkOldAlarmsAsRead()
        //{
        //    using (var db = new TMNModelDataContext())
        //    {
        //        try
        //        {
        //            Logger.WriteInfo("Marking old alarms as read...");
        //            int cnt = db.ExecuteCommand("UPDATE LogAlarm SET IsRead=1 WHERE [Time] < DATEADD(HOUR,-24, GETDATE());");
        //            Logger.WriteInfo("{0} old alarms marked as read.", cnt);
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Write(ex);
        //        }
        //    }
        //}

        private void DeleteOldData()
        {
            using (var db = DB.Instance)
            {
                //try
                //{
                //    Logger.WriteInfo("Deleting old alarms...");
                //    int cnt = db.ExecuteCommand("DELETE FROM LogAlarm WHERE [Time] < GETDATE()-1;");
                //    Logger.WriteInfo("{0} records deleted.", cnt);
                //}
                //catch (Exception ex)
                //{
                //    Logger.Write(ex);
                //}

                try
                {
                    Logger.WriteInfo("Deleting old sensor data...");
                    int days = Setting.Get(Setting.SENSOR_EXPIRE_DAYS, Setting.DEFAULT_SENSOR_EXPIRE_DAYS);
                    int cnt = db.ExecuteCommand("DELETE FROM SensorData WHERE [Date] < GETDATE()-{0};", days);
                    Logger.WriteInfo("{0} records deleted.", cnt);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.WriteStart("TMN Maintenance Service {0} started. (Interval= {1} seconds)", Assembly.GetExecutingAssembly().GetName().Version, timer.Interval / 1000);
            timer.Start();
        }

        protected override void OnStop()
        {
            Logger.WriteEnd("TMN Maintenance Service stopped.");
            timer.Stop();
        }


    }
}
