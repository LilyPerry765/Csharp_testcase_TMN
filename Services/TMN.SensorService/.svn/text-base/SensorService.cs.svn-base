using System.ServiceProcess;

namespace TMN
{
    public partial class SensorService : ServiceBase
    {
        ServiceCore core;

        public SensorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            if (core == null)
            {
                core = new ServiceCore();
            }
            core.Start(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
            core.Stop();
        }


    }
}
