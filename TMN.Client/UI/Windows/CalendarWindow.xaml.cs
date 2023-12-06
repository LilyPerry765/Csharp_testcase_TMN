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
using System.Windows.Shapes;
using TMN.Views.Lists;
using TMN.Views.Details;
using TMN.UI.Windows;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {

        private ItemsListHolderWindow taskListWindow, logsListWindow;
        private CenterReportsWindow reportsWindow;
        private Modes mode;

        public CalendarWindow()
        {
            InitializeComponent();
            InitializeTabControl();
            CalendarItemTemplate.Instances.Clear();
            MainWindow.Instance.Tree.RedrawNeeded += new EventHandler(Tree_RedrawNeeded);
        }

        TabControl tabControl = new TabControl()
        {
            Background = Brushes.Transparent
        };

        TabItem tasksTab = new TabItem()
        {
            Header = "تست ها"
        };

        TabItem reportsTab = new TabItem()
        {
            Header = "گزارش روزانه"
        };

        TabItem logsTab = new TabItem()
        {
            Header = "Log Book"
        };

        void Tree_RedrawNeeded(object sender, EventArgs e)
        {
            RefreshCalendarItems();
        }

        private void InitializeTabControl()
        {
            Calendar.DayTemplate.Add(tabControl);

            tabControl.Items.Add(reportsTab);
            tabControl.Items.Add(tasksTab);
            tabControl.Items.Add(logsTab);
            reportsWindow = new CenterReportsWindow(Calendar);
            taskListWindow = new ItemsListHolderWindow(EntityTypes.Task, Calendar.Date.ToGregorian());
            logsListWindow = new ItemsListHolderWindow(EntityTypes.Event, Calendar.Date.ToGregorian());
            reportsTab.Content = reportsWindow.ExtractContent();
            tasksTab.Content = taskListWindow.ExtractContent();
            logsTab.Content = logsListWindow.ExtractContent();
        }

        public Modes Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                switch (mode)
                {
                    case Modes.Tests:
                        tabControl.SelectedItem = tasksTab;
                        break;
                    case Modes.Reports:
                        tabControl.SelectedItem = reportsTab;
                        break;
                    case Modes.LogBook:
                        tabControl.SelectedItem = logsTab;
                        break;
                }
            }
        }

        private void Calendar_DateChanged(object sender, EventArgs e)
        {
            RefreshCalendarItems();
            (logsListWindow.View as EventsListView).Date = (taskListWindow.View as TasksListView).Date = reportsWindow.Date = Calendar.Date.ToGregorian();
        }

        private void Calendar_ViewChanging(object sender, EventArgs e)
        {
            RefreshCalendarItems();
        }

        private static void RefreshCalendarItems()
        {
            CalendarItemTemplate.RefreshSources();
            foreach (var item in CalendarItemTemplate.Instances)
            {
                item.RefreshDisplay();
            }
        }

        public enum Modes
        {
            Tests,
            Reports,
            LogBook
        }



    }
}
