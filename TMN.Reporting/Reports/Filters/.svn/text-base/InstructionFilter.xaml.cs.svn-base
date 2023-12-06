using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;
using System;

namespace TMN.Reports.Filters
{

    public partial class InstructionFilter : UserControl, IReportFilter
    {

        public InstructionFilter()
        {
            InitializeComponent();
            DestinationComboBox.ItemsSource = DB.Instance.Centers.Where(c => c.ID != Center.CurrentCenterID);
        }


        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("Instructions");
            src.Value = (from i in DB.Instance.Instructions
                         where (!IsDoneCheckBox.IsChecked.HasValue || (i.IsDone ?? false) == IsDoneCheckBox.IsChecked)
                         && (DestinationComboBox.SelectedIndex == -1 || i.Destination == (Guid?)DestinationComboBox.SelectedValue)
                         && (FromIssueDatePersianDateBox.IsDateNull || i.IssueDate.Value >= FromIssueDatePersianDateBox.Date)
                         && (ToIssueDatePersianDateBox.IsDateNull || i.IssueDate.Value <= ToIssueDatePersianDateBox.Date)
                         && i.User.CenterID == Center.CurrentCenterID
                         select new Model.Instruction(i)).ToList();

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
