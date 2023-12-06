using System;
using System.Linq;
using System.ServiceProcess;
using System.Windows;
using Enterprise;

namespace TMN
{

    public partial class SettingsWindow : Window
    {
        
        public SettingsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new TMNModelDataContext())
            {
                centersCombo.ItemsSource = db.Centers.OrderBy(c => c.Name).Select(c => new { c.Name, c.ID, c.DisplayName }).OrderBy(c => c.Name);
            }
            centersCombo.SelectedValue = Center.CurrentCenterID;
            ewsdAlarmLogPathTextBox.Text = RegSettings.Get("ewsdAlarmLogPath") as string;
            ewsdAlarmLogPaternTextBox.Text = RegSettings.Get("ewsdAlarmLogPatern", @".+\.txt") as string;
            ewsdAlarmLogFixStringTextBox.Text = RegSettings.Get("ewsdAlarmLogFixString", "") as string;
            userNameTextBox.Text = RegSettings.Get("ewsdUserName") as string;
            //passwordTextBox.Text = RegSettings.Get("ewsdPassword") as string;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save("ewsdAlarmLogPath", ewsdAlarmLogPathTextBox.Text.Trim());
            RegSettings.Save("ewsdAlarmLogPatern", ewsdAlarmLogPaternTextBox.Text.Trim());
            RegSettings.Save("ewsdAlarmLogFixString", ewsdAlarmLogFixStringTextBox.Text.Trim());
            RegSettings.Save("ewsdUserName", userNameTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
                RegSettings.Save("ewsdPassword", Cryptographer.Encode(passwordTextBox.Text));
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestartService();
        }

        private void RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("EwsdAlarmService"))
                {
                    if (svc.CanStop)
                        svc.Stop();
                    while (svc.Status != ServiceControllerStatus.Stopped)
                        svc.Refresh();
                    svc.Start();
                }
                MessageBox.ShowInfo("تغييرات با موفقيت ذخيره و سرويس راه اندازی شد.", "");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN EWSD Alarm Service");
            }
        }

        private void resetFileLocation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.ShowQuestion("آيا از انجام اين کار مطمئن هستيد؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RegSettings.Save("lastPosition", 0);
                Logger.WriteInfo("Last file position set to 0.");
            }
        }


    }
}
