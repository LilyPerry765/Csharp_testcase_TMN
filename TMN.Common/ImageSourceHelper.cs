using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TMN
{
    public static class ImageSourceHelper
    {
        public static string FindPath(string path)
        {
            //string asmName = Assembly.GetExecutingAssembly().GetName().Name;
            string asmName = "Resources";
            string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}", asmName, path);
            return uriString;
        }

		public static string FindPathCircuit(string path)
		{
			//string asmName = Assembly.GetExecutingAssembly().GetName().Name;
			string asmName = "TMN.CircuitResources";
			string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}", asmName, path);
			return uriString;
		}


        public static ImageSource GetImageSource(string path)
        {
            ImageSource imgSrc = new BitmapImage(new Uri(FindPath(path)));
            return imgSrc;
        }

        public static ImageSource GetImageSourceFromFile(string path)
        {
            ImageSource imgSrc = new BitmapImage(new Uri(path));
            return imgSrc;
        }

		//public static ImageSource GetImageCircuit(string path)
		//{
		//    ImageSource imgSrc = new BitmapImage(new Uri(FindPathCircuit(path)));
		//    return imgSrc;
		//}


		public static string FindPathCircuit(string resource,string path)
		{
			//string asmName = Assembly.GetExecutingAssembly().GetName().Name;
			//string asmName = "TMN.CircuitResources";
			string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}", resource, path);
			return uriString;
		}

		public static ImageSource GetImageCircuit(string resource,string path)
		{
			ImageSource imgSrc = new BitmapImage(new Uri(FindPathCircuit(resource,path)));
			return imgSrc;
		}


        public static ImageSource GetImageSourceFromEnum(Regions region)
        {
            switch (region)
            {
                case Regions.Region2:
                    return new BitmapImage(new Uri(FindPath("Region2.jpg")));

                case Regions.Region3:
                    return new BitmapImage(new Uri(FindPath("region3.jpg")));

                case Regions.Region4:
                    return new BitmapImage(new Uri(FindPath("region4.jpg")));

                case Regions.Region5:
                    return new BitmapImage(new Uri(FindPath("region5.jpg")));

                case Regions.Region6:
                    return new BitmapImage(new Uri(FindPath("region6.jpg")));

                case Regions.Region7:
                    return new BitmapImage(new Uri(FindPath("region7.jpg")));

                case Regions.Region8:
                    return new BitmapImage(new Uri(FindPath("region8.jpg")));

                case Regions.Home15 :
                    return new BitmapImage(new Uri(FindPath("region15.jpg")));

                case Regions.Home18 :
                    return new BitmapImage(new Uri(FindPath("region18.jpg")));

                case Regions.Home20 :
                    return new BitmapImage(new Uri(FindPath("region20.jpg")));

                case Regions.Home24 :
                    return new BitmapImage(new Uri(FindPath("region24.jpg")));

                case Regions.Home28 :
                    return new BitmapImage(new Uri(FindPath("region28.jpg")));
            }
            return null;
        }
    }
}
