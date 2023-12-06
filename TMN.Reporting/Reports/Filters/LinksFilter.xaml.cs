using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;
using Microsoft.Reporting.WinForms;
using System;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for RoutesReport.xaml
    /// </summary>
    public partial class LinksFilter : UserControl, IReportFilter
    {
        public LinksFilter()
        {
            InitializeComponent();
            SwitchTypeComboBox.ItemsSource = DB.Instance.SwitchTypes;
        }

        public ReportDataSource GetDataSource()
        {
            TMNModelDataContext db = new TMNModelDataContext();
            ReportDataSource src = new ReportDataSource("Links");

            var channels = (from l in db.Links
                            join c in db.Channels on l.ID equals c.LinkID
                            join r in db.Routes on c.RouteID equals r.ID
                            where l.CenterID == Center.CurrentCenterID
                            select c).ToArray();

            var links = (from c in channels
                         where (
                                ((signalCheckBox.IsChecked == false) || (c.Route.IsSignaling ?? false))
                                && c.Route.TGNO.IsNull("").ToUpper().Contains(tgnoTextBox.Text.Trim().ToUpper())
                                && c.Link.Address.IsNull("").Contains(AddressTextBox.Text.Trim())
                                && c.Route.RouteName.IsNull("").ToUpper().Contains(routNameTextBox.Text.Trim().ToUpper())
                                && (c.Route.Destination == null || c.Route.Destination.Name.ToUpper().Contains(destTextBox.Text.Trim().ToUpper()))
                                && (CenterTypeComboBox.SelectedValue == null || c.Route.Destination == null || c.Route.Destination.CenterType.Value == (int)CenterTypeComboBox.SelectedValue)
                                && (SwitchTypeComboBox.SelectedValue == null || c.Route.Destination == null || c.Route.Destination.Switch == (Guid)SwitchTypeComboBox.SelectedValue))
                         group c by c.Link into g
                         select g.Key).Distinct().OrderBy(l => l.Address).ToArray();

            if (freeLinksCheckBox.IsChecked == true)
                links = links.Union(db.Links.Where(l => !l.Channels.Any()).ToArray()).ToArray();

            src.Value = (from l in links
                         select new TMN.Reports.Model.Links()
                          {
                              Address = l.Address,
                              DDF = l.DDF,
                              CIC = l.CIC.ToString(),
                              Sys = l.Sys.ToString(),
                              FirstChannel = l.Channels.FirstOrDefault(),
                          });
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
