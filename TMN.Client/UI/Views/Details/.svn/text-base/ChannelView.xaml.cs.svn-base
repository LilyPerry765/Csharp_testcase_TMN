using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;
using System.Data.SqlClient;
using Enterprise;

namespace TMN.Views.Details
{

    public partial class ChannelView : UserControl, IDetailsView
    {
        private bool isNew;
        private Link link;
        public ChannelView(Link link)
        {
            InitializeComponent();
            this.link = link;
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.SourceCenter == Center.CurrentCenterID).OrderBy(r => r.TGNO);
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Channels.Where(p => p.ID == (entity as Channel).ID).SingleOrDefault();
            link = DataContext.As<Channel>().Link;
        }

        public void BeginInsert()
        {
            isNew = true;
            DataContext = new Channel()
            {
                ID = Guid.NewGuid(),
                LinkID = this.link.ID,
            };
        }

        public Entity SaveData()
        {
            if (isNew)
                DataSource.Channels.InsertOnSubmit(DataContext as Channel);
            this.EndEdit();
            try
            {
                DataSource.SubmitChanges();
                return DataContext as Entity;
            }
            catch (SqlException ex)
            {
                Logger.Write(ex);
                MessageBox.ShowError("کانال يا تايم اسلات تکراری می باشد.");
                (DataContext as Channel).Release();
                db = new TMNModelDataContext();
            }
            return null;
        }

        public bool Validate()
        {
            return RouteComboBox.IsAnswered("مسير")
                    && TimeSlotNumericUpDown.IsAnswered("TimeSlot")
                    && ChannelNumericUpDown.IsAnswered("کانال");
        }

        private void StackPanel_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CICUpDown.Value = (decimal?)link.CIC;
        }
    }
}
