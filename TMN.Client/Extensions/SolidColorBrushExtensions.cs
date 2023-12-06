using System.Windows.Media;


namespace TMN
{
    public static class SolidColorBrushExtensions
    {

        /// <summary>
        /// Brightens a color according to the given brightness factor.
        /// </summary>
        /// <param Name="brightness">Brightness factor. 0 returns the original color and 255 returns white.</param>
        /// <returns>Returns a brighter color.</returns>
        public static SolidColorBrush Brighten(this SolidColorBrush c, byte brightness)
        {
            return new SolidColorBrush(c.Color.Brighten(brightness));
        }

        /// <summary>
        /// Darkens a color according to the given darkness factor.
        /// </summary>
        /// <param Name="brightness">Darkness factor. 0 returns the original color and 255 returns black.</param>
        /// <returns>Returns a darkener color.</returns>
        public static SolidColorBrush Darken(this SolidColorBrush c, byte darkness)
        {
            return new SolidColorBrush(c.Color.Darken(darkness));
        }

        public static SolidColorBrush SetAlpha(this SolidColorBrush c, byte value)
        {
            return new SolidColorBrush(c.Color.SetAlpha(value));
        }

    }
}
