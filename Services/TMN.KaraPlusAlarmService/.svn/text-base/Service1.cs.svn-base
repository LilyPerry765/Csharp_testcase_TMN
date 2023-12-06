using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Enterprise;
using System.Data.OleDb;
using System.Timers;
using System.Reflection;

namespace TMN.KaraAlarmService
{
    public partial class Service1 : ServiceBase
    {
        KaraServiceCore core = new KaraServiceCore();

        public Service1()
        {
            InitializeComponent();
        }

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
