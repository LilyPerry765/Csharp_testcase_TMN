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
using System.Text.RegularExpressions;
using Enterprise;
using TMN.UI.Windows;

namespace TMN.UserControls
{

    public partial class SensorDisplay : UserControl
    {
        public event Action RequestsAlarmChanged;

        public Guid RoomID;

        public SensorDisplay()
        {
            InitializeComponent();
        }

        public void Refresh(IEnumerable<KeyValuePair<Sensor, double?>> data)
        {
            //var sensorData = data.Where(d => d.Key.RoomID == RoomID && d.Key.SensorType == this.SensorType);

			var sensorData = data.Where(d => d.Key.RoomID == RoomID && (SensorTypes)d.Key.TypeID == this.SensorType);

            var average = sensorData.Average(s => s.Value);

            //Logger.WriteInfo("Average is {0}", average);

            IsConnected = average.HasValue;

            ShowWarning(sensorData.Any(d => d.Value == null));
            if (isConnected)
            {
                SetTitle(average);
                IsAlarming = sensorData.Any(s => s.Value.HasValue && (s.Value < s.Key.Min || s.Value > s.Key.Max));
            }
        }

        private void ShowWarning(bool show)
        {
            warningImage.Visibility = show ? Visibility.Visible : Visibility.Hidden;
        }

        public bool RequestsAlarm
        {
            get
            {
                return isAlarming && !soundButton.IsMute;
            }
        }

        private void SetTitle(double? value)
        {
            if (value.HasValue)
            {
                string format;
                switch (SensorType)
                {
                    case SensorTypes.Temperature:
                        format = " {0:0.0} °C";
                        break;
                    case SensorTypes.Humidity:
                        format = "  {0:0.0}%";
                        break;
                    default:
                        format = "{0:0.0}";
                        Logger.WriteWarning("Invalid sensor type: \"{0}\"", SensorType);
                        break;
                }
                led.Title = string.Format(format, value.Value);
            }
            else
            {
                led.Title = null;
            }
        }

        private bool isAlarming;
        private bool IsAlarming
        {
            set
            {
                isAlarming = value;
                SetDisplayMode();
                OnRequestsAlarmChanged();
            }
        }

        private bool isConnected;
        private bool IsConnected
        {
            set
            {
                isConnected = value;
                SetDisplayMode();
            }
        }

        public bool IsMute
        {
            get
            {
                return soundButton.IsMute;
            }
            set
            {
                soundButton.IsMute = value;
            }
        }

        private void SetDisplayMode()
        {
            if (isConnected)
            {
                if (isAlarming)
                {
                    led.DisplayMode = DisplayModes.Blinking;
                }
                else
                {
                    led.DisplayMode = DisplayModes.On;
                }
            }
            else
            {
                led.DisplayMode = DisplayModes.Off;
            }
        }

        private void led_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new SensorChartWindow(RoomID, SensorType).Show();
        }

        private SensorTypes sensorType;
        public SensorTypes SensorType
        {
            get
            {
                return sensorType;
            }
            set
            {
                sensorType = value;
                switch (value)
                {
                    case SensorTypes.Temperature:
                        iconImage.Source = ImageSourceHelper.GetImageSource("thermo.png");
                        iconImage.Height = 25;
                        iconImage.ToolTip = "دما";
                        led.InnerBackground = Brushes.Orange;
                        break;
                    case SensorTypes.Humidity:
                        iconImage.Source = ImageSourceHelper.GetImageSource("drop.png");
                        iconImage.Height = 16;
                        iconImage.ToolTip = "رطوبت";
                        led.InnerBackground = Brushes.LightSkyBlue;
                        break;
                }
            }
        }

        private void soundButton_IsMuteChanged(object sender, EventArgs e)
        {
            OnRequestsAlarmChanged();
        }

        private void OnRequestsAlarmChanged()
        {
            if (RequestsAlarmChanged != null)
            {
                RequestsAlarmChanged();
            }
        }

    }
}
