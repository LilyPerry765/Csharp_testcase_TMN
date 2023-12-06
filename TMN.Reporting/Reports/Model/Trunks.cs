using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    public class Trunks
    {
        private Channel c;

        public Trunks(Channel channel)
        {
            this.c = channel;
        }

        public string Address
        {
            get
            {
                return c.Link.Address;
            }
        }

        public String Sys
        {
            get
            {
                return c.Link.Sys.ToString();
            }
        }

        public string CIC
        {
            get
            {
                return c.Link.CIC == null ? null : c.Link.CIC.ToString() + "-" + c.TimeSlot.ToString();
            }
        }

        public string TimeSlot
        {
            get
            {
                return c.TimeSlot.ToString();
            }
        }

        public string Channel
        {
            get
            {
                return c.LNO.ToString();
            }
        }

        public string OPC
        {
            get
            {
                return c.Route.OPC;
            }
        }

        public string DPC
        {
            get
            {
                return c.Route.DPC;
            }
        }

        public string TGNO
        {
            get
            {
                return c.Route.TGNO;
            }
        }

        public string DestCenterName
        {
            get
            {
                return c.Route.Destination == null ? null : c.Route.Destination.Name;
            }
        }

        public string Dest
        {
            get
            {
                if (c.Route.Dest != null)
                    return c.Route.Dest.Name;
                return null;
            }
        }

        public string CenterType
        {
            get
            {
                return c.Route.Destination == null ? null : ((CenterTypes)c.Route.Destination.CenterType).ToString();
            }
        }

        public string RouteName
        {
            get
            {
                return c.Route.RouteName;
            }
        }

        public string FX
        {
            get
            {
                return c.Route.Destination == null ? null : c.Route.Destination.FX;
            }
        }

        public string Test
        {
            get
            {
                return c.Route.Destination == null ? null : c.Route.Destination.TestNo;
            }
        }

        public string IsSignalling
        {
            get
            {
                return c.Route.IsSignaling ?? false ? "*" : "";
            }
        }
    }
}
