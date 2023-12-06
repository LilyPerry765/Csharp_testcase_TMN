using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MessageSenderService
{
    partial class MessageSenderService : ServiceBase
    {
        ServiceCore core;

        public MessageSenderService()
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
            core.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            core.Stop();
        }
    }
}
