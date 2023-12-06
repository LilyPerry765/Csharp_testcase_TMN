using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TMN
{
    public static class OtherExtensions
    {
        /// <summary>
        /// Returns a substitute value if the source is null and the source otherwise.
        /// </summary>
        /// <typeparam x:Name="T"></typeparam>
        /// <param x:Name="source">The object being tested</param>
        /// <param x:Name="substitue">A replacement for the null object</param>
        /// <returns>Substitute value if the source is null and the source otherwise</returns>
        /// <example>
        /// String str = MyString.IsNull("-");
        /// Button btn = NullButton.IsNull(new Button());
        /// </example>
        public static T IsNull<T>(this T source, T substitue) where T : class
        {
            if (source == null)
            {
                return substitue;
            }
            else
            {
                return source;
            }
        }


        //public static TResult SafeSelect<TSource, TResult>(this TSource source, Func<TSource, TResult> selector) where TSource : class
        //{
        //    if (source == null)
        //    {
        //        return default(TResult);
        //    }
        //    else
        //    {
        //        return selector(source);
        //    }
        //}


        /// <summary>
        /// Determines if the instance is between the given values including the bounds. 
        /// </summary>
        /// <example>
        /// if( "hello".IsBetween("example1", "sample2") ) { ... }
        /// if( someInt.IsBetween(5,6) ){...}
        /// </example>
        public static bool IsBetween<T>(this IComparable<T> instance, T value1, T value2)
        {
            return instance.CompareTo(value1) >= 0 && instance.CompareTo(value2) <= 0;
        }

        public static T As<T>(this object objectToCast)
        {
            if (objectToCast == null)
                return default(T);
            else
                return (T)objectToCast;
        }

        public static T Clone<T>(this T source) where T : new()
        {
            T newT = new T();
            foreach (var p in typeof(T).GetProperties())
            {
                if (p.CanWrite)
                {
                    p.SetValue(newT, p.GetValue(source, null), null);
                }
            }
            return newT;
        }

        public static void CopyTo<T1, T2>(this T1 source, T2 destObject) where T2 : T1
        {
            foreach (var p in typeof(T1).GetProperties())
            {
                if (p.CanWrite)
                {
                    p.SetValue(destObject, p.GetValue(source, null), null);
                }
            }
        }

        public static void SetImageSource(this Image img, string path)
        {
            //string asmName = Assembly.GetExecutingAssembly().GetName().Name;
            //string uriString = string.Format("pack://application:,,,/{0};component/Images/{1}", asmName, path);
            //ImageSource imgSrc = new BitmapImage(new Uri(uriString));
            if (img == null)
            {
                img = new Image();
            }
            img.Source = ImageSourceHelper.GetImageSource(path);
        }
    }
}
