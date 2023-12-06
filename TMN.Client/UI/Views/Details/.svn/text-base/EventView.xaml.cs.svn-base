using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for EventView.xaml
    /// </summary>
    public partial class EventView : UserControl, IDetailsView
    {
        private DateTime date;
        public EventView(DateTime date)
        {
            InitializeComponent();
            cmbType.ItemsSource = DataSource.EventTypes;
            this.date = new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Events.Where(p => p.ID == (entity as Event).ID).SingleOrDefault();
            chkIsLocked.Visibility = Event.User.ID == User.Current.ID ? Visibility.Visible : Visibility.Collapsed;
        }

        public void BeginInsert()
        {
            DataContext = new Event()
            {
                Time = date,
                Reporter = User.Current.ID,
                Shift = (int)User.Current.Shift,
                IsLocked = false
            };
        }

        public Entity SaveData()
        {
            if (Event.ID == Guid.Empty)
            {
                Event.ID = Guid.NewGuid();
                DataSource.Events.InsertOnSubmit(DataContext as Event);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        private Event Event
        {
            get
            {
                return DataContext as Event;
            }
        }

        public bool Validate()
        {
            return cmbType.IsAnswered("نوع عمليات") && LogDate.IsAnswered("تاريخ");
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
