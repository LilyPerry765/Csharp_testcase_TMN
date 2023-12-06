using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace TMN.Converters
{
    public class PersianDateConverter : IValueConverter
    {
        public static PersianDateConverter Instance = new PersianDateConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime? date = value as DateTime?;
            if (parameter == null)
            {
                parameter = "d";
            }
            if (date.HasValue)
                // DateTime
                if (parameter.ToString() == "dt")
                {
                    return string.Format("{3}:{4} {0:0000}/{1:00}/{2:00}"
                       , pc.GetYear(date.Value)
                       , pc.GetMonth(date.Value)
                       , pc.GetDayOfMonth(date.Value)
                       , date.Value.Hour
                       , date.Value.Minute);
                }
                // Date
                else if (parameter.ToString() == "d")
                {
                    return string.Format("{0:0000}/{1:00}/{2:00}"
                       , pc.GetYear(date.Value)
                       , pc.GetMonth(date.Value)
                       , pc.GetDayOfMonth(date.Value)
                       , date.Value.Hour
                       , date.Value.Minute);
                }
                // Time
                else if (parameter.ToString() == "t")
                {
                    return string.Format("{0:HH:mm}", date);
                }
                else
                    return "-";

            else
                return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
