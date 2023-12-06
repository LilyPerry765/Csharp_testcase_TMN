using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using Enterprise;
using Microsoft.Win32;
using System.Windows.Input;

namespace TMN.UI.Windows
{

    public partial class SensorChartWindow : Window
    {
        private Guid? roomID;
        private Center center;
        private int? moduleNumber = null;
        private SensorTypes sensorType;
        private Timer timer = new Timer();
        private System.Threading.Thread refresherThread;
        private const string threasholdSeriesNamePrefix = "th";

        private SensorChartWindow()
        {
            InitializeComponent();
            refresherThread = new System.Threading.Thread(Refresh);
            chart.Printing.PrintDocument.DefaultPageSettings.Landscape = true;
            dateFromDateBox.Date = DateTime.Now.AddHours(-24);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 2000;
            // Timer will start if user selects live chart
            // timer.Start();
        }

        public SensorChartWindow(Center center)
            : this()
        {
            timer.Interval = 5000;
            this.center = center;
            Title += " - " + center.DisplayName;
            BeginRefresh();
        }

        public SensorChartWindow(Guid roomID, SensorTypes sensorType)
            : this()
        {
            this.roomID = roomID;
            this.sensorType = sensorType;
            Title += " - " + DB.Instance.Rooms.Single(r => r.ID == roomID).Center.DisplayName;
            BeginRefresh();
        }

        public SensorChartWindow(Center center, int moduleNumber)
            : this()
        {
            this.moduleNumber = moduleNumber;
            this.center = center;
            Title += " - " + center.DisplayName;
            BeginRefresh();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Dispatcher.BeginInvoke((Action)delegate()
            {
                BeginRefresh();
            });
            timer.Start();
        }

        private DateTime? FromDate
        {
            get
            {
                return dateFromDateBox.Date;
            }
        }

        private DateTime? ToDate
        {
            get
            {
                return dateToDateBox.Date;
            }
        }

        private void BeginRefresh()
        {
            if (ToDate < FromDate)
            {
                MessageBox.ShowError("تاريخ شروع از تاريخ پايان بزرگتر است.");
            }
            else
            {
                if (refresherThread.ThreadState != System.Threading.ThreadState.Running)
                {
                    var x = chart.ChartAreas[0].AxisX;
                    x.Minimum = FromDate.HasValue ? FromDate.Value.ToOADate() : double.NaN;
                    x.Maximum = (ToDate ?? DateTime.Now).ToOADate();
                    refresherThread = new System.Threading.Thread(Refresh);
                    refresherThread.Start(new Range<DateTime?>(FromDate, ToDate));
                }
                
            }
        }

        private void Refresh(object dateRange)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate()
                     {
                         Cursor = Cursors.AppStarting;
                     });
            using (var db = new TMNModelDataContext())
            {
                var fromDate = ((Range<DateTime?>)dateRange).From;
                var toDate = ((Range<DateTime?>)dateRange).To;

                IEnumerable<Sensor> sensors;
                if (moduleNumber != null)
                {

                    sensors = (from s in db.Sensors
                               join r in db.Rooms on s.RoomID equals r.ID
                               where r.CenterID == center.ID && s.ModulNumber == moduleNumber
                               select s).ToArray();
                }
                else if (roomID != null)
                {
                    var room = db.Rooms.Single(r => r.ID == roomID.Value);
                    sensors = room.GetSensorsByType(sensorType).ToArray();
                }
                else
                {
                    sensors = db.Sensors.Where(s => s.Room.Center == center).ToArray();
                }
                Func<SensorData, bool> timeCondition = d => (toDate == null || d.Date <= toDate) && (fromDate == null || d.Date >= fromDate);
                var sensorsCache = (from s in sensors
                                    orderby s.RoomID, s.ModulNumber
                                    select Tuple.Create(s, s.SensorDatas.OrderByDescending(d => d.Date).Where(timeCondition).ToArray())).ToArray();

                var sensorData = from s in sensorsCache
                                 select new
                                 {
                                     SensorDatas = s.Item2,
                                     s.Item1.ModulNumber,
                                     s.Item1.Room,
                                     s.Item1.Title,
                                     s.Item1.Max,
                                     s.Item1.Min,
                                     Value = s.Item2.Select(sd => sd.Value).FirstOrDefault(),
                                     MaxVal = s.Item2.Any() ? s.Item2.Max(d => d.Value) : (double?)null,
                                     MinVal = s.Item2.Any() ? s.Item2.Min(d => d.Value) : (double?)null,
                                     AvgVal = s.Item2.Any() ? s.Item2.Average(d => d.Value) : (double?)null,
                                 };

                Dispatcher.Invoke((Action)delegate()
                                     {
                                         try
                                         {
                                             var selectedIndex = sensorsDataGrid.SelectedIndex;
                                             if (sensorData.Any())
                                                 sensorsDataGrid.ItemsSource = sensorData;
                                             else
                                                 sensorsDataGrid.ItemsSource = null;
                                             sensorsDataGrid.SelectedIndex = selectedIndex;
                                             System.Windows.Forms.Application.DoEvents();
                                             if (chart.Series.Count(s => !s.Name.StartsWith(threasholdSeriesNamePrefix)) != sensors.Count())
                                                 DrawChart(sensorsCache);
                                             else
                                                 RefreshChart(sensorsCache);
                                         }
                                         catch (Exception ex)
                                         {
                                             Logger.Write(ex);
                                         }
                                         finally
                                         {
                                             Cursor = Cursors.Arrow;
                                         }
                                     });
            }
        }

        private void DrawChart(IEnumerable<Tuple<Sensor, SensorData[]>> sensorsCache)
        {
            chart.Series.Clear();
            foreach (var sensorTuple in sensorsCache)
            {
                try
                {
                    Sensor sensor = sensorTuple.Item1;
                    SensorData[] data = sensorTuple.Item2;

                    var series = chart.Series.Add(sensor.ModulNumber.ToString());
                    series.LegendText = sensor.Title;
                    series.BackSecondaryColor = System.Drawing.Color.Black;
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 2;
                    series.XValueType = ChartValueType.DateTime;
                    //series.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    series.ToolTip = string.Format("{0}\nValue: #VALY\nDate: #VALX{{yyyy/MM/dd HH:mm}}", sensor.Title);
                    series.Points.DataBind(data.Where(d => d.Value < 100).OrderBy(d => d.Date), "Date", "Value", null);

                    AddThreasholdLine(sensor, series.Color);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
        }

        private void AddThreasholdLine(Sensor sensor, System.Drawing.Color color)
        {
            Series series = new Series(threasholdSeriesNamePrefix + sensor.ModulNumber);
            series.Color = color;
            series.IsVisibleInLegend = false;
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.BorderDashStyle = ChartDashStyle.Dot;
            BindThreasholdLine(series, sensor);
            chart.Series.Add(series);
        }

        private void BindThreasholdLine(Series series, Sensor sensor)
        {
            series.Points.DataBind(new DateTime[] { FromDate.Value, ToDate ?? DateTime.Now }.Select(d => Tuple.Create(d, sensor.Max)), "Item1", "Item2", null);
        }

        private void RefreshChart(IEnumerable<Tuple<Sensor, SensorData[]>> sensorsCache)
        {
            foreach (var sensorTuple in sensorsCache)
            {
                Sensor sensor = sensorTuple.Item1;
                SensorData[] data = sensorTuple.Item2;

                System.Windows.Forms.Application.DoEvents();
                var series = chart.Series.FindByName(sensor.ModulNumber.ToString());
                if (series != null)
                    series.Points.DataBind(data.Where(d => d.Value < 100).OrderBy(d => d.Date), "Date", "Value", null);

                var threasholdSeries = chart.Series.FindByName(threasholdSeriesNamePrefix + sensor.ModulNumber);
                if (threasholdSeries != null)
                    BindThreasholdLine(threasholdSeries, sensor);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            new SensorsWindow().ShowDialog();
        }

        private void chartCopyMenuItem_Click(object sender, EventArgs e)
        {
            using (MemoryStream str = new MemoryStream())
            {
                chart.SaveImage(str, ChartImageFormat.Bmp);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(str);
                System.Windows.Forms.Clipboard.SetImage(bmp);
                bmp.Dispose();
            }
        }

        private void saveChartMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
                        {
                            AddExtension = true,
                            FileName = "SPDG KPI Browser Chart",
                            DefaultExt = "png",
                            Filter = "Png Image (*.png)|*.png|Vector Image(*.emf)|*.emf",
                        };
            if (dlg.ShowDialog() == true)
            {

                string ext = System.IO.Path.GetExtension(dlg.FileName).ToLower();
                switch (ext)
                {
                    case ".png":
                        chart.SaveImage(dlg.FileName, ChartImageFormat.Png);
                        break;
                    case ".emf":
                        chart.SaveImage(dlg.FileName, ChartImageFormat.Emf);
                        break;
                    default:
                        break;
                }
            }
        }

        private void printChartMenuItem_Click(object sender, EventArgs e)
        {
            chart.Printing.Print(true);
        }

        private void pageSetupChartMenuItem_Click(object sender, EventArgs e)
        {
            chart.Printing.PageSetup();
        }

        private void printPreviewMenuItem_Click(object sender, EventArgs e)
        {
            chart.Printing.PrintPreview();
        }

        private void chart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            //Logger.WriteInfo(e.Text);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            BeginRefresh();
        }

        private void liveMenuItem_Click(object sender, EventArgs e)
        {
            liveMenuItem.Checked = true;
            timer.Enabled = liveMenuItem.Checked;
        }

        private void axisYInterval_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (chart == null)
                return;

            try
            {
                if (axisYInterval.Value == null)
                    chart.ChartAreas[0].AxisY.Interval = 2;
                else
                    chart.ChartAreas[0].AxisY.Interval = (double)axisYInterval.Value.Value;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void zeroBasedAxisCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            chart.ChartAreas[0].AxisY.IsStartedFromZero = true;
        }

        private void zeroBasedAxisCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }

        private void autoDateDetectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectdCenterSensorData = DB.Instance.SensorDatas.Where(sd => sd.Sensor.Room.CenterID == Center.Selected.ID);

            dateToDateBox.Date = selectdCenterSensorData.Max(d => d.Date);
            dateFromDateBox.Date = selectdCenterSensorData.Min(d => d.Date);
        }


    }
}
