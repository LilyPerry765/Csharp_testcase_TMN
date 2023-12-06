using System.Windows.Markup;
using System.Windows;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using System.Windows.Controls;

namespace TMN.Assets
{
    public class ImageSourceExtension : MarkupExtension
    {
        public string Path
        {
            get;
            set;
        }

        public ImageSourceExtension()
            : base()
        {
        }

        public ImageSourceExtension(string path)
            : this()
        {
            Path = path;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {

                var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                var source = new BitmapImage();
                try
                {
                    source = new BitmapImage(new Uri(ImageSourceHelper.FindPath(Path)));
                }
                catch (Exception)
                {
                    try
                    {
                        source = new BitmapImage(new Uri(ImageSourceHelper.FindPathCircuit(Path)));
                    }
                    catch (Exception)
                    {
                        source = new BitmapImage(new Uri(ImageSourceHelper.FindPath("close1.png")));
                    }
                }

                var prop = target.TargetProperty as DependencyProperty;
                if (prop == null || prop.PropertyType != typeof(ImageSource))
                {
                    var img = new Image();
                    if (Width != 0) img.Width = Width;
                    if (Height != 0) img.Height = Height;
                    img.Source = source;
                    return img;
                }

                return source;
            }
            catch (Exception)
            {
                return new BitmapImage();
            }
        }

        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }


    }
}
