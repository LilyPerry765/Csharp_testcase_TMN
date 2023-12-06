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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMN.UI.Windows;
using System.ComponentModel;
using Enterprise.Wpf;
using Enterprise;

namespace TMN.Views.Lists
{

    public partial class ShiftsListView : ItemsListBase
    {
        private Button AutoFillButton;

        public ShiftsListView()
        {
            InitializeComponent();
            RefreshWhenCenterTreeChanges = true;
        }

        public override void Refresh(bool selectLast)
        {
            var q = UserShift.GetShiftsOfThisMonth(DB.Instance);
            base.Refresh(q, selectLast);
            HostWindow.btnAdd.IsEnabled = q.Count() == 0 || q.Max(p => p.Date) < PersianDateTime.Today.MonthInfo.LastDay.ToGregorian();
            if (AutoFillButton != null)
            {
                AutoFillButton.IsEnabled = HostWindow.btnAdd.IsEnabled && ListView.Items.Count > 0;
            }
            HostWindow.btnDelete.IsEnabled = ListView.Items.Count > 0;
        }


        protected override void OnHostWindowChanged()
        {
            AutoFillButton = AddToolbarButton("AutoFillButton", "تکميل خودکار", "auto_fill.png", 1, btn_Click);
            AddToolbarButton("settingsButton", "تنظيم ساعت شيفت ها", "gear.png", HostWindow.toolBar.Items.Count - 1, settingsButton_Click);
        }

        void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            new ShiftConfigWindow().ShowDialog(this);
        }

        public override void Delete()
        {
            if (MessageBox.Show("کليه شيفت ها از جدول حذف شود؟", "حذف", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TMNModelDataContext db = DB.Instance;
                db.UserShifts.DeleteAllOnSubmit(UserShift.GetShiftsOfThisMonth(db));

                // user log
                db.UserLogs.InsertOnSubmit(new UserLog
                {
                    ID = Guid.NewGuid(),
                    CenterID = Center.Current.ID,
                    UserID = User.Current.ID,
                    Date = DateTime.Now,
                    Description = ""
                });

                db.SubmitChanges();
                Refresh();
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("جدول شيفت با تکرار شيفت های وارد شده پر شود؟", "تکميل خودکار", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                PersianDateTime t = PersianDateTime.Today;
                var db = DB.Instance;

                List<UserShift> givens = UserShift.GetShiftsOfThisMonth(db).ToList();
                int count = givens.Count();
                if (count < t.DaysInMonth)
                {
                    int d = count;
                    while (d++ < t.DaysInMonth)
                    {
                        db.UserShifts.InsertOnSubmit(new UserShift()
                        {
                            CenterID = Center.CurrentCenterID,
                            Date = t.SetDay(d).ToGregorian(),
                            MorningUser = givens[(d - 1) % count].MorningUser,
                            AfterNoonUser = givens[(d - 1) % count].AfterNoonUser,
                            NightUser = givens[(d - 1) % count].NightUser,
                        });
                    }
                    db.SubmitChanges();
                    Refresh();
                }
            }
        }
    }

}
