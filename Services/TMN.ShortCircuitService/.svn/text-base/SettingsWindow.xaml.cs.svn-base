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
        private ServiceController circuitServiceController = new ServiceController(Program.SERVICE_NAME);
        private Timer timer = new Timer(3000);
        private DateTime lastActivityTime;

        public SettingsWindow()
        {
            try
            {
                InitializeComponent();
                CircuitManager.CircuitStatusReceived += new EventHandler<CircuitStatusEventArgs>(CircuitManager_CircuitDataReceived);
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
                           UpdateServiceStatus();
                       }
                   });

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                if (circuitServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    if (MessageBox.ShowQuestion("سرويس اجرا نيست. پيش از خروج سرويس اجرا شود؟", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        CircuitManager.StopRequestingCircuitStatusPeriodically();
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

        void CircuitManager_CircuitDataReceived(object sender, CircuitStatusEventArgs e)
        {
            try
            {
                lastActivityTime = DateTime.Now;
                Dispatcher.Invoke((Action)delegate()
                                     {
                                         if (e.Status.Digits.Keys.Min() != 101)
                                             return;
                                         ledpc1.DisplayBrush = e.Status[101] != CircuitEnum.OpenCircuit ? (e.Status[101] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc2.DisplayBrush = e.Status[102] != CircuitEnum.OpenCircuit ? (e.Status[102] == CircuitEnum.NORMAL ? Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc3.DisplayBrush = e.Status[103] != CircuitEnum.OpenCircuit ? (e.Status[103] == CircuitEnum.NORMAL ? Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc4.DisplayBrush = e.Status[104] != CircuitEnum.OpenCircuit ? (e.Status[104] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc5.DisplayBrush = e.Status[105] != CircuitEnum.OpenCircuit ? (e.Status[105] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc6.DisplayBrush = e.Status[106] != CircuitEnum.OpenCircuit ? (e.Status[106] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc7.DisplayBrush = e.Status[107] != CircuitEnum.OpenCircuit ? (e.Status[107] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc8.DisplayBrush = e.Status[108] != CircuitEnum.OpenCircuit ? (e.Status[108] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc9.DisplayBrush = e.Status[109] != CircuitEnum.OpenCircuit ? (e.Status[109] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc10.DisplayBrush = e.Status[110] != CircuitEnum.OpenCircuit ? (e.Status[110] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc11.DisplayBrush = e.Status[111] != CircuitEnum.OpenCircuit ? (e.Status[111] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc12.DisplayBrush = e.Status[112] != CircuitEnum.OpenCircuit ? (e.Status[112] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc13.DisplayBrush = e.Status[113] != CircuitEnum.OpenCircuit ? (e.Status[113] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc14.DisplayBrush = e.Status[114] != CircuitEnum.OpenCircuit ? (e.Status[114] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc15.DisplayBrush = e.Status[115] != CircuitEnum.OpenCircuit ? (e.Status[115] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc16.DisplayBrush = e.Status[116] != CircuitEnum.OpenCircuit ? (e.Status[116] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc17.DisplayBrush = e.Status[117] != CircuitEnum.OpenCircuit ? (e.Status[117] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc18.DisplayBrush = e.Status[118] != CircuitEnum.OpenCircuit ? (e.Status[118] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc19.DisplayBrush = e.Status[119] != CircuitEnum.OpenCircuit ? (e.Status[119] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc20.DisplayBrush = e.Status[120] != CircuitEnum.OpenCircuit ? (e.Status[120] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc21.DisplayBrush = e.Status[121] != CircuitEnum.OpenCircuit ? (e.Status[121] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc22.DisplayBrush = e.Status[122] != CircuitEnum.OpenCircuit ? (e.Status[122] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc23.DisplayBrush = e.Status[123] != CircuitEnum.OpenCircuit ? (e.Status[123] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc24.DisplayBrush = e.Status[124] != CircuitEnum.OpenCircuit ? (e.Status[124] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc25.DisplayBrush = e.Status[125] != CircuitEnum.OpenCircuit ? (e.Status[125] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc26.DisplayBrush = e.Status[126] != CircuitEnum.OpenCircuit ? (e.Status[126] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc27.DisplayBrush = e.Status[127] != CircuitEnum.OpenCircuit ? (e.Status[127] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc28.DisplayBrush = e.Status[128] != CircuitEnum.OpenCircuit ? (e.Status[128] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc29.DisplayBrush = e.Status[129] != CircuitEnum.OpenCircuit ? (e.Status[129] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
                                         ledpc30.DisplayBrush = e.Status[130] != CircuitEnum.OpenCircuit ? (e.Status[130] == CircuitEnum.NORMAL ?  Brushes.LimeGreen : Brushes.Red) : Brushes.Gray;
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
            comTextBox.Text = RegSettings.Get(Program.PORT_NAME_KEY, Program.DEFAULT_PORT_NAME) as string;
            circuitIntervalTextBox.Text = Setting.Get(Setting.SENSOR_QUERY_INTERVAL, Setting.DEFAULT_SENSOR_QUERY_INTERVAL).ToString();
            deviceNumberTextBox.Text = RegSettings.Get(Program.DEVICE_NUMBER_KEY_CIRCUIT, "1").ToString();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegSettings.Save(Program.PORT_NAME_KEY, comTextBox.Text);
                RegSettings.Save(Program.DEVICE_NUMBER_KEY_CIRCUIT, deviceNumberTextBox.Text);
                Center.CurrentCenterID = (Guid)centersCombo.SelectedValue;
                Logger.WriteInfo("Settings saved. Restarting service...");
                CircuitManager.MarkForReInitialize();
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
                circuitServiceController.Refresh();
                statusTextblock.Text = Program.SERVICE_NAME + " is " + circuitServiceController.Status.ToString();
                if (circuitServiceController.Status != ServiceControllerStatus.Running)
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
                statusTextblock.Text = "Warning: Circuit service is not installed!";
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

        private void circuitSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new TMN.UI.Windows.CircuitsWindow().ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            CircuitManager.StopRequestingCircuitStatusPeriodically();
            base.OnClosed(e);
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(calibrasionTabItem))
            {
                ChangeServiceState(ServiceControllerStatus.Stopped);
                CircuitManager.StartRequestingCircuitStatusPeriodically(3000);
            }
            else
            {
                CircuitManager.StopRequestingCircuitStatusPeriodically();
            }
        }

        private bool ChangeServiceState(ServiceControllerStatus newState)
        {
            try
            {
                Logger.WriteInfo("Changing service state to {0}.", newState);
                circuitServiceController.Refresh();
                if (circuitServiceController.Status != newState)
                {
                    switch (newState)
                    {
                        case ServiceControllerStatus.Running:
                            circuitServiceController.Start();
                            break;
                        case ServiceControllerStatus.Stopped:
                            if (circuitServiceController.CanStop)
                            {
                                circuitServiceController.Stop();
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
                    circuitServiceController.WaitForStatus(newState);
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

        //private void CircuitLed_Click(CircuitLedButton sender)
        //{
        //    try
        //    {
        //        CircuitManager.ResetCalibration(sender.CircuitNumber);
        //        CalibrationWindow win = new CalibrationWindow();
        //        win.DisplayBrush = sender.CircuitNumber == 4 ? Brushes.LimeGreen : Brushes.Red;
        //        Thread.Sleep(100);
        //        var circuitStatus = CircuitManager.RequestCircuitStatus();
        //        win.Value = circuitStatus[sender.CircuitNumber];
        //        win.Owner = this;
        //        win.CircuitTitle = sender.Title;
        //        if (win.ShowDialog() == true)
        //        {
        //            CircuitManager.Calibrate(sender.CircuitNumber, win.Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex);
        //        MessageBox.ShowError(ex.Message);
        //    }
        //}
    }
}

