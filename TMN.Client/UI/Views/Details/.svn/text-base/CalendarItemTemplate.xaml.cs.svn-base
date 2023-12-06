using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.UserControls.Calendar;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for CalenderItemTemplate.xaml
    /// </summary>
    public partial class CalendarItemTemplate : UserControl
    {
        private static List<CalendarItemTemplate> instances = new List<CalendarItemTemplate>();

        public CalendarItemTemplate()
        {
            InitializeComponent();
            instances.Add(this);
        }

        public static List<CalendarItemTemplate> Instances
        {
            get
            {
                return instances;
            }
        }

        public TMN.UserControls.Calendar.Calendar.CalendarView CalendarView
        {
            get;
            set;
        }

        private static List<Task> tasksSource;
        private static List<Task> TasksSource
        {
            get
            {
                if (tasksSource == null)
                {
                    tasksSource = DB.Instance.Tasks.ToList();
                }
                return tasksSource;
            }
        }

        private static List<Event> eventsSource;
        private static List<Event> EventsSource
        {
            get
            {
                if (eventsSource == null)
                {
                    eventsSource = DB.Instance.Events.ToList();
                }
                return eventsSource;
            }
        }

        public static void RefreshSources()
        {
            //tasksSource = DB.Instance.Tasks.Where(p => p.User.Center == Center.Current).ToList();
            //eventsSource = DB.Instance.Events.Where(p => p.User.Center == Center.Current).ToList();

            tasksSource = DB.Instance.Tasks.Where(p => p.User.Center == Center.Selected ).ToList();
            eventsSource = DB.Instance.Events.Where(p => p.User.Center == Center.Selected).ToList();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            int doneTasks = 0, incompleteTasks = 0, events = 0;
            if (CalendarView == TMN.UserControls.Calendar.Calendar.CalendarView.MonthView)
            {
                doneTasks = TasksSource.Where(p => p.DueDate.Value.Date == (DateTime)DataContext && p.IsDone == true ).Count();
                incompleteTasks = TasksSource.Where(p => p.DueDate.Value.Date == (DateTime)DataContext && p.IsDone == false ).Count();
                events = EventsSource.Where(p => p.Time.Value.Date == (DateTime)DataContext ).Count();
            }
            else if (CalendarView == TMN.UserControls.Calendar.Calendar.CalendarView.YearView)
            {
                doneTasks = TasksSource.Where(p =>
                       p.DueDate.Value.ToPersianDate().Year == ((DateTime)DataContext).ToPersianDate().Year
                    && p.DueDate.Value.ToPersianDate().Month == ((DateTime)DataContext).ToPersianDate().Month
                    && p.IsDone == true).Count();

                incompleteTasks = TasksSource.Where(p =>
                       p.DueDate.Value.ToPersianDate().Year == ((DateTime)DataContext).ToPersianDate().Year
                    && p.DueDate.Value.ToPersianDate().Month == ((DateTime)DataContext).ToPersianDate().Month
                    && p.IsDone == false).Count();

                events = EventsSource.Where(p =>
                    p.Time.Value.ToPersianDate().Year == ((DateTime)DataContext).ToPersianDate().Year
                    && p.Time.Value.ToPersianDate().Month == ((DateTime)DataContext).ToPersianDate().Month).Count();
            }
            else
            {
                throw new NotSupportedException("CalendarView is not set correctly.");
            }

            if (events > 0)
            {
                eventsText.Text = events.ToString() + " رخداد ثبت شده";
                eventsText.Visibility = Visibility.Visible;
            }
            else
            {
                eventsText.Visibility = Visibility.Collapsed;
            }
            if (incompleteTasks > 0)
            {
                incompleteTasksText.Text = incompleteTasks.ToString() + " تست برای انجام";
                incompleteTasksText.Visibility = Visibility.Visible;
            }
            else
            {
                incompleteTasksText.Visibility = Visibility.Collapsed;
            }
            if (doneTasks > 0)
            {
                doneTasksText.Text = doneTasks.ToString() + " تست انجام شده";
                doneTasksText.Visibility = Visibility.Visible;
            }
            else
            {
                doneTasksText.Visibility = Visibility.Collapsed;
            }
        }
    }
}
