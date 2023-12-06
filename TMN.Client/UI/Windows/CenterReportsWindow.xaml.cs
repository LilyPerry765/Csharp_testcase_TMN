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
using TMN.UserControls.Calendar;
using Enterprise;
using System.Transactions;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;

namespace TMN.UI.Windows
{

    public partial class CenterReportsWindow : Window
    {
        TMNModelDataContext db = DB.Instance;
        TMN.UserControls.Calendar.Calendar calendar;
        DateTime _Date;

        void reportsGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void reportsGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        void reportsGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        public CenterReportsWindow(TMN.UserControls.Calendar.Calendar calendar)
        {
            InitializeComponent();
            this.calendar = calendar;
            Date = calendar.Date.ToGregorian();

            cmbUsers.ItemsSource = db.Users.OrderBy(r => r.UserName).ToList();
            

            reportsGrid.DataContextChanged += new DependencyPropertyChangedEventHandler(reportsGrid_DataContextChanged);
            Center.SelectedChanged += new Action(Center_SelectedChanged);

            //cmbUsers.SelectedIndex = 0;

        }

        void Center_SelectedChanged()
        {

            

            lblCenterName.Text = "نام مرکز :" + Center.Selected.DisplayName;

            // Find();
            Find((Guid)cmbUsers.SelectedValue);


            //Report report = Report.Find(db, Date, User.Current.Shift);

            //if (report == null)
            //{
            //    IsReportCreated = false;
            //}
            //else
            //{
            //    CurrentReport = report;
            //    IsReportCreated = true;
            //}


            //List <SortReprtItems> list = null;

            //for (int i = 0; i < reportsGrid.Items .Count ; i++)
            //{
            //    DataGridRow row = (DataGridRow)reportsGrid.ItemContainerGenerator.ContainerFromIndex(i);

            //    for (int j = 0; j < reportsGrid .Columns.Count ; j++)
            //    {
            //        list.Add(new SortReprtItems() 
            //        {

            //            //TextBlock cellContent = reportsGrid.Columns[j].GetCellContent(row) as TextBlock;    

            //        });

            //        DataGridCell cell = (DataGridCell)reportsGrid.ItemContainerGenerator.ContainerFromIndex();

            //    }
            //}
        }

        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                OnDateChanged();
            }
        }

        private void OnDateChanged()
        {
            //   SaveChanges();
            LoadData();
        }

        private void LoadData()
        {

            SetPanelData();

            Find();

        }

        private void Find(Guid userid)
        {
            if (userid != null)
            {
                Report report = Report.Find(db, Date, User.Current.Shift, Center.Selected.ID, userid);
                //List<ReportItem> items = report.ReportItems.OrderBy(r => r.ReportType.Rank).ToList();
                //report.ReportItems.Clear();
                //report.ReportItems.SetSource(items);


                if (report == null)
                {
                    IsReportCreated = false;
                }
                else
                {

                    //IEnumerable<ReportItem> items = report.ReportItems.OrderBy(s => s.ReportType.Rank);
                    //reportsGrid.ItemsSource = items;
                    IsReportCreated = true;

                    //ICollectionView dataView = CollectionViewSource.GetDefaultView(report.ReportItems);

                    //if (dataView != null)
                    //{
                    //    if(dataView.SortDescriptions.Count == 0)
                    //        dataView.SortDescriptions.Add(new SortDescription("ReportType.Rank", ListSortDirection.Ascending));
                    //    dataView.Refresh();
                    //}

                    CurrentReport = report;

                    DateTime date = DateTime.Today.AddDays(0);
                    if (report != null)
                    {
                        if (report.UserID == User.Current.ID && CurrentReport.Date.Value == date)
                        {
                            reportsGrid.Columns[2].IsReadOnly = false;
                            DescriptionTextBox.IsEnabled = true ;
                        }
                        else
                        {
                            reportsGrid.Columns[2].IsReadOnly = true;
                            DescriptionTextBox.IsEnabled = false ;
                        }
                    }
                }
            }

        }

        private void Find()
        {
            Report report = Report.Find(db, Date, User.Current.Shift, Center.Selected.ID);
            //List<ReportItem> items = report.ReportItems.OrderBy(r => r.ReportType.Rank).ToList();
            //report.ReportItems.Clear();
            //report.ReportItems.SetSource(items);


            if (report == null)
            {
                IsReportCreated = false;
            }
            else
            {

                //IEnumerable<ReportItem> items = report.ReportItems.OrderBy(s => s.ReportType.Rank);
                //reportsGrid.ItemsSource = items;
                IsReportCreated = true;

                //ICollectionView dataView = CollectionViewSource.GetDefaultView(report.ReportItems);

                //if (dataView != null)
                //{
                //    if(dataView.SortDescriptions.Count == 0)
                //        dataView.SortDescriptions.Add(new SortDescription("ReportType.Rank", ListSortDirection.Ascending));
                //    dataView.Refresh();
                //}

                CurrentReport = report;

                DateTime date = DateTime.Today.AddDays(0);
                if (report != null)
                {
                    if (report.UserID == User.Current.ID && CurrentReport.Date.Value == date)
                    {
                        reportsGrid.Columns[2].IsReadOnly = false;
                        DescriptionTextBox.IsEnabled   = true ;
                    }
                    else
                    {
                        reportsGrid.Columns[2].IsReadOnly = true;
                        DescriptionTextBox.IsEnabled  = false  ;
                    }
                }

            }
        }

        private void Refresh()
        {
            db = DB.Instance;
            LoadData();
        }

        private void SetPanelData()
        {
            PersianDateLabel.Content = "تاريخ شمسی: " + Date.ToPersianDate().ToString("yyyy/MM/dd");
            EnglishDateLabel.Content = "تاريخ ميلادی: " + Date.ToString("yyyy/MM/dd");
            WeekDayNameLabel.Content = "روز هفته: " + Date.ToPersianDate().WeekDayName;
            //ShiftLabel.Content = "شيفت: " + User.Current.Shift.ToString();
            //UserLabel.Content = "نام پرسنل: " + User.Current.FullName;

            lblCenterName.Text = "نام مرکز :" + Center.Selected.DisplayName;

            //cmbUsers.SelectedValue = User.Current.ID;
        }

        private bool IsReportCreated
        {
            get
            {
                return DataFormGrid.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    DataFormGrid.Visibility = Visibility.Visible;
                    EmptyGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    EmptyGrid.Visibility = Visibility.Visible;
                    DataFormGrid.Visibility = Visibility.Collapsed;
                }
                DeleteButton.IsEnabled = IsReportCreated;
            }
        }

        private Report InsertNewReport()
        {
        
                // check current date and current user
                if (DateTime.Now.ToShortDateString() == Date.ToShortDateString()) // && (Guid)cmbUsers.SelectedValue == User.Current.ID)
                {

                    //reportsGrid.Columns[2].IsReadOnly = false;
                    //DescriptionTextBox.IsEnabled = true;
                    

                    Report r = new Report()
                    {
                        ID = Guid.NewGuid(),
                        Shift = (int)User.Current.Shift,
                        UserID = User.Current.ID,
                        Date = Date,
                        CenterID = Center.Selected.ID
                    };
                    db.Reports.InsertOnSubmit(r);
                    foreach (var reportType in db.ReportTypes.Where(p => p.SwitchType == null || p.SwitchType == Center.Selected.SwitchType).OrderBy(rp => rp.Rank))
                    {
                        r.ReportItems.Add(new ReportItem()
                        {
                            ID = Guid.NewGuid(),
                            ReportType = reportType,
                            Report = r
                        });

                    }
                    db.SubmitChanges();

                    return r;

                }
                else
                {
                    IsReportCreated = false;
                    //EmptyGrid.Visibility = Visibility.Visible;
                    //DescriptionTextBox.IsEnabled = false;
                    
                }
                return null;
      
        }

        //private void SetupReportItems()
        //{
        //    List<ReportItem> repItems = new List<ReportItem>();
        //    foreach (var item in db.ReportTypes)
        //    {
        //        repItems.Add(new ReportItem()
        //        {
        //            ID = Guid.NewGuid(),
        //            ReportTypeID = item.ID
        //        });
        //    }
        //}

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            RootGrid.EndEdit();



            db.SubmitChanges();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            sender.As<Border>().FadeIn();
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            sender.As<Border>().FadeOut(.5);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && CanAdd())
            {
                IsReportCreated = true;
                cmbUsers.SelectedValue = User.Current.ID;
                CurrentReport = InsertNewReport();
            }
        }

        private bool CanAdd()
        {
            //if (User.Current.Center == null || User.Current.Center.ID != Center.CurrentCenterID)
            //{
            //    MessageBox.Show(MessageTypes.UnAuthorizedCenter);
            //    return false;
            //}
            //return true;

            if (User.Current.Center == null)
            {
                MessageBox.Show(MessageTypes.UnAuthorizedCenter);
                return false;
            }
            return true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // if is current user then it can delete report
            DateTime date = DateTime.Today.AddDays(0);

            if (CurrentReport.UserID == User.Current.ID && CurrentReport.Date.Value == date)
            {
                try
                {
                    Report.Find(db, Date, User.Current.Shift, Center.Selected.ID).Delete();
                }
                catch (Exception)
                {
                    MessageBox.ShowInfo("اين گزارش توسط کاربر ديگری حذف شده است.", "حذف");
                }
                Refresh();
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void autoFillButton_Click(object sender, RoutedEventArgs e)
        {
            // if is current user then it can fill auto values 
            if (CurrentReport.UserID == User.Current.ID)
            {

                //    db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, db.ReportTypes);
                var reportTypes = db.ReportTypes.Where(p => p.SwitchType == Center.Selected.SwitchType || p.SwitchType == null).ToList();

                if (CurrentReport.ReportItems != null)
                {
                    foreach (var item in CurrentReport.ReportItems)
                    {
                        if (item.Value.IsNullOrEmpty())
                            item.Value = reportTypes.Single(r => r.ID == item.ReportTypeID).DefaultValue;
                    }
                }
                reportsGrid.Focus();
            }
        }

        private Report CurrentReport
        {
            get
            {
                return RootGrid.DataContext as Report;
            }
            set
            {
                RootGrid.DataContext = value;

                //reportsGrid.ItemsSource = value.ReportItems.OrderBy(s => s.ReportType.Rank);
            }
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender.As<UIElement>().IsVisible)
            {
                MainWindow.Instance.toolbarTray.ToolBars.Add(MyToolbar.Extract());
                // MiscTextBox.Focus();
            }
        }

        private void RootGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            //SaveChanges();
            RootGrid.Children.Insert(0, MyToolbar.Extract());
        }

        private void cmbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Find((Guid)cmbUsers.SelectedValue);
        }

        //private void newReportButton_Click(object sender, RoutedEventArgs e)
        //{
        //    IsReportCreated = true;
        //    CurrentReport = InsertNewReport();
        //}
    }
}

