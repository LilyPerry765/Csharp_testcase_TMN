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
using Enterprise;
using System.ServiceProcess;


namespace TMN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            centersCombo.ItemsSource = DB.Instance.Centers.OrderBy(c => c.Name);
            centersCombo.SelectedValue = Center.CurrentCenterID;
            comTextBox.Text = RegSettings.Get("PortName", "COM1") as string;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save("PortName", comTextBox.Text);
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestartService();
        }

        private static void RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("SensorService"))
                {
                    if (svc.CanStop)
                        svc.Stop();
                    while (svc.Status != ServiceControllerStatus.Stopped)
                        svc.Refresh();
                    svc.Start();
                }
                MessageBox.ShowInfo(".تغييرات با موفقيت ذخيره و سرويس راه اندازی شد", "");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN SensorService Service");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void sensorSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new TMN.UI.Windows.SensorsWindow().ShowDialog();
        }
    }
}
