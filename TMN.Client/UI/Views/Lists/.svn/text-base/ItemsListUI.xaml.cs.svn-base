using System;
using System.Windows;
using System.Windows.Controls;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for ItemsListViewUI.xaml
    /// </summary>
    public partial class ItemsListUI : UserControl
    {
        public event Action SearchQueryRequested;

        public ItemsListUI()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchQueryRequested!=null)
            {
                SearchQueryRequested();
            }
        }

    }
}
