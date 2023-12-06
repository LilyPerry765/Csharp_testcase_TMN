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
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for TasksReport.xaml
    /// </summary>
    public partial class TasksFilter : UserControl, IReportFilter
    {
        TMNModelDataContext db = DB.Instance;
        public TasksFilter()
        {
            InitializeComponent();
            ShiftCombo.ItemsSource = EnumEnumerator<Shifts>.Enumerator;
            TitleCombo.ItemsSource = db.TaskTypes;
        }

        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("Tasks");
            src.Value = (from result in
                             (from task in db.Tasks
                              where task.User.Center == Center.Current
                              && (ShiftCombo.SelectedIndex == -1 || !task.Shift.HasValue || task.Shift == (int)ShiftCombo.SelectedValue.IsNull(0))
                              && (TitleCombo.SelectedIndex == -1 || task.TaskType == (TitleCombo.SelectedItem as TaskType))
                              select new TMN.Reports.Model.Tasks()
                              {
                                  Title = task.TaskType.Name,
                                  ChannelCount = task.ChannelCount,
                                  Route = task.Route,
                                  Comment = task.Comment,
                                  UserName = task.User.FullName,
                                  FinishDate = task.FinishDate,
                                  DueDate = task.DueDate,
                                  IsDone = task.IsDone,
                                  Shift =  Enum.GetName(typeof(Shifts), task.Shift ?? 0)
                              }).ToList()
                         where (DateFromDateBox.IsDateNull || result.FinishDate >= DateFromDateBox.Date.Value.Date)
                                && (DateToDateBox.IsDateNull || result.FinishDate < DateToDateBox.Date.Value.AddDays(1))
                         orderby result.DueDate
                         select result).ToList();
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
