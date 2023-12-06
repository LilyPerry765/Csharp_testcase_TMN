using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TMN.Diagram;
using TMN.Converters;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Timers;
using Enterprise;
using System.Transactions;
using System.Xml.Linq;
using TMN.UserControls;
//using System.Threading;


namespace TMN.UI.Windows
{

    public partial class CircuitRegionWindow : Window, ITabedWindow
    {
        # region Private Variables

        private const int minDistance = 5;
        private Point startPosition;
        private CircuitItem selectedCircuitItem;
        private CircuitItem prevCircuitItem;
        private TMNModelDataContext db;
        private Line currentLine;
        private bool isDrawingLine;
        private bool isMovingLine;
        //private int itemOffset = 0;
        //private Point prevScrollPosition;
        private Timer timer;

        #endregion
        public bool IsOpen
        {
            get
            {
                try
                {
                    bool isop = false;
                    Dispatcher.Invoke(new Action(() =>
                        {
                            if(MainWindow.Instance.tabControl.SelectedItem != null)
                                isop = ((MainWindow.Instance.tabControl.SelectedItem as TabItem).Tag.ToString() == "Circuit Region Panel");
                        }));
                    return isop;
                }
                catch
                {
                    return false;
                }

            }
        }

        #region Constructor

        public CircuitRegionWindow()
        {
            InitializeComponent();
            Root.MouseMove += new System.Windows.Input.MouseEventHandler(Root_MouseMove);
            Root.PreviewMouseUp += new MouseButtonEventHandler(Root_PreviewMouseUp);
            Root.MouseLeftButtonDown += new MouseButtonEventHandler(Root_MouseLeftButtonDown);
            Root.MouseWheel += new MouseWheelEventHandler(Root_MouseWheel);
            
            //LoadLinkTypes();
            RefreshDB();
            loadRegions();
            loadCenters();
            loadSensors();
            RefreshDiagram();
            //RefreshTree();
            Icon = MainWindow.Instance.Icon;
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed); 
            timer.Interval = 500;
            timer.Start();


            muteAllSoundButton.IsMute = bool.Parse(TMN.TextSettings.Get("MUTE_REGION", "false"));

            //Root.Cursor = Cursors.Hand;
        }





        private void loadCenters()
        {
            if(cmbRegion.SelectedValue != null)
                cmbCenter.ItemsSource = db.Centers.Where(c => c.RegionID == Guid.Parse("" + cmbRegion.SelectedValue)) ;
        }

        //XmlDocument regions = null;
        private void loadRegions()
        {
            //XDocument doc = new XDocument();
            //cmbRegion.ItemsSource = from p in doc.Root.Elements("region") select new {Name = p.Attribute("name").Value, connection = p.Attribute("connectionString").Value};
            cmbRegion.ItemsSource = db.Regions;
        }

        void  timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Refresh(IsOpen);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }

        }

        private SoundAlarmSeverities severityForAlarmSound = SoundAlarmSeverities.None;
        private DateTime voiceStartTime = DateTime.Now;
        private int voiceInterval = Setting.Get(Setting.VOICE_ALARM_INTERVAL, Setting.DEFAULT_VOICE_ALARM_INTERVAL);
        private List<SoundAlarmSeverities> unhandledAlarms = new List<SoundAlarmSeverities>();
        private void ManageSound()
        {
            double playedSeconds = DateTime.Now.Subtract(voiceStartTime).TotalSeconds;
            if (muteAllSoundButton.IsMute || severityForAlarmSound == SoundAlarmSeverities.None)
            {
                AlarmPlayer.Stop("circuit");
                voiceStartTime = DateTime.Now;
            }
            else
            {
                if (playedSeconds > voiceInterval * 2)
                {
                    //unhandledAlarms.Add(severityForAlarmSound);
                    severityForAlarmSound = SoundAlarmSeverities.None;
                    AlarmPlayer.Stop("circuit");
                }
                else if (voiceInterval != 0 && playedSeconds > voiceInterval)
                    AlarmPlayer.Stop("circuit");
                else
                {
                    //if (AlarmPlayer.isPlaying == false)
                    //    voiceStartTime = DateTime.Now;
                    //unhandledAlarms.Remove(severityForAlarmSound);
                    AlarmPlayer.Play(severityForAlarmSound, "circuit");
                }
            }
        }


        private void CheckConnectivity()
        {

            Logger.WriteDebug("Region: refreshing connectivity ...");
            using (var db = new TMNModelDataContext())
            {
                if (db.ServiceStates.ToArray().Any(s => s.ServiceType == (int)ServiceTypes.CircuitService && s.IsConnected == false))
                {
                    if (severityForAlarmSound != SoundAlarmSeverities.Information && !unhandledAlarms.Contains(SoundAlarmSeverities.Information))
                    {
                        voiceStartTime = DateTime.Now;
                        severityForAlarmSound = SoundAlarmSeverities.Information; //use information for play alarms of disconnection
                    }
                    Dispatcher.Invoke(new Action(() =>
                {
                    connectLed.DisplayMode = DisplayModes.On;
                    connectLed.InnerBackground = Brushes.Red;
                }));
                }
                else
                {
                    if (unhandledAlarms.Contains(SoundAlarmSeverities.Information))
                        unhandledAlarms.Remove(SoundAlarmSeverities.Information);
                    severityForAlarmSound = SoundAlarmSeverities.None;

                    Dispatcher.Invoke(new Action(() =>
                {
                    connectLed.DisplayMode = DisplayModes.On;
                    connectLed.InnerBackground = Brushes.LimeGreen;
                }));
                }
            }

        }


        private void Refresh(bool openStatus)
        {
           timer.Stop();
            if (openStatus)
            {
                try
                {
                    //refreshStatusTextblock.Visibility = Visibility.Visible;
                    Setting.IsDirty();
                    CheckConnectivity();
                    RefreshAlarms();
                    ManageSound();
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
                finally
                {
                    //refreshStatusTextblock.Visibility = Visibility.Hidden;
                }
            }
            else
                AlarmPlayer.Stop("circuit");

            timer.Start();
        }
        private void loadSensors()
        {
            cmbSensor.ItemsSource = from s in db.Sensors
                                    join cl in db.CircuitLinks on s.ID equals cl.CircuitID into scl_join
                                    from j in scl_join.DefaultIfEmpty()
                                    where j.CircuitID==null && s.Room.Center == cmbCenter.SelectedItem && s.ModulNumber > 100
                                    select s;
                //db.Sensors.Join(db.CenterLinks.Select()   //.Where(s => s.Room.Center == cmbCenter.SelectedItem && s.ModulNumber > 100);
        }

        #endregion

        #region Properties

        //private Point ItemPosition
        //{
        //    get
        //    {
        //        Point currentScrollPosition = new Point(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset);
        //        if (currentScrollPosition != prevScrollPosition)
        //        {
        //            itemOffset = 0;
        //            prevScrollPosition = currentScrollPosition;
        //        }
        //        if (itemOffset == 100)
        //        {
        //            itemOffset = 0;
        //        }
        //        else
        //        {
        //            itemOffset += 2;
        //        }
        //        return currentScrollPosition + new Vector(itemOffset, itemOffset);
        //    }
        //}

        /// <summary>
        /// Gets whether we are in line mode
        /// </summary>
        private bool IsInLineMode
        {
            get
            {
                return btnLine.IsChecked ?? false;
            }
        }

        private bool IsInCircleMode
        {
            get
            {
                return btnCircle.IsChecked ?? false;
            }
        }
        #endregion

        #region Refresh Methods

        private void RefreshDiagram()
        {
            CircuitItem circuitItem;
            Root.Children.Clear();
            foreach (CircuitInMap item in db.CircuitInMaps)
            {
                AddCircuitItem(new CircuitItem(item, db));
            }
            List<CircuitLink> links = null;
            if (cmbFilterCenter.SelectedValue != null)
                links = db.CircuitLinks.Where(cl => cl.CenterID == (Guid)cmbFilterCenter.SelectedValue).ToList();
            else
                links = db.CircuitLinks.ToList();

            foreach (CircuitLink circuitLink in links)
            {
                Line line = CreateLinkLine(circuitLink);
                if ((circuitItem = FindCircuitItem(circuitLink.CircuitInMap)) == null)
                {
                    circuitItem = AddCircuitItem(new CircuitItem(circuitLink.CircuitInMap, db));
                }
                circuitItem.StartLinking(line, CircuitItem.LinkingMode.Loading);

                if ((circuitItem = FindCircuitItem(circuitLink.CircuitInMap1)) == null)
                {
                    circuitItem = AddCircuitItem(new CircuitItem(circuitLink.CircuitInMap1, db));
                }
                if (!circuitItem.TerminateLinking(line, CircuitItem.LinkingMode.Loading))
                    circuitItem.CancelLinking(line);
            }

        }

        public class sensorvalue
        {
            public sensorvalue(Sensor s, double? lv)
            {
                sensor = s;
                lastValue = lv;
            }
            public Sensor sensor;
            public double? lastValue;
        }
        List<sensorvalue> sensors = null; 
        private DateTime lastGetDate = DateTime.Now;

        private void RefreshAlarms()
        {
                      
            if(DateTime.Now.Subtract(lastGetDate).Minutes >= 2 || sensors == null)
            {
            var sens = db.Sensors.Where(s => s.ModulNumber > 100).Select(s => new {Sensor = s, Last = s.SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => (double?)sd.Value).FirstOrDefault()}).Where(t => t.Last != (double)CircuitEnum.NORMAL).ToArray();
            sensors = sens.Select(s=> new sensorvalue(s.Sensor, s.Last)).ToList();
            lastGetDate = DateTime.Now;
            }
            Dispatcher.Invoke(new Action(()=>
                {
                    
                    foreach (object item in Root.Children)
                    {
                        if (item is Line)
                        {
                            Line l = (Line)item;
                            CircuitLink cl = l.DataContext as CircuitLink;
                            Brush b = null;
                            var sn = sensors.SingleOrDefault(s => s.sensor.ID == cl.CircuitID);
                            if (sn != null)
                            {
                                if ((CircuitEnum)sn.lastValue.Value == CircuitEnum.OpenCircuit)
                                {
                                    b = Brushes.DarkRed;
                                }
                                if ((CircuitEnum)sn.lastValue.Value == CircuitEnum.ShortCircuit)
                                {
                                    b = Brushes.DarkOrange;
                                }
                                if (l.Stroke == b)
                                    l.Stroke = (Brush)new BrushConverter().ConvertFromString(cl.Color);
                                else
                                    l.Stroke = b;
                            }
                        }
                    }
                    RefreshTree();
                }));
        }

        
        private void RefreshDB()
        {
            if (Tree != null)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db = DB.Instance;
            }
        }

        private void RefreshTree()
        {
            //Tree.Items.Clear();
            Tree.ItemsSource = sensors;
            //foreach (Sensor s in sensors.Select(s => s.sensor))
            //{
            //    Tree.Items.Add(s);
            //}
        }


        private void TreeViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                CircuitLink link = (Tree.SelectedItem as sensorvalue).sensor.CircuitLinks.FirstOrDefault();
                if (link != null)
                {
                    int x = link.CircuitInMap.X > link.CircuitInMap1.X ? link.CircuitInMap.X : link.CircuitInMap1.X;
                    int y = link.CircuitInMap.Y > link.CircuitInMap1.Y ? link.CircuitInMap.Y : link.CircuitInMap1.Y;
                    ZoomSlider.Value = 1;
                    scrollViewer.ScrollToVerticalOffset(y - scrollViewer.ViewportHeight / 2);
                    scrollViewer.ScrollToHorizontalOffset(x - scrollViewer.ViewportWidth / 2);
                    
                }
            }
        }

        //private void TreeViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        AddCircuitItem(new CircuitItem(Tree.SelectedItem as Center, db));
        //        btnLine.IsChecked = false;
        //        RefreshTree();
        //    }
        //}
        #endregion

        //private void LoadLinkTypes()
        //{
        //    foreach (var item in Enum.GetValues(typeof(LinkTypes)))
        //    {
        //        CheckBox chk = new CheckBox()
        //        {
        //            Content = item,
        //            Foreground = (Brush)Converters.LinkTypesColorConverter.Instance.Convert(item, null, null, null)
        //        };
        //        chk.Checked += new RoutedEventHandler(chk_Checked);
                
        //        LinkTypesListBox.Items.Add(chk);
        //    }
        //}

        void chk_Checked(object sender, RoutedEventArgs e)
        {
            btnLine.IsChecked = true;
        }

        //private List<Center> FilteredCenters()
        //{
        //    return db.Centers.Where(c =>
        //            (cmbCenterType.SelectedValue == null
        //            || (CenterTypes)cmbCenterType.SelectedValue == CenterTypes.Null
        //            || ((CenterTypes?)c.CenterType == (CenterTypes?)cmbCenterType.SelectedValue)
        //            || (c == Center.Current))).ToList();
        //}

        private CircuitItem AddCircuitItem(CircuitItem ci)
        {
            if (!Root.Children.Contains(ci))
            {
                ci.MouseDown += new MouseButtonEventHandler(CircuitItem_MouseDown);
                ci.Removed += (s, ee) =>
                {
                    //db.CenterInMaps.DeleteOnSubmit(ci.CircuitInMap);
                    db.SubmitChanges();
                    RefreshDiagram();
                    //RefreshTree();
                };
                Root.Children.Add(ci);
                Canvas.SetZIndex(ci, 100);
                //if (ci.Center.X == null && ci.Center.Y == null)
                {
                    //Canvas.SetTop(ci, ci.Y);
                    //Canvas.SetLeft(ci, ci.X);
                }
            }
            return ci;
        }

        private Line CreateLinkLine(CircuitLink dataContext)
        {
            Line l = new Line()
                {
                    Stroke = (Brush)new BrushConverter().ConvertFromString(dataContext.Color),
                    DataContext = dataContext,
                    StrokeThickness = 6,
                };
            BindToolTip(l);
            l.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed && CanMoveLine(l))
                {
                    StartMovingLine(l, e.GetPosition(l));
                }
            };
            return l;
        }

        private void BindToolTip(DependencyObject objectToBind)
        {
            MultiBinding binding = new MultiBinding()
            {
                Converter = StringFormatConverter.Instance,
                ConverterParameter = "مسیر:" + " {0}" + "\n" + "مرکز:" + " {1}"
            };
            binding.Bindings.Add(new Binding("Title"));
            binding.Bindings.Add(new Binding("Center.Name"));
            //{
            //    Converter = LinkTypesConverter.Instance
            //}
            BindingOperations.SetBinding(objectToBind, Line.ToolTipProperty, binding);
        }

        private void StartMovingLine(Line l, Point startPoint)
        {
            isMovingLine = true;
            currentLine = l;
            CircuitItem ci1 = prevCircuitItem = FindNearestCircuitItemFromLine(l, startPoint);
            selectedCircuitItem = FindOtherCircuitItemOfLine(l, ci1);
            ci1.StartMovingLink(l);
        }


        #region Find Methods

        /// <summary>
        /// Finds the CircuitItem whose Center is given.
        /// </summary>
        /// <param x:Name="center">The Center to find its associated CircuitItem</param>
        /// <returns>The associated CircuitItem with the given center</returns>
        private CircuitItem FindCircuitItem(CircuitInMap cim)
        {
            foreach (var item in Root.Children)
            {
                if (item is CircuitItem && (item as CircuitItem).CircuitInMap.ID == cim.ID)
                {
                    return item as CircuitItem;
                }
            }
            return null;
        }

        private CircuitItem FindOtherCircuitItemOfLine(Line l, CircuitItem thisCircuitItem)
        {
            if ((l.DataContext as CircuitLink).CircuitInMap == thisCircuitItem.CircuitInMap)
            {
                return FindCircuitItem((l.DataContext as CircuitLink).CircuitInMap1);
            }
            else
            {
                return FindCircuitItem((l.DataContext as CircuitLink).CircuitInMap);
            }
        }

        /// <summary>
        /// Gets the nearest CircuitItem to the specified Point on the given Line
        /// </summary>
        private CircuitItem FindNearestCircuitItemFromLine(Line line, Point p)
        {
            if (Math.Abs(p.Y - line.Y1) < Math.Abs(p.Y - line.Y2) || Math.Abs(p.X - line.X1) < Math.Abs(p.X - line.X2))
            {

                return FindCircuitItem((line.DataContext as CircuitLink).CircuitInMap1);
            }
            else
            {
                return FindCircuitItem((line.DataContext as CircuitLink).CircuitInMap);
            }
        }

        #endregion

        #region Mouse Event Handlers

        void CircuitItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount == 2)
            //{
            //    if (`)
            //    {
            //        //RefreshDB();
            //        RefreshDiagram();
            //    }
            //}
            //else
            {
                startPosition = e.GetPosition(sender as IInputElement);
                selectedCircuitItem = sender as CircuitItem;
                if (IsInLineMode)
                {
                    isDrawingLine = true;
                    currentLine = CreateLinkLine(
                     new CircuitLink()
                        {
                            ID = Guid.NewGuid(),
                            Title = (cmbSensor.SelectedItem as Sensor).Title,
                            Color = cmbColor.SelectedColor.ToString(),
                            CenterID  = (Guid)cmbCenter.SelectedValue,
                            CircuitID = (Guid)cmbSensor.SelectedValue,
                            AMapID = selectedCircuitItem.CircuitInMap.ID
                        });
                    selectedCircuitItem.StartLinking(currentLine, CircuitItem.LinkingMode.Creating);
                }
            }
        }

        //private int CollectLinkTypes()
        //{
        //    int result = 0;
        //    foreach (var item in LinkTypesListBox.Items.Cast<CheckBox>())
        //    {
        //        if (item.IsChecked == true)
        //        {
        //            result |= (int)item.Content;
        //        }
        //    }
        //    return result;
        //}

        void Root_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            lastDragPoint = Mouse.GetPosition(Root);

            if (e.Delta > 0)
            {
                ZoomSlider.Value += .01;
            }
            if (e.Delta < 0)
            {
                ZoomSlider.Value -= .01;
            }

            e.Handled = true;

        }

        void Root_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && selectedCircuitItem != null)
            {
                if (isDrawingLine || isMovingLine)
                {
                    // Decreasing one pixel is for preventing the line from dropping under the cursor and resulting in passing the line instead of the CircuitItem to the MouseUp event
                    currentLine.X2 = e.GetPosition(Root).X - 1;
                    currentLine.Y2 = e.GetPosition(Root).Y - 1;
                }
                else
                {
                    double w = 1;
                    double h = 1;
                    double left = minDistance + Math.Round((e.GetPosition(Root).X - startPosition.X) / w) * w;
                    double top = minDistance + Math.Round((e.GetPosition(Root).Y - startPosition.Y) / h) * h;
                    if (left >= minDistance & left <= Root.ActualWidth - minDistance - selectedCircuitItem.Width)
                    {
                        Canvas.SetLeft(selectedCircuitItem, left);
                    }
                    if (top >= minDistance & top <= Root.ActualHeight - minDistance - selectedCircuitItem.Height)
                    {
                        Canvas.SetTop(selectedCircuitItem, top);
                    }
                }
            }
            else if (e.LeftButton == MouseButtonState.Pressed )
            {
                Point p = e.GetPosition(scrollViewer);
                PanView(p);

            }
        }

        private void PanView(Point posNow)
        {
            if (lastDragPoint.HasValue)
            {
              
                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }
        }

        private bool CanMoveLine(Line line)
        {
            // Currently there is no relation between centerlink and other parts of system, so there is no restriction for deleting.
            return true;

            //if (Route.ExistsOn(line.DataContext.As<CircuitLink>().Center, line.DataContext.As<CircuitLink>().Center1))
            //{
            //    MessageBox.ShowError("اين ارتباط قابل تغيير نيست.");
            //    return false;
            //}
            //return true;
        }

        private bool CanDeleteCurrentCircuitLink()
        {
            // Currently there is no relation between centerlink and other parts of system, so there is no restriction for deleting.
            return true;

            //if (!Route.ExistsOn(selectedCircuitItem.Center, prevCircuitItem.Center))
            //    return true;
            //MessageBox.Show(MessageTypes.CannotDelete);
            //return false;
        }

        Point? lastDragPoint;
        void Root_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y <
                scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                Root.Cursor = Cursors.Hand;
                lastDragPoint = mousePos;
                Mouse.Capture(Root);
            }

        }
        
        void Root_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            if (isDrawingLine || isMovingLine)
            {
                if (e.Source is Canvas || e.Source is Line) // Canceling: Line released in a free space on canvas or on another line
                {
                    if (!isMovingLine || (MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes && CanDeleteCurrentCircuitLink()))
                    {
                        selectedCircuitItem.CancelLinking(currentLine);
                    }
                    else
                    {
                        selectedCircuitItem.CancelLinking(currentLine, prevCircuitItem);
                    }
                }
                else // Finishing: Line released on a CircuitItem or its child elements
                {
                    CircuitItem targetCircuitItem = (e.Source as FrameworkElement).GetParent<CircuitItem>();
                    if (targetCircuitItem == selectedCircuitItem)
                    {
                        selectedCircuitItem.CancelLinking(currentLine);
                    }
                    else
                    {
                        if (!targetCircuitItem.TerminateLinking(currentLine, CircuitItem.LinkingMode.Creating))
                            selectedCircuitItem.CancelLinking(currentLine, prevCircuitItem);
                    }
                }
                isDrawingLine = false;
                isMovingLine = false;
            }
            else if(selectedCircuitItem == null && IsInCircleMode == true)
            {
               
                Point p = e.GetPosition(Root);
                CircuitItem ci = new CircuitItem(new CircuitInMap() { ID= Guid.NewGuid(), X = Convert.ToInt32(p.X - 15), Y = Convert.ToInt32(p.Y - 15), Color = Colors.Green.ToString(), Title = "" }, db);
                AddCircuitItem(ci);

                db.CircuitInMaps.InsertOnSubmit(ci.CircuitInMap);
                db.SubmitChanges();
            }
            
            selectedCircuitItem = null;
            prevCircuitItem = null;
            Root.ReleaseMouseCapture();
            Root.Cursor = Cursors.Arrow;
            lastDragPoint = null;


        }


        #endregion


        #region UI Event Handlers

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDB();
            RefreshDiagram();
            //RefreshTree();
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value += (ZoomSlider.Maximum - ZoomSlider.Minimum) / 5;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value -= (ZoomSlider.Maximum - ZoomSlider.Minimum) / 5;
        }


        #endregion




        private void btnLine_Checked(object sender, RoutedEventArgs e)
        {

            if (cmbCenter.SelectedValue == null)
            {
                MessageBox.ShowWarning("مرکز انتخاب نشده است", MessageBoxButton.OK);
                btnLine.IsChecked = false;
            }
            else if (cmbSensor.SelectedValue == null)
            {
                MessageBox.ShowWarning("کابل انتخاب نشده است", MessageBoxButton.OK);
                btnLine.IsChecked = false;
            }
            else if (cmbColor.SelectedColor == null)
            {
                MessageBox.ShowWarning("رنگ انتخاب نشده است", MessageBoxButton.OK);
                btnLine.IsChecked = false;
            }
            if (btnLine.IsChecked == true)
            {
                cmbCenter.IsEnabled = false;
                cmbSensor.IsEnabled = false;
                cmbColor.IsEnabled = false;
                lblLineLabel.Content = "پایان";
                btnCircle.IsChecked = false;
                Root.Cursor = Cursors.Pen;
            }
        }
    
        private void btnLine_Unchecked(object sender, RoutedEventArgs e)
        {
            Root.Cursor = Cursors.Arrow;
            lblLineLabel.Content = " کابل";
            cmbCenter.IsEnabled = true;
            cmbSensor.IsEnabled = true;
            cmbColor.IsEnabled = true;
            loadSensors();
  
    
        }

        private void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadCenters();
        }

        private void btnCircle_Checked(object sender, RoutedEventArgs e)
        {
            Root.Cursor = Cursors.Pen;
            btnLine.IsChecked = false;
        }

        private void btnCircle_Unchecked(object sender, RoutedEventArgs e)
        {
            Root.Cursor = Cursors.Arrow;

        }

        private void cmbCenter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSensors();
        }

        private void cmbFilterCenter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDiagram();
        }

        private void cmbFilterRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }




        public void TabClosed()
        {
            //throw new NotImplementedException();
        }

        public void TabOpened()
        {
            //throw new NotImplementedException();
        }

        private void connectLed_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            voiceStartTime = DateTime.MinValue;
            ManageSound();
            new ServiceStateWindow(ServiceTypes.CircuitService).Show();
        }

        private void muteAllSoundButton_IsMuteChanged(object sender, EventArgs e)
        {
            TMN.TextSettings.Set("MUTE_REGION", muteAllSoundButton.IsMute.ToString());
            ManageSound();
        }



    }
}
