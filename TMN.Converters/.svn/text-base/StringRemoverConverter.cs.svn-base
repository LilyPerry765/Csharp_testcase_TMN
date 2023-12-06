using System;
using System.Windows.Data;

namespace TMN.Converters
{
    public class StringRemoverConverter : IValueConverter
    {
        public static StringRemoverConverter Instance = new StringRemoverConverter();

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Replace(parameter.ToString(), "");
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("");
        }


    }
}
