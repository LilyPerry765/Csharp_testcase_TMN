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
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.Collections;

namespace TMN.UI.Windows
{
    public partial class AlarmPanelWindow : Window, ITabedWindow
    {
        Timer monitoringTimer = new Timer();
        private DateTime voiceStartTime = DateTime.Now;
        private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);
        private bool isLocalSensors = true;
        private Center currentCenter = null;
        
	
        public AlarmPanelWindow()
        {
            Logger.WriteDebug("Alarm Panel Initing components...");
            InitializeComponent();
            Logger.WriteDebug("Alarm Panel Initialized.");
            monitoringTimer.Interval = Setting.Get(Setting.CENTER_ALARM_PANEL_INTERVAL, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL) * 1000;
            monitoringTimer.Elapsed += new ElapsedEventHandler(monitoringTimer_Elapsed); 

            Center.SelectedChanged -= new Action(Center_SelectedChanged);
            Center.SelectedChanged += new Action(Center_SelectedChanged);
            MainWindow.Instance.TabChanged -= new SelectionChangedEventHandler(tabControl_SelectionChanged);
            MainWindow.Instance.TabChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);

			CustomizeForSelectedCenter();
			RefreshAlarmPanel(true);
        }

        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (MainWindow.Instance.tabControl.SelectedItem != null)
            //    IsOpen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Panel");
            //else
            //    IsOpen = false;
            //if (IsOpen)
            {
                HasNewAlarm = AlarmSeverities.None;
                
                //Dispatcher.BeginInvoke((Action)delegate()
                if (Center.Selected != currentCenter)
                {
                    Center.Selected = currentCenter;
                    //CustomizeForSelectedCenter();
                    //RefreshAlarmPanel(IsOpen);
                }//, DispatcherPriority.Background);
            }
            //else
            //    AlarmPlayer.Stop("panel");
        }

        void monitoringTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                RefreshAlarmPanel(IsOpen);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        void Center_SelectedChanged()
        {
            HasNewAlarm = AlarmSeverities.None;
            //Dispatcher.BeginInvoke((Action)delegate()
            {
                CustomizeForSelectedCenter();
                RefreshAlarmPanel(IsOpen);
            } //, DispatcherPriority.Background);
        }

        private void CustomizeForSelectedCenter()
        {
            string pointCode = Center.Selected.PointCode;

            masterSound.IsMute = bool.Parse(TextSettings.Get("MUTE_ALL_" + pointCode, "false"));
            critSound.IsMute = bool.Parse(TextSettings.Get("MUTE_CRI_" + pointCode, "false"));
            majorSound.IsMute = bool.Parse(TextSettings.Get("MUTE_MAJ_" + pointCode, "false"));
            minorSound.IsMute = bool.Parse(TextSettings.Get("MUTE_MIN_" + pointCode, "false"));

            CenterNameLabel.Content = string.Format("آلارم پنل مرکز {0}", Center.Selected.DisplayName);
            //LoadSensors(alarmPanelModel);

            LoadSensors();
        }

        public void TabOpened()
        {

        }

        public void TabClosed()
        {
            //Center.SelectedChanged -= new Action(Center_SelectedChanged);
        }

        private void RefreshDateTime()
        {
            Dispatcher.Invoke(new Action(() =>
                {
                    PersianDateTimeLabel.Content = PersianDateTime.Now.ToString("dddd d MMMM yyyy ساعت HH:mm");
                    DateTimeLabel.Content = DateTime.Now.ToString("dddd, MMMM d, yyyy");
                }));
        }

		private void RefreshAlarmPanel(bool openStatus)
		{

			monitoringTimer.Stop();
			if (openStatus)
			{
				try
				{
					HasNewAlarm = AlarmSeverities.None;
					Setting.IsDirty();
					//AlarmPlayer.StopInActiveApplication("panel");
					RefreshDateTime();
					RefreshAlarmsStatistics();
					LoadNewAlarms();
					RefreshSensorData();
					RefreshAlarmServiceStateLED();
					RefreshPowerAlarmLED();

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
				AlarmPlayer.Stop("panel");
			monitoringTimer.Start();
		}

		private void RefreshAlarmsStatistics()
		{
			try
			{
				using (var db = new TMNModelDataContext())
				{
					var alarmStatistics = db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID).GroupBy(a => new
					{
						IsRead = a.IsRead.Value,
						Severity = (AlarmSeverities)a.Severity.Value
					}).Select(g => new
					{
						g.Key.Severity,
						g.Key.IsRead,
						Count = g.Count()
					}).ToArray();

					var criticalCount = alarmStatistics.Where(s => s.Severity == AlarmSeverities.Critical).Sum(s => s.Count);
					var majorCount = alarmStatistics.Where(s => s.Severity == AlarmSeverities.Major).Sum(s => s.Count);
					var minorCount = alarmStatistics.Where(s => s.Severity == AlarmSeverities.Minor).Sum(s => s.Count);
					var otherCount = alarmStatistics.Where(s => s.Severity == AlarmSeverities.Information).Sum(s => s.Count);
					var ppCount = alarmStatistics.Where(s => (int)s.Severity >= 11 && (int)s.Severity <= 13).Sum(s => s.Count);

					var criticalCountUnread = alarmStatistics.Where(a => !a.IsRead && a.Severity == AlarmSeverities.Critical).Select(s => s.Count).SingleOrDefault();
					var majorCountUnread = alarmStatistics.Where(a => !a.IsRead && a.Severity == AlarmSeverities.Major).Select(s => s.Count).SingleOrDefault();
					var minorCountUnread = alarmStatistics.Where(a => !a.IsRead && a.Severity == AlarmSeverities.Minor).Select(s => s.Count).SingleOrDefault();

					Dispatcher.Invoke(new Action(() =>
					{
						SetCount(criticalLed, criticalCount, criticalCountUnread);
						SetCount(majorLed, majorCount, majorCountUnread);
						SetCount(minorLed, minorCount, minorCountUnread);
						infoLed.Title = "" + otherCount;
						ppLed.Title = "" + ppCount;
					}));
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void LoadNewAlarms()
		{

			LogAlarm[] selectedAlarms;
			using (var db = new TMNModelDataContext())
			{
				const int TOP = 5;
				var unreadAlarms = db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID && a.IsRead == false && a.Title != null && a.Title != string.Empty);
				selectedAlarms = unreadAlarms.Where(a => (AlarmSeverities)a.Severity == AlarmSeverities.Critical).Take(TOP)
							.Union(unreadAlarms.Where(a => (AlarmSeverities)a.Severity == AlarmSeverities.Major).Take(TOP))
							.Union(unreadAlarms.Where(a => (AlarmSeverities)a.Severity == AlarmSeverities.Minor).Take(TOP)).ToArray();
			}


			Dispatcher.Invoke(new Action(() =>
				{
					newCriticalAlarmsList.Items.Clear();
					newMajorAlarmsList.Items.Clear();
					newMinorAlarmsList.Items.Clear();
					foreach (var alarm in selectedAlarms)
					{
						BlinkingLed led = new BlinkingLed();
						led.Margin = new Thickness(0, 2, 0, 2);
						led.Height = 55;
						led.Width = 264;
						led.FontSize = 14;
						led.VerticalAlignment = System.Windows.VerticalAlignment.Center;
						led.Title = alarm.Title;
						if (!string.IsNullOrWhiteSpace(alarm.Location))
							led.Title += "\n" + alarm.Location;
						led.DisplayMode = DisplayModes.On;
						led.Tag = alarm.ID;
						led.PreviewMouseUp += new MouseButtonEventHandler(led_PreviewMouseUp);

						switch ((AlarmSeverities)alarm.Severity)
						{
							case AlarmSeverities.Critical:
								led.InnerBackground = criticalLed.InnerBackground;
								newCriticalAlarmsList.Items.Add(led);
								break;
							case AlarmSeverities.Major:
								led.InnerBackground = majorLed.InnerBackground;
								newMajorAlarmsList.Items.Add(led);
								break;
							case AlarmSeverities.Minor:
								led.InnerBackground = minorLed.InnerBackground;
								newMinorAlarmsList.Items.Add(led);
								break;
						}
					}
				}));

		}

		private void RefreshSensorData()
		{
			try
			{
				KeyValuePair<Sensor, double?>[] sensorsValue = null;

				// LoadSensors(alarmPanelModel);

				//if (isLocalSensors)
				//    sensorsValue = alarmPanelModel.SensorsValue1;
				//else
				//    sensorsValue = alarmPanelModel.SensorsValue2;
				//Logger.WriteDebug("Loading sensor data...");

				using (var db = new TMNModelDataContext())
				{

					//if (isLocalSensors)
					//    sensorsValue = db.Sensors.Where(s => (s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature) && s.Room.CenterID == Center.Selected.ID).ToArray()
					//      .Select(s => KeyValuePair.Create(s, s.LastValue)).ToArray();
					//else
					//    sensorsValue = db.Sensors.Where(s => (s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature) && s.Room.Center.Name == Center.Selected.Name).ToArray()
					//      .Select(s => KeyValuePair.Create(s, s.LastValue)).ToArray();

					if (isLocalSensors)
						sensorsValue = db.Sensors.Where(s => (s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature) && s.Room.CenterID == Center.Selected.ID).ToArray()
							.Select(s => KeyValuePair.Create(s, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();
					else
						sensorsValue = db.Sensors.Where(s => (s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature) && s.Room.Center.Name == Center.Selected.Name).ToArray()
							.Select(s => KeyValuePair.Create(s, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();

				}

				Logger.WriteDebug("finished loading sensor data.");
				//Logger.WriteDebug("sensor ui...");

				Dispatcher.Invoke(new Action(() =>
					{
						bool sensorPanelIsEmpty = !sensorsPanel.Children.Cast<UIElement>().Any(ui => !(ui is TextBox));
						if (sensorPanelIsEmpty)
							LoadSensors();

						foreach (SensorPair sensorPair in sensorsPanel.Children)
						{
							sensorPair.RefreshDisplays(sensorsValue);

						}
					}));
				Logger.WriteDebug("sensor ui finished.");
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void RefreshAlarmServiceStateLED()
		{
			using (var db = new TMNModelDataContext())
			{
				Dispatcher.Invoke(new Action(() =>
					{
						if (db.ServiceStates.Where(s => s.CenterID == Center.Selected.ID).ToArray().All(ss => ss.IsConnected))
						{
							alarmServiceLed.InnerBackground = Brushes.LimeGreen;
							alarmServiceLed.DisplayMode = DisplayModes.On;
							alarmServiceLed.ToolTip = "Alarm service is OK";
							HasNewAlarm = AlarmSeverities.None;
						}
						else
						{
							alarmServiceLed.InnerBackground = Brushes.Red;
							alarmServiceLed.DisplayMode = DisplayModes.Blinking;
							alarmServiceLed.ToolTip = "Problem detected in alarm service process. View LogViewer for more info.";
							HasNewAlarm = AlarmSeverities.Information;
						}
					}));
			}
		}

		private void RefreshPowerAlarmLED()
		{

			using (var db = new TMNModelDataContext())
			{
				if (db.LogAlarms.Where(a => a.CenterID == Center.Selected.ID &&
					a.IsRead == false &&
					(a.Data.ToUpper().Contains("POWER") ||
					a.Data.ToUpper().Contains("AC") ||
					a.Data.ToUpper().Contains("DC") ||
					a.Data.ToUpper().Contains("RECTIFIRE") ||
					a.Data.ToUpper().Contains("EMFU") ||
					a.Data.ToUpper().Contains("CONV")))
					.ToList()
					.Any(a => Regex.IsMatch(a.Data.ToUpper(), @"[\s\r\n](POWER|AC|DC|RECTIFIRE|EMFU|CONV)[\s\r\n]")))
				{
					Dispatcher.Invoke(new Action(() =>
					{
						PoweralarmLed.InnerBackground = Brushes.Red;
						PoweralarmLed.DisplayMode = DisplayModes.Blinking;
						PoweralarmLed.ToolTip = "Problem detected in Power. View Logs for more info.";
						HasNewAlarm = AlarmSeverities.Information;
					}));
				}
				else
				{
					Dispatcher.Invoke(new Action(() =>
					{
						PoweralarmLed.InnerBackground = Brushes.LimeGreen;
						PoweralarmLed.DisplayMode = DisplayModes.On;
						PoweralarmLed.ToolTip = "No power alarm";
						Setting.Set(Setting.NEW_POWER_ALARM_INSERTED + Center.Selected.PointCode, "false");
						HasNewAlarm = AlarmSeverities.None;
					}));

				}
			}

		}

		private void LoadSensors()
		{

			string pointCode = Center.Selected.PointCode;

			try
			{
				using (var db = new TMNModelDataContext())
				{
					var rooms = db.Rooms.Where(r => r.CenterID == Center.Selected.ID).ToArray();

					if (rooms.Count() == 0)
					{
						rooms = db.Rooms.Where(r => r.Center.Name == Center.Selected.Name).ToArray();

						isLocalSensors = false;
					}

					sensorsPanel.Children.Clear();
					foreach (var room in rooms)
					{
						var ui = new SensorPair(room.ID);
						ui.SoundAlertManagementRequested += ManageSoundAlert;
						ui.HumidityDisplay.led.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(sensore_PreviewMouseLeftButtonUp);
						ui.TemperatureDisplay.led.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(sensore_PreviewMouseLeftButtonUp);

						ui.HumidityDisplay.IsMute = bool.Parse(TextSettings.Get("MUTE_SEN_" + pointCode, "false"));
						ui.TemperatureDisplay.IsMute = bool.Parse(TextSettings.Get("MUTE_SEN_" + pointCode, "false"));

						sensorsPanel.Children.Add(ui);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

        void sensore_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AnySensorRequestsAlarm())
            {
                HasNewAlarm = AlarmSeverities.None;
                ManageSoundAlert();
            }
        }

        void led_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Guid alarmID = (Guid)(sender as BlinkingLed).Tag;

            ShowLog(alarmID);
        }

        private void SetCount(BlinkingLed led, int totalCount, int unreadCount)
        {
            led.DisplayMode = unreadCount > 0 ? DisplayModes.Blinking : totalCount > 0 ? DisplayModes.On : DisplayModes.Off;

            if (unreadCount > 0)
            {
                led.Title = string.Format("{0} Active Alarm(s)!", unreadCount);
            }
            else if (totalCount > 0)
            {
                led.Title = string.Format("Alarm archive ({0})", totalCount);
            }
            else
            {
                led.Title = null;
            }

        }

        private bool AnySensorRequestsAlarm()
        {
            if (sensorsPanel != null)
            {
                return sensorsPanel.Children.Cast<UIElement>().Any(u => u is SensorPair && (u as SensorPair).RequestsAlarm);
            }
            return false;
        }

        private void ManageSoundAlert()
        {
            try
            {
                //shahab 900815
                //determine time intervals sound alarm playing
                //if (criticalLed.DisplayMode != DisplayModes.Blinking &&
                //    majorLed.DisplayMode != DisplayModes.Blinking &&
                //    minorLed.DisplayMode != DisplayModes.Blinking &&
                //    alarmServiceLed.DisplayMode != DisplayModes.Blinking &&
                //    !AnySensorRequestsAlarm())
                //    voiceStartTime = DateTime.Now;

                if (HasNewAlarm != AlarmSeverities.None)
                {
                    if (AlarmPlayer.isPlaying == false)
                        voiceStartTime = DateTime.Now;
                    Dispatcher.Invoke(new Action(() =>
                        {
                            if (masterSound.IsMute)
                            {
                                AlarmPlayer.Stop("panel");
                            }
                            else if (voiceInterval != 0 && DateTime.Now.Subtract(voiceStartTime).TotalSeconds > voiceInterval) //shahab 900815 //determine criteria for duration of alarm playing 
                            {
                                AlarmPlayer.Stop("panel");
                            }
                            else
                            {
                                if (PoweralarmLed.DisplayMode == DisplayModes.Blinking)
                                    AlarmPlayer.Play(SoundAlarmSeverities.Power, "panel");
                                else if (!critSound.IsMute && criticalLed.DisplayMode == DisplayModes.Blinking || AnySensorRequestsAlarm())
                                    AlarmPlayer.Play(SoundAlarmSeverities.Critical, "panel");
                                else if (!majorSound.IsMute && majorLed.DisplayMode == DisplayModes.Blinking)
                                    AlarmPlayer.Play(SoundAlarmSeverities.Major, "panel");
                                else if (!minorSound.IsMute && minorLed.DisplayMode == DisplayModes.Blinking)
                                    AlarmPlayer.Play(SoundAlarmSeverities.Minor, "panel");
                                else if (alarmServiceLed.DisplayMode == DisplayModes.Blinking)
                                    AlarmPlayer.Play(SoundAlarmSeverities.Information, "panel");
                                else
                                    AlarmPlayer.Stop("panel");
                            }
                        }));
                }
                else
                {
                    AlarmPlayer.Stop("panel");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void criticalLed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ShowLog(AlarmSeverities.Critical);
            }
        }

        private void infoLed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ShowLog(AlarmSeverities.Information);
            }
        }

        private void minorLed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ShowLog(AlarmSeverities.Minor);
            }
        }

        private void majorLed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ShowLog(AlarmSeverities.Major);
            }
        }

        private void ShowLog(AlarmSeverities severity)
        {
            ShowLog(severity, null);
        }

        private void ShowLog(Guid alarmID)
        {
            ShowLog(null, alarmID);
        }

        private void ShowLog(AlarmSeverities? severity, Guid? alarmID, bool postPone = false)
        {
            HasNewAlarm = AlarmSeverities.None;
            ManageSoundAlert();
            MainWindow.Instance.Cursor = Cursors.Wait;
            //System.Windows.Forms.Application.DoEvents();
            //  Logger.WriteDebug("Showing viewer form...");
            AlarmLogViewerWindow logviewer = null;
            if (alarmID == null && postPone == false)
                logviewer = new AlarmLogViewerWindow(severity.Value);
            else if (postPone == false)
                logviewer = new AlarmLogViewerWindow(alarmID.Value);
            else
                logviewer = new AlarmLogViewerWindow(postPone);
            if (logviewer.ShowDialog() == true)
                RefreshAlarmPanel(IsOpen);
            MainWindow.Instance.Cursor = Cursors.Arrow;
        }

        private void Sound_IsMuteChanged(object sender, EventArgs e)
        {
            ManageSoundAlert();
            string pointCode = Center.Selected.PointCode;

            TextSettings.Set("MUTE_ALL_" + pointCode, masterSound.IsMute.ToString());
            TextSettings.Set("MUTE_CRI_" + pointCode, critSound.IsMute.ToString());
            TextSettings.Set("MUTE_MAJ_" + pointCode, majorSound.IsMute.ToString());
            TextSettings.Set("MUTE_MIN_" + pointCode, minorSound.IsMute.ToString());

            TextSettings.Set("MUTE_SEN_" + pointCode, sensorsPanel.Children.Cast<UIElement>().Any(u => u is SensorPair && (u as SensorPair).IsMute).ToString());



        }

        private void Led_DisplayModeChanged(object sender, EventArgs e)
        {
            if (IsOpen)
                ManageSoundAlert();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.WriteTodo("Sensor restart requested. Implement remote service restart here.");
        }

        private void remoteButton_Click(object sender, RoutedEventArgs e)
        {
            Center.Selected.Connect();
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

        private void alarmServiceLed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                HasNewAlarm = AlarmSeverities.None;
                ManageSoundAlert();
                new ServiceStateWindow(Center.Selected).ShowDialog();
            }
        }

        private void ppLed_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ShowLog(AlarmSeverities.None, null, true);
            }
        }


		public AlarmSeverities HasNewAlarm
		{
			get
			{
				try
				{
					return (AlarmSeverities)Enum.Parse(typeof(AlarmSeverities), Setting.Get(Setting.NEW_ALARM_INSERTED + Center.Selected.PointCode, AlarmSeverities.None.ToString()));
				}
				catch
				{
					return AlarmSeverities.None;
				}
			}
			set
			{
				Setting.Set(Setting.NEW_ALARM_INSERTED + Center.Selected.PointCode, value.ToString());
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
						isopen = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Panel");
					}));
					return isopen;
				}
				catch
				{
					return false;
				}
			}
		}
    }
}
