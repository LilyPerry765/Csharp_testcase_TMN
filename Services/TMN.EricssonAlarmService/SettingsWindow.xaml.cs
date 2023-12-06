using System;
using System.Linq;
using System.Windows;
using Enterprise;
using System.ServiceProcess;
using System.Windows.Media;
using System.Threading;
using Timer = System.Timers.Timer;

namespace TMN
{
    public partial class SettingsWindow : Window
    {

        public SettingsWindow()
        {
            try
            {
                InitializeComponent();
                Logger.WriteStart("Ericsson settings started.");
                LoadSettings();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void LoadSettings()
        {
            using (var db = new TMNModelDataContext())
            {
                centersCombo.ItemsSource = db.Centers.OrderBy(c => c.Name).Select(c => new { c.Name, c.ID, c.DisplayName }).OrderBy(c => c.Name);
            }
            centersCombo.SelectedValue = Center.CurrentCenterID;
            comTextBox.Text = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
            comBaudRateTextBox.Text = RegSettings.Get(Program.PORT_BAUD_RATE_KEY, Program.DEFAULT_PORT_BAUD_RATE) as string;
            userCodeTextbox.Text = RegSettings.Get(Program.USER_CODE_KEY, Program.DEFAULT_USER_CODE) as string;
            passwordTextbox.Text = Cryptographer.Decode((RegSettings.Get(Program.PASSWORD_KEY, Program.DEFAULT_PASSWORD) as string));
            IpTextBox.Text = RegSettings.Get(Program.IP_KEY, Program.DEFAULT_IP) as string;
            ConnectionMethod = RegSettings.Get(Program.CONNECTION_METHOD_KEY, ConnectionMethods.Telnet.ToString()) as string;
        }

        private string ConnectionMethod
        {
            set
            {
                if (value == ConnectionMethods.Telnet.ToString())
                {
                    telnetComboBoxItem.IsSelected = true;
                }
                else
                {
                    serialPortComboboxItem.IsSelected = true;
                }
            }
            get
            {
                if (telnetComboBoxItem.IsSelected)
                {
                    return ConnectionMethods.Telnet.ToString();
                }
                else if (serialPortComboboxItem.IsSelected)
                {
                    return ConnectionMethods.SerialPort.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegSettings.Save(Program.PORT_NAME_KEY, comTextBox.Text);
                RegSettings.Save(Program.PORT_BAUD_RATE_KEY, comBaudRateTextBox.Text);
                RegSettings.Save(Program.IP_KEY, IpTextBox.Text);
                RegSettings.Save(Program.USER_CODE_KEY, userCodeTextbox.Text);
                RegSettings.Save(Program.PASSWORD_KEY, Cryptographer.Encode(passwordTextbox.Text));
                RegSettings.Save(Program.CONNECTION_METHOD_KEY, ConnectionMethod);
                Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
                Logger.WriteInfo("Settings saved. Restarting service...");
                if (RestartService())
                    MessageBox.ShowInfo("تغييرات با موفقيت ذخيره و سرويس راه اندازی شد.", "");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.ShowError(ex.Message);
            }
        }

        private bool RestartService()
        {
            try
            {
                using (ServiceController svc = new ServiceController(Program.SERVICE_NAME))
                {
                    if (svc.CanStop)
                    {
                        svc.Stop();
                        svc.WaitForStatus(ServiceControllerStatus.Stopped);
                    }
                    svc.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, Program.SERVICE_NAME);
            }
            return false;
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void connectionComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (serialPortComboboxItem.IsSelected)
            {
                serialSettingsPanel.Visibility = System.Windows.Visibility.Visible;
                telnetSettingsPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                telnetSettingsPanel.Visibility = System.Windows.Visibility.Visible;
                serialSettingsPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


    }
}
