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
    /// Interaction logic for InstructionView.xaml
    /// </summary>
    public partial class InstructionView : UserControl, IDetailsView
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public InstructionView()
        {
            InitializeComponent();
            cmbSource.ItemsSource = DataSource.Centers;
            cmbDestination.ItemsSource = DataSource.Centers;
            cmbExecuter.ItemsSource = DataSource.Users.Where(p => p.Center == Center.Current);
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Instructions.Where(p => p.ID == (entity as Instruction).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new Instruction()
            {
                IsDone = false
            };
        }

        public Entity SaveData()
        {
            if ((DataContext as Instruction).ID == Guid.Empty)
            {
                (DataContext as Instruction).ID = Guid.NewGuid();
                DataSource.Instructions.InsertOnSubmit(DataContext as Instruction);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;

        }

        public bool Validate()
        {
            this.EndEdit();
            return txtIneffect.IsAnswered("Ineffect")
               && cmbIssuer.IsAnswered("صادر كننده")
               && cmbSource.IsAnswered("مبدا")
               && cmbDestination.IsAnswered("مقصد")
               && cmbExecuter.IsAnswered("اجراكننده")
               && cmbInstructionType.IsAnswered("نوع")
               && txtIssueDate.IsAnswered("تاريخ صدور")
               && txtNumber.IsAnswered("شماره")
               && ValidateCenters()
               && DataContext.As<Instruction>().IsUnique();
        }

        private bool ValidateCenters()
        {
            if (cmbSource.SelectedItem == cmbDestination.SelectedItem)
            {
                MessageBox.Show("مقاصد مبدا و مقصد نمی توانند يکسان باشند.", "خطا", MessageBoxImage.Error);
                return false;
            }
            else
                return true;
        }

    }
}
