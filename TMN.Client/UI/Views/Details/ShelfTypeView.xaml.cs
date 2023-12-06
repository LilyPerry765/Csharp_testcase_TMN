using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for ShelfTypeView.xaml
    /// </summary>
    public partial class ShelfTypeView : UserControl, IDetailsView
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public ShelfTypeView()
        {
            InitializeComponent();
            cmbSwitch.ItemsSource = DataSource.SwitchTypes;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.ShelfTypes.Where(p => p.ID == (entity as ShelfType).ID).SingleOrDefault();
            if (ShelfType.IsInUse)
            {
                cmbSwitch.IsEnabled = nudCapacity.IsEnabled = false;
            }
        }

        ShelfType ShelfType
        {
            get
            {
                return DataContext as ShelfType;
            }
        }

        public void BeginInsert()
        {
            DataContext = new ShelfType()
            {
                SwitchType = db.SwitchTypes.Where(p => p.ID == Center.Selected.SwitchType.ID).SingleOrDefault()
            };
        }

        public Entity SaveData()
        {
            if ((DataContext as ShelfType).ID == Guid.Empty)
            {
                (DataContext as ShelfType).ID = Guid.NewGuid();
                DataSource.ShelfTypes.InsertOnSubmit(DataContext as ShelfType);
            }

            // user log
            //UserLog.Log(DataSource, ActionType.ShelfInsert, string.Format("Name={0}", (DataContext as ShelfType).Name),
				//string.Format("ID={0} , Name={1} ", (DataContext as ShelfType).ID, (DataContext as ShelfType).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }


        public bool Validate()
        {
            return txtName.IsAnswered("نام")
                && cmbSwitch.IsAnswered("سوييچ")
                && nudCapacity.IsAnswered("ظرفيت")
                && ShelfType.IsUnique(txtName.Text.Trim());
        }
    }
}
