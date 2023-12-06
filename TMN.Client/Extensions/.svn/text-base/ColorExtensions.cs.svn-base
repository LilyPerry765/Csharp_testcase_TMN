using System;

namespace TMN
{
    public static class ColorExtensions
    {

        /// <summary>
        /// Brightens a color according to the given brightness factor.
        /// </summary>
        /// <param Name="brightness">Brightness factor. 0 returns the original color and 255 returns white.</param>
        /// <returns>Returns a brighter color.</returns>
        public static System.Windows.Media.Color Brighten(this System.Windows.Media.Color c, byte brightness)
        {
            return System.Windows.Media.Color.FromRgb((byte)Math.Min(c.R + brightness, 255)
                , (byte)Math.Min(c.G + brightness, 255)
                , (byte)Math.Min(c.B + brightness, 255));
        }

        /// <summary>
        /// Darkens a color according to the given darkness factor.
        /// </summary>
        /// <param Name="brightness">Darkness factor. 0 returns the original color and 255 returns black.</param>
        /// <returns>Returns a darkener color.</returns>
        public static System.Windows.Media.Color Darken(this System.Windows.Media.Color c, byte darkness)
        {
            return System.Windows.Media.Color.FromRgb((byte)Math.Max(c.R - darkness, 0)
                , (byte)Math.Max(c.G - darkness, 0)
                , (byte)Math.Max(c.B - darkness, 0));
        }

        public static System.Windows.Media.Color SetAlpha(this System.Windows.Media.Color c, byte value)
        {
            return System.Windows.Media.Color.FromArgb(value, c.R, c.G, c.B);
        }
        
        public static System.Windows.Media.Color ToWpfColor(this System.Drawing.Color c)
        {
            return (System.Windows.Media.Color)Converters.ColorConverter.Instance.Convert(c, typeof(System.Windows.Media.Color), null, null);
        }

    }
}
