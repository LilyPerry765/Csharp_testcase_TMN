using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for AlarmView.xaml
    /// </summary>
    public partial class AlarmView : UserControl, IDetailsView, IInsertableBasedOn
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public AlarmView()
        {
            InitializeComponent();
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.SourceCenter == Center.CurrentCenterID).OrderBy(r => r.TGNO);
            AlarmTypeComboBox.ItemsSource = db.AlarmTypes;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Alarms.SingleOrDefault(p => p.ID == (entity as Alarm).ID);
        }

        public void BeginInsert()
        {
            DataContext = new Alarm()
            {
                CenterID = Center.CurrentCenterID,
                ReportTime = DateTime.Now,
                Reporter = User.Current.ID,
                Shift = (int)User.Current.Shift
            };
        }

        /// <summary>
        /// Begin insertion of a copy of an existing alarm.
        /// </summary>
        /// <param name="sourceEntity">The source alarm to be copied.</param>
        public void BeginInsert(Entity sourceEntity)
        {
            Alarm newAlarm = db.Alarms.SingleOrDefault(p => p == sourceEntity.As<Alarm>()).Clone();
            newAlarm.ID = Guid.NewGuid();
            DataContext = newAlarm;
        }

        public Entity SaveData()
        {
            if ((DataContext as Alarm).ID == Guid.Empty)
            {
                (DataContext as Alarm).ID = Guid.NewGuid();
                DataSource.Alarms.InsertOnSubmit(DataContext as Alarm);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return RouteComboBox.IsAnswered("مسير")
                && FailedLinkComboBox.IsAnswered("لينک")
                && AlarmTypeComboBox.IsAnswered("نوع آلارم")
                && DisconnectTimePersianDateBox.IsAnswered("زمان شروع");
        }

        private void RouteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FailedLinkComboBox.ItemsSource = db.Channels.Where(p => p.Route == RouteComboBox.SelectedItem.As<Route>()).Select(p => p.Link).Distinct();
        }
    }
}
