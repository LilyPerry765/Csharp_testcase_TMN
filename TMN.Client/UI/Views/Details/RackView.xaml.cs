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
    /// Interaction logic for RackView.xaml
    /// </summary>
    public partial class RackView : UserControl, IDetailsView
    {

        private Guid centerID;
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }
        public RackView(Center parent)
        {
            InitializeComponent();
            cmbType.ItemsSource = DataSource.RackTypes.Where(sh => sh.SupportingSwitch == parent.Switch);
            centerID = parent.ID;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Racks.Where(p => p.ID == (entity as Rack).ID).SingleOrDefault();
            if ((DataContext as Rack).HasChild)
            {
                cmbType.IsEnabled = false;
            }
        }

        public void BeginInsert()
        {
            DataContext = new Rack();           
        }

        public Entity SaveData()
        {
            if ((DataContext as Rack).ID == Guid.Empty)
            {
                (DataContext as Rack).ID = Guid.NewGuid();
                DataSource.Racks.InsertOnSubmit(DataContext as Rack);
            }
            (DataContext as Rack).CenterID = centerID;
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return cmbType.IsAnswered("نوع")
                && txtName.IsAnswered("نام")
                && (DataContext as Rack).IsUnique(txtName.Text);
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Text = cmbType.Text;
            }
        }

        private void cmbType_DropDownOpened(object sender, EventArgs e)
        {
            if (txtName.Text == cmbType.Text)
            {
                txtName.Text = "";
            }
        }


    }
}
