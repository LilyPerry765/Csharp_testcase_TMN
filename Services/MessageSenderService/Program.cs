using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Enterprise;

namespace MessageSenderService
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				if (args.Contains("/debug") || args.Contains("/d"))
				{
					new DebugWindow().ShowDialog();
				}
				else if (args.Contains("/settings") || args.Contains("/s"))
				{
					new SettingsWindow().ShowDialog();
				}
				else
				{
					ServiceBase[] ServicesToRun;
					ServicesToRun = new ServiceBase[] 
					{ 
						new MessageSenderService() 
					};
					ServiceBase.Run(ServicesToRun);
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}
	}
}
