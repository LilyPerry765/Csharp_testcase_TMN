using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for InactiveSensorsWindow.xaml
    /// </summary>
    public partial class InactiveSensorsWindow : Window
    {
        public InactiveSensorsWindow()
        {
            InitializeComponent();
            Refresh();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = DB.Instance.GetInActiveSensors((int)(hoursNumericUpDown.Value ?? 24));
        }
    }
}
