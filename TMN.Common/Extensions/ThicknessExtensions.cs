using System.Windows;


namespace TMN
{
    public static class ThicknessExtensions
    {
        public static double GetHeight(this Thickness thickness)
        {
            return thickness.Top + thickness.Bottom;
        }

        public static double GetWidth(this Thickness thickness)
        {
            return thickness.Left + thickness.Right;
        }
    }
}
