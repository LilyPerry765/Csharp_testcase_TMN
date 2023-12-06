using System.ServiceProcess;
using System;
using Enterprise;

namespace TMN.HuaweiAlarmService
{
    public partial class Service1 : ServiceBase
    {

        ServiceCore serviceCore = new ServiceCore();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            serviceCore.Start(args);
        }

        protected override void OnStop()
        {
            serviceCore.Stop();
        }

    }
}
