using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.Views.Lists
{

    public partial class ChannelListView : ItemsListBase
    {
        Link link;
        public ChannelListView(Link link)
        {
            InitializeComponent();
            this.link = link;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from c in DB.Instance.Channels
                         where c.LinkID == link.ID
                         orderby c.Route.RouteName, c.LNO, c.TimeSlot
                         select c
                         , selectLast);
            HostWindow.btnAdd.IsEnabled = ListView.Items.Count < 31;
        }

        private void ItemsListBase_Loaded(object sender, RoutedEventArgs e)
        {
            HostWindow.Title = string.Format("Channels ({0})", link.Address);
        }

    }
}
