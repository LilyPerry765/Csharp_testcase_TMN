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

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for EventsListView.xaml
    /// </summary>
    public partial class EventsListView : ItemsListBase
    {
        private DateTime date;
        public EventsListView(DateTime date)
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
            base.Refresh(DB.Instance.Events.Where(e => e.Time.Value.Date == this.Date.Date && e.User.Center == Center.Current).OrderBy(p => p.Time), selectLast);
        }

        private void ItemsListView_Adding(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (User.Current.Center == null || User.Current.Center.ID != Center.CurrentCenterID)
            {
                e.Cancel = true;
                MessageBox.Show(MessageTypes.UnAuthorizedCenter);
            }
        }

    }
}
