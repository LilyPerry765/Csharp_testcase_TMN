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
using TMN.UI.Windows;
using System.ComponentModel;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for TasksListView.xaml
    /// </summary>
    public partial class TasksListView : ItemsListBase
    {
        private DateTime date;
        public TasksListView(DateTime date)
        {
            InitializeComponent();
            RefreshWhenCenterTreeChanges = true;
            this.Date = date;
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                if (HostWindow != null)
                {
                    HostWindow.Arg = date;
                }
                this.Refresh();
            }
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(DB.Instance.Tasks.Where(e => e.DueDate.Value.Date == this.Date.Date && e.User.Center == Center.Current), selectLast);
        }

        private void ItemsListView_Adding(object sender, CancelEventArgs e)
        {
            if (User.Current.Center == null || User.Current.Center.ID != Center.CurrentCenterID)
            {
                e.Cancel = true;
                MessageBox.Show(MessageTypes.UnAuthorizedCenter);
            }
        }

    }
}
