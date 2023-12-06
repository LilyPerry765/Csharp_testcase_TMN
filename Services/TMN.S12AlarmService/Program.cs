using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TMN
{
    static class Program
    {

        [STAThread]
        static void Main(string[] args)
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
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
				{ 
					new S12Service() 
				};
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}