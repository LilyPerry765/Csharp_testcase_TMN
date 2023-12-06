using System;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media;


namespace TMN
{
    public static class CommonExtensions
    {

        public static bool IsAnswered(this Control control, string displayName, params object[] invalidValues)
        {
            bool result;
            string msg;
            if (control is ComboBox)
            {
                result = (control as ComboBox).SelectedIndex > -1;
                if (result == true)
                {
                    foreach (var item in invalidValues)
                    {
                        if ((control as ComboBox).SelectedIndex == (int)item)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                msg = "انتخاب";
            }
            else if (control is TextBox)
            {
                result = !string.IsNullOrEmpty((control as TextBox).Text);
                if (result == true)
                {
                    foreach (string item in invalidValues)
                    {
                        if ((control as TextBox).Text == item)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                msg = "وارد";
            }
            else if (control is Enterprise.Wpf.PersianDateBox)
            {
                result = !(control as Enterprise.Wpf.PersianDateBox).IsDateNull;
                msg = "وارد";
            }
            else if (control is Enterprise.Wpf.NumericUpDown)
            {
                result = (control as Enterprise.Wpf.NumericUpDown).Value.HasValue;
                foreach (decimal item in invalidValues)
                {
                    if ((control as Enterprise.Wpf.NumericUpDown).Value == item)
                    {
                        result = false;
                        break;
                    }
                }

                msg = "انتخاب";
            }
            else if (control is CheckBox)
            {
                result = (control as CheckBox).IsChecked.HasValue;
                msg = "مشخص";
            }
            else
            {
                throw new System.NotImplementedException(string.Format("\"IsAnswered\" logic is not yet implemented for \"{0}\".", control.GetType().FullName));
            }
            result = result || control.Visibility == Visibility.Hidden || control.Visibility == Visibility.Collapsed;
            if (!result)
            {
                MessageBox.Show(string.Format("هيچ مقدار مناسبی براي {0} {1} نشده است.", displayName, msg), "خطا"
                    , MessageBoxButton.OK, MessageBoxImage.Error);
                control.Focus();
                if (control is ComboBox)
                {
                    (control as ComboBox).Expand();
                }
            }
            return result;
            ;
        }


        public static void EnsureVisible(this TreeViewItem node)
        {
            if (node != null)
            {
                node.IsExpanded = true;
                EnsureVisible(node.Parent as TreeViewItem);
            }
        }

        /// <summary>
        /// Updates the underlying data object, without needing to leave the bound controls in a dependency object.
        /// </summary>
        /// <remarks>Normally the underlying data source won't change in a dependency object untill you leave the bound control or you have to set UpdateSourceTrigger property of the binding to "PropertyChanged" value so that it updates the underlying data object on changes of the bound property of your control. 
        /// Calling this method updates the data source without needing to leave the control. 
        /// It useful specially when you use shortcut keys for updating the data source rather than pressing a button.</remarks>
        /// <param x:Name="parent">The parent object which owns the bound control; usually your form.</param>
        /// <example>this.EdnEdit();</example>
        public static void EndEdit(this DependencyObject parent)
        {
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (System.Windows.Data.BindingOperations.IsDataBound(parent, entry.Property))
                {
                    System.Windows.Data.BindingExpression binding = System.Windows.Data.BindingOperations.GetBindingExpression(parent, entry.Property);
                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
            }

            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                EndEdit(child);
            }
        }

        public static void Expand(this ComboBox cmb)
        {
            (new System.Windows.Automation.Peers.ComboBoxAutomationPeer(cmb)
                .GetPattern(System.Windows.Automation.Peers.PatternInterface.ExpandCollapse)
                as System.Windows.Automation.Provider.IExpandCollapseProvider).Expand();

        }
      


          
        /// <summary>
        /// Retrievs the object defined in the ItemTemplate of the Given ItemsControl for the given item of it.
        /// </summary>
        /// <param name="item">The item to retrieve the template control for</param>
        /// <remarks>
        /// Assume that you have a ListBox with CheckBox as ItemControl. When you enumerate ListBox.Items you just get the bound entities.
        /// Using this method you can get the checkBox for ecah item.
        /// </remarks>
        public static DependencyObject GetTemplateContent(this ItemsControl ic, object item)
        {
            return VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(ic.ItemContainerGenerator.ContainerFromItem(item), 0), 0), 0);
        }

    }
}
