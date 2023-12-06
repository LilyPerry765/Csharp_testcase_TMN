using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace TMN
{
    static class Program
    {

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
               });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
