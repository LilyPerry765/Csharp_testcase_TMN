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

namespace TMN.UserControls
{

    public partial class MuteCenter : UserControl
    {
        public event EventHandler IsMuteChanged;

        public MuteCenter()
        {
            InitializeComponent();
        }


        public bool IsMute
        {
            get
            {
                return crossImage.Visibility == System.Windows.Visibility.Visible;
            }
            set
            {
                if (value != IsMute)
                {
                    if (value)
                    {
                        crossImage.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        crossImage.Visibility = System.Windows.Visibility.Collapsed;
                    }

                }
            }
        }

       
        public AlarmSeverities Severity
        {
            set
            {
                if (value == AlarmSeverities.Critical)
                {
                    redImage.Visibility = System.Windows.Visibility.Visible;
                    orangeImage.Visibility = System.Windows.Visibility.Hidden;
                    yellowImage.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (value == AlarmSeverities.Minor)
                {
                    redImage.Visibility = System.Windows.Visibility.Hidden;
                    orangeImage.Visibility = System.Windows.Visibility.Hidden;
                    yellowImage.Visibility = System.Windows.Visibility.Visible;
                }
                else if (value == AlarmSeverities.Major)
                {
                    redImage.Visibility = System.Windows.Visibility.Hidden;
                    orangeImage.Visibility = System.Windows.Visibility.Visible;
                    yellowImage.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                IsMute = !IsMute;
                if (IsMuteChanged != null)
                {
                    IsMuteChanged(this, EventArgs.Empty);
                }
            }
        }

        public string Title
        {
            get
            {
                return titleTextBlock.Text;
            }
            set
            {
                titleTextBlock.Text = value;
            }
        }
    }
}
