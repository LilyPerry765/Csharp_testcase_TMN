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

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl, IDetailsView
    {
        private Guid shelfID;

        private CardView(SwitchType supportingSwitch)
        {
            InitializeComponent();
            cmbType.ItemsSource = DataSource.CardTypes.Where(sh => sh.SupportingSwitch == supportingSwitch.ID);
        }

        // Add card to shelf
        public CardView(Shelf parent)
            : this(parent.ShelfType.SwitchType)
        {
            shelfID = parent.ID;
            nudSlotNo.Maximum = parent.ShelfType.Capacity.Value;
        }

        private bool IsNew
        {
            get;
            set;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Cards.Where(p => p.ID == (entity as Card).ID).SingleOrDefault();

            if ((DataContext as Card).HasChild)
            {
                cmbType.IsEnabled = false;
            }
        }

        public void BeginInsert()
        {
            IsNew = true;
            DataContext = new Card()
                {
                    ShelfID = shelfID,
                    ID = Guid.NewGuid()
                };


        }

        //private void InsertLinks(Card card)
        //{
        //    List<Link> newLinks = new List<Link>();
        //    for (int i = 0; i < card.CardType.E1Count; i++)
        //    {
        //        newLinks.Add(new Link()
        //        {
        //            DIU = i,
        //            CardID = card.ID,
        //            ID = Guid.NewGuid()
        //        });
        //    }
        //    db.Links.InsertAllOnSubmit(newLinks);
        //}

        public Entity SaveData()
        {
            if (this.IsNew)
            {
                DataSource.Cards.InsertOnSubmit(DataContext as Card);
                //InsertLinks(DataContext as Card);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return cmbType.IsAnswered("نوع")
                && nudSlotNo.IsAnswered("شماره اسلات")
                && txtName.IsAnswered("نام")
               // && DataContext.As<Card>().IsUnique(txtName.Text)
                && (DataContext as Card).ValidateSlot();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            txtName.Text = cmbType.Text;
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
