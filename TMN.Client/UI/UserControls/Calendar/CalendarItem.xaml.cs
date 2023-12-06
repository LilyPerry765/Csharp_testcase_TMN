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

    public partial class CalendarItem : UserControl
    {
        public event RoutedEventHandler Click;

        public CalendarItem()
        {
            InitializeComponent();
            IsSelected = false;
        }

        /// <summary>
        /// Saves the value of this calendar item. It can be a Day, a Month, or a Year.
        /// </summary>
        public int Value
        {
            get;
            set;
        }

        public Grid ContentPlaceHolder
        {
            get
            {
                return contentPlaceHolder;
            }
        }

        public string Header
        {
            get
            {
                return headerTextBlock.Text;
            }
            set
            {
                headerTextBlock.Text = value;
            }
        }

        public void ClearClickHandlers()
        {
            Click = null;
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                Foreground = value ? FindResource("HighlightBrush") as Brush : SystemColors.ControlTextBrush;
                Border.BorderBrush = value ? FindResource("HighlightBrush") as Brush : Brushes.White;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        public ControlTemplate ItemTemplate
        {
            get
            {
                return TemplateControl.Template;
            }
            set
            {
                TemplateControl.Template = value;
            }
        }

    }
}
