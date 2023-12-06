using System.Windows;
using System.Windows.Controls;
using System;

namespace TMN.UserControls.Calendar
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public event System.EventHandler DateChanged;
        public event System.EventHandler MonthViewItemTemplateChanged;
        public event System.EventHandler YearViewItemTemplateChanged;
        public event System.EventHandler ViewChanging;

        public Calendar()
        {
            InitializeComponent();
            FadeEffectIsEnabled = true;
            dayView.Calendar = monthView.Calendar = yearView.Calendar = this;
            Date = Enterprise.Wpf.PersianDateTime.Today;
        }

        private Enterprise.Wpf.PersianDateTime _PersianDate;
        public Enterprise.Wpf.PersianDateTime Date
        {
            get
            {
                if (_PersianDate == Enterprise.Wpf.PersianDateTime.Empty)
                {
                    _PersianDate = Enterprise.Wpf.PersianDateTime.Now;
                }
                return _PersianDate;
            }
            set
            {
                _PersianDate = value;
                OnDateChanged();
            }
        }

        protected void OnDateChanged()
        {
            if (DateChanged != null)
            {
                DateChanged(this, System.EventArgs.Empty);
            }
        }

        private CalendarView _DefaultView;
        public CalendarView DefaultView
        {
            get
            {
                return _DefaultView;
            }
            set
            {
                _DefaultView = value;
                bool prevValOfFadeEffectIsEnabled = FadeEffectIsEnabled;
                FadeEffectIsEnabled = false;
                CurrentView = value;
                FadeEffectIsEnabled = prevValOfFadeEffectIsEnabled;
            }
        }

        private CalendarView currentView;
        public CalendarView CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                OnViewChanging();
                currentView = value;
                switch (value)
                {
                    case CalendarView.MonthView:
                        if (FadeEffectIsEnabled)
                            monthView.FadeIn();
                        else
                        {
                            monthView.Visibility = Visibility.Visible;
                            yearView.Visibility = dayView.Visibility = Visibility.Hidden;
                        }
                        break;
                    case CalendarView.DayView:
                        if (FadeEffectIsEnabled)
                            dayView.FadeIn();
                        else
                        {
                            dayView.Visibility = Visibility.Visible;
                            monthView.Visibility = yearView.Visibility = Visibility.Hidden;
                        }
                        break;
                    case CalendarView.YearView:
                        if (FadeEffectIsEnabled)
                            yearView.FadeIn();
                        else
                        {
                            yearView.Visibility = Visibility.Visible;
                            monthView.Visibility = dayView.Visibility = Visibility.Hidden;
                        }
                        break;
                    default:
                        throw new NotSupportedException("This calendar view is not supported.");
                }
            }
        }

        private void MonthView_YearViewRequested(object sender, System.EventArgs e)
        {
            if (monthView.Opacity == 1D)
            {
                CurrentView = CalendarView.YearView;
                monthView.FadeOut();
            }
        }

        protected void OnViewChanging()
        {
            if (ViewChanging != null)
            {
                ViewChanging(this, EventArgs.Empty);
            }
        }

        private void MonthView_DayViewRequested(object sender, System.EventArgs e)
        {
            if (monthView.Opacity == 1D)
            {
                CurrentView = CalendarView.DayView;
                monthView.FadeOut();
            }
        }

        private void YearView_MonthViewRequired(object sender, System.EventArgs e)
        {
            if (yearView.Opacity == 1D)
            {
                CurrentView = CalendarView.MonthView;
                yearView.FadeOut();
            }
        }

        public bool FadeEffectIsEnabled
        {
            get;
            set;
        }

        private void DayView_MonthViewRequired(object sender, System.EventArgs e)
        {
            if (dayView.Opacity == 1D)
            {
                CurrentView = CalendarView.MonthView;
                dayView.FadeOut();
            }
        }

        public enum CalendarView
        {
            MonthView,
            DayView,
            YearView
        }

        public UIElementCollection DayTemplate
        {
            get
            {
                return dayView.ContentGrid.Children;
            }
        }

        private ControlTemplate monthViewItemTemplate;
        public ControlTemplate MonthViewItemTemplate
        {
            get
            {
                return monthViewItemTemplate;
            }
            set
            {
                monthViewItemTemplate = value;
                if (MonthViewItemTemplateChanged != null)
                {
                    MonthViewItemTemplateChanged(this, EventArgs.Empty);
                }
            }
        }

        private ControlTemplate yearViewItemTemplate;
        public ControlTemplate YearViewItemTemplate
        {
            get
            {
                return yearViewItemTemplate;
            }
            set
            {
                yearViewItemTemplate = value;
                if (YearViewItemTemplateChanged != null)
                {
                    YearViewItemTemplateChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}
