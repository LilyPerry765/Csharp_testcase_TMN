using System;
using System.Windows;
using Microsoft.Win32;
using System.ServiceProcess;
using Enterprise;
using System.Linq;

namespace TMN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private const short Minute = 60;

        public SettingsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            centersCombo.ItemsSource = DB.Instance.Centers.OrderBy(c => c.Name);
            centersCombo.SelectedValue = Center.CurrentCenterID;
            sqlServerTextBox.Text = (string)RegSettings.Get("KaraSqlServerAddress");
            sqlUserTextBox.Text = (string)RegSettings.Get("KaraSqlServerUser");
            sqlPassTextBox.Text = (string)RegSettings.Get("KaraSqlServerPass");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            RegSettings.Save("KaraSqlServerAddress", sqlServerTextBox.Text.Trim());
            RegSettings.Save("KaraSqlServerUser", sqlUserTextBox.Text.Trim());
            RegSettings.Save("KaraSqlServerPass", sqlPassTextBox.Text.Trim());
            Logger.WriteInfo("Settings saved. Restarting service...");
            restartService();
        }

        private void restartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("KaraAlarmService"))
                {
                    if (svc.CanStop)
                        svc.Stop();
                    while (svc.Status != ServiceControllerStatus.Stopped)
                        svc.Refresh();
                    svc.Start();
                }
                TMN.MessageBox.ShowInfo("تغييرات با موفقيت ثبت و سرويس مجددا راه اندازی شد", "ثبت تغييرات");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN Kara Alarm Service");
                TMN.MessageBox.ShowError("راه اندازی سرويس با مشکل مواجه شد.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
