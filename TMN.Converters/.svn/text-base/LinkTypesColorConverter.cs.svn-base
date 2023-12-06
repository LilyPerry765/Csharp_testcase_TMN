using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Media;

namespace TMN.Converters
{
    public class LinkTypesColorConverter : IValueConverter
    {
        private static LinkTypesColorConverter instance = new LinkTypesColorConverter();
        public static LinkTypesColorConverter Instance
        {
            get
            {
                return instance;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<Color> colors = new List<Color>();
            foreach (LinkTypes item in Enum.GetValues(typeof(LinkTypes)))
            {
                if (((LinkTypes)value & item) != 0)
                {
                    colors.Add(GetColorOfLinkType(item));
                }
            }

            RadialGradientBrush brush = new RadialGradientBrush()
            {
                SpreadMethod = GradientSpreadMethod.Repeat
            };

            brush.GradientStops.Add(new GradientStop()
            {
                Color = colors[0],
                Offset = 0
            });
            double offset = 0;
            for (int i = 0; i < colors.Count - 1; i++)
            {
                offset = ((double)1 / (colors.Count)) * (i + 1);
                brush.GradientStops.Add(new GradientStop()
                 {
                     Color = colors[i],
                     Offset = (offset += .1)
                 });

                brush.GradientStops.Add(new GradientStop()
                 {
                     Color = colors[i + 1],
                     Offset = (offset += .1)
                 });
            }
            if (colors.Count > 1)
            {
                brush.GradientStops.Add(new GradientStop()
                {
                    Color = colors[colors.Count - 1],
                    Offset = 1 
                });
            }
            return brush;
        }

        private Color GetColorOfLinkType(LinkTypes value)
        {
            Color color;
            switch ((LinkTypes)value)
            {
                case LinkTypes.راديويي:
                    color = Colors.Blue;
                    break;
                case LinkTypes.فيبرنوري:
                    color = Colors.Red;
                    break;
                case LinkTypes.PCM:
                    color = Colors.Green;
                    break;
                default:
                    color = Colors.Black;
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
