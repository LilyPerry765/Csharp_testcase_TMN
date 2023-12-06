using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class CenterTypesConverter : IValueConverter
    {
        private static CenterTypesConverter instance = new CenterTypesConverter();
        public static CenterTypesConverter Instance
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
            return (CenterTypes)(int)(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return (int)(CenterTypes)value;
            }
            else
            {
                return null;
            }
        }
    }
}
