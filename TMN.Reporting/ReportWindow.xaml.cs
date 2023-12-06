using System.Windows;
using System.Windows.Controls;
using Microsoft.Reporting.WinForms;
using TMN.Interfaces;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        IReportFilter Filter;

        public ReportWindow(string reportName, IReportFilter filter)
        {
            InitializeComponent();
            this.ReportViewer.LocalReport.ReportEmbeddedResource = @"TMN.Reports.RDLC." + reportName + ".rdlc";
            this.Filter = filter;
            Control ctrl = filter.GetControl();
            FilterHolder.Children.Add(ctrl);
            Grid.SetColumn(ctrl, 0);
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            if (Filter.Validate())
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(Filter.GetDataSource());
                ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                ReportViewer.ZoomMode = ZoomMode.PageWidth;
                ReportViewer.RefreshReport();
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

    }
}
