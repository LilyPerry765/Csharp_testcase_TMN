using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TMN.Reports.Model
{
    class Events : Event
    {
        public string UserName
        {
            get;
            set;
        }

        public string Date
        {
            get
            {
                return Time.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public string Title
        {
            get;
            set;
        }

        public new string Shift
        {
            get;
            set;
        }
    }
}
