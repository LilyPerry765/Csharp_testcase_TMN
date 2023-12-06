using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;
using System;

namespace TMN.Reports.Filters
{

    public partial class EventsFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public EventsFilter()
        {
            InitializeComponent();
            TitleCombo.ItemsSource = db.EventTypes;
        }

        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("Events");
            src.Value = (from evnt in
                             (from e in db.Events
                              where e.User.Center == Center.Current
                              && (TitleCombo.SelectedIndex == -1 || e.EventType == (TitleCombo.SelectedItem as EventType))
                              && (e.Comment == null || e.Comment.Contains(ContextTextBox.Text))
                              select new Events()
                              {
                                  Title = e.EventType.Name,
                                  Comment = e.Comment,
                                  UserName = e.User.FullName,
                                  Time = e.Time,
                                  Shift = Enum.GetName(typeof(Shifts), e.Shift ?? 0)
                              }).ToList()
                         where (DateFromDateBox.IsDateNull || evnt.Time.Value.Date >= DateFromDateBox.Date.Value.Date)
                                && (DateToDateBox.IsDateNull || evnt.Time.Value.Date <= DateToDateBox.Date.Value.Date)
                         orderby evnt.Time
                         select evnt).ToList();
            return src;
        }

        public Control GetControl()
        {
            return this;
        }

        public bool Validate()
        {
            return true;
        }

        private void TitleCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                (sender as ComboBox).SelectedIndex = -1;
            }
        }



    }
}
