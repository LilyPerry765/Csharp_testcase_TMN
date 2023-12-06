using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Folder.EMQ;

namespace TMN
{
    [ServiceModule("TMN.S12", ServiceModuleSide.Server)]
    public class S12ServerModule : ServiceModuleServer
    {
        public S12ServerModule()
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
