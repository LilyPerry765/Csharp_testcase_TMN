using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace TMN.Converters
{
    public class BooleanFontWeightConverter : IValueConverter
    {
        public static BooleanFontWeightConverter Instance = new BooleanFontWeightConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return FontWeights.Normal;
            }
            else
                return FontWeights.Bold;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
