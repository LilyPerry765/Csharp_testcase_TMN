using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Timers;
using Enterprise;
using System.Runtime.InteropServices;
using System.Security.Principal;


namespace TMN
{
    partial class EwsdAlarmServcie : ServiceBase
    {
        ServiceCore core = new ServiceCore();
        public EwsdAlarmServcie()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            core.Start(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
            core.Stop();
        }
    }

}
