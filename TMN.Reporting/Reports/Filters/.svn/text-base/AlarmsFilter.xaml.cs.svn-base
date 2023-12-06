using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;
using System;

namespace TMN.Reports.Filters
{

    public partial class AlarmsFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public AlarmsFilter()
        {
            InitializeComponent();
            ShiftComboBox.ItemsSource = EnumEnumerator<Shifts>.Enumerator;
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.SourceCenter == Center.CurrentCenterID).OrderBy(r => r.TGNO);
            TypeComboBox.ItemsSource = db.AlarmTypes;
        }

        public ReportDataSource GetDataSource()
        {
            db = DB.Instance;
            ReportDataSource src = new ReportDataSource("Alarm");
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.Alarms);
            var q = from a in db.Alarms.ToArray()
                    where a.CenterID == Center.CurrentCenterID
                        && (ShiftComboBox.SelectedIndex == -1 || a.Shift == (int)ShiftComboBox.SelectedValue.IsNull(0))
                        && (RouteComboBox.SelectedIndex == -1 || a.Route == RouteComboBox.SelectedItem)
                        && (TypeComboBox.SelectedIndex == -1 || a.AlarmType == TypeComboBox.SelectedItem)
                        && (FromReportTimePersianDateBox.IsDateNull || a.ReportTime.Value.Date >= FromReportTimePersianDateBox.Date)
                        && (ToReportTimePersianDateBox.IsDateNull || a.ReportTime.Value.Date <= ToReportTimePersianDateBox.Date)
                        && (CenterTypeComboBox.SelectedIndex == -1 || (CenterTypes)CenterTypeComboBox.SelectedValue == CenterTypes.Null || (a.Route != null && a.Route.Destination != null && a.Route.Destination.CenterType == (int)CenterTypeComboBox.SelectedValue))
                        && (!fromDurationNumericUpDown.Value.HasValue || fromDurationNumericUpDown.Value.Value <= (long)a.DisconnectTimespan.TotalHours)
                        && (!toDurationNumericUpDown.Value.HasValue || toDurationNumericUpDown.Value.Value >= (long)a.DisconnectTimespan.TotalHours)
                    orderby a.DisconnectTime
                    select new Model.Alarm(a);

            src.Value = q.ToArray();
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

    }
}
