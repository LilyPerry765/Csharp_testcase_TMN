using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;
using System.Windows.Input;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for CenterView.xaml
    /// </summary>
    public partial class CenterView : UserControl, IDetailsView
    {
        //RasPhoneBook phone = new RasPhoneBook();

        public CenterView()
        {
            InitializeComponent();
            cmbSwitch.ItemsSource = DataSource.SwitchTypes;
            cmbRegions.ItemsSource = DataSource.Regions;
            if (Center.CurrentCenterID != Guid.Empty)
            {
                DefaultCenterCheckBox.Visibility = Visibility.Collapsed;
            }
        }

        public void BeginEdit(Entity entity)
        {
            cmbSwitch.IsEnabled = DataSource.Centers.Where(c => c.ID == (entity as Center).ID).SingleOrDefault().Racks.Count == 0;

            DataContext = DataSource.Centers.Where(p => p.ID == (entity as Center).ID).SingleOrDefault();
            passwordRepeat.Password = password.Password = Center.Password;
        }

        public void BeginInsert()
        {
            DataContext = new Center();
        }

        private Center Center
        {
            get
            {
                return (DataContext as Center);
            }
        }

        private void PreSaveOperations()
        {
            if (Center.ID == Guid.Empty)
            {
                Center.ID = Guid.NewGuid();
                DataSource.Centers.InsertOnSubmit(DataContext as Center);
            }
            Center.Password = password.Password;
            if (!Center.Dests.Any())
            {
                Center.Dests.Add(new Dest()
                {
                    Name = "پيشفرض"
                });
            }
        }


        private bool hasVPN(string name)
        {
            //foreach (RasConnection connection in RasConnection.GetActiveConnections())
            //{
            //    if (connection.EntryName == name)
            //        return true;
            //}

            //return false;

            //RasPhoneBook p = new RasPhoneBook();
            //p.Open();
            //foreach (RasEntry entry in p.Entries)
            //{
            //    if (entry.Name == name)
            //        return true;
            //}
            return false;
        }

        private void VPNCreate(string name, string ip)
        {
            //phone.Open();

            //RasEntry entry = RasEntry.CreateVpnEntry(name, ip , RasVpnStrategy.PptpFirst,
            //    RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn), false);

            //phone.Entries.Add(entry);
        }

        public Entity SaveData()
        {
            PreSaveOperations();
            this.EndEdit();


            // user log
			//UserLog.Log(db, ActionType.CenterInsert, string.Format("Name={0}", txtName.Name), "");


            DataSource.SubmitChanges();

            foreach (Dest dest in DataSource.Dests.Where(d => d.Center == null))
                dest.Delete();

            SetAsDefaultCenter();

            if (!hasVPN(Center.DisplayName))
                VPNCreate(Center.DisplayName, ipTextBox.Text);

            return DataContext as Entity;
        }

        private void SetAsDefaultCenter()
        {
            if (DefaultCenterCheckBox.Visibility == Visibility.Visible && DefaultCenterCheckBox.IsChecked == true)
            {
                Center.IsDefault = true;
            }
        }

        public bool Validate()
        {
            return cmbSwitch.IsAnswered("سوييچ")
                && cmbCenterType.IsAnswered("نوع مركز", 0)
                && txtName.IsAnswered("نام")
                && txtPointCode.IsAnswered("PointCode")
                && ValidatePassword()
                && Center.IsUnique();
        }

        private bool ValidatePassword()
        {
            if (password.Password == passwordRepeat.Password)
            {
                return true;
            }
            else
            {
                MessageBox.ShowError("کلمه عبور و تکرار آن مطابقت ندارند.");
                return false;
            }
        }

        private TMNModelDataContext db = new TMNModelDataContext();
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        private void destDataGrid_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Delete && !((e.Source as DataGrid).SelectedItem as Dest).CanBeDeleted)
            {
                e.Handled = true;
                MessageBox.ShowError("این Dest در سيستم استفاده شده و امکان حذف آن وجود ندارد.");
            }
        }

    }
}
