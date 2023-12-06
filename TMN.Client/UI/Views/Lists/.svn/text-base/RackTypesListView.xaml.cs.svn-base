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
    /// Interaction logic for RackTypesListView.xaml
    /// </summary>
    public partial class RackTypesListView : ItemsListBase
    {

        public RackTypesListView()
        {
            InitializeComponent();
            SupportingSwitchComboBox.ItemsSource = DB.Instance.SwitchTypes;
        }

        public override void Refresh(bool selectLast)
        {
            var q = from racktype in DB.Instance.RackTypes
                    where (!IsSearching || (SupportingSwitchComboBox.SelectedValue == null
                         || racktype.SwitchType == (SupportingSwitchComboBox.SelectedItem as SwitchType))
                            && racktype.Name.Contains(NameTextBox.Text))
                    orderby racktype.Name
                    select racktype;
            base.Refresh(q, selectLast);
        }




    }

}
