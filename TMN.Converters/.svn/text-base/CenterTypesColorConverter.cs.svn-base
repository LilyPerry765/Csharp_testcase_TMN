using System;
using System.Collections.Generic;
using System.Windows.Data;
using Enterprise;
using System.Configuration;

namespace TMN.Converters
{
    public class CenterTypesColorConverter : IValueConverter
    {
        private static CenterTypesColorConverter instance = new CenterTypesColorConverter();
        public static CenterTypesColorConverter Instance
        {
            get
            {
                return instance;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                System.Drawing.Color drwColor = (System.Drawing.Color)Properties.Settings.Default["Color_" + ((CenterTypes)value).ToString()];
                return Converters.ColorConverter.Instance.Convert(drwColor, null, null, null);
            }
            catch (SettingsPropertyNotFoundException ex)
            {
                Logger.Write(ex, "CenterTypes-Color Converter");
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
