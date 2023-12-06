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

    public partial class PipeIcon : UserControl
    {
        public event EventHandler DisplayModeChanged;

        Timer timer = new Timer(500);
        static Brush offBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        static Brush onBrush = Brushes.Transparent;

        public PipeIcon()
        {
            InitializeComponent();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            DisplayMode = DisplayModes.Off;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FontSizeProperty)
            {
                alarmLabelText.FontSize = (double)e.NewValue;
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
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
                return blinkGrid.Background == onBrush;
            }
            set
            {
                if (value)
                {
                    blinkGrid.Background = onBrush;
                    alarmLabelText.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    blinkGrid.Background = offBrush;
                    alarmLabelText.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private DisplayModes mode = DisplayModes.UnSpecified;
        public DisplayModes DisplayMode
        {
            get
            {
                return mode;
            }
            set
            {
                if (mode != value)
                {
                    mode = value;

                    timer.Enabled = value == DisplayModes.Blinking;
                    switch (value)
                    {
                        case DisplayModes.Off:
                            IsOn = false;
                            break;
                        case DisplayModes.On:
                            IsOn = true;
                            break;
                    }

                    if (DisplayModeChanged != null)
                        DisplayModeChanged(this, EventArgs.Empty);
                }
            }
        }

        public int TitleMaxLength = 30;
        public string Title
        {
            get
            {
                return alarmLabelText.Text;
            }
            set
            {
                string title = value ?? "";
                int maxLength = TitleMaxLength;
                alarmLabelText.Text = title.Length <= maxLength ? title : title.Substring(0, maxLength - 3) + "...";
                if (value != null && DisplayMode == DisplayModes.Off)
                {
                    DisplayMode = DisplayModes.On;
                }
            }
        }


        public Brush InnerBackground
        {
            get
            {
                return rootBorder.Background;
            }
            set
            {
                rootBorder.Background = value;
            }
        }

        private DisplayModes prevState = DisplayModes.UnSpecified;
        private void Border_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                if (prevState != DisplayModes.UnSpecified)
                {
                    DisplayMode = prevState;
                }
            }
            else // It is going to hide
            {
                prevState = DisplayMode;
                DisplayMode = DisplayModes.Off;
            }
        }

        //~BlinkingLed()
        //{
        //    Logger.WriteInfo("~BlinkingLed");
        //}
    }

//    public enum DisplayModes
//    {
//        UnSpecified,
//        Off,
//        On,
//        Blinking
//    }
}
