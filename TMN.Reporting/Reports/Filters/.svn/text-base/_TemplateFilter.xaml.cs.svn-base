using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;
using System;

namespace TMN.Reports.Filters
{

    public partial class _TemplateFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public _TemplateFilter()
        {
            InitializeComponent();
        }

        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("Report_DataSource_Name");
            //src.Value = db.Samples.Where(Condition).ToList();
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
