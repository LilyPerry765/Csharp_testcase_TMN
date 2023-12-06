using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.UI.RoutingWizard
{
    /// <summary>
    /// Interaction logic for LinksStep.xaml
    /// </summary>
    public partial class LinksStep : UserControl, IValidator
    {

        private List<Link> links;
        private IQueryable<Link> sourceLinks = DB.Instance.Links.Where(p => p.Card.Shelf.Rack.Center == Center.Current && p.Address != "");
        public LinksStep(List<Link> links)
        {
            InitializeComponent();
            listView.ItemsSource = this.links = links;
            linkComboBox.ItemsSource = sourceLinks;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItems.Count > 0 && MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes)
            {
                links.Remove(listView.SelectedItem as Link);
                Refresh();
            }
        }

        private void Refresh()
        {
            listView.ItemsSource = null;
            listView.ItemsSource = links;
        }

        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedIndex > 0)
            {
                MoveSelectedItem(MoveDirection.Up);
            }
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedIndex < listView.Items.Count - 1)
            {
                MoveSelectedItem(MoveDirection.Down);
            }
        }

        private void MoveSelectedItem(MoveDirection direction)
        {
            Link selectedItem = listView.SelectedItem as Link;
            int selectedIndex = links.IndexOf(selectedItem);
            links.Remove(selectedItem);
            links.Insert(direction == MoveDirection.Down ? ++selectedIndex : --selectedIndex, selectedItem);
            Refresh();
            listView.SelectedIndex = selectedIndex;
            listView.ScrollIntoView(selectedItem);
        }

        private enum MoveDirection
        {
            Up, Down
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Link foundLink = DB.Instance.Links.SingleOrDefault(l => l.Card.Shelf.Rack.Center == Center.Current && l.Address == linkComboBox.Text.Trim());
            if (foundLink != null)
            {
                int newSelectedIndex = ++listView.SelectedIndex;
                links.Insert(newSelectedIndex, foundLink);
                Refresh();
                listView.SelectedIndex = newSelectedIndex;
                listView.ScrollIntoView(foundLink);
                linkComboBox.Text = "";
                linkComboBox.Focus();
            }
            else
            {
                MessageBox.ShowError("آدرس وارد شده معتبر نمی باشد.");
                linkComboBox.Focus();
                linkComboBox.Text = "";
            }
        }



        #region IValidator Members

        public bool Validate()
        {
            if (listView.Items.Count > 0)
            {
                return true;
            }
            else
            {
                MessageBox.ShowError("هيچ لينکی در ليست موجود نيست.");
                return false;
            }
        }

        #endregion

        private void linkComboBox_DropDownOpened(object sender, EventArgs e)
        {
            linkComboBox.ItemsSource = sourceLinks.Where(p => !links.Contains(p));
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = moveDownButton.IsEnabled = moveUpButton.IsEnabled = listView.SelectedItems.Count > 0;

        }

    }
}
