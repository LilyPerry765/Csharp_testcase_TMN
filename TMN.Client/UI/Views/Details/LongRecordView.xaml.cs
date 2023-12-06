using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;


namespace TMN.Views.Details
{

    public partial class LongRecordView : UserControl, IDetailsView
    {
        private bool isNew;

        public LongRecordView()
        {
            InitializeComponent();
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
            DataContext = DataSource.LongRecords.Where(p => p == entity as LongRecord).SingleOrDefault();
        }

        public void BeginInsert()
        {
            isNew = true;
            DataContext = new LongRecord()
            {
                ID = Guid.NewGuid(),
                UserID = User.Current.ID,
                Date = DateTime.Now,
                Shift = (int)User.Current.Shift
            };
        }

        public Entity SaveData()
        {
            if (isNew)
                DataSource.LongRecords.InsertOnSubmit(DataContext as LongRecord);
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return RouteComboBox.IsAnswered("مسير")
                && ANumberTextBox.IsAnswered("مشترک A")
                && BNumberTextBox.IsAnswered("مشترک B")
                && ChannelNumericUpDown.IsAnswered("شماره کانال")
                && StateComboBox.IsAnswered("وضعيت کانال")
                && CheckSameNumbers();
        }

        private bool CheckSameNumbers()
        {
            if (ANumberTextBox.Text.Trim() == BNumberTextBox.Text.Trim())
            {
                MessageBox.ShowError("شماره مبدا و مقصد يکسان است.");
                return false;
            }
            return true;
        }
    }
}
