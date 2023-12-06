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
using Enterprise;

namespace TMN
{

    public partial class CalibrationWindow : Window
    {
        public CalibrationWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public double Value
        {
            get;
            set;
        }

        public Brush DisplayBrush
        {
            get;
            set;
        }

        public string CircuitTitle
        {
            get;
            set;
        }

        public Bound Threashold
        {
            get
            {
                if (Value > 0)
                {
                    return new Bound(Value - 10, Value + 10);
                }
                else
                {
                    Logger.WriteWarning("Value is not set. Cannot calculate threashold.");
                    return new Bound(0, 100);
                }
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }




    }
}
