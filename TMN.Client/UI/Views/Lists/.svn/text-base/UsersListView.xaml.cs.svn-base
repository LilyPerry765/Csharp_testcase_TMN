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

    public partial class UserListView : ItemsListBase
    {
        private Button AutoFillButton;

        public UserListView()
        {
            InitializeComponent();
            RefreshWhenCenterTreeChanges = true;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(DB.Instance.Users, false); //.Where(u => u.Center == Center.Current)

            if (AutoFillButton != null)
            {
                AutoFillButton.IsEnabled = HostWindow.btnAdd.IsEnabled && ListView.Items.Count > 0;
            }
        }

        protected override void OnHostWindowChanged()
        {
            //   AutoFillButton = AddToolbarButton("AutoFillButton", "تکميل خودکار", "auto_fill.png", 1, btn_Click);
            AutoFillButton = AddToolbarButton("settingsButton", "مدیریت منابع", "gear.png", HostWindow.toolBar.Items.Count - 1, settingsButton_Click);
        }

        void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            new RolesWindow().ShowDialog(this);
        }
    }
}
