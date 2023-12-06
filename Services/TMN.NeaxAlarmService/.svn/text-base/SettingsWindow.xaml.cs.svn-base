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
            pathTextBox.Text = (string)RegSettings.Get("VitaOneDataFilePath");
        }

        private void browsButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "Access Files |*.accdb;*.mdb",

            };
            if (dlg.ShowDialog() == true)
            {
                pathTextBox.Text = dlg.FileName;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            RegSettings.Save("VitaOneDataFilePath", pathTextBox.Text.Trim());
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestaretService();
        }

        private void RestaretService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("NeaxAlarmService"))
                {
                    if (svc.CanStop)
                        svc.Stop();
                    while (svc.Status != ServiceControllerStatus.Stopped)
                        svc.Refresh();
                    svc.Start();
                }
                TMN.MessageBox.ShowInfo("تغييرات با موفقيت ثبت و سريس مجددا راه اندازی شد", "ثبت تغييرات");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN Neax Alarm Service");
                TMN.MessageBox.ShowError("راه اندازی سرويس با مشکل مواجه شد.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
