using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    class LongRecord : TMN.LongRecord
    {

        public LongRecord(TMN.LongRecord lr)
        {
            lr.CopyTo(this);
        }

        public string ChannelState
        {
            get
            {
                return ((ChannelStatus)State.Value).ToString();
            }
        }

        public string PDate
        {
            get
            {
                return Date.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public new string Shift
        {
            get
            {
                if (base.Shift.HasValue)
                {
                    return ((Shifts)base.Shift.Value).ToString();
                }
                return string.Empty;
            }
        }
    }
}
