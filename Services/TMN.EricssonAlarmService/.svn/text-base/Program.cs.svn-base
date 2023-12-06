using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Enterprise;

namespace TMN
{
    static class Program
    {
        internal const string PORT_NAME_KEY = "EricssonPortName";
        internal const string PORT_BAUD_RATE_KEY = "EricssonPortBaudRate";
        internal const string DEFAULT_PORT_BAUD_RATE = "9600";
        internal const string DEFAULT_PORT_NAME = "COM1";
        internal const string SERVICE_NAME = "EricssonAlarmService";
        internal const string USER_CODE_KEY = "EricssonUserCode";
        internal const string PASSWORD_KEY = "EricssonPassword";
        internal const string IP_KEY = "EricssonIP";
        internal const string DEFAULT_USER_CODE = "charging";
        internal const string DEFAULT_PASSWORD = "TCTiran1";
        internal const string DEFAULT_IP = "10.0.0.10";
        internal const string CONNECTION_METHOD_KEY = "EricssonConnectionMethod";


        static void Main(string[] args)
        {
            if (args.Any())
            {
                ShowUI(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new AlarmService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static void ShowUI(string[] args)
        {
            var thread = new Thread(() =>
               {
                   try
                   {
                       if (args.Contains("/debug"))
                       {
                           new DebugWindow().ShowDialog();
                       }
                       else if (args.Contains("/settings"))
                       {
                           new SettingsWindow().ShowDialog();
                       }
                       else if (args.Contains("/?"))
                       {
                           MessageBox.ShowInfo("/debug /settings /?", "Supported Args");
                       }
                   }
                   catch (Exception ex)
                   {
                       Logger.Write(ex);
                   }
               });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
