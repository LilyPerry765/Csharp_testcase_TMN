using System;
using System.Text.RegularExpressions;
using System.Windows;
using Enterprise;
using System.Reflection;

namespace TMN
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title += string.Format(" {0}", Assembly.GetExecutingAssembly().GetName().Version);
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                usernameTextBox.Text = Setting.UpdaterUserName;
                if (Setting.UpdaterPassword != null)
                    currentPassword.Content = new string('*', Setting.UpdaterPassword.Length);
                sensorQueryIntervalTextbox.Text = (Setting.Get(Setting.SENSOR_QUERY_INTERVAL, Setting.DEFAULT_SENSOR_QUERY_INTERVAL) / 1000).ToString();
                sensorSaveIntervalTextbox.Text = Setting.Get(Setting.SENSOR_SAVE_INTERVAL, Setting.DEFAULT_SENSOR_SAVE_INTERVAL).ToString();
                sensorActivityTimeoutTextbox.Text = Setting.Get(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT).ToString();
                sensorExpireTextbox.Text = Setting.Get(Setting.SENSOR_EXPIRE_DAYS, Setting.DEFAULT_SENSOR_EXPIRE_DAYS).ToString();
                alarmActivityTimeoutTextbox.Text = Setting.Get(Setting.ALARM_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_ALARM_SERVICE_ACTIVITY_TIMEOUT).ToString();
                alarmQueryIntervalTextbox.Text = Setting.Get(Setting.ALARM_QUERY_INTERVAL, Setting.DEFAULT_ALARM_QUERY_INTERVAL).ToString();
                centerPanelIntervalTextbox.Text = Setting.Get(Setting.CENTER_ALARM_PANEL_INTERVAL, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL).ToString();
                regionPanelIntervalTextbox.Text = Setting.Get(Setting.REGIONAL_ALARM_PANEL_INTERVAL, Setting.DEFAULT_REGIONAL_ALARM_PANEL_INTERVAL).ToString();
                VoiceAlarmIntervalTextbox.Text = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL).ToString();
                //DisconnectVisibilityIntervalTextbox.Text = Setting.Get(Setting.DC_VISIBILITY_INTERVAL, Setting.DEFAULT_DC_VISIBILITY_INTERVAL).ToString();
                alarmExpireTextBox.Text = Setting.Get(Setting.ALARM_EXPIRE_SECONDS, Setting.DEFAULT_ALARM_EXPIRE_SECONDS).ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSecuritySettings();
                SaveServicesSettings();
                SaveAlarmPanelSettings();
                MessageBox.Show("All valid settings saved.");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void SaveAlarmPanelSettings()
        {
            ValidateAndSaveSetting(Setting.CENTER_ALARM_PANEL_INTERVAL, centerPanelIntervalTextbox.Text, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL);
            ValidateAndSaveSetting(Setting.REGIONAL_ALARM_PANEL_INTERVAL, regionPanelIntervalTextbox.Text, Setting.DEFAULT_REGIONAL_ALARM_PANEL_INTERVAL);
            ValidateAndSaveSetting(Setting.VOICE_ALARM_INTERVAL, VoiceAlarmIntervalTextbox.Text, Setting.DEFAULT_VOICE_ALARM_INTERVAL);
            //ValidateAndSaveSetting(Setting.DC_VISIBILITY_INTERVAL, DisconnectVisibilityIntervalTextbox.Text, Setting.DEFAULT_DC_VISIBILITY_INTERVAL);
        }

        private void SaveSecuritySettings()
        {
            Setting.UpdaterUserName = usernameTextBox.Text;
            if (!string.IsNullOrEmpty(passwordTextBox.Text))
                Setting.UpdaterPassword = passwordTextBox.Text;
        }

        private void SaveServicesSettings()
        {
            ValidateAndSaveSetting(Setting.SENSOR_SAVE_INTERVAL, sensorSaveIntervalTextbox.Text, Setting.DEFAULT_SENSOR_SAVE_INTERVAL);
            ValidateAndSaveSetting(Setting.SENSOR_EXPIRE_DAYS, sensorExpireTextbox.Text, Setting.DEFAULT_SENSOR_EXPIRE_DAYS);
            ValidateAndSaveSetting(Setting.ALARM_EXPIRE_SECONDS, alarmExpireTextBox.Text, Setting.DEFAULT_ALARM_EXPIRE_SECONDS);
            var sensorQueryInterval = ValidateAndSaveSetting(Setting.SENSOR_QUERY_INTERVAL, sensorQueryIntervalTextbox.Text, Setting.DEFAULT_SENSOR_QUERY_INTERVAL);
            var sensorActivityTimeout = ValidateAndSaveSetting(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, sensorActivityTimeoutTextbox.Text, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT);
            var alarmActivityTimeout = ValidateAndSaveSetting(Setting.ALARM_SERVICE_ACTIVITY_TIMEOUT, alarmActivityTimeoutTextbox.Text, Setting.DEFAULT_ALARM_SERVICE_ACTIVITY_TIMEOUT);
            var alarmQueryInterval = ValidateAndSaveSetting(Setting.ALARM_QUERY_INTERVAL, alarmQueryIntervalTextbox.Text, Setting.DEFAULT_ALARM_QUERY_INTERVAL);

            if (sensorActivityTimeout <= sensorQueryInterval / 1000.0)
            {
                MessageBox.Show("\"Sensor Activity Timeout\" must be less than \"Sensor Query Interval\". Otherwise the service status always remains disconnected.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (alarmActivityTimeout <= alarmQueryInterval)
            {
                MessageBox.Show("\"Alarm Activity Timeout\" must be less than \"Alarm Query Interval\". Otherwise the service status always remains disconnected.", "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int? ValidateAndSaveSetting(string settingName, string value, int defaultValue)
        {
            int intValue;
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue.ToString();
            }
            else if (settingName == Setting.SENSOR_QUERY_INTERVAL)
            {
                value = (Convert.ToInt32(value) * 1000).ToString();
            }
            if (int.TryParse(value, out intValue))
            {
                Setting.Set(settingName, value);
                return intValue;
            }
            else
            {
                MessageBox.Show(settingName + " is invalid!");
                return null;
            }
        }

    }
}
