using System.Windows.Data;
using System;

namespace TMN.Converters
{
    public class ColorConverter : IValueConverter
    {
        public static ColorConverter Instance = new ColorConverter();

        /// <summary>
        /// Converts the given value (System.Drawing.Color, System.Windows.Media.Brush or System.Windows.Media.Color) to System.Windows.Media.Brush or System.Windows.Media.Color
        /// </summary>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Color mediaColor;

            if (value is System.Windows.Media.Brush)
                mediaColor = value.As<System.Windows.Media.SolidColorBrush>().Color;
            else if (value is System.Drawing.Color)
            {
                System.Drawing.Color drwColor;
                drwColor = (System.Drawing.Color)value;
                mediaColor = System.Windows.Media.Color.FromRgb(drwColor.R, drwColor.G, drwColor.B);
            }
            else if (value is System.Windows.Media.Color)
                mediaColor = value.As<System.Windows.Media.Color>();
            else
                throw new NotSupportedException(value.GetType().FullName + " is not supported for ColorConverter.");

            if (targetType == typeof(System.Windows.Media.Brush))
            {
                return new System.Windows.Media.SolidColorBrush(mediaColor);
            }
            return mediaColor;
        }

        /// <summary>
        /// Converts the given value (System.Windows.Media.Brush, System.Windows.Media.Color) to System.Drawing.Color
        /// </summary>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Color c;

            if (value is System.Windows.Media.Brush)
                c = value.As<System.Windows.Media.SolidColorBrush>().Color;
            else if (value is System.Windows.Media.Color)
                c = (System.Windows.Media.Color)value;
            else
                throw new NotSupportedException(value.GetType().FullName + " is not supported for ColorConverter.");

            return System.Drawing.Color.FromArgb(c.R, c.G, c.B);
        }


    }
}
