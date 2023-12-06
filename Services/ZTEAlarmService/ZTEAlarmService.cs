using System.ServiceProcess;

namespace ZTEAlarmService
{
	partial class ZTEAlarmService : ServiceBase
	{

		ServiceCore core = new ServiceCore();

		public ZTEAlarmService()
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
