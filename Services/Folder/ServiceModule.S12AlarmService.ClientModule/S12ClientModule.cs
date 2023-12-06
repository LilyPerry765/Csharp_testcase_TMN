using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Folder.EMQ;

namespace TMN
{
    [ServiceModule("TMN.S12", ServiceModuleSide.Client)]
    public class S12ClientModule : ServiceModuleClient
    {
        public S12ClientModule()
        {
        }

        public override bool Start()
        {
            return base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
