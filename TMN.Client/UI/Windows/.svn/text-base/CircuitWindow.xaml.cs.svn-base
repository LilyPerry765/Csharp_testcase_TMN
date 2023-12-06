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

    public partial class CircuitWindow : Window
    {


        public CircuitWindow()
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


                //if (!User.LoginDefaultUserIfNoUserDefined())
                //    LogIn();
                    

//----------------------------------------------------------------------------------

                InitializeComponent();
                Logger.WriteInfo("Components initialized.");
                SetCenterDelegates();
                Title += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3) + (Environment.GetCommandLineArgs().Contains("/multi") ? " (Multi Instance Mode)" : "");
                Logger.WriteInfo("Initializing ClockTimer...");
                //InitializeClockTimer();
                Logger.WriteInfo("ClockTimer initialized.");
                DatabaseDiagnostics.Disconnected += new Action(DatabaseDiagnostics_Disconnected);
                DatabaseDiagnostics.Connected += new Action(DatabaseDiagnostics_Connected);
                //DatabaseDiagnostics.ActiveConnectionChanged += new Action(ShowServerName);
                tabControl.SelectionChanged += new SelectionChangedEventHandler(tabControl_SelectionChanged);
                //ShowServerName();
                //InitToolsMenu();

                //UpdateUserStatus();
                CircuitLink_Click(null, null);

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
                    RegSettings.Save("updateTime", DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
                else if (newUpdateVer.Substring(0, 4) != oldUpdateVer.Substring(0, 4))
                    RegSettings.Save("updateTime", DateTime.Now.AddDays(10).ToString("yyyy-MM-dd"));
                else
                    RegSettings.Save("updateTime", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                RegSettings.Save("UpdateVersion", newUpdateVer);
            }
            string updateTime = "" + RegSettings.Get("updateTime", DateTime.Now.ToString("yyyy-MM-dd"));
            //if (DateTime.Parse(updateTime) > DateTime.Now)
            //    mnuNewUpdate_Click(null, null);
            
        }

        //void ShowServerName()
        //{
        //    serverStatus.Content = "Server: " + DatabaseDiagnostics.ServerName;
        //}

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


        //private void InitToolsMenu()
        //{
        //    Logger.WriteInfo("Initializing ToolsMenu...");
        //    System.IO.DirectoryInfo toolsDir = new System.IO.DirectoryInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Tools");
        //    if (toolsDir.Exists && toolsDir.GetDirectories().Length > 0)
        //    {
        //        MenuItem toolsMenu = new MenuItem()
        //        {
        //            Header = "ابزارها"
        //        };
        //        foreach (var dir in toolsDir.GetDirectories())
        //        {
        //            MenuItem mnuFolder = new MenuItem()
        //            {
        //                Header = dir.Name
        //            };
        //            toolsMenu.Items.Add(mnuFolder);
        //            foreach (var f in dir.GetFiles("*.exe"))
        //            {
        //                MenuItem mnuFile = new MenuItem()
        //                {
        //                    Header = f.Name,
        //                    Icon = new Image()
        //                    {
        //                        Source = ShellIcon.Get(f.FullName)
        //                    }
        //                };
        //                mnuFile.Click += new RoutedEventHandler((o, r) =>
        //                {
        //                    System.Diagnostics.Process.Start(f.FullName);
        //                });
        //                mnuFolder.Items.Add(mnuFile);
        //            }
        //        }
        //        menu1.Items.Insert(menu1.Items.IndexOf(mnuCenter) + 1, toolsMenu);
        //    }
        //    Logger.WriteInfo("ToolsMenu Initialized.");
        //}

        //private void InitializeClockTimer()
        //{
        //    Timer clockTimer = new Timer(60000);
        //    clockTimer.Elapsed += new ElapsedEventHandler((s, e) =>
        //    {
        //        clockTimer.Stop();
        //        Dispatcher.Invoke(new Action(() => ClockText.Text = DateTime.Now.ToPersianDate().ToString("hh:mm yyyy/MM/dd")));
        //        // SimulateSensors();
        //        clockTimer.Start();
        //    });
        //    clockTimer.Start();
        //}

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

        private static CircuitWindow instance = null;
        public static CircuitWindow Instance
        {
            get
            {
                if (Application.Current == null)
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                if (instance == null)
                    instance = new CircuitWindow();
                return instance; // Application.Current.CircuitWindow as CircuitWindow;
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
                        Logger.WriteCritical("Invalid License !");
                        MessageBox.Show("شما مجوز استفاده از اين نرم افزار را نداريد.", "خطا", MessageBoxImage.Error);
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

        private void CircuitLink_Click(object sender, RoutedEventArgs e)
        {
			//CircuitRegionWindow win = Singleton<CircuitRegionWindow>.Instance;
			//win.Title = sender is MenuItem ? (sender as MenuItem).Header.ToString() : (sender as Button).ToolTip.ToString();
			//win.ShowAsSingleTab(tabControl, "Circuit Region Panel");
        }

    }
}
