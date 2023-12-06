using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using Enterprise;

namespace TMN.UserControls
{

    public partial class CenterSpotPowerItem : UserControl
    {
        public event RoutedEventHandler Click;
        public event EventHandler MuteCenterClick; 
        private bool IsSensorServiceWorking, IsSwitchServiceWorking;
        private Color NormalColor = Color.FromArgb(0xFF, 0x7A, 0xC7, 0x00);
        //    private Timer flashTimer = new Timer(500);

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

        //private AlarmSeverities _muteSeverity = AlarmSeverities.None;
        //public AlarmSeverities MuteSeverity
        //{
        //    get
        //    {
        //        return _muteSeverity;
        //    }
        //    set
        //    {
        //        _muteSeverity = value;
        //        switch (_muteSeverity)
        //        {
        //            case AlarmSeverities.None:
        //            case AlarmSeverities.Information:
        //                muteBorder.BorderBrush = Brushes.Transparent;
        //                break;
        //            case AlarmSeverities.Critical:
        //                muteBorder.BorderBrush = Brushes.Red;
        //                break;
        //            case AlarmSeverities.Major:
        //                muteBorder.BorderBrush = Brushes.Orange;
        //                break;
        //            case AlarmSeverities.Minor:
        //                muteBorder.BorderBrush = Brushes.Yellow;
        //                break;
        //        }

        //    }
        //}
        /// <summary>
        /// check switch or sensor disconnect duration.
        /// and show disconnect to users after interval determine by user
        /// </summary>
        //private int disconnectCountDown = Setting.Get(Setting.DC_VISIBILITY_INTERVAL, Setting.DEFAULT_DC_VISIBILITY_INTERVAL);
        //private int sensorDCInterval = 0;
        //private int switchDCInterval = 0;

        public CenterSpotPowerItem(Center center)
        {
            InitializeComponent();
            this.Center = center;
            this.DataContext = this;

            //   flashTimer.Elapsed += new ElapsedEventHandler(flashTimer_Elapsed);
           
        }

        //void flashTimer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (jellyButton.DarkColor == Colors.Black)
        //    {
        //        UpdateColor();
        //    }
        //    else
        //    {
        //        jellyButton.DarkColor = Colors.Black;
        //    }
        //}

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

        public void CheckConnectivity(IEnumerable<ServiceState> serviceStates)
        {
            var myServiceStates = serviceStates.Where(s => s.CenterID == Center.ID);
            var myActiveStates = myServiceStates.Where(s => s.IsConnected);
            IsSensorServiceWorking = myActiveStates.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService);
            IsSwitchServiceWorking = myActiveStates.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);
            bool hasSensorPart = myServiceStates.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService);
            if (!hasSensorPart)
                HideSensorPart();
            else
            {
                bool hasSwitchPart = myServiceStates.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.AlarmService);
                if (!hasSwitchPart)
                    HideSwitchPart();
            }

            //set mute bottom visibility
            this.muteButton.Visibility = (IsMute || IsMuteCritical || IsMuteMajor || IsMuteMinor) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            this.muteMenuItem.IsChecked = IsMute;
            this.muteCriticalMenuItem.IsChecked = IsMuteCritical;
            this.muteMajorMenuItem.IsChecked = IsMuteMajor;
            this.muteMinorMenuItem.IsChecked = IsMuteMinor;

            if(IsMute)
                muteBorder.BorderBrush = Brushes.Transparent;
            else if (IsMuteCritical)
                muteBorder.BorderBrush = Brushes.Red;
            else if (IsMuteMajor)
                muteBorder.BorderBrush = Brushes.Orange;
            else if (IsMuteMinor)
                muteBorder.BorderBrush = Brushes.Yellow;
            else
                muteBorder.BorderBrush = Brushes.Transparent;

        }

        private void HideSwitchPart()
        {
            switchBorder.Visibility = switchNameLabel.Visibility = System.Windows.Visibility.Collapsed;
            sensorBorder.CornerRadius = new CornerRadius(sensorBorder.CornerRadius.BottomLeft);
            if (containerGrid.RowDefinitions.Count > 1)
                containerGrid.RowDefinitions.RemoveAt(0);
        }

        private void HideSensorPart()
        {
            sensorBorder.Visibility = sensorLabel.Visibility = System.Windows.Visibility.Collapsed;
            switchBorder.CornerRadius = new CornerRadius(switchBorder.CornerRadius.TopLeft);
            if (containerGrid.RowDefinitions.Count > 1)
                containerGrid.RowDefinitions.RemoveAt(1);
        }

        public void CheckSwitchAlarm(IEnumerable<Tuple<Guid, AlarmSeverities>> alarmCache)
        {
            if (IsSwitchServiceWorking)
            {
                try
                {
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
        }

        /// <summary>
        /// Returns true if there is an alarm or sensor is not working; returns false otherwise.
        /// </summary>        
        public bool CheckSensorAlarm(IEnumerable<Tuple<Sensor, double?>> sensorCache)
        {
            try
            {
                if (IsSensorServiceWorking)
                {
                    var mySensors = sensorCache.Select(t => new
                    {
                        Sensor = t.Item1,
                        RecentVal = t.Item2
                    }).Where(s => s.Sensor.Room.CenterID == Center.ID);


                    bool hasSensorAlarm = mySensors.Any(s => s.RecentVal > s.Sensor.Max || s.RecentVal < s.Sensor.Min);

                    if (hasSensorAlarm)
                    {
                        SensorColor.Color = Colors.Red;
                        sensorLabel.Foreground = Brushes.White;
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

        private AlarmSeverities severity;
        public AlarmSeverities Severity
        {
            get
            {
                return severity;
            }
            set
            {
                severity = value;
                //    flashTimer.Enabled = severity.HasValue;
            }
        }

        private void UpdateSwitchColor()
        {
            if (IsSwitchServiceWorking)
            {
                switch (severity)
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

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Click != null && !IsMoving)
            {
                Click(this, e);
            }
        }

        private void connectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Center.Connect();
        }

        private void ackMenuItem_Click(object sender, RoutedEventArgs e)
        {

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

    }
}
