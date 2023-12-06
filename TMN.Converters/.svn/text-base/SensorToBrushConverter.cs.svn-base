using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class SensorToBrushConverter : IMultiValueConverter
    {
        public static SensorToBrushConverter Instance = new SensorToBrushConverter();

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = values[0] as double?;
            var min = values[1] as double?;
            var max = values[2] as double?;
            if (val.HasValue)
            {
                if ((max.HasValue && val > max) || (min.HasValue && val < min))
                    return Brushes.Red;
            }
            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
