using System;
using System.Collections.Generic;
using System.Linq;
using Enterprise.Wpf;

namespace TMN
{
    public partial class UserShift : Entity
    {
        public static IEnumerable<UserShift> GetShiftsOfThisMonth(TMNModelDataContext db)
        {
            DateTime date1 = PersianDateTime.Today.MonthInfo.FirstDay.ToGregorian();
            DateTime date2 = PersianDateTime.Today.MonthInfo.LastDay.ToGregorian();
            return db.UserShifts.Where(p => p.Center == Center.Current && p.Date >= date1 && p.Date <= date2).OrderBy(p => p.Date);
        }
    }
}
