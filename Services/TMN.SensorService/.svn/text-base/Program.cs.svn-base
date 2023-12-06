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
        internal const string PORT_NAME_KEY = "PortName";
        internal const string BUZZER_ACTIVATION_KEY = "IsBuzzerActivated";
        internal const string SWITCH_LINE_STATE = "SwitchLineState";
        internal const string POWER_CONDUCTOR_NOC = "PowerConductorNOC";
        internal const string DEFAULT_PORT_NAME = "COM1";
        internal const string SERVICE_NAME = "SensorService";
        internal const string DEVICE_NUMBER_KEY = "SensorDeviceNumber";

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
                    new SensorService() 
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
