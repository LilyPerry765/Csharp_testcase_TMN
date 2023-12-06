using System.ServiceProcess;

namespace ZTEAlarmService
{
	partial class Service : ServiceBase
	{

		ServiceCore core = new ServiceCore();

		public Service()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			base.OnStart(args);
			core.Start();
		}

		protected override void OnStop()
		{
			core.Stop();
		}
	}
}
