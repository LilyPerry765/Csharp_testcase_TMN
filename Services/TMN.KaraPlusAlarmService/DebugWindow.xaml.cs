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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMN
{


    public partial class DebugWindow : Window
    {
        KaraServiceCore core = new KaraServiceCore();

        public DebugWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            core.Start(new string[] { });
        }

        private void StoptButton_Click(object sender, RoutedEventArgs e)
        {
            core.Stop();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}
