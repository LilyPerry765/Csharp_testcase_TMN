using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for EventsReport.xaml
    /// </summary>
    public partial class FailureReasonPieFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public FailureReasonPieFilter()
        {
            InitializeComponent();
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.Alarms.Count() > 0 && r.SourceCenter == Center.CurrentCenterID);
        }

        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("TMN_Reports_Model_FailurePieChart");
            src.Value = (from f in db.Alarms
                         where f.Route == RouteComboBox.SelectedItem
                         && (!DateFromDateBox.Date.HasValue || f.DisconnectTime >= DateFromDateBox.Date.Value)
                         && (!f.ConnectTime.HasValue || !DateToDateBox.Date.HasValue || f.ConnectTime <= DateToDateBox.Date.Value)
                         select f).ToList();

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
