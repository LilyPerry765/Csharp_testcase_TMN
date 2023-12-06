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
    public partial class SwitchTypeView : UserControl, IDetailsView
    {
        public SwitchTypeView()
        {
            InitializeComponent();
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.SwitchTypes.Where(p => p.ID == (entity as SwitchType).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new SwitchType();
        }

        public Entity SaveData()
        {
            if ((DataContext as SwitchType).ID == Guid.Empty)
            {
                (DataContext as SwitchType).ID = Guid.NewGuid();
                DataSource.SwitchTypes.InsertOnSubmit(DataContext as SwitchType);
            }

            // user log
			//UserLog.Log(DataSource, ActionType.SwitchInsert, string.Format("Name={0}", (DataContext as SwitchType).Name), 
				//string.Format("ID={0} , Name={1} , Company={2}", (DataContext as SwitchType).ID, (DataContext as SwitchType).Name, (DataContext as SwitchType).Company));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return txtName.IsAnswered("نام") && (DataContext as SwitchType).IsUnique(txtName.Text);
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
