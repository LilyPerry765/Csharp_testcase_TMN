using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    public class Routes : Route
    {
        public new string Protocol
        {
            get;
            set;
        }

        public new string DPC
        {
            get;
            set;
        }

        public new string OPC
        {
            get;
            set;
        }

        public new string Traffic
        {
            get
            {
                return Destination == null ? "" : Converters.CenterTypesConverter.Instance.Convert(Destination.CenterType, typeof(string), null, null).IsNull("-").ToString();
            }
        }

        public int LinkCount
        {
            get;
            set;
        }

        public int ChannelCount
        {
            get;
            set;
        }

        public string SignalingTS
        {
            get;
            set;
        }

        public string SignalSign
        {
            get
            {
                return IsSignaling == true ? "*" : "";
            }
        }

        public string DestName
        {
            get
            {
                return Dest == null ? null : Dest.Name;
            }
        }

        public string TestNo
        {
            get
            {
                return Destination == null ? null : Destination.TestNo;
            }
        }

        public string FX
        {
            get
            {
                return Destination == null ? null : Destination.FX;
            }
        }

        public string ContactNo
        {
            get
            {
                return Destination == null ? null : Destination.ContactNo;
            }
        }


    }
}
