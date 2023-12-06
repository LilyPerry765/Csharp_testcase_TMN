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
using System;

namespace TMN.UserControls.Calendar
{

    public partial class YearView : UserControl
    {
        public event EventHandler MonthViewRequired;
        private bool IsLayoutSuspended;
        private CalendarItem[] items = new CalendarItem[12];

        public YearView()
        {
            InitializeComponent();
        }

        void Calendar_DateChanged(object sender, EventArgs e)
        {
            Navigator.Text = Calendar.Date.Year.ToString();
            if (!IsLayoutSuspended)
                ArangeMonths();
        }

        private void Navigator_NavigateForward(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddYears(1);
        }

        private void Navigator_NavigateBackward(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddYears(-1);
        }

        private Calendar calendar;
        public Calendar Calendar
        {
            get
            {
                return calendar;
            }
            set
            {
                calendar = value;
                Calendar.DateChanged += new EventHandler(Calendar_DateChanged);
                Calendar.YearViewItemTemplateChanged += new EventHandler(Calendar_YearViewItemTemplateChanged);
            }
        }

        void Calendar_YearViewItemTemplateChanged(object sender, EventArgs e)
        {
            foreach (var item in items)
            {
                item.ItemTemplate = Calendar.YearViewItemTemplate;
            }
        }

        private void ArangeMonths()
        {
            ContentGrid.Children.Clear();
            for (int m = 0; m < 12; m++)
            {
                CalendarItem item = items[m];
                if (item == null)
                    item = items[m] = new CalendarItem();
                item.Header = ((Enterprise.Wpf.PersianDateTime.MonthNames)(m + 1)).ToString();
                item.Value = m + 1;
                item.DataContext = Calendar.Date.SetMonth(item.Value).SetDay(1).ToGregorian();
                item.IsSelected = Calendar.Date.Year == Enterprise.Wpf.PersianDateTime.Today.Year
                               && m + 1 == Enterprise.Wpf.PersianDateTime.Today.Month;
                item.ClearClickHandlers();
                item.Click += new RoutedEventHandler(item_Click);
                ContentGrid.Children.Add(item);
                Grid.SetColumn(item, m % 3);
                Grid.SetRow(item, m / 3);
            }
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            if (MonthViewRequired != null)
            {
                IsLayoutSuspended = true;
                Calendar.Date = Calendar.Date.SetMonth((sender as CalendarItem).Value);
                IsLayoutSuspended = false;
                MonthViewRequired(this, e);
            }
        }

        private void Navigator_DisplayClick(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.SetYear(Enterprise.Wpf.PersianDateTime.Today.Year);
        }

    }
}
