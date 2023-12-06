using System.Linq;
using System.Windows.Controls;
using Microsoft.Reporting.WinForms;
using TMN.Interfaces;
using TMN.Reports.Model;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for RoutesReport.xaml
    /// </summary>
    public partial class RoutesFilter : UserControl, IReportFilter
    {
        public RoutesFilter()
        {
            InitializeComponent();
        }

        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("Routes");
            TMNModelDataContext db = DB.Instance;
            src.Value = (from r in db.Routes
                         where r.SourceCenter == Center.CurrentCenterID
                             && (txtInternalRouteName.Text.IsNullOrEmpty() || r.TGNO.Contains(txtInternalRouteName.Text))
                             && (txtRouteName.Text.IsNullOrEmpty() || r.RouteName.Contains(txtRouteName.Text))
                             && (txtOPC.Text.IsNullOrEmpty() || r.Center.PointCode == txtOPC.Text)
                             && (txtDPC.Text.IsNullOrEmpty() || r.Dest.Center.PointCode == txtDPC.Text)
                         orderby r.TGNO
                         select new Routes
                         {
                             RouteName = r.RouteName,
                             TGNO = r.TGNO,
                             Dest = r.Dest,
                             Protocol = Converters.ProtocolsConverter.Instance.Convert(r.Protocol, typeof(string), null, null).IsNull("-").ToString(),
                             DPC = r.DPC,
                             OPC = r.OPC,
                             LinkCount = r.Channels.GroupBy(p => p.LinkID).Count(),
                             ChannelCount = r.Channels.Count(),
                             IsSignaling = r.IsSignaling
                         }).ToList();
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
