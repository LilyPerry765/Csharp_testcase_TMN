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
        private ServiceController sensorServiceController = new ServiceController(Program.SERVICE_NAME);
        private Timer timer = new Timer(3000);
        private DateTime lastActivityTime;

        public SettingsWindow()
        {
            try
            {
                InitializeComponent();
                SensorManager.SensorStatusReceived += new EventHandler<SensorStatusEventArgs>(SensorManager_SensorDataReceived);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Start();
                LoadSettings();
                UpdateServiceStatus();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            Dispatcher.Invoke((Action)delegate()
                   {
                       if ((DateTime.Now - lastActivityTime).TotalSeconds > 1)
                       {
                           temp1.Value = temp2.Value = temp3.Value = humidity.Value = null;
                           UpdateServiceStatus();
                       }
                   });

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                if (sensorServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    if (MessageBox.ShowQuestion("سرويس اجرا نيست. پيش از خروج سرويس اجرا شود؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SensorManager.StopRequestingSensorStatusPeriodically();
                        ChangeServiceState(ServiceControllerStatus.Running);
                    }
                }
                timer.Stop();
                timer.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        void SensorManager_SensorDataReceived(object sender, SensorStatusEventArgs e)
        {
            try
            {
                lastActivityTime = DateTime.Now;
                Dispatcher.Invoke((Action)delegate()
                                     {
                                         temp1.Value = e.Status.Temperature1;
                                         temp2.Value = e.Status.Temperature2;
                                         temp3.Value = e.Status.Temperature3;
                                         humidity.Value = e.Status.Humidity;


                                         ledpc1.DisplayBrush = e.Status.Power(11) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc2.DisplayBrush = e.Status.Power(12) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc3.DisplayBrush = e.Status.Power(13) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc4.DisplayBrush = e.Status.Power(14) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc5.DisplayBrush = e.Status.Power(15) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc6.DisplayBrush = e.Status.Power(16) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc7.DisplayBrush = e.Status.Power(17) == false ? Brushes.LimeGreen : Brushes.Red;
                                         ledpc8.DisplayBrush = e.Status.Power(18) == false ? Brushes.LimeGreen : Brushes.Red;
                                     });
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void LoadSettings()
        {
            centersCombo.ItemsSource = DB.Instance.Centers.OrderBy(c => c.Name);
            centersCombo.SelectedValue = Center.CurrentCenterID;
            buzzerCheckbox.IsChecked = Convert.ToBoolean(RegSettings.Get(Program.BUZZER_ACTIVATION_KEY, true));
            comTextBox.Text = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
            sensorIntervalTextBox.Text = Setting.Get(Setting.SENSOR_QUERY_INTERVAL, Setting.DEFAULT_SENSOR_QUERY_INTERVAL).ToString();
            deviceNumberTextBox.Text = RegSettings.Get(Program.DEVICE_NUMBER_KEY, "1").ToString();

            string powerNOCMode = "" + RegSettings.Get(Program.POWER_CONDUCTOR_NOC, "00000000");
            string switchLine = "" + RegSettings.Get(Program.SWITCH_LINE_STATE, "10"); //openfirstline closeSecondLine

            char[] powermodes = powerNOCMode.ToCharArray();
            pc1.IsChecked = powermodes[0] == '1' ? true: false;
            pc2.IsChecked = powermodes[1] == '1' ? true : false;
            pc3.IsChecked = powermodes[2] == '1' ? true: false;
            pc4.IsChecked = powermodes[3] == '1' ? true: false;
            pc5.IsChecked = powermodes[4] == '1' ? true: false;
            pc6.IsChecked = powermodes[5] == '1' ? true: false;
            pc7.IsChecked = powermodes[6] == '1' ? true: false;
            pc8.IsChecked = powermodes[7] == '1' ? true: false;

            char[] linestate = switchLine.ToCharArray();
            SwitchLine1.IsChecked = linestate[0] == '1' ? true : false;
            SwitchLine2.IsChecked = linestate[1] == '1' ? true : false;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegSettings.Save(Program.PORT_NAME_KEY, comTextBox.Text);
                RegSettings.Save(Program.BUZZER_ACTIVATION_KEY, buzzerCheckbox.IsChecked.Value);
                RegSettings.Save(Program.POWER_CONDUCTOR_NOC, string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", pc1.IsChecked.Value ? 1 : 0, pc2.IsChecked.Value ? 1 : 0, pc3.IsChecked.Value ? 1 : 0, pc4.IsChecked.Value ? 1 : 0, pc5.IsChecked.Value ? 1 : 0, pc6.IsChecked.Value ? 1 : 0, pc7.IsChecked.Value ? 1 : 0, pc8.IsChecked.Value ? 1 : 0));
                RegSettings.Save(Program.SWITCH_LINE_STATE, string.Format("{0}{1}", SwitchLine1.IsChecked.Value ? 1 : 0, SwitchLine2.IsChecked.Value ? 1 : 0));
                Setting.Set(Setting.SENSOR_QUERY_INTERVAL, sensorIntervalTextBox.Text);
                RegSettings.Save(Program.DEVICE_NUMBER_KEY, deviceNumberTextBox.Text);

                Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
                Logger.WriteInfo("Settings saved. Restarting service...");
                SensorManager.MarkForReInitialize();
                if (RestartService())
                    MessageBox.ShowInfo("تغييرات با موفقيت ذخيره و سرويس راه اندازی شد.", "");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.ShowError(ex.Message);
            }
        }

        private void UpdateServiceStatus()
        {
            try
            {
                sensorServiceController.Refresh();
                statusTextblock.Text = Program.SERVICE_NAME + " is " + sensorServiceController.Status.ToString();
                if (sensorServiceController.Status != ServiceControllerStatus.Running)
                {
                    statusTextblock.Foreground = Brushes.Red;
                }
                else
                {
                    statusTextblock.Foreground = Brushes.Black;
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                statusTextblock.Text = "Warning: Sensor service is not installed!";
                statusTextblock.Foreground = Brushes.Red;
            }
        }

        private bool RestartService()
        {
            return ChangeServiceState(ServiceControllerStatus.Stopped) &&
               ChangeServiceState(ServiceControllerStatus.Running);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void sensorSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new TMN.UI.Windows.SensorsWindow().ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            SensorManager.StopRequestingSensorStatusPeriodically();
            base.OnClosed(e);
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(calibrasionTabItem))
            {
                ChangeServiceState(ServiceControllerStatus.Stopped);
                SensorManager.StartRequestingSensorStatusPeriodically(3000);
            }
            else
            {
                SensorManager.StopRequestingSensorStatusPeriodically();
            }
        }

        private bool ChangeServiceState(ServiceControllerStatus newState)
        {
            try
            {
                Logger.WriteInfo("Changing service state to {0}.", newState);
                sensorServiceController.Refresh();
                if (sensorServiceController.Status != newState)
                {
                    switch (newState)
                    {
                        case ServiceControllerStatus.Running:
                            sensorServiceController.Start();
                            break;
                        case ServiceControllerStatus.Stopped:
                            if (sensorServiceController.CanStop)
                            {
                                sensorServiceController.Stop();
                            }
                            else
                            {
                                Logger.WriteWarning("Cannot stop service!");
                            }
                            break;
                        default:
                            Logger.WriteWarning("{0} is not a valid state for this method.", newState);
                            return false;
                    }
                    sensorServiceController.WaitForStatus(newState);
                    UpdateServiceStatus();
                    Logger.WriteInfo("Service state is now {0}.", newState);
                }
                else
                {
                    Logger.WriteInfo("Service state is already {0}.", newState);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }

        private void SensorLed_Click(SensorLedButton sender)
        {
            try
            {
                SensorManager.ResetCalibration(sender.SensorNumber);
                CalibrationWindow win = new CalibrationWindow();
                win.DisplayBrush = sender.SensorNumber == 4 ? Brushes.LimeGreen : Brushes.Red;
                Thread.Sleep(100);
                var sensorStatus = SensorManager.RequestSensorStatus();
                win.Value = sensorStatus[sender.SensorNumber];
                win.Owner = this;
                win.SensorTitle = sender.Title;
                if (win.ShowDialog() == true)
                {
                    SensorManager.Calibrate(sender.SensorNumber, win.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.ShowError(ex.Message);
            }
        }


    }
}

