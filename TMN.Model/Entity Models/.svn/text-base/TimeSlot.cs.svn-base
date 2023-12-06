using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public class TimeSlot : Entity
    {
        public TimeSlot(Link link, byte slotNumber)
        {
            this.Link = link;
            this.SlotNumber = slotNumber;
        }

        //public bool IsFree
        //{
        //    get
        //    { 
        //        TMNModelDataContext db = new TMNModelDataContext();
        //        var channelRanges = from cr in db.ChannelRanges
        //                            where cr.Link == Link
        //                            select cr;
        //        foreach (var cr in channelRanges)
        //        {
        //            if (cr.Contains(this.SlotNumber.Value))
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //}

        public byte? SlotNumber
        {
            get;
            set;
        }

        public Link Link
        {
            get;
            set;
        }

        //public bool? IsSignaling
        //{
        //    get
        //    {
        //        try
        //        {
        //            switch (Link.Protocol)
        //            {
        //                case Protocols.BW_No7:
        //                case Protocols.IC_No7:
        //                case Protocols.OG_No7:
        //                    return Link.ContainsSignaling  && Link.SignalingTS == this.SlotNumber;
        //                default:
        //                    return this.SlotNumber == 16;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
