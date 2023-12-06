using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for RackTypeView.xaml
    /// </summary>
    public partial class RackTypeView : UserControl, IDetailsView
    {
        private bool IsNew;
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }


        public RackTypeView()
        {
            InitializeComponent();
            cmbSupportingSwitch.ItemsSource = DataSource.SwitchTypes;
        }

        public void BeginEdit(Entity entity)
        {
            IsNew = false;
            DataContext = DataSource.RackTypes.Where(p => p.ID == (entity as RackType).ID).SingleOrDefault();
            if ((DataContext as RackType).IsInUse)
            {
                cmbSupportingSwitch.IsEnabled = nudCapacity.IsEnabled = chkIsDouble.IsEnabled = false;
            }
        }

        public void BeginInsert()
        {
            IsNew = true;
            DataContext = new RackType()
            {
                IsDouble = false,
                ID = Guid.NewGuid(),
                SwitchType = db.SwitchTypes.SingleOrDefault(p => p.ID == Center.Selected.SwitchType.ID)
            };
        }

        public Entity SaveData()
        {
            if (IsNew)
            {
                DataSource.RackTypes.InsertOnSubmit(DataContext as RackType);
            }
           

            // user log
			//UserLog.Log(DataSource, ActionType.RackInsert, string.Format("Name={1}", (DataContext as RackType).Name),
				//string.Format("ID={0} , Name={1} ", (DataContext as RackType).ID, (DataContext as RackType).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return cmbSupportingSwitch.IsAnswered("سوييچ")
                && nudCapacity.IsAnswered("ظرفيت")
                && txtName.IsAnswered("نام")
                && DataContext.As<RackType>().IsUnique(txtName.Text.Trim());
        }

    }
}
