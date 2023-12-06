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
            centersCombo.ItemsSource = DB.Instance.Centers.OrderBy(c => c.Name);
            centersCombo.SelectedValue = Center.CurrentCenterID;
            ewsdAlarmLogPathTextBox.Text = RegSettings.Get(Constants.FltFolderPath) as string;
            userNameTextBox.Text = RegSettings.Get(Constants.UserName) as string;
            deactiveHourTextBox.Text = RegSettings.Get(Constants.DeactiveHours, "1") as string;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save(Constants.FltFolderPath, ewsdAlarmLogPathTextBox.Text.Trim());
            RegSettings.Save(Constants.UserName, userNameTextBox.Text.Trim());
            RegSettings.Save(Constants.DeactiveHours, deactiveHourTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
                RegSettings.Save(Constants.Password, Cryptographer.Encode(passwordTextBox.Text));
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestartService();
        }

        private  void RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController(Constants.ServiceName))
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
                Logger.Write(ex, "TMN Carin Alarm Service");
            }
        }

        private void resetFileLocation_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.ShowQuestion("آيا از انجام اين کار مطمئن هستيد؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RegSettings.Save(Constants.LastPosition, 0);
                Logger.WriteInfo("Last file position set to 0.");
            }

        }


    }
}
