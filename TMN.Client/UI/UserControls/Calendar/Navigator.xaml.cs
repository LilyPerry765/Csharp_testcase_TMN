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

namespace TMN.UserControls.Calendar
{
    /// <summary>
    /// Interaction logic for Navigator.xaml
    /// </summary>
    public partial class Navigator : UserControl
    {
        public event EventHandler NavigateForward;
        public event EventHandler NavigateBackward;
        public event EventHandler DisplayClick;

        public Navigator()
        {
            InitializeComponent();
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigateBackward != null)
            {
                NavigateBackward(this, e);
            }
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigateForward != null)
            {
                NavigateForward(this, e);
            }
        }

        public string Text
        {
            get
            {
                return DisplayLabel.Content.ToString();
            }
            set
            {
                DisplayLabel.Content = value;
            }
        }

        private void DisplayLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1 && DisplayClick != null)
            {
                e.Handled = true;
                DisplayClick(this, EventArgs.Empty);
            }
        }
    }
}
