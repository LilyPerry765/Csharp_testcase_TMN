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
using System.ComponentModel;

namespace TMN.UserControls
{
    /// <summary>
    /// Interaction logic for JellyButton.xaml
    /// </summary>
    public partial class JellyIcon : UserControl, INotifyPropertyChanged
    {
        public JellyIcon()
        {
            InitializeComponent();
            //TextColor = Colors.White;
        }

        private Color darkColor;
        public Color DarkColor
        {
            get
            {
                return darkColor;
            }
            set
            {
                darkColor = value;
                SendPropertyChanged("DarkColor");
            }
        }

        public Color LightColor
        {
            get;
            set;
        }

        public Color DisableColor
        {
            get;
            set;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void SendPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
