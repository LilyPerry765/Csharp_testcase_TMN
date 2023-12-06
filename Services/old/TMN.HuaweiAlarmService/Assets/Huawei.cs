using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.ComponentModel;
using Enterprise;


namespace TMN
{


    public class Huawei : HuaweiBase
    {
        private ThreadBarrier _threadBarrier;
        
        public Huawei(IPAddress ip, string username, string password, int commandtimeout)
            : base(ip, username, password, commandtimeout)
        {
            this._threadBarrier = new ThreadBarrier();
        }

        protected override void OnAlarm(SwitchEventArgs e)
        {
            this._threadBarrier.Post(base.OnAlarm, e);
        }

        protected override void OnRecieve(SwitchEventArgs e)
        {
            this._threadBarrier.Post(base.OnRecieve, e);
        }

        protected override void OnChanegeState(SwitchStatusEventArgs State)
        {
            this._threadBarrier.Post(base.OnChanegeState, State);
        }
    }



}
