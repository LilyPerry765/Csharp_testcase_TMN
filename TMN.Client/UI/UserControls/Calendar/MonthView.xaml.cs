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
using System.Diagnostics;

namespace TMN.UserControls.Calendar
{

    public partial class MonthView : UserControl
    {
        public event System.EventHandler YearViewRequested;
        public event System.EventHandler DayViewRequested;
        private bool IsLayoutSuspended;
        private CalendarItem[,] items = new CalendarItem[6, 7];

        public MonthView()
        {
            InitializeComponent();
            Mode = CalendarSize.Normal;
            ArrangeWeekDayNames();
        }

        void Calendar_DateChanged(object sender, EventArgs e)
        {
            Navigator.Text = string.Format("{0} ,{1}", Calendar.Date.MonthName, Calendar.Date.Year);
            ArrangeDaysOnCalendar();
        }

        private CalendarSize _Mode;
        public CalendarSize Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
                foreach (var colDef in ContentGrid.ColumnDefinitions)
                {
                    colDef.MinWidth = value == CalendarSize.Compressed ? 30 : 70;
                }
                ArrangeWeekDayNames();
            }
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
                Calendar.MonthViewItemTemplateChanged += new EventHandler(Calendar_MonthViewItemTemplateChanged);
            }
        }

        void Calendar_MonthViewItemTemplateChanged(object sender, EventArgs e)
        {
            foreach (var item in items)
            {
                item.ItemTemplate = Calendar.MonthViewItemTemplate;
            }
        }

        private void Navigator_NavigateForward(object sender, System.EventArgs e)
        {
            NextMonth();
        }

        private void NextMonth()
        {
            Calendar.Date = Calendar.Date.AddMonths(1);
        }

        private void PreviousMonth()
        {
            Calendar.Date = Calendar.Date.AddMonths(-1);
        }

        private void Navigator_NavigateBackward(object sender, System.EventArgs e)
        {
            PreviousMonth();
        }

        private void ArrangeWeekDayNames()
        {
            ContentGrid.Children.RemoveRange(0, 7);
            for (int i = 0; i < 7; i++)
            {
                Label weekDayNameLabel = new Label();
                if (Mode == CalendarSize.Normal)
                {
                    weekDayNameLabel.Content = ((Enterprise.Wpf.PersianDateTime.NormalWeekDay)i).ToString().Replace("_", " ");
                }
                else if (Mode == CalendarSize.Compressed)
                {
                    weekDayNameLabel.Content = ((Enterprise.Wpf.PersianDateTime.ShortWeekDay)i).ToString().Replace("_", " ");
                }
                ContentGrid.Children.Add(weekDayNameLabel);
                Grid.SetRow(weekDayNameLabel, 0);
                Grid.SetColumn(weekDayNameLabel, i);
            }
        }

        private void ArrangeDaysOnCalendar()
        {
            if (IsLayoutSuspended)
                return;

            ContentGrid.Children.RemoveRange(7, 42);
            int lastRow = 0;

            int firstWeekDayOfMonth = (int)Calendar.Date.SetDay(1).DayOfWeek;
            // Previous Month
            for (int d = 0; d < firstWeekDayOfMonth; d++)
            {
                if (items[0, d] == null)
                    items[0, d] = new CalendarItem();
                CalendarItem item = items[0, d];
                item.Opacity = .3;
                item.ClearClickHandlers();
                item.Click += new RoutedEventHandler(PrevItem_Click);
                item.Value = Calendar.Date.AddMonths(-1).DaysInMonth - firstWeekDayOfMonth + d + 1;
                item.DataContext = Calendar.Date.AddMonths(-1).SetDay(item.Value).ToGregorian();
                item.Header = item.Value.ToString();
                item.IsSelected = Calendar.Date.AddMonths(-1).SetDay(item.Value).IsToday;
                ContentGrid.Children.Add(item);
                Grid.SetColumn(item, d);
                Grid.SetRow(item, 1);
            }

            // Current Month
            for (int d = 0; d < Calendar.Date.DaysInMonth; d++)
            {
                int day = d + 1;
                GridPart g = GridPartFromDate(Calendar.Date.Year, Calendar.Date.Month, day);
                g.Row += 1;
                if (items[g.Row - 1, g.Column] == null)
                    items[g.Row - 1, g.Column] = new CalendarItem();
                CalendarItem item = items[g.Row - 1, g.Column];
                item.ClearClickHandlers();
                item.Opacity = 1;
                item.Click += new RoutedEventHandler(ActiveItem_Click);
                item.Value = day;
                item.Header = item.Value.ToString();
                item.DataContext = Calendar.Date.SetDay(item.Value).ToGregorian();
                item.IsSelected = Calendar.Date.SetDay(item.Value).IsToday;
                ContentGrid.Children.Add(item);
                Grid.SetColumn(item, g.Column);
                Grid.SetRow(item, g.Row);
                lastRow = g.Row;
            }


            // Next Month
            int lastWeekDay = (int)Calendar.Date.SetDay(Calendar.Date.DaysInMonth).DayOfWeek;

            // If the last row is 5 it should be completed with the next month days and the next row(6) must be filled too, so maximum 14 days may be needed,
            // if the last row is 6 only that row must be completed so maximum 7 days is enough 
            for (int i = lastWeekDay + 1; i < (lastRow == 6 ? 7 : 14); i++)
            {
                int d = i % 7;
                int row = lastRow + i / 7;

                if (items[row - 1, d] == null)
                    items[row - 1, d] = new CalendarItem();
                CalendarItem item = items[row - 1, d];
                item.Opacity = .3;
                item.ClearClickHandlers();
                item.Click += new RoutedEventHandler(NextItem_Click);
                item.Value = i - lastWeekDay;
                item.Header = item.Value.ToString();
                item.DataContext = Calendar.Date.AddMonths(1).SetDay(item.Value).ToGregorian();
                item.IsSelected = Calendar.Date.AddMonths(1).SetDay(item.Value).IsToday;
                ContentGrid.Children.Add(item);
                Grid.SetColumn(item, d);
                Grid.SetRow(item, row);
            }
        }

        private void PrevItem_Click(object sender, RoutedEventArgs e)
        {
            Calendar.Date = Calendar.Date.AddMonths(-1).SetDay((sender as CalendarItem).Value);
            if (DayViewRequested != null)
            {
                DayViewRequested(this, EventArgs.Empty);
            }
        }

        private void NextItem_Click(object sender, RoutedEventArgs e)
        {
            Calendar.Date = Calendar.Date.AddMonths(1).SetDay((sender as CalendarItem).Value);
            if (DayViewRequested != null)
            {
                DayViewRequested(this, EventArgs.Empty);
            }
        }

        private void ActiveItem_Click(object sender, RoutedEventArgs e)
        {
            IsLayoutSuspended = true;
            Calendar.Date = Calendar.Date.SetDay((sender as CalendarItem).Value);
            if (DayViewRequested != null)
            {
                DayViewRequested(this, EventArgs.Empty);
            }
            IsLayoutSuspended = false;
        }

        private struct GridPart
        {
            public GridPart(int row, int column)
            {
                Row = row;
                Column = column;
            }
            public int Row;
            public int Column;
        }

        private GridPart GridPartFromDate(int year, int month, int day)
        {
            int firstWeekDay = (int)new Enterprise.Wpf.PersianDateTime(year, month, 1).DayOfWeek;
            int daySequance = firstWeekDay + day - 1;
            return new GridPart((daySequance / 7), daySequance % 7);
        }

        private void Navigator_DisplayClick(object sender, System.EventArgs e)
        {
            if (YearViewRequested != null)
            {
                YearViewRequested(this, e);
            }
        }

        public enum CalendarSize
        {
            Normal,
            Compressed
        }
    }
}
