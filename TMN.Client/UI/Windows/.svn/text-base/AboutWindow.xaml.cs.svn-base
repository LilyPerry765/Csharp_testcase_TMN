using System.Windows;
using System.Reflection;
using System.IO;
using System.Windows.Controls;
using System;
using System.Linq;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
            VersionLabel.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] namePatterns = { "TMN.", "Enterprise", "Resources" };
            VersionListBox.ItemsSource = AppDomain.CurrentDomain.GetAssemblies().Where(a =>
              namePatterns.Any(p => a.FullName.StartsWith(p))).Select(a => string.Format("{0}\t\t\t{1}", a.GetName().Name, a.GetName().Version));
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
