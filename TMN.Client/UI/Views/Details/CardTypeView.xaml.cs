using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for CardTypeView.xaml
    /// </summary>
    public partial class CardTypeView : UserControl, IDetailsView
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public CardTypeView()
        {
            InitializeComponent();
            cmbSupportingSwitch.ItemsSource = DataSource.SwitchTypes;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.CardTypes.Where(p => p.ID == (entity as CardType).ID).SingleOrDefault();
            if ((DataContext as CardType).IsInUse)
            {
                cmbSupportingSwitch.IsEnabled = chkIsControlCard.IsEnabled = nudE1Count.IsEnabled = false;
            }
        }

        public void BeginInsert()
        {
            DataContext = new CardType()
            {
                IsControlCard = false,
                SwitchType = db.SwitchTypes.Where(p => p.ID == Center.Selected.SwitchType.ID).SingleOrDefault()
            };
        }

        public Entity SaveData()
        {
            if ((DataContext as CardType).ID == Guid.Empty)
            {
                (DataContext as CardType).ID = Guid.NewGuid();
                DataSource.CardTypes.InsertOnSubmit(DataContext as CardType);
            }
           
            // user log
			//UserLog.Log(db, ActionType.CenterInsert, string.Format("Name={0}", (DataContext as CardType).Name),
				//string.Format("ID={0} , Name={1} ", (DataContext as CardType).ID, (DataContext as CardType).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return cmbSupportingSwitch.IsAnswered("سوييچ")
                && chkIsControlCard.IsAnswered("کنترلی")
                && (chkIsControlCard.IsChecked.Value || nudE1Count.IsAnswered("تعداد E1", (decimal)0))
                && txtName.IsAnswered("نام")
                && (DataContext as CardType).IsUnique(txtName.Text);
        }

        private void chkIsControlCard_Checked(object sender, RoutedEventArgs e)
        {
            nudE1Count.IsEnabled = !(chkIsControlCard.IsChecked ?? false);
            if ((chkIsControlCard.IsChecked ?? false) == true)
            {
                nudE1Count.Value = 0;
            }
        }
    }
}
