using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TMN.Converters
{
    public class DDFConverter : IMultiValueConverter
    {
        public static DDFConverter Instance = new DDFConverter();



        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (var val in values)
            {
                if (val == System.Windows.DependencyProperty.UnsetValue || val == null)
                {
                    return "-";
                }
            }
            return string.Format((parameter ?? "{0}-{1}-{2}").ToString(), values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
