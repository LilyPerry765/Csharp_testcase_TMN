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
	public partial class AlarmPowerWindow : Window, ITabedWindow
	{
		DispatcherTimer monitoringTimer = new DispatcherTimer(DispatcherPriority.Background);
		private DateTime voiceStartTime = DateTime.Now;
		private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);

		

		public AlarmPowerWindow()
		{
			Logger.WriteDebug("Alarm Power Initing components...");
			InitializeComponent();
			Logger.WriteDebug("Alarm Power Initialized.");
			monitoringTimer.Interval = new TimeSpan(0, 0, Setting.Get(Setting.CENTER_ALARM_PANEL_INTERVAL, Setting.DEFAULT_CENTER_ALARM_PANEL_INTERVAL));
			monitoringTimer.Tick += new EventHandler(monitoringTimer_Tick);

			Center.SelectedChanged -= new Action(Center_SelectedChanged);
			Center.SelectedChanged += new Action(Center_SelectedChanged);

			//MainWindow.Instance.TabChanged -= new SelectionChangedEventHandler(tabControl_SelectionChanged);
			//MainWindow.Instance.TabChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);

			CustomizeForSelectedCenter();
			RefreshAlarmPower(IsOpen);
		}

		void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CustomizeForSelectedCenter();
			RefreshAlarmPower(IsOpen);
		}

		void monitoringTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				RefreshAlarmPower(IsOpen);
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		void Center_SelectedChanged()
		{
			CustomizeForSelectedCenter();
			RefreshAlarmPower(IsOpen);
		}

		private void CustomizeForSelectedCenter()
		{
			string pointCode = Center.Selected.PointCode;
			HasNewAlarm = false;

			masterSound.IsMute = bool.Parse(TextSettings.Get("MUTE_POW_" + pointCode, "false"));
			M1Title.Text = M2Title.Text = M3Title.Text = M4Title.Text = M5Title.Text = M6Title.Text = M7Title.Text = M8Title.Text = "";
			M1Led.Visibility = M2Led.Visibility = M3Led.Visibility = M4Led.Visibility = Visibility.Hidden;
			M5Led.Visibility = M6Led.Visibility = M7Led.Visibility = M8Led.Visibility = Visibility.Hidden;
			//critSound.IsMute = bool.Parse(TextSettings.Get("MUTE_CRI_" + pointCode, "false"));
			//majorSound.IsMute = bool.Parse(TextSettings.Get("MUTE_MAJ_" + pointCode, "false"));
			//minorSound.IsMute = bool.Parse(TextSettings.Get("MUTE_MIN_" + pointCode, "false"));

			//M1Led.InnerBackground = M2Led.InnerBackground = M3Led.InnerBackground = M4Led.InnerBackground = Brushes.LightGray;
			//M5Led.InnerBackground = M6Led.InnerBackground = M7Led.InnerBackground = M8Led.InnerBackground = Brushes.LightGray;

			CenterNameLabel.Content = string.Format("آلارم پاور مرکز {0}", Center.Selected.DisplayName);
			LoadSensors();
		}

		public void TabOpened()
		{

		}

		public void TabClosed()
		{
			//Center.SelectedChanged -= new Action(Center_SelectedChanged);
		}

		private void RefreshAlarmPower(bool openStatus)
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
					AlarmPlayer.Stop("power");
				monitoringTimer.Start();
		
		}

		private void RefreshAlarmServiceStateLED()
		{



			using (var db = new TMNModelDataContext())
			{
				if (db.ServiceStates.Where(s => s.CenterID == Center.Selected.ID && s.ServiceType == (int)ServiceTypes.SensorService).ToArray().All(ss => ss.IsConnected))
				{
					alarmServiceLed.InnerBackground = Brushes.LimeGreen;
					alarmServiceLed.DisplayMode = DisplayModes.On;
					alarmServiceLed.ToolTip = "Power Alarm service is OK";
					HasNewAlarm = false;
				}
				else
				{
					alarmServiceLed.InnerBackground = Brushes.Red;
					alarmServiceLed.DisplayMode = DisplayModes.Blinking;
					alarmServiceLed.ToolTip = "Problem detected in power alarm service process. View LogViewer for more info.";
					HasNewAlarm = true;
				}
			}
		}

		private bool isLocalSensors = true;
		private void LoadSensors()
		{


			try
			{
				//Logger.WriteDebug("Center: Loading sensors...");
				using (var db = new TMNModelDataContext())
				{
					Room room = db.Rooms.Where(r => r.CenterID == Center.Selected.ID && r.Sensors.Any(s => s.TypeID == (byte)SensorTypes.Humidity || s.TypeID == (byte)SensorTypes.Temperature)).FirstOrDefault();

					if (room == null)
					{
						room = db.Rooms.Where(r => r.Center.Name == Center.Selected.Name).OrderBy(r => r.Name).FirstOrDefault();
						isLocalSensors = false;
					}
					sensorsPanel.Children.Clear();
					//foreach (var room in rooms)
					//{
					// It is expected that only one room exists
					var ui = new SensorPair(room.ID);
					ui.SoundAlertManagementRequested += ManageSoundAlert;
					ui.HumidityDisplay.led.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(sensore_PreviewMouseLeftButtonUp);
					ui.TemperatureDisplay.led.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(sensore_PreviewMouseLeftButtonUp);
					sensorsPanel.Children.Add(ui);
					//}

					var powerSensors = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID && s.TypeID == (byte)SensorTypes.POWER).ToArray();
					foreach (var sensor in powerSensors)
					{
						switch (sensor.ModulNumber)
						{
							case 11:
								M1Title.Text = sensor.Title;
								M1Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 12:
								M2Title.Text = sensor.Title;
								M2Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 13:
								M3Title.Text = sensor.Title;
								M3Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 14:
								M4Title.Text = sensor.Title;
								M4Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 15:
								M5Title.Text = sensor.Title;
								M5Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 16:
								M6Title.Text = sensor.Title;
								M6Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 17:
								M7Title.Text = sensor.Title;
								M7Led.Visibility = System.Windows.Visibility.Visible;
								break;
							case 18:
								M8Title.Text = sensor.Title;
								M8Led.Visibility = System.Windows.Visibility.Visible;
								break;
						}
					}
				}
				//Logger.WriteDebug("Center: Finished loading sensors");
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
				HasNewAlarm = false;
				ManageSoundAlert();
			}
		}

		private void RefreshSensorData()
		{



			try
			{
				bool sensorPanelIsEmpty = !sensorsPanel.Children.Cast<UIElement>().Any(ui => !(ui is TextBox));
				if (sensorPanelIsEmpty)
					LoadSensors();

				if (alarmServiceLed.DisplayMode == DisplayModes.On)
				{
					Logger.WriteDebug("Loading sensor data...");

					KeyValuePair<Sensor, double?>[] sensorsValue;
					using (var db = new TMNModelDataContext())
					{
						//if (isLocalSensors)
						//    sensorsValue = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID).ToArray().Select(s => KeyValuePair.Create(s, s.LastValue)).ToArray();
						//else
						//    sensorsValue = db.Sensors.Where(s => s.Room.Center.Name == Center.Selected.Name).ToArray().Select(s => KeyValuePair.Create(s, s.LastValue)).ToArray();


						if (isLocalSensors)
							sensorsValue = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID && s.TypeID == (byte)SensorTypes.POWER).ToArray().
								Select(s => KeyValuePair.Create(s, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();
						else
							sensorsValue = db.Sensors.Where(s => s.Room.Center.Name == Center.Selected.Name && s.TypeID == (byte)SensorTypes.POWER).ToArray()
								.Select(s => KeyValuePair.Create(s, s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault())).ToArray();


					}
					foreach (SensorPair sensorPair in sensorsPanel.Children)
					{
						sensorPair.RefreshDisplays(sensorsValue);
					}

					foreach (KeyValuePair<Sensor, double?> sensor in sensorsValue)
					{
						Sensor s = sensor.Key;
						if (s.SensorType == SensorTypes.POWER)
						{
							switch (s.ModulNumber)
							{
								case 11:
									M1Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M1Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 12:
									M2Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M2Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 13:
									M3Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M3Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 14:
									M4Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M4Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 15:
									M5Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M5Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 16:
									M6Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M6Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 17:
									M7Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M7Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
								case 18:
									M8Led.DisplayMode = sensor.Value == 1 ? DisplayModes.Blinking : DisplayModes.On;
									M8Led.InnerBackground = sensor.Value == 1 ? Brushes.Red : Brushes.LimeGreen;
									break;
							}
						}
					}
				}
				else
				{
					M1Led.DisplayMode = M2Led.DisplayMode = M3Led.DisplayMode = M4Led.DisplayMode = DisplayModes.Off;
					M5Led.DisplayMode = M6Led.DisplayMode = M7Led.DisplayMode = M8Led.DisplayMode = DisplayModes.Off;
				}
				//Logger.WriteDebug("sensor ui finished.");
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void RefreshDateTime()
		{
			PersianDateTimeLabel.Content = PersianDateTime.Now.ToString("dddd d MMMM yyyy ساعت HH:mm");
			DateTimeLabel.Content = DateTime.Now.ToString("dddd, MMMM d, yyyy");
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
				if (HasNewAlarm != false)
				{
					if (AlarmPlayer.isPlaying == false)
						voiceStartTime = DateTime.Now;

					if (masterSound.IsMute)
					{
						AlarmPlayer.Stop("power");
					}
					else if (voiceInterval != 0 && DateTime.Now.Subtract(voiceStartTime).TotalSeconds > voiceInterval) //shahab 900815 //determine criteria for duration of alarm playing 
					{
						AlarmPlayer.Stop("power");
					}
					else
					{
						if (alarmServiceLed.InnerBackground == Brushes.Red)
							AlarmPlayer.Play(SoundAlarmSeverities.Information, "circuit");
						else if (AnySensorRequestsAlarm())
							AlarmPlayer.Play(SoundAlarmSeverities.Critical, "power");
						else if (M1Led.DisplayMode == DisplayModes.Blinking ||
							M2Led.DisplayMode == DisplayModes.Blinking ||
							M3Led.DisplayMode == DisplayModes.Blinking ||
							M4Led.DisplayMode == DisplayModes.Blinking ||
							M5Led.DisplayMode == DisplayModes.Blinking ||
							M6Led.DisplayMode == DisplayModes.Blinking ||
							M7Led.DisplayMode == DisplayModes.Blinking ||
							M8Led.DisplayMode == DisplayModes.Blinking
							)
							AlarmPlayer.Play(SoundAlarmSeverities.Power, "power");
						else
							AlarmPlayer.Stop("power");
					}
				}
				else
				{
					AlarmPlayer.Stop("power");
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void M1Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 11).Show();

			}
		}

		private void M2Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 12).Show();

			}
		}

		private void M3Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 13).Show();

			}
		}

		private void M4Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 14).Show();

			}
		}

		private void M5Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 15).Show();

			}
		}

		private void M6Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 16).Show();

			}
		}

		private void M7Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 17).Show();

			}
		}

		private void M8Led_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				new SensorChartWindow(Center.Selected, 18).Show();

			}
		}

		private void Sound_IsMuteChanged(object sender, EventArgs e)
		{
			ManageSoundAlert();
			string pointCode = Center.Selected.PointCode;

			TextSettings.Set("MUTE_POW_" + pointCode, masterSound.IsMute.ToString());

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
				HasNewAlarm = false;
				ManageSoundAlert();
				new ServiceStateWindow(Center.Selected).ShowDialog();
			}
		}


		public bool IsOpen
		{
			get
			{
				try
				{
					return ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Alarm Power Panel");

				}
				catch
				{
					return false;
				}
			}
		}

		public bool HasNewAlarm
		{
			get
			{
				try
				{
					return bool.Parse(Setting.Get(Setting.NEW_SENSOR_POWER_ALARM_INSERTED + Center.Selected.PointCode, "False"));
				}
				catch
				{
					return false;
				}
			}
			set
			{
				Setting.Set(Setting.NEW_SENSOR_POWER_ALARM_INSERTED + Center.Selected.PointCode, value.ToString());
			}
		}

	}
}
