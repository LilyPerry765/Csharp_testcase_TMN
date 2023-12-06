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
using System.Transactions;
using System.ServiceProcess;
using Enterprise;

namespace TMN.Views.Details
{
    public partial class ContactView : UserControl, IDetailsView
    {



        private bool IsNew;

        public ContactView()
        {
            InitializeComponent();
            CentersListBox.ItemsSource = DataSource.Centers.OrderBy(s => s.Name);
        }

        public Contact contact
        {
            get
            {
                return (DataContext as Contact);
            }
            set
            {
                DataContext = value;
            }
        }

        private void CheckCenters()
        {
            foreach (Center  item in CentersListBox.Items  )
            {
                item.IsChecked = contact.IsInCenter(item.ID);
            }
        }

        public void BeginEdit(Entity entity)
        {
            IsNew = false;
            DataContext = DataSource.Contacts.Where(p => p.ID == (entity as Contact).ID).SingleOrDefault();
            CheckCenters();
        }

        public void BeginInsert()
        {
            IsNew = true;
            DataContext = new Contact();   
        }

        private void SetCenter()
        {
            Contact con = new Contact()
            {
                ID = Guid.NewGuid(),
                Name = txtName.Text,
                Position = txtPosition.Text,
                Number = txtNumber.Text,
                Description = txtDescription.Text
            };
            DataSource.Contacts.InsertOnSubmit(con);

            foreach (Center item in CentersListBox.Items)
            {
                if (item.IsChecked)
                {
                    CenterContact cc = new CenterContact()
                    {
                        CenterID = item.ID,
                        AlarmType = (int)MessageAlarmType.AlarmOnTime ,
                        ContactID = con.ID
                    };
                    DataSource.CenterContacts.InsertOnSubmit(cc);
                }
            }
        }

        public Entity SaveData()
        {
            if (IsNew)
            {
                SetCenter();

                try
                {
                    DataSource.SubmitChanges();

					Setting.Set("IsContactsChange", "1");
                }
                catch (Exception)
                {

                }
            }
            else
            {
                this.EndEdit();

                DataSource.CenterContacts.DeleteAllOnSubmit(DataSource.CenterContacts.Where(c => c.ContactID == contact.ID));

				Setting.Set("IsContactsChange", "1");

                foreach (Center item in CentersListBox.Items)
                {
                    if (item.IsChecked)
                    {
                        CenterContact cc = new CenterContact()
                        {
                            CenterID = item.ID,
                            AlarmType = (int)MessageAlarmType.AlarmOnTime ,
                            ContactID = contact.ID
                        };
                        DataSource.CenterContacts.InsertOnSubmit(cc);
                    }
                }


                DataSource.SubmitChanges();

		

            }



            return DataContext as Entity;

        }

        public bool Validate()
        {
            return (txtName.IsAnswered("نام")) && (txtNumber.IsAnswered("شماره"));
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }



		private void RestartService()
		{
			try
			{
				using (ServiceController svc = new ServiceController("MessageSenderService"))
				{
					if (svc.CanStop)
						svc.Stop();
					while (svc.Status != ServiceControllerStatus.Stopped)
						svc.Refresh();
					svc.Start();
				}
				Logger.WriteInfo("MessageSenderService restarted successfuly...");
			}
			catch (Exception ex)
			{
				Logger.Write(ex, "Message Sender Service");
			}
		}
    }
}
