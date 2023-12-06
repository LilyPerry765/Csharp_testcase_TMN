using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class StringToLinesConverter : IValueConverter
    {
        private static StringToLinesConverter instance = new StringToLinesConverter();
        public static StringToLinesConverter Instance
        {
            get
            {
                return instance;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return value.ToString().BreakCharsToLines();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
