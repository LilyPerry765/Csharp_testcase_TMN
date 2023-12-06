using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    /// <summary>
    /// Gets a format string as "parameter" and after passing the bound value(s) to the formatting string, returns the formatted string.
    /// </summary>
    public class StringFormatConverter : IValueConverter, IMultiValueConverter
    {
        public static StringFormatConverter Instance = new StringFormatConverter();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format(parameter.ToString(), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 
            for (int i = 0; i < values.Length; i++)
			{
                if (values[i]==System.Windows.DependencyProperty.UnsetValue)
                {
                    values[i]=null;
                }
			}
            return string.Format(parameter.ToString().Replace("\\n","\n"), values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


}
