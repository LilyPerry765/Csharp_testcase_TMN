using System;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Reflection;

namespace TMN.Converters
{
    public class LinkStateToImageConverter : IValueConverter
    {
        public static LinkStateToImageConverter Instance = new LinkStateToImageConverter();

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is LinkStates))
                return null;
            string path;

            switch ((LinkStates)value)
            {
                case LinkStates.Free:
                    path = "link_free";
                    break;
                case LinkStates.Voice:
                    path = "link_voice";
                    break;
                case LinkStates.DDF:
                    path = "link_ddf";
                    break;
                case LinkStates.Signal:
                    path = "link_signal";
                    break;
                default:
                    throw new NotSupportedException(string.Format("value {0} is not a supported value for LinkTypeToImageConverter.", value));
            }

            //string asmName = Assembly.GetExecutingAssembly().GetName().Name;
            //string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}.png", asmName, path);
            //return new BitmapImage(new Uri(uriString));

            return ImageSourceHelper.GetImageSource(path + ".png");
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }



    }
}
