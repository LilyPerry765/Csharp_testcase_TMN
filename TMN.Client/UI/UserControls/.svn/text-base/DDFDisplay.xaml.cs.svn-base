using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TMN.Properties;
using Enterprise;

namespace TMN.UserControls
{


    public partial class DDFDisplay : UserControl
    {

        private Color active = Color.FromArgb(255, 255, 150, 150);
        private Color normal = Color.FromArgb(0, 255, 255, 255);
        private string SavedDisplayStringForUndo;
        public event EventHandler CommitEdit;

        public DDFDisplay(bool ShowAlarm)
        {
            InitializeComponent();
            if (ShowAlarm)
                Background = new SolidColorBrush(active);

        }

        private void lblDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                e.Handled = true;
                IsEditable = true;
            }
        }

        public bool IsEditable
        {
            get
            {
                return txtBox.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    SavedDisplayStringForUndo = txtBox.Text;
                }

                txtBox.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                lblDisplay.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
                txtBox.Focus();
            }
        }

        private void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            IsEditable = false;
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IsEditable = false;
                txtBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                if (CommitEdit != null)
                    CommitEdit(this, EventArgs.Empty);
            }
            else if (e.Key == Key.F2)
            {
                IsEditable = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtBox.Text = SavedDisplayStringForUndo;
                IsEditable = false;
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            const string instructionToolTip = "R-Click : ويرايش\nEnter : تاييد\nEsc : انصراف";

            if (DDF.Links.Any(l => l.ContainsSignaling))
                bgBorder.BorderBrush = Brushes.OrangeRed;
            else
                bgBorder.BorderBrush = Brushes.Transparent;

            if (DDF.IsSTM1)
            {
                bgBorder.Background = new SolidColorBrush(Settings.Default.Color_STM1.ToWpfColor());
                if (DDF.Description.IsNullOrEmpty())
                {
                    DDF.Description = "STM1";
                }
            }
            else if (DDF.Links.Count == 1 && DDF.Links.Single().UniqueRoute != null)
            {
                Center destCenter = DDF.Links.First().UniqueRoute.Destination;
                if (destCenter != null)
                {
                    bgBorder.Background = new SolidColorBrush(GetCenterColor(destCenter.CenterType.Value));
                    ToolTip = string.Format("{0}\n{1}", ((CenterTypes)destCenter.CenterType.Value).ToString(), instructionToolTip);
                }
            }
            ToolTip = instructionToolTip;
        }

        private Color GetCenterColor(int centerType)
        {
           return Converters.CenterTypesColorConverter.Instance.Convert(centerType, null, null, null).As<Color>();
        }

        private DDF DDF
        {
            get
            {
                return DataContext as DDF;
            }
        }

    }
}
