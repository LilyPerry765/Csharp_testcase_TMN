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
using System.Windows.Threading;

namespace TMN.UserControls
{

    public partial class PowerCenterSpot : UserControl
    {
        public event RoutedEventHandler Click;
        public event EventHandler MuteCenterClick; 
        private Color NormalColor = Color.FromArgb(0xFF, 0x7A, 0xC7, 0x00);
        private Timer timer = new Timer();

        public bool IsMute
        {
            get
            {
                return bool.Parse(TextSettings.Get("MUTE_POW_" + Center.PointCode, "False"));
            }
            set
            {
                TextSettings.Set("MUTE_POW_" + Center.PointCode, value.ToString());
            }
        }

        public bool HasPowerAlarm;
        //{
        //    get
        //    {
        //        return bool.Parse(Setting.Get(Setting.NEW_POWER_ALARM_INSERTED + Center.PointCode, "false"));
        //    }
        //}

        public PowerCenterSpot(Center center)
        {
            InitializeComponent();
            this.Center = center;
            this.DataContext = this;
            this.timer = new Timer(500);
            this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        Color _Color = Colors.Black;
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Dispatcher.Invoke(new Action(delegate()
            {

                if (powerColor.Color == Colors.Black)
                {
                    if (_Color != Colors.Black)
                    {
                        powerColor.Color = _Color;
                        powerLabel.Foreground = Brushes.Black;
                        _Color = Colors.Black;
                    }
                }
                else
                {
                    _Color = powerColor.Color;
                    powerColor.Color = Colors.Black;
                    powerLabel.Foreground = Brushes.White;
                }
                if (powerColor.Color != NormalColor)
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

        private bool hasPowerPart = false;
        public void CheckConnectivity(IEnumerable<ServiceState> serviceStates)
        {
            var myServiceStates = serviceStates.Where(s => s.CenterID == Center.ID);
            var myActiveStates = myServiceStates.Where(s => s.IsConnected);
            IsPowerServiceWorking = myActiveStates.Any(s => (ServiceTypes)s.ServiceType == ServiceTypes.SensorService);

            this.muteMenuItem.IsChecked = IsMute;


            if (IsMute)
            {
                muteAllButton.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                muteAllButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public bool CheckPowerAlarm(IEnumerable<Tuple<Sensor, double?>> sensorCache)
        {
            try
            {
                if (IsPowerServiceWorking)
                {
                    var mySensors = sensorCache.Select(t => new
                    {
                        Sensor = t.Item1,
                        RecentVal = t.Item2
                    }).Where(s => s.Sensor.Room.CenterID == Center.ID && s.Sensor.SensorType == SensorTypes.POWER);
                    IsPowerEnable = mySensors.Any();
                    //HidePowerPanel();
                    if (IsPowerEnable)
                    {
                        HasPowerAlarm = mySensors.Any(s => s.RecentVal == 1);
                        if (HasPowerAlarm)
                        {
                            powerColor.Color = Colors.Red;
                            powerLabel.Foreground = Brushes.White;
                            timer.Start();
                        }
                        else
                        {
                            powerColor.Color = NormalColor;
                            powerLabel.Foreground = Brushes.Black;
                        }
                    }
                    else
                    {
                        powerColor.Color = Colors.Black;
                        powerLabel.Foreground = Brushes.White;
                        return false; //when sensor don't add state because not running on server.
                    }
                    return IsPowerEnable;
                }
                else
                {
                    powerColor.Color = Colors.Black;
                    powerLabel.Foreground = Brushes.White;
                    return false; //when sensor don't add state because not running on server.
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
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


        private void Power_click(object sender, MouseButtonEventArgs e)
        {
            if (Click != null && !IsMoving)
            {
                Click(this, e);
            }
        }


        public bool IsPowerServiceWorking { get; set; }

        public bool IsPowerEnable { get; set; }
    }
}
