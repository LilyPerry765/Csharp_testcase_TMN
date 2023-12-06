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
    partial class Service1 : ServiceBase
    {
        NeaxServiceCore serviceCore = new NeaxServiceCore();
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
