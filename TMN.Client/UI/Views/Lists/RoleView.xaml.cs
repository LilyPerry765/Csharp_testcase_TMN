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

namespace TMN
{
    public partial class RoleView : UserControl, IDetailsView
    {
        public RoleView()
        {
            InitializeComponent();
        }

        public void BeginEdit(Entity entity)
        {
           // throw new NotImplementedException();
        }

        public void BeginInsert()
        {
            // throw new NotImplementedException();
        }

        public Entity SaveData()
        {
            DataSource.Roles.InsertOnSubmit(new Role()
            {
                ID = txtEnglishRoleName.Text,
                Name = txtPersianRoleName.Text,
                Description = txtDescription.Text
            });


            // user log
			//UserLog.Log(db, ActionType.NewRole, string.Format("ID={0} , Name={1}", (DataContext as Role).ID, (DataContext as Role).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
           // throw new NotImplementedException();

            return true;
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
