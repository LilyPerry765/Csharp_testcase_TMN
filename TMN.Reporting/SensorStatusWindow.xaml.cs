using System.Linq;
using System.Windows;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;

namespace TMN.UI.Windows
{

    public partial class SensorStatusWindow : Window
    {
        public SensorStatusWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ReportViewer.LocalReport.ReportEmbeddedResource = @"TMN.Reports.RDLC.SensorsStatusReport.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();

            ReportDataSource src = new ReportDataSource("DataSet1");

            var db = new TMNModelDataContext();
            src.Value = db.Sensors.Where(s => s.Room.CenterID == Center.Selected.ID).OrderBy(s=>s.ModulNumber).Select(s => new SensorState(s));

            ReportViewer.LocalReport.DataSources.Add(src);

            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.PageWidth;
            ReportViewer.RefreshReport();
        }
    }
}
