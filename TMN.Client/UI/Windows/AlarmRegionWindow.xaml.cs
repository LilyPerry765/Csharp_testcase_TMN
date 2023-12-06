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
using System.ServiceModel;
//using TMN.ServiceModel;
//using TMN.Service;

namespace TMN.UI.Windows
{

    public partial class AlarmRegionWindow : Window, ITabedWindow
    {
        private ToolBar toolbar;
        private CenterSpot movingItem;
        private List<CenterSpot> centerSpotList = new List<CenterSpot>();
        private Point startPos;
        private static Guid mapID = new Guid("3BA682EB-9152-4A8F-ACF3-0CC45898A20F");
        private Timer timer = new Timer();
        private SoundAlarmSeverities severityForAlarmSound = SoundAlarmSeverities.None;
        private DateTime voiceStartTime = DateTime.Now;
        private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);
        private List<SoundAlarmSeverities> unhandledAlarms = new List<SoundAlarmSeverities>();
        private double zoomFactor = 1;



        private ToolBar CreateToolbar()
        {
            const int imgSize = 25;
            ToolBar tb = new ToolBar();

            Button ziButton = new Button()
            {
                ToolTip = "بزرگ نمايی",
                Content = new Image()
                {
                    Width = imgSize,
                    Height = imgSize,
                    Source = ImageSourceHelper.GetImageSource("zoomin.png")
                },
                ClickMode = ClickMode.Press
            };
            ziButton.Click += new RoutedEventHandler(btnZoomIn_Click);
            Button zoButton = new Button()
            {
                ToolTip = "کوچک نمايی",
                Content = new Image()
                {
                    Width = imgSize,
                    Height = imgSize,
                    Source = ImageSourceHelper.GetImageSource("zoomout.png")
                },
                ClickMode = ClickMode.Press
            };
            zoButton.Click += new RoutedEventHandler(btnZoomOut_Click);

            tb.Items.Add(ziButton);
            tb.Items.Add(zoButton);
            return tb;
        }

        void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomCenterSpot(ZoomType.ZoomOut, 1.1);
        }

        void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomCenterSpot(ZoomType.ZoomIN, 1.1);
        }


        private void ZoomCenterSpot(ZoomType type, double d)
        {
            switch (type)
            {
                case ZoomType.ZoomOut:
                    double zoomOut = zoomFactor / d;
                    for (int i = 0; i < centerSpotList.Count; i++)
                    {
                        centerSpotList[i].Zoom(zoomOut);
                    }
                    zoomFactor = zoomOut;
                    break;

                case ZoomType.ZoomIN:
                    double zoomIn = zoomFactor * d;
                    for (int i = 0; i < centerSpotList.Count; i++)
                    {
                        centerSpotList[i].Zoom(zoomIn);
                    }
                    zoomFactor = zoomIn;
                    break;
            }
        }

        public bool IsOpen
        {
            get
            {
                try
                {
                    bool isopen = false;
                    Dispatcher.Invoke(new Action(() =>
                        {
                            isopen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Region Panel");
                        }));
                    return isopen;
                }
                catch
                {
                    return false;
                }
            }
        }


        public AlarmRegionWindow()
        {
            InitializeComponent();

            //object image = RegSettings.Get("AlarmRegionBachground");

            //if (image != null && image.ToString() != string.Empty)
            //{
            //    GetBackgroundImage(image.ToString());
            //}
            //else
            //{
            //    AssemblyBuildHelper builder = new AssemblyBuildHelper();
            //    regionImage.Source = builder.GetImage(@".\Images.dll");
            //    if (regionImage.Source == null)
            //        regionImage.Source = ImageSourceHelper.GetImageSource(TMN.Properties.Settings.Default.RegionBackground);
            //}

            canvas.PreviewMouseMove += new MouseEventHandler(canvas_PreviewMouseMove);
            timer.Interval = Setting.Get(Setting.REGIONAL_ALARM_PANEL_INTERVAL, Setting.DEFAULT_REGIONAL_ALARM_PANEL_INTERVAL) * 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            //MainWindow.Instance.TabChanged -= new SelectionChangedEventHandler(tabControl_SelectionChanged);
            //MainWindow.Instance.TabChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);
           //Center.SelectedChanged += new Action(Center_SelectedChanged);

            muteAllSoundButton.IsMute = bool.Parse(TMN.TextSettings.Get("MUTE_REGION", "false"));

            PopulateCenters();

            //tabControl_SelectionChanged(null, null);
            //AlarmPlayer.StopInActiveApplication("region");

            Refresh(true);

            toolbar = CreateToolbar();
        }

        private void GetBackgroundImage(string vale)
        {
            switch (vale)
            {
                case "2":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region2);
                    break;
                case "3":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region3);
                    break;
                case "4":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region4);
                    break;
                case "5":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region5);
                    break;
                case "6":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region6);
                    break;
                case "7":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region7);
                    break;
                case "8":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Region8);
                    break;
                case "15":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Home15);
                    break;
                case "18":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Home18);
                    break;
                case "20":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Home20);
                    break;
                case "24":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Home24);
                    break;
                case "28":
                    regionImage.Source = ImageSourceHelper.GetImageSourceFromEnum(Regions.Home28);
                    break;
                default:

                    //AssemblyBuildHelper builder = new AssemblyBuildHelper();
                    //regionImage.Source = builder.GetImage(@".\Images.dll");
                

                    //if (regionImage.Source == null)
                        //regionImage.Source = ImageSourceHelper.GetImageSource(TMN.Properties.Settings.Default.RegionBackground);

                    break;
            }
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
            //    AlarmPlayer.Stop("region");
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
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

        private void ManageSound()
        {
            double playedSeconds = DateTime.Now.Subtract(voiceStartTime).TotalSeconds;
            bool muteAll = false;
            Dispatcher.Invoke(new Action(() =>
                {
                    muteAll = muteAllSoundButton.IsMute;
                }));
            if (muteAll || severityForAlarmSound == SoundAlarmSeverities.None)
            {
                AlarmPlayer.Stop("region");
                voiceStartTime = DateTime.Now;
            }
            else
            {
                if (playedSeconds > voiceInterval * 2)
                {
                    unhandledAlarms.Add(severityForAlarmSound);
                    severityForAlarmSound = SoundAlarmSeverities.None;
                    AlarmPlayer.Stop("region");
                }
                else if (voiceInterval != 0 && playedSeconds > voiceInterval)
                    AlarmPlayer.Stop("region");
                else
                {
                    //if (AlarmPlayer.isPlaying == false)
                    //    voiceStartTime = DateTime.Now;
                    //unhandledAlarms.Remove(severityForAlarmSound);
                    AlarmPlayer.Play(severityForAlarmSound, "region");
                }
            }
        }



        private void Refresh(bool openStatus)
        {
			
				

				timer.Stop();



				if (openStatus)
				{
					try
					{
						//AlarmPlayer.StopInActiveApplication("region");

						Setting.IsDirty();
						CheckConnectivity();
						CheckForAlarms();
						ManageSound();
					}
					catch (Exception ex)
					{
						Logger.Write(ex);
					}
					finally
					{
						//refreshStatusTextblock.Visibility = Visibility.Hidden;
					}
				}
				else
					AlarmPlayer.Stop("region");

				timer.Start();
			
        }

        private List<ServiceState> availableServices = new List<ServiceState>();
		private void CheckForAlarms()
        {








			using (TMNModelDataContext db = new TMNModelDataContext())
			{
				//refreshStatusTextblock.Visibility = Visibility.Visible;
				Tuple<Sensor, Guid, double?>[] sensors = null;
				using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
				{
					// Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
					IsolationLevel = IsolationLevel.ReadUncommitted
				}
				))
				{
					// Since a sonsor may not have any sensor data, the second parameter of Tuble which is LastValue of sensor can be null. 
					// It is necessary to cast the selectd value of sensor data to Nullable, because the sql server will return null for FirstOrDefault if there is no sensor data for a sensor but Linq will try to cast it to double if the type of selected Value is double instead of double? and and exception will occure.
					sensors = db.Sensors.Where(s => s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature || s.TypeID == (byte)SensorTypes.POWER)
						.Select(s => Tuple.Create(s, s.Room.CenterID.Value, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();
				}

				//Tuple<Guid?, double?>[] circuits = null;
				//using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
				//{
				//    // Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
				//    IsolationLevel = IsolationLevel.ReadUncommitted
				//}
				//))
				//{
				//    // Since a sonsor may not have any sensor data, the second parameter of Tuble which is LastValue of sensor can be null. 
				//    // It is necessary to cast the selectd value of sensor data to Nullable, because the sql server will return null for FirstOrDefault if there is no sensor data for a sensor but Linq will try to cast it to double if the type of selected Value is double instead of double? and and exception will occure.
				//    circuits = db.Sensors.Where(s => s.TypeID == (byte)SensorTypes.Circuit).Select(s => new { s.Room.CenterID, Value = s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault() }).GroupBy(g => g.CenterID).Select(g => Tuple.Create(g.Key, g.Max(sd => sd.Value))).ToArray();
				//}

				List<Tuple<Guid, AlarmSeverities>> alarms = new List<Tuple<Guid, AlarmSeverities>>();
				//LogAlarm powerAlarms = db.LogAlarms.Where(a => a.IsRead == false && a.Data.ToUpper().Contains(" POWER "));
				using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
				{
					// Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
					IsolationLevel = IsolationLevel.ReadUncommitted
				}
				))
				{
					alarms = db.LogAlarms.Where(a => a.IsRead == false && a.Severity <= 4).GroupBy(a => a.CenterID).Select(g => Tuple.Create(g.Key.Value, g.Min(a => (AlarmSeverities)a.Severity.Value))).ToList();
				}

				List<Tuple<Guid, byte>> cir = new List<Tuple<Guid, byte>>();
				
				
				//LogAlarm powerAlarms = db.LogAlarms.Where(a => a.IsRead == false && a.Data.ToUpper().Contains(" POWER "));
				using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
				{
					// Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
					IsolationLevel = IsolationLevel.ReadUncommitted
				}
				))
				{
					cir = db.LogAlarms.Where(a => a.IsRead == false && (a.Severity == 116 || a.Severity == 117)).GroupBy(a => a.CenterID)
						.Select(g => Tuple.Create(g.Key.Value, g.Min(a => a.Severity.Value))).ToList();

				}



				if (TextSettings.Get("recheckConnectivity", "false") == "true" || availableServices.Count == 0)
				{
					using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
					{
						// Transaction Isolation level must be set to uncommited because we don't need a lock on data record here and locking record results in deadlock as sensor services are writing to sensordata records.
						IsolationLevel = IsolationLevel.ReadUncommitted
					}
					))
					{
						availableServices = db.ServiceStates.ToList();
						TextSettings.Set("recheckConnectivity", "true");
					}
				}

				bool hasSensorProblems = false;
				bool hasPowerProblems = false;
				bool hasSwitchPowerProblems = false;
				bool hasCircuitProblems = false;
				//string[] newAlarmCenters = Setting.Get(Setting.NEW_ALARM_INSERTED, "").Split('_');


				AlarmSeverities alarmSeverity = AlarmSeverities.Information;
				Dispatcher.Invoke(new Action(() =>
					{
						foreach (var item in canvas.Children)
						{
							CenterSpot cs = item as CenterSpot;
							if (cs != null)
							{

								cs.CheckConnectivity(availableServices, inactiveServices);

								cs.CheckSwitchAlarm(alarms);

								bool cshasSensorProblems = cs.CheckSensorAlarm(sensors);
								//bool cshasPowerProblems = cs.CheckPowerAlarm(sensors);
								//bool cshasCircuitProblems = cs.CheckCircuitAlarm(cir);


								if (!cs.IsMute)
								{
									hasSensorProblems |= !cs.IsMuteSensor && cshasSensorProblems;
									//hasPowerProblems |= !cs.IsMutePower && cshasPowerProblems;
									//hasSwitchPowerProblems |= cs.HasSwitchPowerProblem;
									//hasCircuitProblems |= !cs.IsMuteCircuit && cshasCircuitProblems;

									if (cs.HasNewAlarm != AlarmSeverities.None)
									{
										if (cs.Severity == AlarmSeverities.Critical && !cs.IsMuteCritical)
											alarmSeverity = (AlarmSeverities)Math.Min((byte)alarmSeverity, (byte)cs.Severity);
										else if (cs.Severity == AlarmSeverities.Major && !cs.IsMuteMajor)
											alarmSeverity = (AlarmSeverities)Math.Min((byte)alarmSeverity, (byte)cs.Severity);
										else if (cs.Severity == AlarmSeverities.Minor && !cs.IsMuteMinor)
											alarmSeverity = (AlarmSeverities)Math.Min((byte)alarmSeverity, (byte)cs.Severity);
									}
								}
							}
						}
					}));
				if (alarmSeverity == AlarmSeverities.Information)   // use '.Information' to use Math.Min for check severity in abow lines,
					alarmSeverity = AlarmSeverities.None;           // therefore it's really value is '.None'

				//if (alarmSeverity != AlarmSeverities.Information) //check switchs has alarms (major | minor | critical)
				if (severityForAlarmSound != SoundAlarmSeverities.Information) //check don't had a disconnection alarm
					setSoundAlarmSeverity(hasSensorProblems, hasPowerProblems, hasSwitchPowerProblems, hasCircuitProblems, alarmSeverity);
				//   Logger.Write(LogType.Debug, "Region: refresh finished");
				//refreshStatusTextblock.Visibility = Visibility.Hidden;
			}

		
		}


        private List<ServiceState> inactiveServices = new List<ServiceState>();
		private void CheckConnectivity()
        {


			Logger.WriteDebug("Region: refreshing connectivity ...");
			//if (inactiveCenters.Count == 0 || TextSettings.Get("recheckConnectivity", "false") == "true")
			{
				int sensorTimeout = Setting.Get(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT);
				int alarmTimeout = Setting.Get(Setting.ALARM_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_ALARM_SERVICE_ACTIVITY_TIMEOUT);
				using (var db = new TMNModelDataContext())
				{
					inactiveServices = db.ServiceStates.Where(s => (s.InActiveSeconds > sensorTimeout && s.ServiceType != (int)ServiceTypes.AlarmService)
						|| (s.InActiveSeconds > alarmTimeout && s.ServiceType == (int)ServiceTypes.AlarmService)).ToList();//s.IsConnected == false).ToList();
				}
			}

			if (inactiveServices.Any(s => !centerSpotList.FirstOrDefault(c => c.Center.ID == s.CenterID).IsMute))
			{

				if (severityForAlarmSound != SoundAlarmSeverities.Information && !unhandledAlarms.Contains(SoundAlarmSeverities.Information))
				{
					voiceStartTime = DateTime.Now;
					severityForAlarmSound = SoundAlarmSeverities.Information; //use information for play alarms of disconnection
				}
				Dispatcher.Invoke(new Action(() =>
					{
						connectLed.DisplayMode = DisplayModes.On;
						connectLed.InnerBackground = Brushes.Red;
					}));
			}
			else
			{
				if (unhandledAlarms.Contains(SoundAlarmSeverities.Information))
					unhandledAlarms.Remove(SoundAlarmSeverities.Information);
				severityForAlarmSound = SoundAlarmSeverities.None;
				Dispatcher.Invoke(new Action(() =>
				 {
					 connectLed.DisplayMode = DisplayModes.On;
					 connectLed.InnerBackground = Brushes.LimeGreen;
				 }));
			}


        }

        private void setSoundAlarmSeverity(bool hasSensorProblems, bool hasPowerProblems, bool hasSwitchPowerProblems, bool hasCircuitProblems, AlarmSeverities alarmSeverity)
        {
            if ((hasPowerProblems || hasSwitchPowerProblems) && !unhandledAlarms.Contains(SoundAlarmSeverities.Power))
            {
                if (severityForAlarmSound != SoundAlarmSeverities.Power)
                    voiceStartTime = DateTime.Now;
                severityForAlarmSound = SoundAlarmSeverities.Power;
            }
            else if (hasSensorProblems && !unhandledAlarms.Contains(SoundAlarmSeverities.Sensor))
            {
                if (severityForAlarmSound != SoundAlarmSeverities.Sensor)
                    voiceStartTime = DateTime.Now;
                severityForAlarmSound = SoundAlarmSeverities.Sensor;
            }
            else if (hasCircuitProblems && !unhandledAlarms.Contains(SoundAlarmSeverities.Cable))
            {
                if (severityForAlarmSound != SoundAlarmSeverities.Cable)
                    voiceStartTime = DateTime.Now;
                severityForAlarmSound = SoundAlarmSeverities.Cable;
            }
            else if (severityForAlarmSound != SoundAlarmSeverities.Information)
            {
                if (severityForAlarmSound != (SoundAlarmSeverities)alarmSeverity)
                    voiceStartTime = DateTime.Now;
                severityForAlarmSound = (SoundAlarmSeverities)alarmSeverity;
            }

            if (!hasPowerProblems && !hasSwitchPowerProblems && unhandledAlarms.Contains(SoundAlarmSeverities.Power))
                unhandledAlarms.Remove(SoundAlarmSeverities.Power);
            if (!hasSensorProblems && unhandledAlarms.Contains(SoundAlarmSeverities.Sensor))
                unhandledAlarms.Remove(SoundAlarmSeverities.Sensor);
            if (!hasCircuitProblems && unhandledAlarms.Contains(SoundAlarmSeverities.Cable))
                unhandledAlarms.Remove(SoundAlarmSeverities.Cable);
        }

        private void PopulateCenters()
        {


			Dispatcher.Invoke(new Action(() =>
				{
					var db = new TMNModelDataContext();
					var region2 = new Guid("11b5c556-c369-4f6e-8408-8aeb14b87843");
					canvas.Children.Clear();
					centerSpotList.Clear();
					foreach (var center in db.Centers) //.Where(c => c.RegionID == region2))
					{
						CenterSpot centerSpot = new CenterSpot(center);

						centerSpot.PreviewMouseDown += new MouseButtonEventHandler(centerSpot_PreviewMouseDown);
						centerSpot.PreviewMouseUp += new MouseButtonEventHandler(centerSpot_PreviewMouseUp);
						centerSpot.MuteCenterClick += new EventHandler(centerSpot_MuteCenterClick);
						centerSpot.Click += new RoutedEventHandler(centerSpot_Click);
						//centerSpot.PowerClick += new RoutedEventHandler(centerSpot_PowerClick);
						//centerSpot.CircuitClick += new RoutedEventHandler(centerSpot_CircuitClick);

						canvas.Children.Add(centerSpot);

						centerSpotList.Add(centerSpot);

						CenterInMap centerInMap = db.CenterInMaps.SingleOrDefault(cm => cm.MapID == mapID && cm.CenterID == center.ID);
						if (centerInMap != null)
						{
							Canvas.SetLeft(centerSpot, (double)centerInMap.X);
							Canvas.SetTop(centerSpot, (double)centerInMap.Y);
						}
					}

					double z = double.Parse(TMN.TextSettings.Get("ZOOM_FACTOR", "1.0"));
					for (int i = 0; i < centerSpotList.Count; i++)
					{
						centerSpotList[i].Zoom(z);
					}

					zoomFactor = z;
				}), DispatcherPriority.Background);
        }

        void centerSpot_CircuitClick(object sender, RoutedEventArgs e)
        {
    //        if (movingItem == null || movingItem.IsMoving == false)
    //        {
    //            new AlarmCircuitWindow().ShowAsSingleTab(MainWindow.Instance.tabControl, "Alarm Circuit");
				//Center.Selected = (sender as CenterSpot).Center;
 
				////Center.Selected = (sender as CenterSpot).Center;

				 
    //        } 
        }

        void centerSpot_PowerClick(object sender, RoutedEventArgs e)
        {
            if (movingItem == null || movingItem.IsMoving == false)
            {
                new AlarmPowerWindow().ShowAsSingleTab(MainWindow.Instance.tabControl, "Alarm Power Panel");
				Center.Selected = (sender as CenterSpot).Center;
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
                new AlarmPanelWindow().ShowAsSingleTab(MainWindow.Instance.tabControl, "Alarm Panel");
                Center .Selected  = (sender as CenterSpot).Center;
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
        //    if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.CENTERS_EDIT))
          //  {
                if (e.ChangedButton == MouseButton.Left && !lockMenuItem.IsChecked)
                {
                    startPos = e.GetPosition(canvas);
                    movingItem = sender as CenterSpot;
                }
          //  }
          //  else
          //  {
          //      MessageBox.Show(MessageTypes.AccessDenied);
          //  } 
        }

        private void connectLed_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            voiceStartTime = DateTime.MinValue;
            ManageSound();
            new ServiceStateWindow().Show();
        }

        private void refreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TextSettings.Set("recheckConnectivity", "true");
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
            TMN.TextSettings.Set("MUTE_REGION", muteAllSoundButton.IsMute.ToString());
            ManageSound();
        }

        private void muteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ("" + (sender as MenuItem).CommandParameter)
            {
                case "A":
                    foreach (var item in canvas.Children)
                    {
                        CenterSpot cs = item as CenterSpot;
                        if (cs != null)
                        {
                            cs.IsMute = muteMenuItem.IsChecked;
                        }
                    }
                    break;
                case "C":
                    foreach (var item in canvas.Children)
                    {
                        CenterSpot cs = item as CenterSpot;
                        if (cs != null)
                        {
                            cs.IsMuteCritical = muteCriticalMenuItem.IsChecked;
                        }
                    }
                    break;
                case "J":
                    foreach (var item in canvas.Children)
                    {
                        CenterSpot cs = item as CenterSpot;
                        if (cs != null)
                        {
                            cs.IsMuteMajor = muteMajorMenuItem.IsChecked;
                        }
                    }
                    break;
                case "I":
                    foreach (var item in canvas.Children)
                    {
                        CenterSpot cs = item as CenterSpot;
                        if (cs != null)
                        {
                            cs.IsMuteMinor = muteMinorMenuItem.IsChecked;
                        }
                    }
                    break;
                case "S":
                    foreach (var item in canvas.Children)
                    {
                        CenterSpot cs = item as CenterSpot;
                        if (cs != null)
                        {
                            cs.IsMuteSensor = muteSensorMenuItem.IsChecked;
                        }
                    }
                    break;
            }


			CheckConnectivity();
			CheckForAlarms();
            
			ManageSound();
        }

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.toolbarTray.ToolBars.Add(toolbar.Extract());
        }

        private void grid_Unloaded(object sender, RoutedEventArgs e)
        {
            toolbar.Extract();
            TextSettings.Set("ZOOM_FACTOR", zoomFactor.ToString());

        }
    }
}
