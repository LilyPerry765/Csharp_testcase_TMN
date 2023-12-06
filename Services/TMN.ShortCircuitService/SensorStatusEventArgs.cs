using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;

namespace TMN
{
    class CircuitStatusEventArgs : EventArgs
    {
        public CircuitStatusEventArgs(CircuitStatus status)
        {
            this.Status = status;
        }

        public CircuitStatus Status
        {
            get;
            set;
        }
    }



}
