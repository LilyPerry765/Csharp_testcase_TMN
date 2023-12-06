using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public class CommunicationData
    {

        public Protocols Protocol
        {
            get;
            set;
        }

        public byte SignalingTimeSlot
        {
            get;
            set;
        }
    }
}
