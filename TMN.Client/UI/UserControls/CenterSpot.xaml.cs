﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Enterprise;

namespace TMN.UserControls
{

    public partial class CenterSpot : UserControl
    {
		public event RoutedEventHandler Click;
		//public event RoutedEventHandler PowerClick;
		//public event RoutedEventHandler CircuitClick;
		public event EventHandler MuteCenterClick;

        private bool IsSensorServiceWorking, IsSwitchServiceWorking;//, IsCircuitServiceWorking;
		private Color NormalColor = Color.FromArgb(0xFF, 0x7A, 0xC7, 0x00);
		private Timer timer = new Timer();
		//VPNHelper vpn = new VPNHelper();
		//ServiceController serviceController;

		public bool IsMute
		{
			get
			{
				return bool.Parse(TextSettings.Get("MUTE_ALL_" + Center.PointCode, "False"));
			}
			set
			{
				TextSettings.Set("MUTE_ALL_" + Center.PointCode, value.ToString());
			}
		}

		public bool IsMuteCritical
		{
			get
			{
				return bool.Parse(TextSettings.Get("MUTE_CRI_" + Center.PointCode, "False"));
			}
			set
			{
				TextSettings.Set("MUTE_CRI_" + Center.PointCode, value.ToString());
			}
		}

		public bool IsMuteMajor
		{
			get
			{
				return bool.Parse(TextSettings.Get("MUTE_MAJ_" + Center.PointCode, "False"));
			}
			set
			{
				TextSettings.Set("MUTE_MAJ_" + Center.PointCode, value.ToString());
			}
		}

		public bool IsMuteMinor
		{
			get
			{
				return bool.Parse(TextSettings.Get("MUTE_MIN_" + Center.PointCode, "False"));
			}
			set
			{
				TextSettings.Set("MUTE_MIN_" + Center.PointCode, value.ToString());
			}
		}

		public bool IsMuteSensor
		{
			get
			{
				return bool.Parse(TextSettings.Get("MUTE_SEN_" + Center.PointCode, "False"));
			}
			set
			{
				TextSettings.Set("MUTE_SEN_" + Center.PointCode, value.ToString());
			}
		}

		//public bool IsMutePower
		//{
		//	get
		//	{
		//		return bool.Parse(TextSettings.Get("MUTE_POW_" + Center.PointCode, "False"));
		//	}
		//	set
		//	{
		//		TextSettings.Set("MUTE_POW_" + Center.PointCode, value.ToString());
		//	}
		//}

		//public bool IsMuteCircuit
		//{
		//	get
		//	{
		//		return bool.Parse(TextSettings.Get("MUTE_CUT_" + Center.PointCode, "False"));
		//	}
		//	set
		//	{
		//		TextSettings.Set("MUTE_CUT_" + Center.PointCode, value.ToString());
		//	}
		//}

		//public bool HasSwitchPowerProblem
		//{
		//	get
		//	{
		//		return bool.Parse(Setting.Get(Setting.NEW_POWER_ALARM_INSERTED + Center.PointCode, "false"));
		//	}
		//}

		//private static string[] newAlarmCenters;
		//public static string NewAlarmCenters
		//{
		//    set
		//    {
		//        newAlarmCenters = null;
		//        newAlarmCenters = value.Split('_');
		//    }
		//}

		public AlarmSeverities HasNewAlarm;
        //{
        //    get
        //    {
        //        try
        //        {
        //            return (AlarmSeverities)Enum.Parse(typeof(AlarmSeverities), Setting.Get(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString()));
        //        }
        //        catch
        //        {
        //            return AlarmSeverities.None;
        //        }
        //    }
        //}

  //      private string _NewCircuitOpenAlarm = "";
		//public string NewCircuitOpenAlarm
		//{
		//	set
		//	{
		//		_NewCircuitOpenAlarm = value;
		//		string OldCircuitOpenAlarm = Setting.Get(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, "");
		//		newCircuitOpen = value.Split('_').Where(s => s != "" && !OldCircuitOpenAlarm.Contains(s)).ToList();
		//	}
		//}

		//private string _NewCircuitShortAlarm = "";
		//public string NewCircuitShortAlarm
		//{
		//	set
		//	{

		//		_NewCircuitShortAlarm = value;
		//		string OldCircuitShortAlarm = Setting.Get(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, "");
		//		newCircuitShort = value.Split('_').Where(s => s != "" && !OldCircuitShortAlarm.Contains(s)).ToList();
		//	}
		//}

		//public List<string> newCircuitOpen;
		//public List<string> newCircuitShort;

		//{
		//    get
		//    {
		//        try
		//        {
		//            return bool.Parse(Setting.Get(Setting.NEW_CIRCUIT_ALARM_INSERTED + Center.PointCode, "False"));
		//        }
		//        catch
		//        {
		//            return false;
		//        }
		//    }
		//}

		public CenterSpot(Center   center)
		{
			InitializeComponent();


			this.Center = center;
			this.DataContext = this;
			this.timer = new Timer(500);
			this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

			//if ((vpn.hasVPN(Center.DisplayName)) && (vpn.IsConnect(Center.DisplayName)))
			//    connectVPN.IsChecked = true;
			//else
			//    connectVPN.IsChecked = false;

		}

		Color _switchColor = Colors.Gray;
		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			timer.Stop();

			Dispatcher.Invoke(new Action(delegate()
			{
				if (HasNewAlarm != AlarmSeverities.None && switchBorder.Opacity == 1)
					switchBorder.Opacity = .2;
				else
					switchBorder.Opacity = 1;

				//if ((newCircuitShort.Count > 0 || newCircuitOpen.Count > 0) && circuitBorder.Opacity == 1)
					//circuitBorder.Opacity = .2;
				//else
					//circuitBorder.Opacity = 1;
				//if (SwitchColor.Color != NormalColor)
				timer.Start();
			}), System.Windows.Threading.DispatcherPriority.Normal);
			System.Windows.Forms.Application.DoEvents();

		}

		public Center Center
		{
			get;
			set;
		}

		public bool IsMoving
		{
			get;
			set;
		}
		private bool hasSensorPart = true;
		private bool hasSwitchPart = true;
		//private bool hasCircuitPart = true;
		//private bool hasPowerPart = false;


		private void HideSwitchPart()
		{
			//sensorBorder.CornerRadius = new CornerRadius(powerBorder.CornerRadius.BottomLeft);
			// {
			containerGrid.RowDefinitions[0].Height = new GridLength(0);
			//  }
		}

		private void HideSensorPart()
		{
			switchBorder.CornerRadius = new CornerRadius(switchBorder.CornerRadius.TopLeft);

			containerGrid.RowDefinitions[1].Height = new GridLength(0);
			containerGrid.RowDefinitions[2].Height = new GridLength(0);  // power

		}

		//private void HideCircuitPart()
		//{
			//switchBorder.CornerRadius = new CornerRadius(switchBorder.CornerRadius.TopLeft);

			//containerGrid.RowDefinitions[3].Height = new GridLength(0);
		//}



		public void CheckConnectivity( List<ServiceState> availableServices, List<ServiceState> inactiveServices )
		{


			try
			{
				HasNewAlarm = (AlarmSeverities)Enum.Parse(typeof(AlarmSeverities), Setting.Get(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString()));
			}
			catch (Exception ex)
			{
				HasNewAlarm = AlarmSeverities.None;
				Logger.Write(ex);
			}
			//NewCircuitOpenAlarm = Setting.Get(Setting.NEW_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, "");
			//NewCircuitShortAlarm = Setting.Get(Setting.NEW_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, "");
			var myServices = availableServices.Where(s => s.CenterID == Center.ID);
			var myInActiveServices = inactiveServices.Where(s => s.CenterID == Center.ID);

			hasSensorPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService);
			//hasCircuitPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.CircuitService);
			hasSwitchPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);

			IsSensorServiceWorking = hasSensorPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService); // || s.ServiceType == ServiceTypes.CircuitService);
			IsSwitchServiceWorking = hasSwitchPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);
			//IsCircuitServiceWorking = hasCircuitPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.CircuitService);


			//if (myActiveStates.Count() != 0)
			//serviceController = myActiveStates.FirstOrDefault().Controller;

			if (!hasSensorPart && !hasSwitchPart /*&& !hasCircuitPart*/)
			{
				this.Visibility = System.Windows.Visibility.Hidden;
			}
			if (!hasSensorPart)
				HideSensorPart();
			if (!hasSwitchPart)
				HideSwitchPart();
			//if (!hasCircuitPart)
				//HideCircuitPart();


			if (!hasSensorPart /*&& !hasCircuitPart*/)
				containerGrid.RowDefinitions[0].Height = new GridLength(50);
			else if (!hasSwitchPart /*&& !hasCircuitPart && !hasPowerPart*/)
				containerGrid.RowDefinitions[1].Height = new GridLength(50);

			//set mute bottom visibility
			this.muteMenuItem.IsChecked = IsMute;
			this.muteCriticalMenuItem.IsChecked = IsMuteCritical;
			this.muteMajorMenuItem.IsChecked = IsMuteMajor;
			this.muteMinorMenuItem.IsChecked = IsMuteMinor;

			this.muteSensorMenuItem.IsChecked = IsMuteSensor;

			if (IsMute)
			{
				muteAllButton.Visibility = System.Windows.Visibility.Visible;
				muteButton.Visibility = System.Windows.Visibility.Hidden;
			}
			else if (IsMuteCritical)
			{
				muteButton.Visibility = System.Windows.Visibility.Visible;
				muteButton.Severity = AlarmSeverities.Critical;
				muteAllButton.Visibility = System.Windows.Visibility.Hidden;
			}
			else if (IsMuteMajor)
			{
				muteButton.Visibility = System.Windows.Visibility.Visible;
				muteButton.Severity = AlarmSeverities.Major;
				muteAllButton.Visibility = System.Windows.Visibility.Hidden;
			}
			else if (IsMuteMinor)
			{
				muteButton.Visibility = System.Windows.Visibility.Visible;
				muteButton.Severity = AlarmSeverities.Minor;
				muteAllButton.Visibility = System.Windows.Visibility.Hidden;
			}
			else
			{
				muteButton.Visibility = System.Windows.Visibility.Hidden;
				muteAllButton.Visibility = System.Windows.Visibility.Hidden;
			}

		}


		public bool CheckSensorAlarm( IEnumerable<Tuple<Sensor, Guid, double?>> sensorCache  )
		{


			try
			{
				if (IsSensorServiceWorking)
				{
					var mySensors = sensorCache.Select(t => new
					{
						Sensor = t.Item1,
						CenterID = t.Item2,
						RecentVal = t.Item3
					}).Where(s => s.CenterID == Center.ID && ((SensorTypes)s.Sensor.TypeID == SensorTypes.Humidity || (SensorTypes)s.Sensor.TypeID == SensorTypes.Temperature));

					bool hasSensorAlarm = mySensors.Any(s => s.RecentVal > s.Sensor.Max || s.RecentVal < s.Sensor.Min);
					if (hasSensorAlarm)
					{
						SensorColor.Color = Colors.Red;
						sensorLabel.Foreground = Brushes.Black;
					}
					else
					{
						SensorColor.Color = NormalColor;
						sensorLabel.Foreground = Brushes.Black;
					}
					return hasSensorAlarm;
				}
				else
				{
					SensorColor.Color = Colors.Black;
					sensorLabel.Foreground = Brushes.White;
					return false; //when sensor don't add state because not running on server.
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
			return false;
		}

		//public bool CheckPowerAlarm( IEnumerable<Tuple<Sensor, Guid, double?>> sensorCache )
		//{


		//	try
		//	{
		//		if (IsSensorServiceWorking)
		//		{
		//			var mySensors = sensorCache.Select(t => new
		//			{
		//				Sensor = t.Item1,
		//				CenterID = t.Item2,
		//				RecentVal = t.Item3
		//			}).Where(s => s.CenterID == Center.ID && (SensorTypes)s.Sensor.TypeID == SensorTypes.POWER);

		//			if (mySensors.Any())
		//			{
		//				hasPowerPart = true;
		//				containerGrid.RowDefinitions[2].Height = new GridLength(25);
		//			}
		//			else
		//			{
		//				hasPowerPart = false;
		//				containerGrid.RowDefinitions[2].Height = new GridLength(0);
		//			}


		//			if (mySensors.Any(a => a.RecentVal == 1))
		//			{
		//				powerColor.Color = Colors.Red;
		//				powerLabel.Foreground = Brushes.Black;
		//				return true;
		//			}
		//			else
		//			{
		//				powerColor.Color = NormalColor;
		//				powerLabel.Foreground = Brushes.Black;
		//				return false;
		//			}
		//		}
		//		else
		//		{
		//			powerColor.Color = Colors.Black;
		//			powerLabel.Foreground = Brushes.White;
		//			return false; //when sensor don't add state because not running on server.
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.Write(ex);
		//	}
		//	return false;
		//}


		List<string> popupCenters = new List<string>();
		//static double topPosition = 20;
		//public bool CheckCircuitAlarm(IEnumerable<Tuple<Guid, byte>> circuitCache)
		//{
  //          return false;
		//	//try
		//	//{

		//	//	if (IsCircuitServiceWorking)
		//	//	{
		//	//		var alarm = circuitCache.FirstOrDefault(a => a.Item1 == Center.ID);
		//	//		var cirsev = AlarmSeverities.None;
		//	//		if (alarm == null)
		//	//		{
		//	//			cirsev = AlarmSeverities.None;
		//	//		}
		//	//		else
		//	//		{
		//	//			if (alarm.Item2 == 6)
		//	//				cirsev = AlarmSeverities.CircuitOpen;
		//	//			else if (alarm.Item2 == 7)
		//	//				cirsev = AlarmSeverities.CircuitShort;
		//	//			else
		//	//				cirsev = (AlarmSeverities)alarm.Item2;

		//	//			if (newCircuitOpen.Count > 0)
		//	//				cirsev = AlarmSeverities.CircuitOpen;
		//	//			else if (newCircuitShort.Count > 0)
		//	//				cirsev = AlarmSeverities.CircuitShort;
		//	//		}
		//	//		//var mySensors = circuitCache.Select(t => new
		//	//		//{
		//	//		//    CenterID = t.Item1,
		//	//		//    RecentVal = t.Item2
		//	//		//}).Where(s => s.CenterID == Center.ID); // && s.Sensor.SensorType == SensorTypes.Circuit);


		//	//		//-------------------

		//	//		//if (mySensors.Any(a => a.RecentVal == (byte)CircuitEnum.OpenCircuit))
		//	//		if (cirsev == AlarmSeverities.CircuitOpen)
		//	//		{
		//	//			circuitColor.Color = Colors.Red;
		//	//			CircuitLabel.Foreground = Brushes.Black;


		//	//			double leftPosition = 800;


		//	//			{
		//	//				//foreach (string item in newCircuitOpen)
		//	//				{
		//	//					//int mudole = (int.Parse(item)) - 100;
		//	//					if (newCircuitOpen.Count == 0 && popupCenters.Contains(Center.PointCode))
		//	//						popupCenters.Remove(Center.PointCode);

		//	//					if (newCircuitOpen.Count > 0 && popupCenters.Contains(Center.PointCode) == false)
		//	//					{
		//	//						popupCenters.Add(Center.PointCode);
		//	//						CircuitDialogWindow dialog = new CircuitDialogWindow(Center, newCircuitOpen);
		//	//						dialog.WindowStyle = System.Windows.WindowStyle.ToolWindow;
		//	//						dialog.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
		//	//						dialog.Top = topPosition;
		//	//						dialog.Left = leftPosition;
		//	//						dialog.Topmost = true;
		//	//						topPosition += 70;
		//	//						dialog.Show();
		//	//					}


		//	//				}
		//	//			}
		//	//			return newCircuitOpen.Count > 0;
		//	//		}
		//	//		else if (cirsev == AlarmSeverities.CircuitShort) // (mySensors.Any(a => a.RecentVal == (byte)CircuitEnum.ShortCircuit))
		//	//		{
		//	//			circuitColor.Color = Colors.Yellow;
		//	//			CircuitLabel.Foreground = Brushes.Black;

		//	//			return newCircuitShort.Count > 0;
		//	//		}
		//	//		else
		//	//		{
		//	//			circuitColor.Color = NormalColor;
		//	//			CircuitLabel.Foreground = Brushes.Black;
		//	//			newCircuitShort.Clear();
		//	//			newCircuitOpen.Clear();
		//	//			Setting.Set(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, _NewCircuitOpenAlarm);
		//	//			Setting.Set(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, _NewCircuitShortAlarm);

		//	//			return false;
		//	//		}


		//	//		//--------------------

		//	//	}

		//	//	else
		//	//	{
		//	//		circuitColor.Color = Colors.Black;
		//	//		CircuitLabel.Foreground = Brushes.White;
		//	//		return false; //when sensor don't add state because not running on server.
		//	//	}
		//	//}
		//	//catch (Exception ex)
		//	//{
		//	//	Logger.Write(ex);
		//	//}
		//	//return false;
		//}

		public void CheckSwitchAlarm( List<Tuple<Guid, AlarmSeverities>> alarmCache)
		{


			if (IsSwitchServiceWorking)
			{
				try
				{
					//powerButton.IsEnabled = HasSwitchPowerProblem;

					var alarm = alarmCache.SingleOrDefault(a => a.Item1 == Center.ID);
					if (alarm == null)
					{
						Severity = AlarmSeverities.None;
					}
					else
					{
						Severity = alarm.Item2;
					}
				}
				catch (Exception ex)
				{
					Logger.Write(ex);
				}
			}
			UpdateSwitchColor();
			if (HasNewAlarm != AlarmSeverities.None || switchBorder.Opacity != 1)
				timer.Start();
			//else if (newCircuitOpen.Count > 0 || newCircuitShort.Count > 0 || circuitBorder.Opacity != 1)
				//timer.Start();
			else
			{
				timer.Stop();
				//switchBorder.Opacity = 1;
			}

		}

		private AlarmSeverities severity;
		public AlarmSeverities Severity
		{
			get
			{

				AlarmSeverities ts = severity;
				if (HasNewAlarm != AlarmSeverities.None)
					ts = HasNewAlarm;

				return ts;
			}
			set
			{
				severity = value;
				if (severity == AlarmSeverities.None)
				{
					HasNewAlarm = AlarmSeverities.None;
					Setting.Set(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString());
				}
			}
		}

		private void UpdateSwitchColor()
		{
			if (IsSwitchServiceWorking)
			{
				switch (Severity)
				{
					case AlarmSeverities.Critical:
						SwitchColor.Color = Colors.Red;
						break;
					case AlarmSeverities.Major:
						SwitchColor.Color = Colors.Orange;
						break;
					case AlarmSeverities.Minor:
						SwitchColor.Color = Colors.Yellow;
						break;
					default:
						SwitchColor.Color = NormalColor;
						break;
				}
				switchNameLabel.Foreground = Brushes.Black;
			}
			else
			{
				SwitchColor.Color = Colors.Black;
				switchNameLabel.Foreground = Brushes.White;
			}
		}

		//private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		//{

		//}

		private void connectMenuItem_Click(object sender, RoutedEventArgs e)
		{
			//if (serviceController  != null)
			//{
			//    if (serviceController.Status == ServiceControllerStatus.Running)
			//    {
			//        serviceController.Stop();
			//        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
			//    }
			//    serviceController.Start(new string[] { User.Current.UserName, User.Current.Password });
			//    serviceController.WaitForStatus(ServiceControllerStatus.Running);


			//    Center.Connect(User.Current.UserName, User.Current.Password);
			//}

			Center.Connect();

			

			
		}

		private void OnMuteCenterClick()
		{
			if (MuteCenterClick != null)
				MuteCenterClick(this, null);
		}

		private void muteMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (muteMenuItem.IsChecked)
			{
				IsMute = true;
			}
			else
			{
				IsMute = false;
			}
			OnMuteCenterClick();
		}

		private void muteCriticalMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (muteCriticalMenuItem.IsChecked)
			{
				IsMuteCritical = true;
			}
			else
			{
				IsMuteCritical = false;
			}
			OnMuteCenterClick();
		}

		private void muteMajorMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (muteMajorMenuItem.IsChecked)
			{
				IsMuteMajor = true;
			}
			else
			{
				IsMuteMajor = false;
			}
			OnMuteCenterClick();
		}

		private void muteMinorMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (muteMinorMenuItem.IsChecked)
			{
				IsMuteMinor = true;
			}
			else
			{
				IsMuteMinor = false;
			}
			OnMuteCenterClick();
		}

		private void Switch_click(object sender, MouseButtonEventArgs e)
		{
			if (Click != null && !IsMoving)
			{
				Click(this, e);
			}
		}

		//private void Power_click(object sender, MouseButtonEventArgs e)
		//{
		//	if (PowerClick != null && !IsMoving)
		//	{
		//		PowerClick(this, e);
		//	}
		//}

		//private void enablePowerPanelMenuItem_Click(object sender, RoutedEventArgs e)
		//{
		//    //if (enablePowerPanelMenuItem.IsChecked)
		//    //{
		//    //    IsPowerPanelEnable = true;
		//    //}
		//    //else
		//    //{
		//    //    IsPowerPanelEnable = false;
		//    //}
		//    //CheckPowerPanelVisibility();
		//}

		//private void Circuit_click(object sender, MouseButtonEventArgs e)
		//{
		//	//if (CircuitClick != null && !IsMoving)
		//	//{
		//		//CircuitClick(this, e);
		//	//}
		//}

		private void muteSensorMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (muteSensorMenuItem.IsChecked)
			{
				IsMuteSensor = true;
			}
			else
			{
				IsMuteSensor = false;
			}
			OnMuteCenterClick();
		}

		//private void mutePowerMenuItem_Click(object sender, RoutedEventArgs e)
		//{
		//	if (mutePowerMenuItem.IsChecked)
		//	{
		//		IsMutePower = true;
		//	}
		//	else
		//	{
		//		IsMutePower = false;
		//	}
		//	OnMuteCenterClick();
		//}

		//private void muteCircuitMenuItem_Click(object sender, RoutedEventArgs e)
		//{
		//	//if (muteCircuitMenuItem.IsChecked)
		//	//{
		//		//IsMuteCircuit = true;
		//	//}
		//	//else
		//	//{
		//		//IsMuteCircuit = false;
		//	//}
		//	//OnMuteCenterClick();
		//}

		private void connectVPN_Click(object sender, RoutedEventArgs e)
		{
			//if (connectVPN.IsChecked)
			//{
			//	if (vpn.hasVPN(Center.DisplayName))
			//	{
			//		vpn.DisconnectAll();
			//		vpn.Connect(Center.DisplayName, Center.UserName, Center.Password);
			//	}
			//	else
			//	{
			//		vpn.Create(Center.DisplayName, Center.IPAddress);
			//		vpn.DisconnectAll();
			//		vpn.Connect(Center.DisplayName, Center.UserName, Center.Password);



			//		var db = DB.Instance;

			//		// user log
			//		UserLog.Log(db, ActionType.ConnectToVPN, Center.IPAddress, "");

			//	}

			//}
			//else
			//{
			//	if ((vpn.hasVPN(Center.DisplayName)) && (vpn.IsConnect(Center.DisplayName)))
			//		vpn.Disconnect(Center.DisplayName);
			//}
		}


		private void ContextMenu_Opened(object sender, RoutedEventArgs e)
		{
			//if ((vpn.IsConnect(Center.DisplayName)))
			//	connectVPN.IsChecked = true;
			//else
			//	connectVPN.IsChecked = false;
		}

        //public event RoutedEventHandler Click;
        //public event RoutedEventHandler PowerClick;
        //public event RoutedEventHandler CircuitClick;
        //public event EventHandler MuteCenterClick;

        //private bool IsSensorServiceWorking, IsSwitchServiceWorking, IsCircuitServiceWorking;
        //private Color NormalColor = Color.FromArgb(0xFF, 0x7A, 0xC7, 0x00);
        //private Timer timer = new Timer();
        //VPNHelper vpn = new VPNHelper();
        ////ServiceController serviceController;

        //public bool IsMute
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_ALL_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_ALL_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMuteCritical
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_CRI_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_CRI_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMuteMajor
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_MAJ_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_MAJ_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMuteMinor
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_MIN_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_MIN_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMuteSensor
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_SEN_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_SEN_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMutePower
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_POW_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_POW_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool IsMuteCircuit
        //{
        //    get
        //    {
        //        return bool.Parse(TextSettings.Get("MUTE_CUT_" + Center.PointCode, "False"));
        //    }
        //    set
        //    {
        //        TextSettings.Set("MUTE_CUT_" + Center.PointCode, value.ToString());
        //    }
        //}

        //public bool HasSwitchPowerProblem
        //{
        //    get
        //    {
        //        return bool.Parse(Setting.Get(Setting.NEW_POWER_ALARM_INSERTED + Center.PointCode, "false"));
        //    }
        //}

        ////private static string[] newAlarmCenters;
        ////public static string NewAlarmCenters
        ////{
        ////    set
        ////    {
        ////        newAlarmCenters = null;
        ////        newAlarmCenters = value.Split('_');
        ////    }
        ////}

        //public AlarmSeverities HasNewAlarm;
        ////{
        ////    get
        ////    {
        ////        try
        ////        {
        ////            return (AlarmSeverities)Enum.Parse(typeof(AlarmSeverities), Setting.Get(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString()));
        ////        }
        ////        catch
        ////        {
        ////            return AlarmSeverities.None;
        ////        }
        ////    }
        ////}
        //private string _NewCircuitOpenAlarm = "";
        //public string NewCircuitOpenAlarm
        //{
        //    set
        //    {
        //        _NewCircuitOpenAlarm = value;
        //        string OldCircuitOpenAlarm = Setting.Get(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, "");
        //        newCircuitOpen = value.Split('_').Where(s => s != "" && !OldCircuitOpenAlarm.Contains(s)).ToList();
        //    }
        //}

        //private string _NewCircuitShortAlarm = "";
        //public string NewCircuitShortAlarm
        //{
        //    set
        //    {

        //        _NewCircuitShortAlarm = value;
        //        string OldCircuitShortAlarm = Setting.Get(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, "");
        //        newCircuitShort = value.Split('_').Where(s => s != "" && !OldCircuitShortAlarm.Contains(s)).ToList();
        //    }
        //}

        //public List<string> newCircuitOpen; 
        //public List<string> newCircuitShort;

        ////{
        ////    get
        ////    {
        ////        try
        ////        {
        ////            return bool.Parse(Setting.Get(Setting.NEW_CIRCUIT_ALARM_INSERTED + Center.PointCode, "False"));
        ////        }
        ////        catch
        ////        {
        ////            return false;
        ////        }
        ////    }
        ////}

        //public CenterSpot(Center center)
        //{
        //    InitializeComponent();


        //    this.Center = center;
        //    this.DataContext = this;
        //    this.timer = new Timer(500);
        //    this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

        //    //if ((vpn.hasVPN(Center.DisplayName)) && (vpn.IsConnect(Center.DisplayName)))
        //    //    connectVPN.IsChecked = true;
        //    //else
        //    //    connectVPN.IsChecked = false;

        //}

        //Color _switchColor = Colors.Gray;
        //void timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    timer.Stop();

        //    Dispatcher.Invoke(new Action(delegate()
        //    {
        //            if (HasNewAlarm != AlarmSeverities.None && switchBorder.Opacity == 1)
        //            switchBorder.Opacity = .2;
        //        else
        //            switchBorder.Opacity = 1;

        //            if ((newCircuitShort.Count > 0 || newCircuitOpen.Count > 0)  && circuitBorder.Opacity == 1)
        //                circuitBorder.Opacity = .2;
        //            else
        //                circuitBorder.Opacity = 1;
        //        //if (SwitchColor.Color != NormalColor)
        //        timer.Start();
        //    }), System.Windows.Threading.DispatcherPriority.Normal);
        //    System.Windows.Forms.Application.DoEvents();

        //}

        //public Center Center
        //{
        //    get;
        //    set;
        //}

        //public bool IsMoving
        //{
        //    get;
        //    set;
        //}
        //private bool hasSensorPart = true;
        //private bool hasSwitchPart = true;
        //private bool hasCircuitPart = true;
        //private bool hasPowerPart = false;


        //private void HideSwitchPart()
        //{
        //    //sensorBorder.CornerRadius = new CornerRadius(powerBorder.CornerRadius.BottomLeft);
        //   // {
        //        containerGrid.RowDefinitions[0].Height = new GridLength(0);
        //  //  }
        //}

        //private void HideSensorPart()
        //{
        //    switchBorder.CornerRadius = new CornerRadius(switchBorder.CornerRadius.TopLeft);

        //    containerGrid.RowDefinitions[1].Height = new GridLength(0);
        //    containerGrid.RowDefinitions[2].Height = new GridLength(0);  // power

        //}

        //private void HideCircuitPart()
        //{
        //    //switchBorder.CornerRadius = new CornerRadius(switchBorder.CornerRadius.TopLeft);

        //    containerGrid.RowDefinitions[3].Height = new GridLength(0);
        //}



        //public void CheckConnectivity(/* List<ServiceState> availableServices, List<ServiceState> inactiveServices */ List<WCFServiceReference.ServiceState> availableServices, List<WCFServiceReference.ServiceState> inactiveServices)
        //{


        //    try
        //    {
        //        HasNewAlarm = (AlarmSeverities)Enum.Parse(typeof(AlarmSeverities), Setting.Get(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString()));
        //    }
        //    catch(Exception ex)
        //    {
        //        HasNewAlarm = AlarmSeverities.None;
        //        Logger.Write(ex);
        //    }
        //    NewCircuitOpenAlarm = Setting.Get(Setting.NEW_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, "");
        //    NewCircuitShortAlarm = Setting.Get(Setting.NEW_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, "");
        //    var myServices = availableServices.Where(s => s.CenterID == Center.ID);
        //    var myInActiveServices = inactiveServices.Where(s=> s.CenterID == Center.ID);

        //    hasSensorPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService);
        //    hasCircuitPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.CircuitService);
        //    hasSwitchPart = myServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);

        //    IsSensorServiceWorking = hasSensorPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService); // || s.ServiceType == ServiceTypes.CircuitService);
        //    IsSwitchServiceWorking = hasSwitchPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);
        //    IsCircuitServiceWorking = hasCircuitPart && !myInActiveServices.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.CircuitService);


        //    //if (myActiveStates.Count() != 0)
        //    //serviceController = myActiveStates.FirstOrDefault().Controller;

        //    if (!hasSensorPart && !hasSwitchPart && !hasCircuitPart)
        //    {
        //        this.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    if (!hasSensorPart)
        //        HideSensorPart();
        //    if (!hasSwitchPart)
        //        HideSwitchPart();
        //    if (!hasCircuitPart)
        //        HideCircuitPart();


        //    if (!hasSensorPart && !hasCircuitPart)
        //        containerGrid.RowDefinitions[0].Height = new GridLength(50);
        //    else if (!hasSwitchPart && !hasCircuitPart && !hasPowerPart)
        //        containerGrid.RowDefinitions[1].Height = new GridLength(50);

        //    //set mute bottom visibility
        //    this.muteMenuItem.IsChecked = IsMute;
        //    this.muteCriticalMenuItem.IsChecked = IsMuteCritical;
        //    this.muteMajorMenuItem.IsChecked = IsMuteMajor;
        //    this.muteMinorMenuItem.IsChecked = IsMuteMinor;

        //    this.muteSensorMenuItem.IsChecked = IsMuteSensor;

        //    if (IsMute)
        //    {
        //        muteAllButton.Visibility = System.Windows.Visibility.Visible;
        //        muteButton.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    else if (IsMuteCritical)
        //    {
        //        muteButton.Visibility = System.Windows.Visibility.Visible;
        //        muteButton.Severity = AlarmSeverities.Critical;
        //        muteAllButton.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    else if (IsMuteMajor)
        //    {
        //        muteButton.Visibility = System.Windows.Visibility.Visible;
        //        muteButton.Severity = AlarmSeverities.Major;
        //        muteAllButton.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    else if (IsMuteMinor)
        //    {
        //        muteButton.Visibility = System.Windows.Visibility.Visible;
        //        muteButton.Severity = AlarmSeverities.Minor;
        //        muteAllButton.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    else
        //    {
        //        muteButton.Visibility = System.Windows.Visibility.Hidden;
        //        muteAllButton.Visibility = System.Windows.Visibility.Hidden;
        //    }

        //}

        //public void CheckSwitchAlarm(/*TMN.ServiceModel.AlarmRegionModel model */ List<Tuple<Guid, AlarmSeverities>> alarmCache )
        //{

        //    if (IsSwitchServiceWorking)
        //    {
        //        try
        //        {
        //            powerButton.IsEnabled = HasSwitchPowerProblem;

        //            var alarm = alarmCache.SingleOrDefault(a => a.Item1 == Center.ID);
        //            if (alarm == null)
        //            {
        //                Severity = AlarmSeverities.None;
        //            }
        //            else
        //            {
        //                Severity = alarm.Item2;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.Write(ex);
        //        }
        //    }
        //    UpdateSwitchColor();
        //    if (HasNewAlarm != AlarmSeverities.None || switchBorder.Opacity != 1)
        //        timer.Start();
        //    else if (newCircuitOpen.Count > 0 || newCircuitShort.Count > 0 || circuitBorder.Opacity != 1)
        //        timer.Start();
        //    else
        //    {
        //        timer.Stop();
        //        //switchBorder.Opacity = 1;
        //    }

        //}

        //public bool CheckSensorAlarm(/* IEnumerable<Tuple<Sensor, Guid, double?>> sensorCache */ List<Tuple<WCFServiceReference.Sensor, Guid, double?>> sensorCache)
        //{


        //    try
        //    {
        //        if (IsSensorServiceWorking)
        //        {
        //            var mySensors = sensorCache.Select(t => new
        //            {
        //                Sensor = t.Item1,
        //                CenterID = t.Item2,
        //                RecentVal = t.Item3
        //            }).Where(s => s.CenterID == Center.ID && ((SensorTypes)s.Sensor.TypeID == SensorTypes.Humidity || (SensorTypes)s.Sensor.TypeID == SensorTypes.Temperature));

        //            bool hasSensorAlarm = mySensors.Any(s => s.RecentVal > s.Sensor.Max || s.RecentVal < s.Sensor.Min);
        //            if (hasSensorAlarm)
        //            {
        //                SensorColor.Color = Colors.Red;
        //                sensorLabel.Foreground = Brushes.Black;
        //            }
        //            else
        //            {
        //                SensorColor.Color = NormalColor;
        //                sensorLabel.Foreground = Brushes.Black;
        //            }
        //            return hasSensorAlarm;
        //        }
        //        else
        //        {
        //            SensorColor.Color = Colors.Black;
        //            sensorLabel.Foreground = Brushes.White;
        //            return false; //when sensor don't add state because not running on server.
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex);
        //    }
        //    return false;
        //}

        //public bool CheckPowerAlarm(/* IEnumerable<Tuple<Sensor, Guid, double?>> sensorCache */List<Tuple<WCFServiceReference.Sensor, Guid, double?>> sensorCache)
        //{


        //    try
        //    {
        //        if (IsSensorServiceWorking)
        //        {
        //            var mySensors = sensorCache.Select(t => new
        //            {
        //                Sensor = t.Item1,
        //                CenterID = t.Item2,
        //                RecentVal = t.Item3
        //            }).Where(s => s.CenterID == Center.ID && (SensorTypes)s.Sensor.TypeID == SensorTypes.POWER);

        //            if (mySensors.Any())
        //            {
        //                hasPowerPart = true;
        //                containerGrid.RowDefinitions[2].Height = new GridLength(25);
        //            }
        //            else
        //            {
        //                hasPowerPart = false;
        //                containerGrid.RowDefinitions[2].Height = new GridLength(0);
        //            }


        //            if (mySensors.Any(a => a.RecentVal == 1))
        //            {
        //                powerColor.Color = Colors.Red;
        //                powerLabel.Foreground = Brushes.Black;
        //                return true;
        //            }
        //            else
        //            {
        //                powerColor.Color = NormalColor;
        //                powerLabel.Foreground = Brushes.Black;
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            powerColor.Color = Colors.Black;
        //            powerLabel.Foreground = Brushes.White;
        //            return false; //when sensor don't add state because not running on server.
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex);
        //    }
        //    return false;
        //}


        //List<string> popupCenters = new List<string>();
        //static double topPosition = 20;
        //public bool CheckCircuitAlarm(List<Tuple<Guid, byte>> cirCatch) ///*TMN.ServiceModel.AlarmRegionModel model */ IEnumerable<Tuple<Guid?, double?>> circuitCache )
        //{

        //    try
        //    {

        //        if (IsCircuitServiceWorking)
        //        {
        //            var alarm = cirCatch.FirstOrDefault(a => a.Item1 == Center.ID );
        //            var cirsev = AlarmSeverities.None;
        //            if (alarm == null)
        //            {
        //                cirsev = AlarmSeverities.None;
        //            }
        //            else
        //            {
        //                if (alarm.Item2 == 6)
        //                    cirsev = AlarmSeverities.CircuitOpen;
        //                else if (alarm.Item2 == 7)
        //                    cirsev = AlarmSeverities.CircuitShort;
        //                else
        //                    cirsev = (AlarmSeverities)alarm.Item2;

        //                if (newCircuitOpen.Count > 0)
        //                    cirsev = AlarmSeverities.CircuitOpen;
        //                else if (newCircuitShort.Count > 0)
        //                    cirsev = AlarmSeverities.CircuitShort;
        //            }
        //            //var mySensors = circuitCache.Select(t => new
        //            //{
        //            //    CenterID = t.Item1,
        //            //    RecentVal = t.Item2
        //            //}).Where(s => s.CenterID == Center.ID); // && s.Sensor.SensorType == SensorTypes.Circuit);


        //            //-------------------

        //            //if (mySensors.Any(a => a.RecentVal == (byte)CircuitEnum.OpenCircuit))
        //            if (cirsev == AlarmSeverities.CircuitOpen)
        //            {
        //                circuitColor.Color = Colors.Red;
        //                CircuitLabel.Foreground = Brushes.Black;


        //                double leftPosition = 800;


        //                {
        //                    //foreach (string item in newCircuitOpen)
        //                    {
        //                        //int mudole = (int.Parse(item)) - 100;
        //                        if (newCircuitOpen.Count == 0 && popupCenters.Contains(Center.PointCode))
        //                            popupCenters.Remove(Center.PointCode);

        //                        if (newCircuitOpen.Count > 0 && popupCenters.Contains(Center.PointCode) == false)
        //                        {
        //                            popupCenters.Add(Center.PointCode);
        //                            CircuitDialogWindow dialog = new CircuitDialogWindow(Center, newCircuitOpen);
        //                            dialog.WindowStyle = System.Windows.WindowStyle.ToolWindow;
        //                            dialog.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
        //                            dialog.Top = topPosition;
        //                            dialog.Left = leftPosition;
        //                            dialog.Topmost = true;
        //                            topPosition += 70;
        //                            dialog.Show();
        //                        }


        //                    }
        //                }
        //                return newCircuitOpen.Count > 0;
        //            }
        //            else if (cirsev == AlarmSeverities.CircuitShort) // (mySensors.Any(a => a.RecentVal == (byte)CircuitEnum.ShortCircuit))
        //            {
        //                circuitColor.Color = Colors.Yellow;
        //                CircuitLabel.Foreground = Brushes.Black;

        //                return newCircuitShort.Count > 0;
        //            }
        //            else
        //            {
        //                circuitColor.Color = NormalColor;
        //                CircuitLabel.Foreground = Brushes.Black;
        //                newCircuitShort.Clear();
        //                newCircuitOpen.Clear();
        //                Setting.Set(Setting.OLD_CIRCUIT_OPEN_ALARM_INSERTED + Center.PointCode, _NewCircuitOpenAlarm);
        //                Setting.Set(Setting.OLD_CIRCUIT_SHORT_ALARM_INSERTED + Center.PointCode, _NewCircuitShortAlarm);

        //                return false;
        //            }


        //            //--------------------

        //        }

        //        else
        //        {
        //            circuitColor.Color = Colors.Black;
        //            CircuitLabel.Foreground = Brushes.White;
        //            return false; //when sensor don't add state because not running on server.
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(ex);
        //    }
        //    return false;
        //}



        //private AlarmSeverities severity;
        //public AlarmSeverities Severity
        //{
        //    get
        //    {

        //        AlarmSeverities ts = severity;
        //        if (HasNewAlarm != AlarmSeverities.None)
        //            ts = HasNewAlarm;

        //        return ts;
        //    }
        //    set
        //    {
        //        severity = value;
        //        if (severity == AlarmSeverities.None)
        //        {
        //            HasNewAlarm = AlarmSeverities.None;
        //            Setting.Set(Setting.NEW_ALARM_INSERTED + Center.PointCode, AlarmSeverities.None.ToString());
        //        }
        //    }
        //}

        //private void UpdateSwitchColor()
        //{
        //    if (IsSwitchServiceWorking)
        //    {
        //        switch (Severity)
        //        {
        //            case AlarmSeverities.Critical:
        //                SwitchColor.Color = Colors.Red;
        //                break;
        //            case AlarmSeverities.Major:
        //                SwitchColor.Color = Colors.Orange;
        //                break;
        //            case AlarmSeverities.Minor:
        //                SwitchColor.Color = Colors.Yellow;
        //                break;
        //            default:
        //                SwitchColor.Color = NormalColor;
        //                break;
        //        }
        //        switchNameLabel.Foreground = Brushes.Black;
        //    }
        //    else
        //    {
        //        SwitchColor.Color = Colors.Black;
        //        switchNameLabel.Foreground = Brushes.White;
        //    }
        //}

        ////private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        ////{

        ////}

        //private void connectMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    //if (serviceController  != null)
        //    //{
        //    //    if (serviceController.Status == ServiceControllerStatus.Running)
        //    //    {
        //    //        serviceController.Stop();
        //    //        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
        //    //    }
        //    //    serviceController.Start(new string[] { User.Current.UserName, User.Current.Password });
        //    //    serviceController.WaitForStatus(ServiceControllerStatus.Running);


        //    //    Center.Connect(User.Current.UserName, User.Current.Password);
        //    //}

        //    Center.Connect();


        //}

        //private void OnMuteCenterClick()
        //{
        //    if (MuteCenterClick != null)
        //        MuteCenterClick(this, null);
        //}

        //private void muteMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (muteMenuItem.IsChecked)
        //    {
        //        IsMute = true;
        //    }
        //    else
        //    {
        //        IsMute = false;
        //    }
        //    OnMuteCenterClick();
        //}

        //private void muteCriticalMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (muteCriticalMenuItem.IsChecked)
        //    {
        //        IsMuteCritical = true;
        //    }
        //    else
        //    {
        //        IsMuteCritical = false;
        //    }
        //    OnMuteCenterClick();
        //}

        //private void muteMajorMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (muteMajorMenuItem.IsChecked)
        //    {
        //        IsMuteMajor = true;
        //    }
        //    else
        //    {
        //        IsMuteMajor = false;
        //    }
        //    OnMuteCenterClick();
        //}

        //private void muteMinorMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (muteMinorMenuItem.IsChecked)
        //    {
        //        IsMuteMinor = true;
        //    }
        //    else
        //    {
        //        IsMuteMinor = false;
        //    }
        //    OnMuteCenterClick();
        //}

        //private void Switch_click(object sender, MouseButtonEventArgs e)
        //{
        //    if (Click != null && !IsMoving)
        //    {
        //        Click(this, e);
        //    }
        //}

        //private void Power_click(object sender, MouseButtonEventArgs e)
        //{
        //    if (PowerClick != null && !IsMoving)
        //    {
        //        PowerClick(this, e);
        //    }
        //}

        ////private void enablePowerPanelMenuItem_Click(object sender, RoutedEventArgs e)
        ////{
        ////    //if (enablePowerPanelMenuItem.IsChecked)
        ////    //{
        ////    //    IsPowerPanelEnable = true;
        ////    //}
        ////    //else
        ////    //{
        ////    //    IsPowerPanelEnable = false;
        ////    //}
        ////    //CheckPowerPanelVisibility();
        ////}

        //private void Circuit_click(object sender, MouseButtonEventArgs e)
        //{
        //    if (CircuitClick != null && !IsMoving)
        //    {
        //        CircuitClick(this, e);
        //    }
        //}

        //private void muteSensorMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    if (muteSensorMenuItem.IsChecked)
        //    {
        //        IsMuteSensor = true;
        //    }
        //    else
        //    {
        //        IsMuteSensor = false;
        //    }
        //    OnMuteCenterClick();
        //}

        //private void mutePowerMenuItem_Click(object sender, RoutedEventArgs e)
        //{
            //if (mutePowerMenuItem.IsChecked)
            //{
            //    IsMutePower = true;
            //}
            //else
            //{
            //    IsMutePower = false;
            //}
            //OnMuteCenterClick();
        //}

        //private void muteCircuitMenuItem_Click(object sender, RoutedEventArgs e)
        //{
            //if (muteCircuitMenuItem.IsChecked)
            //{
            //    IsMuteCircuit = true;
            //}
            //else
            //{
            //    IsMuteCircuit = false;
            //}
            //OnMuteCenterClick();
        //}

        //private void connectVPN_Click(object sender, RoutedEventArgs e)
        //{
        //    if (connectVPN.IsChecked)
        //    {
        //        if (vpn.hasVPN(Center.DisplayName))
        //        {
        //            vpn.DisconnectAll();
        //            vpn.Connect(Center.DisplayName, Center.UserName, Center.Password);
        //        }
        //        else
        //        {
        //            vpn.Create(Center.DisplayName, Center.IPAddress);
        //            vpn.DisconnectAll();
        //            vpn.Connect(Center.DisplayName, Center.UserName, Center.Password);



        //            var db = DB.Instance;

        //            // user log
        //            UserLog.Log(db, ActionType.ConnectToVPN, Center.IPAddress, "");

        //        }

        //    }
        //    else
        //    {
        //        if ((vpn.hasVPN(Center.DisplayName)) && (vpn.IsConnect(Center.DisplayName)))
        //            vpn.Disconnect(Center.DisplayName);
        //    }
        //}


        //private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        //{
        //    if ((vpn.IsConnect(Center.DisplayName)))
        //        connectVPN.IsChecked = true;
        //    else
        //        connectVPN.IsChecked = false;
        //}
    }
}
