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
            karaAlarmLogPathTextBox.Text = RegSettings.Get("karaAlarmLogPath") as string;
            //karaAlarmLogPaternTextBox.Text = RegSettings.Get("karaAlarmLogPatern", @".+\.txt") as string;
            //karaAlarmLogFixStringTextBox.Text = RegSettings.Get("karaAlarmLogFixString", "") as string;
            userNameTextBox.Text = RegSettings.Get("karaUserName") as string;
            //passwordTextBox.Text = RegSettings.Get("karaPassword") as string;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save("karaAlarmLogPath", karaAlarmLogPathTextBox.Text.Trim());
            //RegSettings.Save("karaAlarmLogPatern", karaAlarmLogPaternTextBox.Text.Trim());
            //RegSettings.Save("karaAlarmLogFixString", karaAlarmLogFixStringTextBox.Text.Trim());
            RegSettings.Save("karaUserName", userNameTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
                RegSettings.Save("karaPassword", Cryptographer.Encode(passwordTextBox.Text));
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestartService();
        }

        private  void RestartService()
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
                MessageBox.ShowInfo("تغييرات با موفقيت ذخيره و سرويس راه اندازی شد.", "");
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "TMN KARA Alarm Service");
            }
        }

        private void resetFileLocation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.ShowQuestion("آيا از انجام اين کار مطمئن هستيد؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RegSettings.Save("karaLastPosition", 0);
                Logger.WriteInfo("Last file position set to 0.");
            }

        }


    }
}
