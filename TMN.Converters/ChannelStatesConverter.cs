using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TMN.Converters
{
    public class ChannelStatesConverter : IValueConverter
    {
        private static ChannelStatesConverter instance = new ChannelStatesConverter();
        public static ChannelStatesConverter Instance
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
            return (ChannelStatus)(int)(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return (int)(ChannelStatus)value;
        }
    }
}
