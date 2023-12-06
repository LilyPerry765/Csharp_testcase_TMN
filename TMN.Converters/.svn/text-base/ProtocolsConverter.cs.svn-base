using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class ProtocolsConverter : IValueConverter
    {
        private static ProtocolsConverter instance = new ProtocolsConverter();
        public static ProtocolsConverter Instance
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
            return (Protocols)(int)(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return (int)(Protocols)value;
        }
    }
}
