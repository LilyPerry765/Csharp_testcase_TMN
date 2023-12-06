using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TMN.Reports.Model
{
    class Tasks : Task
    {
        public string UserName
        {
            get;
            set;
        }

        public new string Shift
        {
            get;
            set;
        }

        public string PersianFinishDate
        {
            get
            {
                return FinishDate.HasValue ? FinishDate.Value.ToPersianDate().ToString("yyyy/MM/dd") : "-";
            }
        }

        public string PersianDueDate
        {
            get
            {
                return DueDate.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public string Title
        {
            get;
            set;
        }

        public string TGNO
        {
            get
            {
                try
                {
                    return base.Route.TGNO;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
