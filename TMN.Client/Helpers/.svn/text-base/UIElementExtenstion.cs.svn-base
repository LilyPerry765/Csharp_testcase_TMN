using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TMN
{
    public static class UIElementExtenstion
    {
        public static readonly DependencyProperty ResourceNameProperty = DependencyProperty.RegisterAttached("ResourceName", typeof(string), typeof(UIElement),
                           new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetResourceName(UIElement element, string value)
        {
            //if (User.Current.UserName.ToLower() != "admin")
            {
                if (User.Current != null)
                {
                    if (!User.Current.PermissionNames.Contains(value))
                    {

                        element.Visibility = Visibility.Collapsed;
                        element.SetValue(ResourceNameProperty, value);
                    }

                }

            }
        }

        public static string GetResourceName(UIElement element)
        {
            return (string)element.GetValue(ResourceNameProperty);
        }
    }
}
