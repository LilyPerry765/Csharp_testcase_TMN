using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Enterprise;
using Enterprise.Wpf;
using TMN.UserControls;
using System.Transactions;
using System.Windows.Threading;

namespace TMN.UI.Windows
{
    public partial class AlarmCircuitWindow : Window, ITabedWindow
    {
        Timer monitoringTimer = new Timer();
        //private bool IsOpen = false;

        //shahab 900815 //check sound alert must play continous or in period
        private DateTime voiceStartTime = DateTime.Now;
        private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);
        //string connectionString = null;
        public bool IsOpen
        {
            get
            {
                bool isopen = false;
                Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {
                            isopen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Circuit");
                        }
                        catch
                        {
                        }
                    }));
                return isopen;
            }
        }

        //private int sourceIndex = 0;
        //private List<string> dataSources;

        public AlarmCircuitWindow()
        {
            Logger.WriteDebug("Alarmpower ctor. Initing components...");
            InitializeComponent();
            Logger.WriteDebug("Alarmpower ctor. Initialized.");
            monitoringTimer.Interval = 1000 * Setting.Get(Setting.CENTER_ALARM_PANEL_INTERVAL, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL);
            //monitoringTimer.Interval = new TimeSpan(0, 0, Setting.Get(Setting.CENTER_ALARM_PANEL_INTERVAL, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL));
            monitoringTimer.Elapsed += new ElapsedEventHandler(monitoringTimer_Elapsed); //+= new EventHandler(monitoringTimer_Tick);

            Center.SelectedChanged -= new Action(Center_SelectedChanged);
            Center.SelectedChanged += new Action(Center_SelectedChanged);
            MainWindow.Instance.TabChanged -= new SelectionChangedEventHandler(tabControl_SelectionChanged);
            MainWindow.Instance.TabChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);
            //dataSources = Properties.Settings.Default.RegionServers.Split(';').ToList<string>();

            CustomizeForSelectedCenter();
            RefreshAlarms(true);
        }

        void monitoringTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Setting.IsDirty();
                RefreshAlarms(IsOpen);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomizeForSelectedCenter();
            RefreshAlarms(IsOpen);
        }

        void Center_SelectedChanged()
        {
            CustomizeForSelectedCenter();
            RefreshAlarms(IsOpen);
        }

        private void CustomizeForSelectedCenter()
        {
            string pointCode = Center.Selected.PointCode;
            //HasNewAlarm =  false;
            masterSound.IsMute = bool.Parse(TextSettings.Get("MUTE_CUT_" + pointCode, "false"));


            //CenterNameLabel.Content = string.Format("آلارم کابل مرکز {0}", Center.Selected.DisplayName);
            CenterNameLabel.Content = string.Format("آلارم کابل مرکز {0}", Center.Selected.Name);

            LoadSensors();
        }

        void led_DisplayModeChanged(object sender, EventArgs e)
        {
            if (IsOpen)
                ManageSoundAlert();
        }

        void led_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                BlinkingLed led = sender as BlinkingLed;
                Sensor sensor = led.DataContext as Sensor;
                //new SensorChartWindow(Center.Selected, int.Parse((sender as BlinkingLed).Name.Substring(1))).Show();
                new CircuitShowPictureWindow(Center.Selected, sensor.Title).ShowDialog();
            }
        }


        public void TabOpened()
        {
        }

        public void TabClosed()
        {
            //Center.SelectedChanged -= new Action(Center_SelectedChanged);
        }

        private void RefreshAlarms(bool openStatus)
        {
            monitoringTimer.Stop();
            if (openStatus)
            {
                try
                {
                    Setting.IsDirty();
                    RefreshDateTime();
                    RefreshAlarmServiceStateLED();
                    RefreshSensorData();

                    ManageSoundAlert();
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
                finally
                {
                    //  Logger.WriteDebug("Finished refreshing alarm panel.");
                }
            }
            else
                AlarmPlayer.Stop("circuit");
            monitoringTimer.Start();
        }

        private List<ServiceState> faultServices = new List<ServiceState>();
        
        private bool isServicefault = false;
        private void RefreshAlarmServiceStateLED()
        {
            int sensorTimeout = Setting.Get(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT);
            using (var db = new TMNModelDataContext())
            {
                faultServices = db.ServiceStates.Where(s => s.CenterID == Center.Selected.ID && s.ServiceType == (int)ServiceTypes.CircuitService && s.InActiveSeconds > sensorTimeout ).ToList();
                if (faultServices.Count > 0)
                    isServicefault = true;
                else
                    isServicefault = false;
            }
            Dispatcher.Invoke(new Action(() =>
                {
                    if (isServicefault == false)
                    {

                        alarmServiceLed.InnerBackground = Brushes.LimeGreen;
                        alarmServiceLed.DisplayMode = DisplayModes.On;
                        alarmServiceLed.ToolTip = "Circuit Alarm service is OK";
                    }
                    else
                    {
                        alarmServiceLed.InnerBackground = Brushes.Red;
                        alarmServiceLed.DisplayMode = DisplayModes.Blinking;
                        alarmServiceLed.ToolTip = "Problem detected in circuit alarm service process. View LogViewer for more info.";
                    }
                }));
        }

        private Dictionary<Guid, BlinkingLed> leds = new Dictionary<Guid, BlinkingLed>();
        private bool isLocalSensors = true;

        public static List<Tuple<string, Sensor>> AllSensors = new List<Tuple<string, Sensor>>();
        private void LoadSensors()
        {
            try
            {
                leds.Clear();
                //Room room = null;
                List<Sensor> modules;
                //Logger.WriteDebug("Center: Loading sensors...");
                if (AllSensors.Any(s => s.Item1 == Center.Selected.PointCode))
                {
                    modules = AllSensors.Where(t => t.Item1 == Center.Selected.PointCode).Select(t => t.Item2).ToList();
                }
                else
                {
                    using (var db = new TMNModelDataContext())
                    {
                        modules = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID && s.ModulNumber > 100).OrderBy(s => s.Title).ToList();
                        
                        //if (modules.Count == 0)
                        //{
                        //    modules = db.Sensors.Where(s => s.Room.Center.Name == Center.Selected.Name && s.ModulNumber > 100).OrderBy(s => s.Title).ToList();
                        //    isLocalSensors = false;
                        //}
                        AllSensors.AddRange(modules.Select(s => Tuple.Create(Center.Selected.PointCode, s)));
                    }
                }


                Dispatcher.Invoke(new Action(() =>
                {
                    AlarmPanelGrid.Children.Clear();
                    AlarmPanelGrid.RowDefinitions.Clear();
                    AlarmPanelGrid.ColumnDefinitions.Clear();

                    //circuitsensors
                    int columns = 10;
                    int rows = modules.Count / columns;
                    GC.Collect();
                    for (int j = 0; j < columns; j++)
                    {
                        AlarmPanelGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    for (int i = 0; i <= rows; i++)
                    {
                        RowDefinition rd = new RowDefinition();
                        AlarmPanelGrid.RowDefinitions.Add(rd);

                    }
                    for (int i = 0; i < modules.Count; i++)
                    {
                        int row = (i) / columns;
                        int col = i % columns;
                        Sensor sensor = modules[i];
                        BlinkingLed led = new BlinkingLed();
                        led.DisplayMode = sensor.SensorType == SensorTypes.Circuit ? DisplayModes.On : DisplayModes.Off;
                        led.InnerBackground = Brushes.Gray;
                        led.TitleMaxLength = 50;
                        string temp = "" + sensor.Title;
                        int space = temp.IndexOf(' ');
                        if (space != -1)
                            temp = temp.Substring(0, space);
                        led.Title = temp; // sensor.Title.Length > 20 ? sensor.Title.Replace('_', '\n') : sensor.Title;
                        led.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        led.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        led.ToolTip = string.Format("{0}", sensor.Title);
                        led.Name = string.Format("M{0}", sensor.ModulNumber);
                        led.MouseUp += new MouseButtonEventHandler(led_MouseUp);
                        led.DisplayModeChanged += new EventHandler(led_DisplayModeChanged);
                        led.DataContext = sensor;
                        led.Height = 50;
                        led.Width = 60;
                        led.FontSize = 25;
                        led.Margin = new Thickness(5, 5, 5, 5);
                        AlarmPanelGrid.Children.Add(led);
                        Grid.SetRow(led, row);
                        Grid.SetColumn(led, col);
                        leds.Add(sensor.ID, led);
                    }
                }));
                //Logger.WriteDebug("Center: Finished loading sensors");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }


        private void RefreshSensorData()
        {
            try
            {
                if (leds.Count == 0)
                    LoadSensors();

                List<SensorData> sensorsValue;
                using (var db = new TMNModelDataContext())
                {
                    if (isLocalSensors)
                        sensorsValue = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID && s.ModulNumber > 100)
							.Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault()).Where(sd => sd.Value != (double)CircuitEnum.NORMAL).ToList();
                    else
                        sensorsValue = db.Sensors.Where(s => s.Room.Center.Name == Center.Selected.Name && s.ModulNumber > 100)
							.Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault()).Where(sd => sd.Value != (double)CircuitEnum.NORMAL).ToList();
                }

                Dispatcher.Invoke(new Action(() =>
                    {
                        //if (isServiceActive)
                        {


                            List<char> faultServiceIndex = new List<char>();
                            if(isServicefault)
                                faultServiceIndex = faultServices.Where(s=> s.Description != null).Select(s=> s.Description.Last()).ToList();
                            foreach (Guid sensorID in leds.Keys)
                            {
                                BlinkingLed led = leds[sensorID];
                                if (led.DisplayMode != DisplayModes.Off)
                                {

                                    SensorData item = sensorsValue.Where(s => s.SensorID == sensorID).FirstOrDefault();
                                    string moduleNumber = led.Name.Substring(1);
                                    int MN = int.Parse(moduleNumber);

                                    //if (item != null)
                                    {
                                        if (item == null && ( 
                                            (faultServiceIndex.Contains('1') && MN <= 130) ||
                                            (faultServiceIndex.Contains('2') && MN <= 160 && MN > 130) 
                                            )
                                            )
                                        {
                                            led.DisplayMode = DisplayModes.On;
                                            led.InnerBackground = Brushes.Gray;
                                        }
										else if (item != null && item.Value == (double)CircuitEnum.ShortCircuit)
										{
											led.InnerBackground = Brushes.Yellow;
											if (NewAlarmShort.Contains(moduleNumber) && !OldAlarmShort.Contains(moduleNumber))
												led.DisplayMode = DisplayModes.Blinking;
											else
												led.DisplayMode = DisplayModes.On;
										}
                                        else if (item != null && item.Value == (double)CircuitEnum.OpenCircuit)
                                        {
                                            led.InnerBackground = Brushes.Red;
                                            if (NewAlarmOpen.Contains(moduleNumber) && !OldAlarmOpen.Contains(moduleNumber))
                                                led.DisplayMode = DisplayModes.Blinking;
                                            else
                                                led.DisplayMode = DisplayModes.On;
                                        }
                                        else
                                        {
                                            led.InnerBackground = Brushes.LimeGreen;
                                            led.DisplayMode = DisplayModes.On;
                                        }
                                    }
                                }
                            }



                            //Logger.WriteDebug("sensor ui finished.");
                        }
                        //else
                        //{
                        //    foreach (BlinkingLed led in leds.Values)
                        //    {
                        //        led.InnerBackground = Brushes.Gray;
                        //    }
                        //}
                    }));
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }


        private void RefreshDateTime()
        {
            Dispatcher.Invoke(new Action(() =>
                {
                    PersianDateTimeLabel.Content = PersianDateTime.Now.ToString("dddd d MMMM yyyy ساعت HH:mm");
                    DateTimeLabel.Content = DateTime.Now.ToString("dddd, MMMM d, yyyy");
                }));
        }


        private void ManageSoundAlert()
        {
            try
            {

                if (NewAlarmOpen != OldAlarmOpen || NewAlarmShort != OldAlarmShort || faultServices.Count > 0)
                {
                    if (AlarmPlayer.isPlaying == false)
                        voiceStartTime = DateTime.Now;

                    if (masterSound.IsMute)
                    {
                        AlarmPlayer.Stop("circuit");
                    }
                    else if (voiceInterval != 0 && DateTime.Now.Subtract(voiceStartTime).TotalSeconds > voiceInterval) //shahab 900815 //determine criteria for duration of alarm playing 
                    {
                        AlarmPlayer.Stop("circuit");
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() =>
                            {
                                if (faultServices.Count > 0)
                                    AlarmPlayer.Play(SoundAlarmSeverities.Information, "circuit");
                                else if ( leds.Any(l => l.Value.InnerBackground == Brushes.Red && l.Value.DisplayMode == DisplayModes.Blinking))
                                    AlarmPlayer.Play(SoundAlarmSeverities.Critical, "circuit");
                                else if (leds.Any(l => l.Value.InnerBackground == Brushes.Yellow && l.Value.DisplayMode == DisplayModes.Blinking))
                                    AlarmPlayer.Play(SoundAlarmSeverities.Minor, "circuit");
                                else
                                    AlarmPlayer.Stop("circuit");
                            }));
                    }
                }
                else
                {
                    AlarmPlayer.Stop("circuit");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void Sound_IsMuteChanged(object sender, EventArgs e)
        {
            ManageSoundAlert();
            string pointCode = Center.Selected.PointCode;

            TextSettings.Set("MUTE_CUT_" + pointCode, masterSound.IsMute.ToString());

        }

        private void TitleTextBlocks_MouseUp(object sender, MouseButtonEventArgs e)
        {
			//var columnNo = Grid.GetColumn(sender as UIElement);
			//InputDialog dlg = new InputDialog()
			//{
			//    Value = (sender as TextBlock).Text
			//};
			//if (dlg.ShowDialog() == true)
			//{
			//    (sender as TextBlock).Text = dlg.Value;
			//    RegSettings.Save((sender as TextBlock).Name, dlg.Value);
			//}
        }

        private void sensorReportButton_Click(object sender, RoutedEventArgs e)
        {
            new SensorStatusWindow().Show();
        }

		// show service status
        private void alarmServiceLed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //HasNewAlarm = false;
                ManageSoundAlert();
                new ServiceStateWindow(Center.Selected).ShowDialog();
            }
        }



        public string NewAlarmOpen
        {
            get
            {
              
                    return Setting.Get(Setting.NEW_CIRCUIT_OPEN_ALARM_INSERTED + Center.Selected.PointCode,  "");
            }
        }

        public string NewAlarmShort
        {
            get
            {
                    return Setting.Get(Setting.NEW_CIRCUIT_SHORT_ALARM_INSERTED + Center.Selected.PointCode, "");
            }
        }


        public string OldAlarmOpen
        {
            get
            {

                return Setting.Get(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.Selected.PointCode, "");
            }
            set
            {
                Setting.Set(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.Selected.PointCode, value.ToString());
            }
        }

        public string OldAlarmShort
        {
            get
            {
                return Setting.Get(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.Selected.PointCode, "");
            }
            set
            {
                Setting.Set(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.Selected.PointCode, value.ToString());
            }
        }


		// accept new circuit alarms
        private void newAlarmLed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            OldAlarmOpen = NewAlarmOpen;
            OldAlarmShort =  NewAlarmShort;

            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                string[] openCircuits = NewAlarmOpen.Split('_');
                foreach (string item in openCircuits)
                {
                    UserLog.Log(db, ActionType.AcceptCircuitAlarm, string.Format("قطعی کابل شماره {0} مرکز {1} تایید شد", Center.Selected.Name, item),"");
                }
                
            }
            RefreshAlarms(true);
        }

		// refresh
        private void refreshLed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            AllSensors.Clear();

            var current = AllSensors.Where(s => s.Item1 == Center.Selected.PointCode).ToList();
            foreach (var item in current)
                AllSensors.Remove(item);

            CustomizeForSelectedCenter();
            RefreshAlarms(true);
        }
    }
}
