using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;
using System.Transactions;

namespace TMN.Views.Details
{

    public partial class UserView : UserControl, IDetailsView
    {
        public UserView()
            : base()
        {
            InitializeComponent();
            RolesListBox.ItemsSource = db.Roles;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Users.SingleOrDefault(p => p == (entity as User));
            txtPassword2.Password = txtPassword.Password = User.Password;
            CheckRoles();
        }

        private void CheckRoles()
        {
            foreach (Role role in RolesListBox.Items)
                role.IsChecked = User.IsInRole(role.ID);
        }

        public void BeginInsert()
        {
            IsNew = true;
            txtPassword2.Password = txtPassword.Password = string.Empty;
            this.User = new User()
            {
                ID = Guid.NewGuid()
            };
            if (Center.Current != null)
            {
                this.User.CenterID = Center.CurrentCenterID;
            }
        }

        private bool IsNew
        {
            get;
            set;
        }

        public Entity SaveData()
        {
            //TMNModelDataContext dbForMerge = new TMNModelDataContext();

            if (IsNew)
            {
                DataSource.Users.InsertOnSubmit(User);
            }
            User.Password = txtPassword.Password;
            this.EndEdit();
            using (TransactionScope scope = new TransactionScope())
            {



                DataSource.SubmitChanges();
                SetRoles();
                scope.Complete();
            }

            // user log
			//UserLog.Log(DataSource, ActionType.UserInsert, string.Format("Name={0}", (DataContext as User).UserName),
				//string.Format("ID={0} , UserName={1}", (DataContext as User).ID, (DataContext as User).UserName));

            return DataContext as Entity;
        }

        private void SetRoles()
        {
            foreach (Role r in RolesListBox.Items)
            {
                UserRoleRelation relation = db.UserRoleRelations.SingleOrDefault(ur => ur.Role == r && ur.User == User);

                if (r.IsChecked && relation == null)
                    db.UserRoleRelations.InsertOnSubmit(new UserRoleRelation()
                   {
                       User = User,
                       Role = r as Role
                   });
                else if (!r.IsChecked && relation != null)
                    db.UserRoleRelations.DeleteOnSubmit(relation);
            }
            db.SubmitChanges();
        }

        public User User
        {
            get
            {
                return (DataContext as User);
            }
            set
            {
                DataContext = value;
            }
        }

        public bool Validate()
        {
            return txtUserName.IsAnswered("نام کاربری")
                && User.IsUnique
                && txtFullName.IsAnswered("نام حقيقی")
                && IsPasswordValid();
        }

        private bool IsPasswordValid()
        {
            if (txtPassword.Password == txtPassword2.Password)
            {
                return true;
            }
            else
            {
                MessageBox.Show("کلمه عبور و تکرار آن مطابقت ندارند.", "کلمه عبور", MessageBoxImage.Error);
                return false;
            }
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
