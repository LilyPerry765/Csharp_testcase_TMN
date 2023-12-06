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
using TMN.Interfaces;
using Enterprise.Wpf;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for ShiftView.xaml
    /// </summary>
    public partial class ShiftView : UserControl, IDetailsView
    {
        private bool isNew;

        public ShiftView()
        {
            InitializeComponent();
            MorningUserComboBox.ItemsSource = db.Users.Where(u => u.Center == Center.Current);
            AfterNoonUserComboBox.ItemsSource = db.Users.Where(u => u.Center == Center.Current);
            NightUserComboBox.ItemsSource = db.Users.Where(u => u.Center == Center.Current);
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.UserShifts.SingleOrDefault(p => p.Center == Center.Current && p.Date == entity.As<UserShift>().Date);
        }

        public void BeginInsert()
        {
            isNew = true;
            DataContext = new UserShift()
            {
                CenterID = Center.CurrentCenterID,
                Date = UserShift.GetShiftsOfThisMonth(db).Count() > 0 ? UserShift.GetShiftsOfThisMonth(db).Max(p => p.Date).AddDays(1) : PersianDateTime.Today.MonthInfo.FirstDay.ToGregorian()
            };
        }

        public Entity SaveData()
        {
            if (isNew)
                db.SubmitChanges();

            // user log
            DataSource.UserLogs.InsertOnSubmit(new UserLog
            {
                ID = Guid.NewGuid(),
                CenterID = Center.Current.ID,
                UserID = User.Current.ID,
                Date = DateTime.Now,
                Description = ""
            });

            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return true;
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }
    }
}
