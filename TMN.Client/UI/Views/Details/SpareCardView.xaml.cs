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
using TMN.UI.Windows;
using System.Threading;
using System.Collections;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for SpareCardView.xaml
    /// </summary>
    public partial class SpareCardView : UserControl, IDetailsView
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public SpareCardView()
        {
            InitializeComponent();
            TypeComboBox.ItemsSource = db.CardTypes.Where(p => p.SwitchType == Center.Current.SwitchType);
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.SpareCards.Where(p => p.ID == (entity as SpareCard).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new SpareCard()
            {
                CenterID = Center.CurrentCenterID
            };
        }

        public Entity SaveData()
        {
            if ((DataContext as SpareCard).ID == Guid.Empty)
            {
                (DataContext as SpareCard).ID = Guid.NewGuid();
                DataSource.SpareCards.InsertOnSubmit(DataContext as SpareCard);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return TypeComboBox.IsAnswered("نوع کارت")
                && CountNumericUpDown.IsAnswered("تعداد");
        }



    }
}
