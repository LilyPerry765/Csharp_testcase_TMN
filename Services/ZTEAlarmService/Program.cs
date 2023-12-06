using System;
using System.Linq;
using System.ServiceProcess;

namespace ZTEAlarmService
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
			else if (args.Contains("/setting"))
			{
				new SettingsWindow().ShowDialog();
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] 
                { 
                    new ZTEAlarmService() 
                };
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
