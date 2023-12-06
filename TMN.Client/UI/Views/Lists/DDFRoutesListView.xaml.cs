using System.Linq;
namespace TMN.Views.Lists
{

    public partial class DDFRoutesListView : ItemsListBase
    {

        private DDF ddf;
        public DDFRoutesListView(DDF ddf)
        {
            InitializeComponent();
            this.ddf = ddf;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from channel in DB.Instance.Channels
                         where channel.Link.DDF == ddf
                         select channel, false);
        }

        protected override void OnHostWindowChanged()
        {
            HostWindow.btnAdd.Visibility = HostWindow.btnDelete.Visibility = HostWindow.btnEdit.Visibility = System.Windows.Visibility.Collapsed;
        }

    }
}
