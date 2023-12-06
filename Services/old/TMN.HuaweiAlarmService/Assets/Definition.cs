using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;


namespace TMN
{
    public delegate void MHandler(string Result);
    public delegate void NetEvents(object sender);
    public delegate void MEventHandler(SwitchEventArgs e);
    public enum DisconnectType
    {
        Outward, Inward, Both
    };
    public class SwitchEventArgs : EventArgs
    {
        public object Sender;
        public String Response;
        public SwitchEventArgs(object sender, string response)
        {
            this.Response = response;
            this.Sender = sender;
        }
    }

    public delegate void ChangeStateEventHandler(SwitchStatusEventArgs e);
    public class SwitchStatusEventArgs : EventArgs
    {
        public object Sender;
        public SwitchState State;
        public SwitchStatusEventArgs(object sender,SwitchState state)
        {
            this.Sender = sender;
            this.State = state;
        }
    }

    public class ThreadBarrier
    {
        private SynchronizationContext _synchronizationContext;
        public ThreadBarrier()
        {
            this._synchronizationContext = AsyncOperationManager.SynchronizationContext;
        }
        public void Post<T>(Action<T> raiseEventMethod, T e)
        where T : EventArgs
        {
            if (this._synchronizationContext == null)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    raiseEventMethod(e);
                });
            }
            else
            {
                this._synchronizationContext.Post(delegate
                {
                    raiseEventMethod(e);
                }, null);
            }
        }
    }

}
