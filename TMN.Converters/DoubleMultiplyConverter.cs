using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class DoubleMultiplyConverter : IValueConverter, IMultiValueConverter
    {

        public static DoubleMultiplyConverter Instance = new DoubleMultiplyConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double?)value * (double?)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double result = 1;
            foreach (object value in values)
            {
                double? val = 1;
                if (value == null || double.IsNaN((double)value) || (double)value == 0)
                {
                    val = double.Parse(parameter.ToString());
                }
                else
                {
                    val = (double)value;
                }
                result *= val.Value;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
