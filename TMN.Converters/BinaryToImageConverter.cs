using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Data.Linq;
using System.Windows.Data;
using System.IO;

namespace TMN.Converters
{
    public class BinaryToImageConverter : IValueConverter
    {
        public static BinaryToImageConverter Instance = new BinaryToImageConverter();

        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            Binary binaryData = value as Binary;
            if (binaryData == null)
            {
                return null;
            }

            byte[] buffer = binaryData.ToArray();
            if (buffer.Length == 0)
            {
                return null;
            }

            BitmapImage res = new BitmapImage();
            res.BeginInit();
            res.StreamSource = new System.IO.MemoryStream(buffer);
            res.EndInit();
            return res;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage source = value as BitmapImage;
            if (source == null)
            {
                return null;
            }
            if ((source.StreamSource == null) || source.StreamSource.Length == 0)
            {
                Stream strm = new FileStream(source.UriSource.LocalPath, FileMode.Open);
                source.BeginInit();
                source.StreamSource = strm;
                source.EndInit();
            }
            return GetBytesFromStream(source.StreamSource);
        }

        private Binary GetBytesFromStream(System.IO.Stream stream)
        {
            stream.Position = 0;
            byte[] res = new byte[stream.Length + 1];
            using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
            {
                reader.Read(res, 0, (int)stream.Length);
            }
            return new Binary(res);
        }

    }
}
