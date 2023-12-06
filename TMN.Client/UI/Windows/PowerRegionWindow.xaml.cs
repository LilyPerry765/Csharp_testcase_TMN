using System;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Enterprise;
using TMN.UserControls;
using Thread = System.Threading.Thread;
using System.Transactions;
using System.Collections.Generic;
using System.Windows.Threading;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for AlarmRegionWindow.xaml
    /// </summary>
    public partial class PowerRegionWindow : Window, ITabedWindow
    {
        private PowerCenterSpot movingItem;
        private Point startPos;
        private static Guid mapID = new Guid("3BA682EB-9152-4A8F-ACF3-0CC45898A20F");
        private Timer timer = new Timer();
        private SoundAlarmSeverities severityForAlarmSound = SoundAlarmSeverities.None;
        private DateTime voiceStartTime = DateTime.Now;
        private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);


        public bool IsOpen
        {
            get
            {
                try
                {
                    bool isopen = false;
                    Dispatcher.Invoke(new Action(() =>
                        {
                            isopen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Power Region Panel");
                        }));
                    return isopen;
                }
                catch(Exception exp)
                {
                    Logger.Write(exp, "Can't check power region window open or not.");
                    return false;
                }
            }
        }

        public PowerRegionWindow()
        {
            InitializeComponent();
            canvas.PreviewMouseMove += new MouseEventHandler(canvas_PreviewMouseMove);
            timer.Interval = Setting.Get(Setting.REGIONAL_ALARM_PANEL_INTERVAL, Setting.DEFAULT_REGIONAL_ALARM_PANEL_INTERVAL) * 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed); 
            
            MainWindow.Instance.TabChanged -= new SelectionChangedEventHandler(tabControl_SelectionChanged);
            MainWindow.Instance.TabChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);
            regionImage.Source = ImageSourceHelper.GetImageSource(TMN.Properties.Settings.Default.RegionBackground);
            muteAllSoundButton.IsMute = bool.Parse(TMN.TextSettings.Get("MUTE_POWER", "false"));
            PopulateCenters();
            //tabControl_SelectionChanged(null, null);
            Refresh(true);
        }

        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (MainWindow.Instance.tabControl.SelectedItem != null)
            //    IsOpen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Region Panel");
            //else
            //    IsOpen = false;
            //if (IsOpen)
            {
                Refresh(IsOpen);
            }
            //else
            //    AlarmPlayer.Stop("powerregion");
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                Refresh(IsOpen);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void Refresh(bool openStatus)
        {

            timer.Stop();
            if (openStatus)
            {
                Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {
                            Setting.IsDirty();
                            refreshStatusTextblock.Visibility = Visibility.Visible;

                            CheckConnectivity();
                            //System.Windows.Forms.Application.DoEvents();
                            CheckForAlarms();
                            ManageSound();
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex);
                        }
                        finally
                        {
                            refreshStatusTextblock.Visibility = Visibility.Hidden;
                        }
                    }));
            }
            else
                AlarmPlayer.Stop("powerregion");
            timer.Start();
        }



        private void ManageSound()
        {
            if (muteAllSoundButton.IsMute || severityForAlarmSound == SoundAlarmSeverities.None)
            {
                AlarmPlayer.Stop("powerregion");
                voiceStartTime = DateTime.Now;
            }
            else
            {
                if (voiceInterval != 0 && DateTime.Now.Subtract(voiceStartTime).TotalSeconds > voiceInterval)
                    AlarmPlayer.Stop("powerregion");
                else
                {
                    if (AlarmPlayer.isPlaying == false)
                        voiceStartTime = DateTime.Now;

                    AlarmPlayer.Play(severityForAlarmSound, "powerregion");
                }
            }
        }

        private void CheckConnectivity()
        {
            //  Logger.WriteDebug("Region: refreshing connectivity ...");

            using (var db = new TMNModelDataContext())
            {
                if (db.ServiceStates.ToArray().Any(s => s.IsConnected == false && !bool.Parse(TextSettings.Get("MUTE_POW_" + s.Center.PointCode, "False"))))
                {
                    severityForAlarmSound = SoundAlarmSeverities.Information; //use information for play alarms of disconnection
                    connectLed.DisplayMode = DisplayModes.On;
                    connectLed.InnerBackground = Brushes.Red;
                }
                else
                {
                    severityForAlarmSound = SoundAlarmSeverities.None;
                    connectLed.DisplayMode = DisplayModes.On;
                    connectLed.InnerBackground = Brushes.LimeGreen;
                }
            }
        }

        private void CheckForAlarms()
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                //  Logger.Write(LogType.Debug, "Region: loading sensors");
                Tuple<Sensor, double?>[] sensors = null;
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
                {
                    // Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }
                ))
                {
                    // Since a sonsor may not have any sensor data, the second parameter of Tuble which is LastValue of sensor can be null. 
                    // It is necessary to cast the selectd value of sensor data to Nullable, because the sql server will return null for FirstOrDefault if there is no sensor data for a sensor but Linq will try to cast it to double if the type of selected Value is double instead of double? and and exception will occure.
                    sensors = db.Sensors.Select(s => Tuple.Create(s, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();
                }

                //  Logger.Write(LogType.Debug, "Region: loading alarms");


                //Logger.Write(LogType.Debug, "Region: loading servicestates");
                ServiceState[] serviceStates;
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
                {
                    //Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }
                ))
                {
                    serviceStates = db.ServiceStates.ToArray();
                }


                bool hasPowerProblems = false;
                AlarmSeverities alarmSeverity = AlarmSeverities.Information;
                foreach (var item in canvas.Children)
                {
                    PowerCenterSpot cs = item as PowerCenterSpot;
                    if (cs != null)
                    {
                        cs.CheckConnectivity(serviceStates);
                        bool cshasPowerProblems = cs.CheckPowerAlarm(sensors);
                        
                        hasPowerProblems |= cshasPowerProblems; 
                    }
                }
                if (alarmSeverity == AlarmSeverities.Information)   // use '.Information' to use Math.Min for check severity in abow lines,
                    alarmSeverity = AlarmSeverities.None;           // therefore it's really value is '.None'


                //if (alarmSeverity != AlarmSeverities.Information) //check switchs has alarms (major | minor | critical)
                if (hasPowerProblems)
                    severityForAlarmSound = SoundAlarmSeverities.Power;
            }
        }

        private void PopulateCenters()
        {
            var db = new TMNModelDataContext();
            var region2 = new Guid("CA060F4C-95F4-42D6-A0D0-B99DF16657FA");
            canvas.Children.Clear();
            foreach (var center in db.Centers) //.Where(c => c.RegionID == region2))
            {
                PowerCenterSpot centerSpot = new PowerCenterSpot(center);
                centerSpot.PreviewMouseDown += new MouseButtonEventHandler(centerSpot_PreviewMouseDown);
                centerSpot.PreviewMouseUp += new MouseButtonEventHandler(centerSpot_PreviewMouseUp);
                centerSpot.MuteCenterClick += new EventHandler(centerSpot_MuteCenterClick);
                
                centerSpot.Click += new RoutedEventHandler(centerSpot_Click);
                canvas.Children.Add(centerSpot);
                CenterInMap centerInMap = db.CenterInMaps.SingleOrDefault(cm => cm.MapID == mapID && cm.CenterID == center.ID);
                if (centerInMap != null)
                {
                    Canvas.SetLeft(centerSpot, (double)centerInMap.X);
                    Canvas.SetTop(centerSpot, (double)centerInMap.Y);
                }
                //System.Windows.Forms.Application.DoEvents();
            }
        }

        void centerSpot_MuteCenterClick(object sender, EventArgs e)
        {
            CheckConnectivity();
            CheckForAlarms();
            ManageSound();
        }

        void centerSpot_Click(object sender, RoutedEventArgs e)
        {
            //TabClosed();
            if (movingItem == null || movingItem.IsMoving == false)
            {
                Center.Selected = (sender as PowerCenterSpot).Center;
                new AlarmPowerWindow().ShowAsSingleTab(MainWindow.Instance.tabControl, "Alarm Power");
            }
        }

        void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !lockMenuItem.IsChecked && movingItem != null)
            {
                Point currentPos = Mouse.GetPosition(canvas);
                Vector movement = currentPos - startPos;
                
                if (movement.Length > 2)
                {
                    movingItem.IsMoving = true;
                    Canvas.SetLeft(movingItem, double.IsNaN(Canvas.GetLeft(movingItem)) ? 0 : Canvas.GetLeft(movingItem) + movement.X);
                    Canvas.SetTop(movingItem, double.IsNaN(Canvas.GetTop(movingItem)) ? 0 : Canvas.GetTop(movingItem) + movement.Y);
                    startPos = e.GetPosition(canvas);
                }
            }
        }

        void centerSpot_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && !lockMenuItem.IsChecked)
            {
                if (movingItem.IsMoving)
                {
                    movingItem.IsMoving = false;
                    SavePosition(movingItem.Center, new Point(Canvas.GetLeft(movingItem), Canvas.GetTop(movingItem)));
                }
                movingItem = null;
            }
        }

        private static void SavePosition(Center center, Point position)
        {
            //new System.Threading.Thread(() =>
            //{
            try
            {
                var db = DB.Instance;
                var centerInMap = db.CenterInMaps.SingleOrDefault(cm => cm.Center == center && cm.MapID == mapID);
                if (centerInMap == null)
                {
                    centerInMap = new CenterInMap()
                    {
                        CenterID = center.ID,
                        MapID = mapID,
                    };
                    db.CenterInMaps.InsertOnSubmit(centerInMap);
                }
                centerInMap.X = (int)position.X;
                centerInMap.Y = (int)position.Y;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.Show("امکان انجام اين عمليات وجود ندارد."); ;
            }
            //}).Start();
        }

        void centerSpot_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.CENTERS_EDIT))
            //{
                if (e.ChangedButton == MouseButton.Left && !lockMenuItem.IsChecked)
               {
                    startPos = e.GetPosition(canvas);
                    movingItem = sender as PowerCenterSpot;
                }
            //}
            //else
            //{
            //    MessageBox.Show(MessageTypes.AccessDenied);
            //} 
        }

        private void connectLed_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            voiceStartTime = DateTime.MinValue;
            ManageSound();
            new ServiceStateWindow().Show();
        }

        private void refreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh(IsOpen);
        }

        private void reArrangeNewCenters_Click(object sender, RoutedEventArgs e)
        {
            PopulateCenters();
        }


        public void TabClosed()
        {
            //voiceIncremental = 0;
        }

        public void TabOpened()
        {
            //isOpen = true;
            //Timer.Start() is called in Refresh()
            //Refresh();
        }

        private void muteAllSoundButton_IsMuteChanged(object sender, EventArgs e)
        {
            TMN.TextSettings.Set("MUTE_POWER", muteAllSoundButton.IsMute.ToString());
            ManageSound();
        }

        private void muteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in canvas.Children)
            {
                PowerCenterSpot cs = item as PowerCenterSpot;
                if (cs != null)
                {
                    cs.IsMute = muteMenuItem.IsChecked ;
                }
            }
            CheckConnectivity();
            CheckForAlarms();
            ManageSound();
        }

    }
}
