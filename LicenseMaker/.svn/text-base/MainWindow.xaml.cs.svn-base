using System.Windows;

namespace LicenseMaker
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();          
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(licenseComboBox.Text.Trim());     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            macTextBox.Text = TMN.LicenseManager.GetAccessCode();
            licenseComboBox.Text = TMN.LicenseManager.GetLicense(macTextBox.Text.Replace(":", "_"));
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            licenseComboBox.Text = TMN.LicenseManager.GetLicense(macTextBox.Text.Replace("_", ":"));
        }

        private void loadDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            macTextBox.Text = TMN.LicenseManager.GetAccessCode();
            licenseComboBox.Text = TMN.LicenseManager.GetLicense(macTextBox.Text.Replace(":", "_"));
        }

    }
}
