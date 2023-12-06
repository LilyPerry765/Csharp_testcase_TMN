using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace TMN
{
    partial class CarinService : ServiceBase
    {
        public CarinService()
        {
            InitializeComponent();
        }

        ServiceCore core = new ServiceCore();

        protected override void OnStart(string[] args)
        {
            core.Start(args);
        }

        protected override void OnStop()
        {
            core.Stop();
        }
    }

}
