using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for AlarmsListView.xaml
    /// </summary>
    public partial class AlarmsListView : ItemsListBase
    {
        public AlarmsListView()
        {
            InitializeComponent();
            RefreshWhenCenterTreeChanges = true;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(DB.Instance.Alarms.Where(p => p.CenterID == Center.CurrentCenterID).OrderBy(p => p.DisconnectTime), selectLast);
        }

        protected override void OnHostWindowChanged()
        {
            if (!HostWindow.HasButtonOnToolBar("copyButton"))
                CreateCopyButton();
        }

        private void CreateCopyButton()
        {
            Button btn = new Button()
            {
                Name = "copyButton",
                ToolTip = "کپی",
                Content = new Image()
                {
                    Source = ImageSourceHelper.GetImageSource("copy.png")
                }
            };
            btn.Click += new RoutedEventHandler(btn_Click);
            HostWindow.toolBar.Items.Insert(1, btn);
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItem != null)
                AddBasedOn(ListView.SelectedItem as Entity, null);
        }


    }
}
