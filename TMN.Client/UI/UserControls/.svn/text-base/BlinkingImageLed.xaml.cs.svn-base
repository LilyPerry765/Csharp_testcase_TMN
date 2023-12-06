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

    public partial class BlinkingImageLed : UserControl
    {

        Timer timer = new Timer(500);

        public BlinkingImageLed()
        {
            InitializeComponent();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //timer.Enabled = false;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Dispatcher.BeginInvoke((Action)delegate()
            {
                if (timer.Enabled)
                    IsOn = !IsOn;
            });
            timer.Start();
        }

        private bool IsOn
        {
            get
            {
                return blinkingImage.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    blinkingImage.Visibility = Visibility.Visible;
                }
                else
                {
                    blinkingImage.Visibility = Visibility.Collapsed;
                }
            }
        }

        bool _isEnabled = false;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                //timer.Enabled = _isEnabled;
                //if (_isEnabled == true)
                //    timer.Start();
                //else
                //    timer.Stop();
                IsOn = _isEnabled;
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(BlinkingImageLed), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropertyChanged)));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ImageSourceProperty)
            {
                ((BlinkingImageLed)d).blinkingImage.Source = e.NewValue as ImageSource;
            }
        }

    }

}
