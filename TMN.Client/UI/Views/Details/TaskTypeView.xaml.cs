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
    /// Interaction logic for TaskTypeView.xaml
    /// </summary>
    public partial class TaskTypeView : UserControl, IDetailsView
    {

        public TaskTypeView()
        {
            InitializeComponent();
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.TaskTypes.Where(p => p.ID == (entity as TaskType).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new TaskType();
        }

        public Entity SaveData()
        {
            if ((DataContext as TaskType).ID == Guid.Empty)
            {
                (DataContext as TaskType).ID = Guid.NewGuid();
                DataSource.TaskTypes.InsertOnSubmit(DataContext as TaskType);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity ;
        }

        public bool Validate()
        {
            return txtName.IsAnswered("نام") && (DataContext as TaskType).IsUnique(txtName.Text);
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
