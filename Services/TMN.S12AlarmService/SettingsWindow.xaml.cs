using System;
using System.Linq;
using System.ServiceProcess;
using System.Windows;
using Enterprise;

namespace TMN
{

    public partial class SettingsWindow : Window
    {

        public string AlarmPositionFile
        {
            get
            {
                string path = "" + RegSettings.Get(Constants.ServicePath);
                path += Constants.AlarmPositionFile;
                return path;
            }
        }

        public SettingsWindow()
        {

            InitializeComponent();
           
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new TMNModelDataContext())
            {
                centersCombo.ItemsSource = db.Centers.OrderBy(c => c.Name).Select(c=> new {c.Name, c.ID, c.DisplayName}).OrderBy(c => c.Name);
            }
            centersCombo.SelectedValue = Center.CurrentCenterID;
            alcatelAlarmLogPathTextBox.Text = RegSettings.Get(Constants.LogFolderPath) as string;
            alcatelAlarmServicePathTextBox.Text = RegSettings.Get(Constants.ServicePath) as string;
            userNameTextBox.Text = RegSettings.Get(Constants.UserName) as string;
            alcatelAlarmLogFixStringTextBox.Text = RegSettings.Get(Constants.AlarmLogFixString, "") as string;
            RepviewDeactiveMinutesTextBox.Text = RegSettings.Get(Constants.RepviewDeactiveMinutes, "10") as string;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RegSettings.Save(Constants.LogFolderPath, alcatelAlarmLogPathTextBox.Text.Trim());
            RegSettings.Save(Constants.ServicePath, alcatelAlarmServicePathTextBox.Text.Trim());
            RegSettings.Save(Constants.UserName, userNameTextBox.Text.Trim());
            RegSettings.Save(Constants.RepviewDeactiveMinutes, RepviewDeactiveMinutesTextBox.Text.Trim());
            RegSettings.Save(Constants.AlarmLogFixString, alcatelAlarmLogFixStringTextBox.Text.Trim());
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
                RegSettings.Save(Constants.Password, Cryptographer.Encode(passwordTextBox.Text));
            Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
            Logger.WriteInfo("Settings saved. Restarting service...");
            RestartService();
        }

        private static void RestartService()
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
                Logger.Write(ex, "TMN S12 Alarm Service");
            }
        }

        //private void reloadButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MessageBox.ShowQuestion("آيا از انجام اين کار مطمئن هستيد؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    {
        //        System.IO.File.Delete(AlarmPositionFile);
        //        TMN.ServiceCore.alarmLogFiles = null;
        //        Logger.WriteInfo("Last file position set to 0.");
        //    }
        //}




    }
}
