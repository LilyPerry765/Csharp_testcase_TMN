using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using TMN.Reports.Model;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for RoutesReport.xaml
    /// </summary>
    public partial class TrunksFilter : UserControl, IReportFilter
    {
        public TrunksFilter()
        {
            InitializeComponent();
            SwitchTypeComboBox.ItemsSource = DB.Instance.SwitchTypes;
        }

        public ReportDataSource GetDataSource()
        {
            TMNModelDataContext db = new TMNModelDataContext();
            ReportDataSource src = new ReportDataSource("Trunks");

            src.Value = (from c in db.Channels.Where(c => c.Link.CenterID == Center.CurrentCenterID).ToList()
                         where ((signalCheckBox.IsChecked == false) || (c.Route.IsSignaling ?? false))
                             && c.Route.TGNO.IsNull("").ToUpper().Contains(tgnoTextBox.Text.Trim().ToUpper())
                             && c.Route.RouteName.IsNull("").ToUpper().Contains(routNameTextBox.Text.Trim().ToUpper())
                             && c.Link.Address.IsNull("").ToUpper().Contains(AddressTextBox.Text.Trim().ToUpper())
                             && (c.Route.Destination == null || c.Route.Destination.Name.ToUpper().Contains(destTextBox.Text.Trim().ToUpper()))
                             && (CenterTypeComboBox.SelectedValue == null || c.Route.Destination == null || c.Route.Destination.CenterType.Value == (int)CenterTypeComboBox.SelectedValue)
                             && (SwitchTypeComboBox.SelectedValue == null || c.Route.Destination == null || c.Route.Destination.Switch == (Guid)SwitchTypeComboBox.SelectedValue)
                         orderby c.Link.Address, c.LNO
                         select new Trunks(c)).ToList();

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
