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
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;

namespace TMN.UI.Windows
{

    public partial class SensorReportWindow : Window
    {
        public SensorReportWindow()
        {
            InitializeComponent();
            dateFromDateBox.Date = DateTime.Now.AddDays(-1);
            dateToDateBox.Date = DateTime.Now;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ReportViewer.LocalReport.ReportEmbeddedResource = @"TMN.Reports.RDLC.SensorsReport.rdlc";

        
        }

        private void Refresh()
        {
            ReportViewer.LocalReport.DataSources.Clear();

            ReportDataSource src = new ReportDataSource("DataSet1");
            var db = new TMNModelDataContext();
            var dateFrom = dateFromDateBox.Date;
            var datetTo = dateToDateBox.Date;
            src.Value = db.SensorDatas.Where(s => s.Sensor.Room.CenterID == Center.Selected.ID && (dateFrom==null || s.Date >= dateFrom) && (datetTo==null || s.Date <= datetTo)).OrderBy(s => s.Date).ThenBy(s => s.Sensor.Room.ID).Select(s => new SensorVal(s));

            ReportViewer.LocalReport.DataSources.Add(src);

            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.PageWidth;
            ReportViewer.RefreshReport();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
