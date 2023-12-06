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
    /// Interaction logic for FailureReasonView.xaml
    /// </summary>
    public partial class FailureReasonView : UserControl, IDetailsView
    {

        public FailureReasonView()
        {
            InitializeComponent();
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.FailureReasons.Where(p => p.ID == (entity as FailureReason).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new FailureReason();
        }

        public Entity SaveData()
        {
            if ((DataContext as FailureReason).ID == Guid.Empty)
            {
                (DataContext as FailureReason).ID = Guid.NewGuid();
                DataSource.FailureReasons.InsertOnSubmit(DataContext as FailureReason);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity ;
        }

        public bool Validate()
        {
            return txtName.IsAnswered("نام") && (DataContext as FailureReason).IsUnique(txtName.Text);
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
