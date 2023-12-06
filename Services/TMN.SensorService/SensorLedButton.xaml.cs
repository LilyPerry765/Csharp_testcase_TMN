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

namespace TMN
{
    [DefaultEvent("Click")]
    public partial class SensorLedButton : UserControl
    {
        public event Action<SensorLedButton> Click;

        public SensorLedButton()
        {
            InitializeComponent();
            DataContext = this;
            //if (HasNumber.Value == false)
            //    sensorText.Visibility = System.Windows.Visibility.Hidden;
            //else
            //    sensorText.Visibility = System.Windows.Visibility.Visible;
        }


        public Brush DisplayBrush
        {
            get
            {
                return (Brush)GetValue(DisplayBrushProperty);
            }
            set
            {
                SetValue(DisplayBrushProperty, value);
            }
        }

        public static readonly DependencyProperty DisplayBrushProperty =
            DependencyProperty.Register("DisplayBrush", typeof(Brush), typeof(SensorLedButton), new UIPropertyMetadata(Brushes.Red));

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SensorLedButton), new UIPropertyMetadata(null));



        public double? Value
        {
            get
            {
                return (double?)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double?), typeof(SensorLedButton), new FrameworkPropertyMetadata(00.0));


        public int SensorNumber
        {
            get;
            set;
        }

        private void backgroundBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Click != null)
            {
                Click(this);
            }
        }



        public bool? HasNumber
        {
            get
            {
                return (bool?)GetValue(HasNumberProperty);
            }
            set
            {
                SetValue(HasNumberProperty, value);
            }
        }

        public static readonly DependencyProperty HasNumberProperty =
            DependencyProperty.Register("HasNumber", typeof(bool), typeof(SensorLedButton), new UIPropertyMetadata(null));


    }
}
