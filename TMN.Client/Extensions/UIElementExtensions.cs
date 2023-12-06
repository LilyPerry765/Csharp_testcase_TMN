using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;


namespace TMN
{
    public static class UIElementExtensions
    {

        public static void FadeIn(this UIElement uie)
        {
            FadeIn(uie, 1);
        }

        public static void FadeOut(this UIElement uie)
        {
            FadeOut(uie, 0);
        }

        public static void FadeIn(this UIElement uie, double toValue)
        {
            FadeIn(uie, toValue, null);
        }

        public static void FadeOut(this UIElement uie, double toValue)
        {
            FadeOut(uie, toValue, null);
        }

        public static void FadeIn(this UIElement uie, EventHandler callBack)
        {
            FadeIn(uie, 1, callBack);
        }

        public static void FadeIn(this UIElement uie, double toValue, EventHandler callBack)
        {
            uie.Visibility = Visibility.Visible;
            DoubleAnimation ani = new DoubleAnimation(uie.Opacity, toValue, new Duration(TimeSpan.FromMilliseconds(200)));
            ani.SpeedRatio = .5;
            if (callBack != null)
            {
                ani.Completed += callBack;
            }
            uie.BeginAnimation(UIElement.OpacityProperty, ani);
        }

        public static void FadeOut(this UIElement uie, EventHandler callBack)
        {
            FadeOut(uie, 0, callBack);
        }

        public static void FadeOut(this UIElement uie, double toValue, EventHandler callBack)
        {
            DoubleAnimation ani = new DoubleAnimation(uie.Opacity, toValue, new Duration(TimeSpan.FromMilliseconds(200)));
            ani.SpeedRatio = .5;
            ani.Completed += (s, e) =>
            {
                if (toValue == 0)
                    uie.Visibility = Visibility.Hidden;
                if (callBack != null)
                    callBack(s, e);
            };
            uie.BeginAnimation(UIElement.OpacityProperty, ani);
        }

        public static Border Border(this UIElement uie, Thickness borderThickness, CornerRadius cornerRadius, Brush borderBrush, Brush background)
        {
            Border b = new Border()
            {
                Background = background,
                BorderBrush = borderBrush,
                CornerRadius = cornerRadius,
                BorderThickness = borderThickness,
                Child = uie
            };
            return b;
        }
    }
}
