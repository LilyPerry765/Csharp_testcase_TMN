using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections;
using TMN.UI.Windows;
using System.Data.Linq;
using System.Windows.Markup;

namespace TMN.UserControls
{
    [ContentProperty()]
    class EntityComboBox : ComboBox
    {
        ComboBoxItem adderItem = new ComboBoxItem();

        public EntityComboBox()
        {
            InitializeAdderItem();
        }

        private void InitializeAdderItem()
        {
            TextBlock textBlock = new TextBlock();
            adderItem.Content = textBlock;
            textBlock.Text = "[ جديد ]";
            textBlock.Margin = new System.Windows.Thickness(0, 0, 0, 3);
            textBlock.Foreground = Brushes.Blue;
            adderItem.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(adderItem_MouseUp);
            adderItem.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(adderItem_PreviewKeyDown);
            this.KeyDown += new System.Windows.Input.KeyEventHandler(EntityComboBox_KeyDown);
        }

        void EntityComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key== System.Windows.Input.Key.Delete)
            {
                SelectedIndex = -1;
            }
        }

        void adderItem_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key== System.Windows.Input.Key.Enter)
            {
                AddNewItem();
            }
        }

        void adderItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
            AddNewItem();
        }

        private void AddNewItem()
        {
            DetailsHolderWindow win = new DetailsHolderWindow(DetermineEntityType(), null);
            if (win.ShowDialog(this) ?? false == true)
            {
                RefreshData();
                SelectedItem = itemsSource.Cast<TMN.Entity>().Where(p => p.GetID() == win.Entity.GetID()).FirstOrDefault();
            }
        }

        private void RefreshData()
        {
            var context = this.GetParent<TMN.Interfaces.IDetailsView>().DataSource;
            context.Refresh(RefreshMode.OverwriteCurrentValues, context.GetTable(itemsSource.AsQueryable().ElementType));
            ItemsSource = itemsSource;
        }

        IEnumerable itemsSource;
        public new IEnumerable ItemsSource
        {
            set
            {
                itemsSource = value;
                base.Items.Clear();
                foreach (var item in value)
                {
                    base.Items.Add(item);
                }
                base.Items.Add(adderItem);
            }
            get
            {
                return itemsSource;
            }
        }

        public new IEnumerable Items
        {
            get
            {
                return base.Items.Cast<object>().Where(p => p != adderItem);
            }
        }

        private EntityTypes DetermineEntityType()
        {
            // string entityName = System.Text.RegularExpressions.Regex.Match(realItemsSource.AsQueryable().Expression.ToString(), @"^Table\((?<EntityName>\S+)\)").Groups["EntityName"].ToString();
            string entityName = itemsSource.AsQueryable().ElementType.Name;
            return (EntityTypes)Enum.Parse(typeof(EntityTypes), entityName);
        }

    }
}
