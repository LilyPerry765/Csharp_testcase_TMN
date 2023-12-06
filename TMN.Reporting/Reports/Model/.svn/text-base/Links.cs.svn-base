using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    class Links
    {
        public string Address
        {
            get;
            set;
        }

        public Channel FirstChannel
        {
            get;
            set;
        }

        public string RouteName
        {
            get
            {
                if (FirstChannel != null)
                {
                    return FirstChannel.Route.RouteName;
                }
                return null;
            }
        }

        public Center DestCenter
        {
            get
            {
                if (FirstChannel == null)
                {
                    return null;
                }
                return FirstChannel.Route.Destination;
            }
        }

        public string DestCenterName
        {
            get
            {
                if (DestCenter != null)
                    return DestCenter.Name;
                return null;
            }
        }

        public string OPC
        {
            get
            {
                if (FirstChannel != null)
                {
                    return FirstChannel.Route.Source.PointCode;
                }
                return null;
            }
        }

        public string DPC
        {
            get
            {
                if (DestCenter == null)
                    return null;
                return DestCenter.PointCode;
            }
        }

        public string CenterType
        {
            get
            {
                if (DestCenter == null)
                    return null;
                return ((CenterTypes)DestCenter.CenterType.Value).ToString();
            }
        }

        public string FX
        {
            get
            {
                if (DestCenter == null)
                    return null;
                return DestCenter.FX;
            }
        }

        public string Test
        {
            get
            {
                if (DestCenter == null)
                    return null;
                return DestCenter.TestNo;
            }
        }

        public string TGNO
        {
            get
            {
                if (FirstChannel != null)
                {
                    return FirstChannel.Route.TGNO;
                }
                return null;
            }
        }

        public DDF DDF
        {
            set;
            private get;
        }

        public string CIC
        {
            get;
            set;
        }

        public string Sys
        {
            get;
            set;
        }

        public string DDFString
        {
            get
            {
                return DDF == null ? "-" : string.Format("[{0}-{1}-{2}]", DDF.Bay, DDF.Position, DDF.Number);
            }
        }

        
        public string IsSignalling
        {
            get
            {
                return FirstChannel == null ? null : (FirstChannel.Route.IsSignaling ?? false) ? "*" : "";
            }
        }
    }


}
