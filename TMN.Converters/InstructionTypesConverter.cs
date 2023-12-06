using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class InstructionTypesConverter : IValueConverter
    {
        private static InstructionTypesConverter instance = new InstructionTypesConverter();
        public static InstructionTypesConverter Instance
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
            return (InstructionTypes)(int)(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return (int)(InstructionTypes)value;
        }
    }
}
