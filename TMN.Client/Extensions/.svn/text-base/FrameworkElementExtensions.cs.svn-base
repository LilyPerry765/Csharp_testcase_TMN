using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;


namespace TMN
{
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Gets a framework element and finds its parent of the given type.
        /// </summary>
        /// <typeparam name="T">Type of the parent to search for</typeparam>
        /// <param name="element">The elemnt whose parent is to be found</param>
        /// <returns>The parent of the given element if matches the given type or null otherwise</returns>
        public static T GetParent<T>(this FrameworkElement element) where T : class
        {
            {
                if (element is T)
                {
                    return element as T;
                }
                else
                {
                    DependencyObject dpParent = element;
                    do
                    {
                        dpParent = LogicalTreeHelper.GetParent(dpParent);
                    } while (!(dpParent is T) && dpParent != null);
                    return dpParent as T;
                }
            }
        }

        public static void Reset(this FrameworkElement felement)
        {
            if (felement is TextBox)
            {
                (felement as TextBox).Text = string.Empty;
            }
            else if (felement is ComboBox)
            {
                (felement as ComboBox).SelectedIndex = -1;
            }
            else if (felement is Enterprise.Wpf.NumericUpDown)
            {
                (felement as Enterprise.Wpf.NumericUpDown).Value = 0;
            }
            else if (felement is Enterprise.Wpf.PersianDateBox)
            {
                (felement as Enterprise.Wpf.PersianDateBox).IsDateNull = true;
            }
            else if (felement is CheckBox)
            {
                (felement as CheckBox).IsChecked = null;
            }
            else if (felement is Panel)
            {
                foreach (UIElement item in (felement as Panel).Children)
                {
                    if (item is FrameworkElement)
                    {
                        (item as FrameworkElement).Reset();
                    }
                }
            }
        }

        public static void ChangeHeight(this FrameworkElement e, double newHeight)
        {
            e.BeginAnimation(FrameworkElement.HeightProperty, new DoubleAnimation(newHeight, new Duration(TimeSpan.FromMilliseconds(300))));
        }

        public static void ChangeWidth(this FrameworkElement e, double newWidth)
        {
            e.BeginAnimation(FrameworkElement.WidthProperty, new DoubleAnimation(newWidth, new Duration(TimeSpan.FromMilliseconds(300))));
        }

        /// <summary>
        /// Extracts this FrameworkElement from its parent and returns it as an orphan object.
        /// </summary>
        public static T Extract<T>(this T obj) where T : FrameworkElement
        {
            if (obj.Parent != null)
            {
                if (obj.Parent is Panel)
                {
                    obj.Parent.As<Panel>().Children.Remove(obj);
                }
                else if (obj.Parent is Decorator)
                {
                    obj.Parent.As<Decorator>().Child = null;
                }
                else if (obj.Parent is ContentControl)
                {
                    obj.Parent.As<ContentControl>().Content = null;
                }
                else if (obj.Parent is ToolBarTray && obj is ToolBar)
                {
                    obj.Parent.As<ToolBarTray>().ToolBars.Remove(obj.As<ToolBar>());
                }
                else
                {
                    throw new NotSupportedException(string.Format("Type \"{0}\" as parent of \"{1}\" is not supported yet by \"Extract\" method.", obj.Parent.GetType().Name, obj.GetType().Name));
                }
            }
            return obj;
        }


        public static void Zoom(this FrameworkElement uie, double zoomFactor, EventHandler callBack = null)
        {
            if (!(uie.LayoutTransform is ScaleTransform))
            {
                uie.LayoutTransform = new ScaleTransform(1, 1);
            }
            ScaleTransform t = uie.LayoutTransform as ScaleTransform;
            DoubleAnimation aniX = new DoubleAnimation(zoomFactor, new Duration(TimeSpan.FromMilliseconds(200)));
            DoubleAnimation aniY = new DoubleAnimation(zoomFactor, new Duration(TimeSpan.FromMilliseconds(200)));
            aniX.Completed += (s, e) =>
            {
                if (zoomFactor == 0)
                    uie.Visibility = Visibility.Hidden;
                if (callBack != null)
                    callBack(s, e);
            };
            t.BeginAnimation(ScaleTransform.ScaleXProperty, aniX);
            t.BeginAnimation(ScaleTransform.ScaleYProperty, aniY);
        }
    }
}
