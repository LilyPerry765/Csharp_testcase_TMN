using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;
using System;

namespace TMN.Reports.Filters
{

    public partial class LongRecordFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public LongRecordFilter()
        {
            InitializeComponent();
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.SourceCenter == Center.CurrentCenterID).OrderBy(r => r.TGNO);
        }

        public ReportDataSource GetDataSource()
        {
            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.LongRecords);
            ReportDataSource src = new ReportDataSource("LongRecords");
            src.Value = (from lr in db.LongRecords
                         where (FromDatePersianDateBox.IsDateNull || FromDatePersianDateBox.Date <= lr.Date)
                             && (ToDatePersianDateBox.IsDateNull || ToDatePersianDateBox.Date >= lr.Date)
                             && (!FromLengthNumericUpDown.Value.HasValue || FromLengthNumericUpDown.Value <= lr.Length)
                             && (!ToLengthNumericUpDown.Value.HasValue || ToLengthNumericUpDown.Value >= lr.Length)
                             && (RouteComboBox.SelectedIndex == -1 || (Guid?)RouteComboBox.SelectedValue == lr.RouteID)
                             && (ANumberTextBox.Text.Trim() == "" || ANumberTextBox.Text.Trim() == lr.ANumber)
                             && (BNumberTextBox.Text.Trim() == "" || BNumberTextBox.Text.Trim() == lr.BNumber)
                         select new TMN.Reports.Model.LongRecord(lr)).ToList();
            return src;
        }

        public Control GetControl()
        {
            return this;
        }

        public bool Validate()
        {
            // Validation logic goes here...
            return true;
        }

    }
}
