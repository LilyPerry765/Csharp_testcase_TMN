using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TMN.Converters
{
    public class TimeSpanConverter : IValueConverter
    {
        public static TimeSpanConverter Instance = new TimeSpanConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return (DateTime?)(null);
            else
                return (DateTime?)new DateTime(value.As<TimeSpan>().Ticks);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return (TimeSpan?)null;
            else
            {
                return (TimeSpan?)value.As<DateTime?>().Value.TimeOfDay;
            }
        }

    }
}
