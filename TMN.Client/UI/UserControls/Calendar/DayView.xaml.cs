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
    /// Interaction logic for DayView.xaml
    /// </summary>
    public partial class DayView : UserControl
    {
        public event EventHandler MonthViewRequired;


        public DayView()
        {
            InitializeComponent();
        }

        private Calendar _Calendar;
        public Calendar Calendar
        {
            get
            {
                return _Calendar;
            }
            set
            {
                _Calendar = value;
                Calendar.DateChanged += new EventHandler(Calendar_DateChanged);
            }
        }

        void Calendar_DateChanged(object sender, EventArgs e)
        {
            Enterprise.Wpf.PersianDateTime pd = Calendar.Date;
            Navigator.Text = string.Format("{0}، {1} {2} {3}", pd.WeekDayName, pd.Day, pd.MonthName, pd.Year);
            TodaySignerBorder.Visibility = pd.IsToday ? Visibility.Visible : Visibility.Hidden;
        }

        private void Navigator_NavigateBackward(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddDays(-1);
        }

        private void Navigator_NavigateForward(object sender, EventArgs e)
        {
            Calendar.Date = Calendar.Date.AddDays(1);
        }

        private void Navigator_DisplayClick(object sender, EventArgs e)
        {
            if (MonthViewRequired != null)
            {
                MonthViewRequired(this, e);
            }
        }
    }
}
