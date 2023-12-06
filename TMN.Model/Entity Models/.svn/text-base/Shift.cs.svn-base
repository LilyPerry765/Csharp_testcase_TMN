using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public partial class Shift : Entity
    {
        public static Shifts GetShiftBasedOnTime()
        {
            var shifts = DB.Instance.Shifts.OrderBy(p => p.ID).ToList();
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (now.IsBetween(shifts[0].StartTime.Value, shifts[1].StartTime.Value))
                return Shifts.صبح;
            else if (now.IsBetween(shifts[1].StartTime.Value, shifts[2].StartTime.Value))
                return Shifts.بعدازظهر;
            else
                return Shifts.شب;
        }

    }


}
