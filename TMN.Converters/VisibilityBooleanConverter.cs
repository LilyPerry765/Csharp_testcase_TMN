using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class VisibilityBooleanConverter : IValueConverter
    {
        public static VisibilityBooleanConverter Instance = new VisibilityBooleanConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((bool?)value)?? false) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (System.Windows.Visibility)value == System.Windows.Visibility.Visible;
        }
    }
}
