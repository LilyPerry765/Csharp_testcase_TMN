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

    public partial class SoundButton : UserControl
    {
        public event EventHandler IsMuteChanged;

        public SoundButton()
        {
            InitializeComponent();
        }


        public bool IsMute
        {
            get
            {
                return noImage.Visibility == System.Windows.Visibility.Visible;
            }
            set
            {
                if (value != IsMute)
                {
                    if (value)
                    {
                        noImage.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        noImage.Visibility = System.Windows.Visibility.Collapsed;
                    }

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
