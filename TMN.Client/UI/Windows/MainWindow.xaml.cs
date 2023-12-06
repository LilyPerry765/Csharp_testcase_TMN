using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Reports.Filters;
using System.Timers;
using Enterprise;
using System.Reflection;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;

namespace TMN.UI.Windows
{

    public partial class MainWindow : Window
    {

        public MainWindow(bool Maximized)
        {
            if (Maximized)
            {
                //Application.Current.MainWindow.WindowState = System.Windows.WindowState.Maximized;
                //new AlarmRegionWindow().Show();

                new AlarmRegionWindow().Show();

            }
        }

        public MainWindow()
        {
            try
            {

                CheckLicense();
                CheckDatabaseConnection();
                Logger.WriteInfo("Initializing components...");

//--------------------- login ------------------------------------------------------

                SetDefaultCenter();

                //if (User.LoginDefaultUserIfNoUserDefined())
                //    UpdateUserStatus();
                //else
                //    LogIn();


                if (!User.LoginDefaultUserIfNoUserDefined())
                    LogIn();
                    

//----------------------------------------------------------------------------------

                InitializeComponent();
                Logger.WriteInfo("Components initialized.");
                SetCenterDelegates();
                Title += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3) + (Environment.GetCommandLineArgs().Contains("/multi") ? " (Multi Instance Mode)" : "");
                Logger.WriteInfo("Initializing ClockTimer...");
                InitializeClockTimer();
                Logger.WriteInfo("ClockTimer initialized.");
                DatabaseDiagnostics.Disconnected += new Action(DatabaseDiagnostics_Disconnected);
                DatabaseDiagnostics.Connected += new Action(DatabaseDiagnostics_Connected);
                DatabaseDiagnostics.ActiveConnectionChanged += new Action(ShowServerName);
                tabControl.SelectionChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);
                ShowServerName();
                InitToolsMenu();

                UpdateUserStatus();

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }



        private void CheckNewUpdate()
        {
            string newUpdateVer = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            string oldUpdateVer = RegSettings.Get("UpdateVersion", "").ToString();
            if (newUpdateVer != oldUpdateVer)
            {
                if (oldUpdateVer == "" || newUpdateVer.Substring(0, 2) != oldUpdateVer.Substring(0, 2))
                    RegSettings.Save("UpdateTime", DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
                else if (newUpdateVer.Substring(0, 4) != oldUpdateVer.Substring(0, 4))
                    RegSettings.Save("UpdateTime", DateTime.Now.AddDays(10).ToString("yyyy-MM-dd"));
                else
                    RegSettings.Save("UpdateTime", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                RegSettings.Save("UpdateVersion", newUpdateVer);
            }
            string updateTime = "" + RegSettings.Get("UpdateTime", DateTime.Now.ToString("yyyy-MM-dd"));
            if (DateTime.Parse(updateTime) > DateTime.Now)
                mnuNewUpdate_Click(null, null);
            
        }

        void ShowServerName()
        {
            serverStatus.Content = "Server: " + DatabaseDiagnostics.ServerName;
        }

        void DatabaseDiagnostics_Connected()
        {
            MessageBox.ShowInfo("ارتباط با سرور برقرار شد.", "ارتباط");
        }

        void DatabaseDiagnostics_Disconnected()
        {
            MessageBox.ShowError("ارتباط با سرور قطع شد.");
        }

        #region Setting Center Delegates

        private void SetCenterDelegates()
        {
            Logger.WriteInfo("Setting Center Delegates...");
            Center.SetSelectedCenter = SetSelectedCenter;
            //  Center.GetSelectedCenter = GetSelectedCenter;
            Center.SetDefaultCenter = SetDefaultCenter;
            Logger.WriteInfo("Center Delegates Set.");
        }

        public static void SetDefaultCenter()
        {
            if (DB.Instance.Centers.Any())
            {
                while (Center.CurrentCenterID == Guid.Empty)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (MessageBox.ShowWarning("لطفا در قسمت تنظيمات سيستم وارد بخش (مرکز جاری) شده و مرکز خود را مشخص نماييد.", System.Windows.MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        new SettingsWindow().ShowDialog(MainWindow.Instance);
                    else
                        Application.Current.Shutdown();
                }
            }
        }

        //private static Center GetSelectedCenter()
        //{
        //    return Application.Current.Dispatcher.Invoke((Func<Center>)delegate()
        //    {
        //        if (MainWindow.Instance != null)
        //            return MainWindow.Instance.Tree.SelectedCenter;
        //        return null;
        //    }) as Center;
        //}

        private static void SetSelectedCenter(Center value)
        {
            Application.Current.Dispatcher.Invoke((Action<Center>)delegate(Center c)
            {
                if (MainWindow.Instance != null)
                    MainWindow.Instance.Tree.SelectedCenter = value;
            }, value);
        }

        #endregion

        private int lastTabIndex = 0;
        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex != lastTabIndex)
            {
                OnTabChanged();
                lastTabIndex = tabControl.SelectedIndex;
            }
        }

        public event SelectionChangedEventHandler TabChanged;
        public void OnTabChanged()
        {
            if (TabChanged != null)
                TabChanged(this, null);
        }


        private void InitToolsMenu()
        {
            Logger.WriteInfo("Initializing ToolsMenu...");
            System.IO.DirectoryInfo toolsDir = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Tools");
            if (toolsDir.Exists && toolsDir.GetDirectories().Length > 0)
            {
                MenuItem toolsMenu = new MenuItem()
                {
                    Header = "ابزارها"
                };
                foreach (var dir in toolsDir.GetDirectories())
                {
                    MenuItem mnuFolder = new MenuItem()
                    {
                        Header = dir.Name
                    };
                    toolsMenu.Items.Add(mnuFolder);
                    foreach (var f in dir.GetFiles("*.exe"))
                    {
                        MenuItem mnuFile = new MenuItem()
                        {
                            Header = f.Name,
                            Icon = new Image()
                            {
                                Source = ShellIcon.Get(f.FullName)
                            }
                        };
                        mnuFile.Click += new RoutedEventHandler((o, r) =>
                        {
                            System.Diagnostics.Process.Start(f.FullName);
                        });
                        mnuFolder.Items.Add(mnuFile);
                    }
                }
                menu1.Items.Insert(menu1.Items.IndexOf(mnuCenter) + 1, toolsMenu);
            }
            Logger.WriteInfo("ToolsMenu Initialized.");
        }

        private void InitializeClockTimer()
        {
            Timer clockTimer = new Timer(60000);
            clockTimer.Elapsed += new ElapsedEventHandler((s, e) =>
            {
                clockTimer.Stop();
                Dispatcher.Invoke(new Action(() => ClockText.Text = DateTime.Now.ToPersianDate().ToString("hh:mm yyyy/MM/dd")));
                // SimulateSensors();
                clockTimer.Start();
            });
            clockTimer.Start();
        }

        //private void SimulateSensors()
        //{
        //    new System.Threading.Thread(() =>
        //    {
        //        var db = DB.Instance;
        //        var r = new Random();
        //        foreach (Sensor sensor in db.Sensors.Where(s => s.ModulNumber != 3))
        //        {
        //            sensor.SensorDatas.Add(new SensorData()
        //            {
        //                Value = Math.Round(Math.Sin(DateTime.Now.Ticks / 10000.0) * 5.0 + 10.0 + r.Next(0, 5), 2, MidpointRounding.AwayFromZero),
        //                Date = DateTime.Now,
        //            });
        //        }
        //        db.SubmitChanges();
        //    }).Start();
        //}

        public static MainWindow Instance
        {
            get
            {
                if (Application.Current == null)
                    System.Diagnostics.Process.GetCurrentProcess().Kill();

                return Application.Current.MainWindow as MainWindow;
            }
        }

        public void ShowItemsListWindow(object sender, EntityTypes type)
        {
            string title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            ItemsListHolderWindow.GetSingleInstance(type, title).ShowAsSingleTab(tabControl, title);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                ZoomIn();
            }
            else if (e.Key == Key.F11)
            {
                ZoomOut();
            }
            else if (e.Key == Key.F10)
            {
                ZoomNormal();
            }
        }

        private double zoomFactor = 1;
        private const double zoomScale = 1.03;
        private void ZoomOut()
        {
            DockPanel.Zoom(zoomFactor /= zoomScale);
        }

        private void ZoomIn()
        {
            DockPanel.Zoom(zoomFactor *= zoomScale);
        }

        private void ZoomNormal()
        {
            DockPanel.Zoom(zoomFactor = 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SetDefaultCenter();


            //if (User.LoginDefaultUserIfNoUserDefined())
            //    UpdateUserStatus();
            //else
            //    LogIn();

             CheckNewUpdate();
        }

        private void CheckDatabaseConnection()
        {
            Logger.WriteInfo("Checking Database Connection...");
            var cnn = new SqlConnection(DatabaseDiagnostics.GetConnectionString());
            Logger.WriteInfo("Server: {0}", DatabaseDiagnostics.GetServerAddress(cnn.ConnectionString));
            try
            {
                cnn.Open();
                cnn.Close();
                Logger.WriteInfo("Database Connection is OK.");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                MessageBox.Show("امکان ارتباط با Database وجود ندارد.", "خطا", MessageBoxImage.Error);
                Application.Current.Shutdown();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void LogIn()
        {
            if ((new LoginWindow().ShowDialog(this) ?? false) == false && User.Current == null)
                Application.Current.Shutdown();
            //else
            //    UpdateUserStatus();
        }

		

        private void UpdateUserStatus()
        {
           UserText.Content = "کاربر: " + User.Current.FullName + string.Format(" (شيفت {0}) ", User.Current.Shift);
        }

        private void CheckLicense()
        {
            try
            {
                
                Logger.WriteInfo("Checking license...");
                var licenseCheckResult = LicenseManager.IsLicenseValid(TMN.Properties.Settings.Default.LicenseKey);

                if (licenseCheckResult.HasValue)
                {
                    if (licenseCheckResult == true)
                    {
                        Logger.WriteInfo("License is OK.");
                        return;
                    }
                    else
                    {
                        Setting.Set("macAddress_" + Center.Current.PointCode, LicenseManager.GetAccessCode());
                        licenseCheckResult = LicenseManager.IsLicenseValid(Setting.Get("license_"+ Center.Current.PointCode, ""));
                        if (licenseCheckResult.HasValue)
                        {
                            if (licenseCheckResult == true)
                            {
                                Logger.WriteInfo("License is OK.");
                                return;
                            }
                            else
                            {
                                Logger.WriteCritical("Invalid License !");
                                MessageBox.Show("شما مجوز استفاده از اين نرم افزار را نداريد.", "خطا", MessageBoxImage.Error);
                            }
                        }

                    }
                }
                else
                {
                    Logger.WriteCritical("Could not check license. This is may be because you don't have any enabled network adapter.");
                    MessageBox.ShowError("لطفا از فعال بودن اتصال شبکه اطمينان حاصل کنيد.");
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteCritical("License check faild!");
            }
            Application.Current.Shutdown();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        #region Commands

		private void mnuExit_Click(object sender, RoutedEventArgs e)
		{
			//using (TMNModelDataContext db = new TMNModelDataContext())
			//{
			//	TMN.UserLog.Log(db, ActionType.ExitApplication, "خروج از برنامه", "");
			//}

			Application.Current.Shutdown();
		}

        private void mnuTabs_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            mnuCloseAllTabs.IsEnabled = tabControl.Items.Count > 0;
            mnuCloseOtherTabs.IsEnabled = tabControl.Items.Count > 1;
        }

        private void mnuCloseAllTabs_Click(object sender, RoutedEventArgs e)
        {
            while (tabControl.Items.Count > 0)
            {
                tabControl.Items.RemoveAt(0);
            }
        }

        private void mnuCloseOtherTabs_Click(object sender, RoutedEventArgs e)
        {
            while (tabControl.Items.Count > 1)
            {
                if (tabControl.Items[0] != tabControl.SelectedItem)
                {
                    tabControl.Items.RemoveAt(0);
                }
                else
                {
                    tabControl.Items.RemoveAt(1);
                }
            }
        }

        private void mnuChangeUser_Click(object sender, RoutedEventArgs e)
        {
			//LogIn();

			//IEnumerable<Room> r = (from rr in DB.Instance.Rooms
			//                      select rr);

        }

        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
         //   if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.SETTINGS))
          //  {
                new SettingsWindow().ShowDialog(this);
          //  }
         //   else
          //  {
          //      MessageBox.Show(MessageTypes.AccessDenied);
          //  }

        }


        private void mnuZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        private void mnuZoomNormal_Click(object sender, RoutedEventArgs e)
        {
            ZoomNormal();
        }

        private void mnuZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        private void mnuHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string helpFilePath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Help.chm";
                Logger.WriteInfo("Trying to find help file...");
                if (System.IO.File.Exists(helpFilePath))
                {
                    Logger.WriteInfo("Help file found. Starting process to show help...");
                    System.Diagnostics.Process.Start(helpFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void mnuBrief_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string helpFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Help.pdf";
                Logger.WriteInfo("Trying to find help file...");
                if (System.IO.File.Exists(helpFilePath))
                {
                    Logger.WriteInfo("Help file found. Starting process to show help...");
                    System.Diagnostics.Process.Start(helpFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void mnuNewUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string helpFilePath = string.Format(@"{0}\help.chm", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                Logger.WriteInfo("Trying to find help file...");
                if (System.IO.File.Exists(helpFilePath))
                {
                    Logger.WriteInfo("Help file found. Starting process to show help...");
                    System.Diagnostics.Process.Start(string.Format(@"mk:@MSITStore:{0}::/18-updatedSoftware.htm", helpFilePath));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }
        #endregion

        #region Windows

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }

        private void mnuSwitches_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.SwitchType).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuRacks_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.RackType).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuShelves_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.ShelfType).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuCards_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.CardType).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuInstructions_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.Instruction);
        }

        private void mnuRoutes_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.Route).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuSchema_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            new SchemaWindow().ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
            Cursor = Cursors.Arrow;
        }

        private void CenterLink_Click(object sender, RoutedEventArgs e)
        {
            new CenterLinkWindow().ShowAsTab(tabControl);
        }

        private void mnuDDF_Click(object sender, RoutedEventArgs e)
        {
            Singleton<DDFWindow>.Instance.ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
        }

        private void mnuSpareCards_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.SpareCard);
        }

        private void mnuUsers_Click(object sender, RoutedEventArgs e)
        {
            //if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.USERS))
            //{
                ShowItemsListWindow(sender, EntityTypes.User);
            //}
            //else
            //{
            //    MessageBox.Show(MessageTypes.AccessDenied);
            //}
        }

        private void mnuTaskTypes_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.TaskType);
        }

        private void mnuReportTypes_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.ReportType);
        }

        private void mnuEvents_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.EventType);
        }

        private void mnuAlarms_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.Alarm);
        }

        private void mnuShifts_Click(object sender, RoutedEventArgs e)
        {
            //if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.SHIFTS))
            //{
                ShowItemsListWindow(sender, EntityTypes.UserShift);
            //}
            //else
            //{
            //    MessageBox.Show(MessageTypes.AccessDenied);
            //}
        }

        private void mnuLinks_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.Link);
        }

        private void mnuAlarmTypes_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.AlarmType);
        }

        private void mnuFailureReasons_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.FailureReason);
        }

        private void mnuReports_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow win = Singleton<CalendarWindow>.Instance;
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.Mode = CalendarWindow.Modes.Reports;
            win.ShowAsSingleTab(tabControl, "Calendar");
        }

        private void mnuTests_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow win = Singleton<CalendarWindow>.Instance;
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.Mode = CalendarWindow.Modes.Tests;
            win.ShowAsSingleTab(tabControl, "Calendar");
        }

        private void mnuLogBook_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow win = Singleton<CalendarWindow>.Instance;
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.Mode = CalendarWindow.Modes.LogBook;
            win.ShowAsSingleTab(tabControl, "Calendar");
        }

        private void mnuLongRecord_Click(object sender, RoutedEventArgs e)
        {
            ShowItemsListWindow(sender, EntityTypes.LongRecord);
        }

        private void mnuChangePass_Click(object sender, RoutedEventArgs e)
        {
            new ChangePasswordWindow().ShowDialog(this);
        }
        #endregion

        #region Reports


        private void mnuFailurePieChart_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("FailureReasonPieChart", new FailureReasonPieFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuRoutesReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("RoutesReport", new RoutesFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuCardsReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("CardsReport", new CardsFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuSpareCardsReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("SpareCardsReport", new SpareCardsFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuLinksReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("LinksReport", new LinksFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuEventsReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("EventsReport", new EventsFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuTasksReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("TasksReport", new TasksFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuAlarmReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("AlarmsReport", new AlarmsFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }

        private void mnuLongRecordReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("LongRecordReport", new LongRecordFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }


        private void mnuTrunksReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("TrunksReport", new TrunksFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }


        private void mnuInstructionsReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow("InstructionsReport", new InstructionFilter())
            {
                Title = (sender as MenuItem).Header.ToString()
            }.ShowAsTab(tabControl);
        }


        private void mnuSensorReport_Click(object sender, RoutedEventArgs e)
        {
            new SensorReportWindow().Show();
        }

        private void mnuSensorChart_Click(object sender, RoutedEventArgs e)
        {
            new SensorChartWindow(Center.Selected).Show();
        }

        #endregion

        private void alarmPanelMenuItem_Click(object sender, RoutedEventArgs e)
        {

            // AlarmPanelLargWindow win = Singleton<AlarmPanelLargWindow>.Instance;
            // AlarmPanelWindow win = Singleton<AlarmPanelWindow>.Instance;
            AlarmPanelWindow win = new AlarmPanelWindow();
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.ShowAsSingleTab(tabControl, "Alarm Panel");

        }

        private void sensorsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.SENSORS))
            //{
                new SensorsWindow().ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show(MessageTypes.AccessDenied);
            //}
        }

        private void mnuSensorStatusChart_Click(object sender, RoutedEventArgs e)
        {
            new SensorStatusWindow().Show();
        }

        private void mapPanelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AlarmRegionWindow win = Singleton<AlarmRegionWindow>.Instance;
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.ShowAsSingleTab(tabControl, "Alarm Region Panel");
        }

        private void omcServicesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ServiceStateWindow().ShowDialog();
        }

        private void inactiveSensorsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new InactiveSensorsWindow().ShowDialog();
        }

        /// <summary>       
        /// /// <author>khosro</author>     
        /// /// <date>910615</date>        
        /// <description>this method shows ArchiveAlarmWindow</description> 
        /// </summary>     
        private void Archive_Click(object sender, RoutedEventArgs e)
        {
            //ArchiveWindow win = new ArchiveWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Archive Alarm ");
        }

        /// 
        /// <summary>       
        /// /// <author>khosro</author>       
        /// /// <date>910615</date>       
        /// /// <description>this method shows ArchiveAlarmWindow</description>     
        /// /// </summary>    
        /// 
        private void btnArchiv_Click(object sender, RoutedEventArgs e)
        {
            //ArchiveWindow win = new ArchiveWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Archive Alarm ");
        }

        private void powerPanelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AlarmPowerWindow win = Singleton<AlarmPowerWindow>.Instance;
            win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            win.ShowAsSingleTab(tabControl, "Alarm Power Panel");
        }

        private void powerRegionMenuItem_Click(object sender, RoutedEventArgs e)
        {
			//PowerRegionWindow win = Singleton<PowerRegionWindow>.Instance;
			//win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			//win.ShowAsSingleTab(tabControl, "Power Region Panel");
        }

        private void CircuitChartLink_Click(object sender, RoutedEventArgs e)
        {
			//CircuitRegionWindow win = Singleton<CircuitRegionWindow>.Instance;
			//win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			//win.ShowAsSingleTab(tabControl, "Circuit Region Panel");
        }

        private void CircuitLink_Click(object sender, RoutedEventArgs e)
        {
			//CircuitChartRegionWindow win = Singleton<CircuitChartRegionWindow>.Instance;
			//win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			//win.ShowAsSingleTab(tabControl, "Circuit Chart Region Panel");

			//AlarmCircuitWindow win = Singleton<AlarmCircuitWindow>.Instance;
			//win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			//win.ShowAsSingleTab(tabControl, "Alarm Circuit");
        }

        private void btnUserLog_Click(object sender, RoutedEventArgs e)
        {
            //UserLogWindow win = new UserLogWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Userlog ");
        }

        private void UserLog_Click(object sender, RoutedEventArgs e)
        {
            //UserLogWindow win = new UserLogWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Userlog ");
        }

        private void btnSMS_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.Contact).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);

            //SMSWindow win = new SMSWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "SMS");
        }

        private void SMS_Click(object sender, RoutedEventArgs e)
        {
            ItemsListHolderWindow.GetSingleInstance(EntityTypes.Contact).ShowAsSingleTab(tabControl, sender.As<FrameworkElement>().Name);
            
            //SMSWindow win = new SMSWindow();
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "SMS");
        }

        private void CircuitAlarmLink_Click(object sender, RoutedEventArgs e)
        {
            //AlarmCircuitWindow win = Singleton<AlarmCircuitWindow>.Instance;
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Alarm Circuit");

        }

		private void circuitlog_Click(object sender, RoutedEventArgs e)
		{
            //CircuitLogWindow win = Singleton<CircuitLogWindow>.Instance;
            //win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
            //win.ShowAsSingleTab(tabControl, "Circuit Log");
		}

		private void btnAlarmPower_Click(object sender, RoutedEventArgs e)
		{
			AlarmPowerWindow win = Singleton<AlarmPowerWindow>.Instance;
			win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			win.ShowAsSingleTab(tabControl, "Alarm Power Panel");
		}

		//WorldClock window = null;
        private void mnuClock_Click(object sender, RoutedEventArgs e)
        {

			//if (mnuClock.IsChecked )
			//{
			//    window = new WorldClock();
			//    window.Show();
			//}
			//else
			//{
			//    if (window != null)
			//        window.Close();
			//    window = null;
			//}
        }




        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    GC.Collect();
        //    System.Threading.Thread.Sleep(100);
        //  //  new test().ShowAsTab(tabControl);
        //    new test().Show();
        //}

    }
}
