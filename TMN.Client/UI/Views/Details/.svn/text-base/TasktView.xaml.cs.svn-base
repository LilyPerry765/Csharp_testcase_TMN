using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl, IDetailsView
    {
        private DateTime Date;

        public TaskView(DateTime date)
        {
            InitializeComponent();
            cmbType.ItemsSource = DataSource.TaskTypes;
            this.Date = date;
            RouteComboBox.ItemsSource = db.Routes.Where(p => p.SourceCenter == Center.CurrentCenterID).OrderBy(r => r.TGNO);
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Tasks.Where(p => p.ID == (entity as Task).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new Task()
            {
                DueDate = this.Date,
                Definer = User.Current.ID,
                Shift = (int)User.Current.Shift,
                IsDone = false,
            };
        }

        public Entity SaveData()
        {
            if (Task.ID == Guid.Empty)
            {
                Task.ID = Guid.NewGuid();
                DataSource.Tasks.InsertOnSubmit(DataContext as Task);
            }
            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        private Task Task
        {
            get
            {
                return DataContext as Task;
            }
        }

        public bool Validate()
        {
            return cmbType.IsAnswered("عنوان");
        }

        private void chkIsDone_Checked(object sender, RoutedEventArgs e)
        {
            txtFinishDate.Date = DateTime.Now;
        }

        private void chkIsDone_Unchecked(object sender, RoutedEventArgs e)
        {
            txtFinishDate.Date = null;
        }

        private void txtFinishDate_IsCheckedChanged(object sender, RoutedEventArgs e)
        {
            chkIsDone.IsChecked = txtFinishDate.IsChecked;
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
