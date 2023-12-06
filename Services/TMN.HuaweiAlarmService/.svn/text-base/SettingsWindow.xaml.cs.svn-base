using System;
using System.Linq;
using System.Windows;
using Enterprise;
using System.ServiceProcess;

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

            ipTextBox.Text = RegSettings.Get("Huawei_IP") as string;
            switchVersionComboBox.Text = (RegSettings.Get("Huawei_Version", "2") as string).Trim();
            switchTypeComboBox.Text = (RegSettings.Get("Huawei_Type", "Huawei") as string).Trim();
            userNameTextBox.Text = RegSettings.Get("Huawei_UserName") as string;
            passwordTextBox.Password = Cryptographer.Decode(RegSettings.Get("Huawei_Password", "") as string);
            LogInfoComboBox.Text = (RegSettings.Get("Huawei_InsertLogInfo", "true") as string).Trim();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (centersCombo.SelectedIndex > -1)
            {
                RegSettings.Save("Huawei_IP", ipTextBox.Text.Trim());
                RegSettings.Save("Huawei_Version", switchVersionComboBox.Text.Trim());
                RegSettings.Save("Huawei_Type", switchTypeComboBox.Text.Trim());
                RegSettings.Save("Huawei_UserName", userNameTextBox.Text.Trim());
                RegSettings.Save("Huawei_Password", Cryptographer.Encode(passwordTextBox.Password));
                RegSettings.Save("Huawei_InsertLogInfo", LogInfoComboBox.Text.Trim());
                Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
                Logger.WriteInfo("Settings saved. Restarting service...");
                RestartService();
            }
            else
            {
                MessageBox.ShowError("مرکز را انتخاب کنيد.");
            }
        }

        private  void RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController("HuaweiAlarmService"))
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
                Logger.Write(ex, "TMN Huawei Alarm Service");
            }
        }
    }
}
