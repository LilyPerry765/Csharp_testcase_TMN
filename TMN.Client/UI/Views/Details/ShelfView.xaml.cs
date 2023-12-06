using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for ShelfView.xaml
    /// </summary>
    public partial class ShelfView : UserControl, IDetailsView
    {
        private Rack ParentRack;
        private bool isNew;

        public ShelfView(Rack parent)
        {
            InitializeComponent();
            cmbType.ItemsSource = DataSource.ShelfTypes.Where(sh => sh.SupportingSwitch == parent.RackType.SupportingSwitch);
            this.ParentRack = parent;
            PositionNumericUpDown.Maximum = ParentRack.RackType.Capacity.Value;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Shelfs.Where(p => p.ID == (entity as Shelf).ID).SingleOrDefault();
            if ((DataContext as Shelf).HasChild)
            {
                cmbType.IsEnabled = false;
            }
        }

        public void BeginInsert()
        {
            isNew = true;
            DataContext = new Shelf()
            {
                ID = Guid.NewGuid(),
                RackID = ParentRack.ID,
                Position = FindFirstEmptyPosition()
            };
        }

        private int FindFirstEmptyPosition()
        {
            for (int i = 1; i <= ParentRack.RackType.Capacity; i++)
            {
                if (db.Shelfs.Count(sh => sh.RackID == ParentRack.ID && sh.Position == i) == 0)
                {
                    return i;
                }
            }
            throw new Exception("There is no free position on this rack.");
        }

        public Entity SaveData()
        {
            if (isNew)
                DataSource.Shelfs.InsertOnSubmit(DataContext as Shelf);
         

 

            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return cmbType.IsAnswered("نوع")
                && txtName.IsAnswered("نام")
                && (DataContext as Shelf).IsUnique(txtName.Text)
                && PositionNumericUpDown.IsAnswered("مکان")
                && ValidatePosition();
        }

        private bool ValidatePosition()
        {
            if ((DataContext as Shelf).IsOnBusyPosition)
            {
                MessageBox.Show("در اين مکان شلف ديگری نصب شده. لطفا مکان ديگری را انتخاب کنيد.", "توجه", MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
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
