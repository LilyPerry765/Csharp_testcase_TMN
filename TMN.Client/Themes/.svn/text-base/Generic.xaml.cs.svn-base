using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;

namespace TMN.Themes
{
    public partial class Generic
    {
        public void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (sender as ComboBox).SelectedIndex = -1;
                e.Handled = true;
            }
        }
    }
}
