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
    /// Interaction logic for ShelfTypesListView.xaml
    /// </summary>
    public partial class ShelfTypesListView : ItemsListBase
    {

        public ShelfTypesListView()
        {
            InitializeComponent();
            SupportingSwitchComboBox.ItemsSource = DB.Instance.SwitchTypes;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from shelfType in DB.Instance.ShelfTypes
                         where !IsSearching || ((SupportingSwitchComboBox.SelectedValue == null
                              || shelfType.SwitchType == (SupportingSwitchComboBox.SelectedItem as SwitchType))
                         && shelfType.Name.Contains(NameTextBox.Text))
                         orderby shelfType.SwitchType.Name
                         select shelfType, selectLast);
        }


    }
}
