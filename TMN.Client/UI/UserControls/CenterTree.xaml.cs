using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using TMN.UI.Windows;
using TMN.Interfaces;
using System.Diagnostics;
using Enterprise;

namespace TMN.UserControls
{

    public partial class CenterTree : UserControl
    {
        public event EventHandler RedrawNeeded;
        private Guid prevSelectedCenterID;
        internal TMNModelDataContext LocalInstancedDB = DB.Instance;

        #region Constructor

        public CenterTree()
        {
            InitializeComponent();

            treeView1.ContextMenu = null;
        }

        #endregion

        #region Add Objects

        private void btnAddCenter_Click(object sender, RoutedEventArgs e)
        {
            //if (User.Current.IsInRole(Role.ADMINS) || User.Current.IsInRole(Role.CENTERS_ADD))
            //{
                AddNewItem(EntityTypes.Center, null);
            //}
            //else
            //{
            //    MessageBox.Show(MessageTypes.AccessDenied);
            //}
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (treeView1.SelectedItem == null || (sender == mnuNewItem && mnuNewItem.Tag == null))
            {
                AddNewItem(EntityTypes.Center, null);
            }
            else
            {
                Entity subjectEntity;
                if (sender is MenuItem)
                {
                    subjectEntity = (sender as MenuItem).Tag as Entity;
                }
                else
                {
                    subjectEntity = (treeView1.SelectedItem as TreeViewItem).DataContext as Entity;
                }

                AddNewItem(subjectEntity.GetChildType(), subjectEntity);
            }
        }

        public void AddNewItem(EntityTypes type, object parent)
        {
            if (parent is Card && ((parent as Card).CardType.IsControlCard ?? false))
            {
                MessageBox.Show("اين کارت کنترلی است.", "خطا", MessageBoxImage.Error);
            }
            else if (parent is ICapacity && (parent as ICapacity).FreeSpace < 1)
            {
                MessageBox.Show("ظرفيت تکميل است و امکان اضافه نمودن وجود ندارد.", "ظرفيت تکميل", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DetailsHolderWindow win = new DetailsHolderWindow(type, parent);
                if (win.ShowDialog(this) ?? false == true)
                {
                    if (type == EntityTypes.Center)
                    {
                        AddNewNodeToTree(treeView1.Items, win.Entity).IsSelected = true;
                    }
                    // This is not needed anymore because refresh does the same job
                    //else
                    //{
                    //    AddEntityToSelectedNode(win.Entity);
                    //}

                    /* Refresh necessary because otherwise the data context of new item would 
                     * be different from the current data context and no submitchanges would work 
                     * before the first refresh
                    */
                    Refresh();
                }
            }
        }

        #endregion

        #region Edit Objects

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (treeView1.SelectedItem != null)
            {
                Entity field = (treeView1.SelectedItem as TreeViewItem).DataContext as Entity;
               
                //check user has permission to edit center  //shahab 900728
                //if (field is Center)
                //{
                //    if (!User.Current.IsInRole(Role.ADMINS) && !User.Current.IsInRole(Role.CENTERS_EDIT))
                //    {
                //        MessageBox.Show(MessageTypes.AccessDenied);
                //        return;
                //    }
                //}
                
                DetailsHolderWindow win = new DetailsHolderWindow(field);
                if (win.ShowDialog(this) ?? false == true)
                {
                    (treeView1.SelectedItem as TreeViewItem).DataContext = win.Entity;
                    if (win.Entity.EntityType == EntityTypes.Center)
                    {
                        Center.OnSelectedChanged(SelectedCenter);
                    }
                    OnRedrawNeeded(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Delete Objects

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedItem = treeView1.SelectedItem as TreeViewItem;
            if (selectedItem != null && selectedItem.Items.Count == 0)
            {
                Entity selectedEntity = selectedItem.DataContext as Entity;

                //check permission for delete center
                //if (selectedEntity.EntityType == EntityTypes.Center)
                //{
                //    if (!User.Current.IsInRole(Role.ADMINS) && !User.Current.IsInRole(Role.CENTERS_DELETE))
                //    {
                //        MessageBox.Show(MessageTypes.AccessDenied);
                //        return;
                //    }
                //} 

                if ((selectedEntity as IDeletable).Delete())
                {


                    if (selectedEntity.EntityType == EntityTypes.Center)
                    {
                        treeView1.Items.Remove(treeView1.SelectedItem);
                    }
                    else
                    {
                        ((treeView1.SelectedItem as TreeViewItem).Parent as TreeViewItem).Items.Remove(treeView1.SelectedItem);
                    }
                    SetAbilities();
                }
            }
            else
            {
                MessageBox.Show(MessageTypes.CannotDeleteHasItems);
            }
        }


        #endregion

        #region Build Structure

        private void CollectExpandedItems(TreeViewItem root, List<object> buffer)
        {
            if (root.IsExpanded)
            {
                buffer.Add(root.DataContext);
                foreach (var item in root.Items)
                {
                    CollectExpandedItems(item as TreeViewItem, buffer);
                }
            }
        }

        public void BuildStructure()
        {
            this.Cursor = Cursors.Wait;
            try
            {
                object prev = treeView1.SelectedItem;
                List<object> expandedItems = new List<object>();
                foreach (TreeViewItem item in treeView1.Items)
                {
                    CollectExpandedItems(item, expandedItems);
                }
                treeView1.Items.Clear();
                AddCentersToTree();

                if (prev != null)
                {
                    TreeViewItem i = FindNodeByDataContext(treeView1, (prev as TreeViewItem).DataContext);
                    foreach (object dataContext in expandedItems)
                    {
                        FindNodeByDataContext(treeView1, dataContext).IsExpanded = true;
                    }
                    i.IsSelected = true;
                }

                if (treeView1.Items.Count > 0 && treeView1.SelectedItem == null)
                {
                    (treeView1.Items[0] as TreeViewItem).IsSelected = true;
                }
                SetAbilities();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private TreeViewItem FindNodeByDataContext(ItemsControl itemToSearch, object dataContext)
        {
            if (Entity.AreEqual(itemToSearch.DataContext as Entity, dataContext as Entity))
            {
                return itemToSearch as TreeViewItem;
            }
            else
            {
                foreach (var item in itemToSearch.Items)
                {
                    var res = FindNodeByDataContext(item as ItemsControl, dataContext);
                    if (res != null)
                    {
                        return res;
                    }
                }
            }
            return null;
        }

        private void AddCardsToTree(TreeViewItem shelfNode)
        {
            var cards = from card in LocalInstancedDB.Cards
                        where card.Shelf == shelfNode.DataContext as Shelf
                        orderby card.SlotNo
                        select card;
            foreach (var card in cards)
            {
                AddNewNodeToTree(shelfNode.Items, card);
            }
        }

        private void AddShelvesToTree(TreeViewItem rackNode)
        {
            var shelves = from shelf in LocalInstancedDB.Shelfs
                          where shelf.Rack == rackNode.DataContext as Rack
                          orderby shelf.Position descending
                          select shelf;
            foreach (var shelf in shelves)
            {
                AddCardsToTree(AddNewNodeToTree(rackNode.Items, shelf));
            }
        }

        private void AddRacksToTree(TreeViewItem centerNode)
        {
            var racks = from rack in LocalInstancedDB.Racks
                        where rack.Center == centerNode.DataContext as Center
                        orderby rack.Name
                        select rack;
            foreach (var rack in racks)
            {
                AddShelvesToTree(AddNewNodeToTree(centerNode.Items, rack));
            }
        }

        private void AddCentersToTree()
        {
            if (Center.Current == null)
            {
                foreach (var center in LocalInstancedDB.Centers.OrderBy(p => p.Name))
                {
                    AddRacksToTree(AddNewNodeToTree(treeView1.Items, center));
                }
            }
            else
            {
                AddRacksToTree(AddNewNodeToTree(treeView1.Items, LocalInstancedDB.Centers.SingleOrDefault(p => p == Center.Current)));

                foreach (var center in LocalInstancedDB.Centers.Where(p => p != Center.Current).OrderBy(p => p.Name))
                {
                    AddRacksToTree(AddNewNodeToTree(treeView1.Items, center));
                }
            }
        }

        private void AddEntityToSelectedNode(object entity)
        {
            TreeViewItem selectedNode = treeView1.SelectedItem as TreeViewItem;
            selectedNode.IsExpanded = true;
            AddNewNodeToTree(selectedNode.Items, entity);
            OnRedrawNeeded(EventArgs.Empty);
        }

        private TreeViewItem AddNewNodeToTree(ItemCollection reference, object dataContext)
        {
            string displayPath = (dataContext as Entity).InferDisplayPath();
            Label lbl = new Label()
            {
                Foreground = Brushes.White,
                // When runing on win7, effect causes problems, that's why i've temporerily disabled it. Think something to solve it.
                //Effect  = new DropShadowEffect()
                //{
                //    Direction = 225,
                //    ShadowDepth = 1
                //}
            };

            BindingOperations.SetBinding(lbl, Label.ContentProperty, new Binding(displayPath));
            StackPanel stk = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            stk.Children.Add(Entity.GetImage((dataContext as Entity).EntityType));
            stk.Children.Add(lbl);

            TreeViewItem newNode = new TreeViewItem()
            {
                DataContext = dataContext,
                Header = stk,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ContextMenu = contextMenu
            };
            newNode.MouseDown += new MouseButtonEventHandler(newNode_MouseDown);
            newNode.PreviewMouseDoubleClick += new MouseButtonEventHandler(newNode_PreviewMouseDoubleClick);
            reference.Add(newNode);
            BindingOperations.SetBinding(newNode, TreeViewItem.ToolTipProperty, new Binding(displayPath));
            return newNode;
        }

        void newNode_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedNode.DataContext.As<Entity>() is Card)
                ShowLinksWindow(MainWindow.Instance.mnuLinks);
        }

        void newNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = true;
            SetupContextMenu((sender as TreeViewItem).DataContext as Entity);
            e.Handled = true;
        }

        public void SetupContextMenu(Entity subjectEntity)
        {
            string header = "";
            mnuNewItem.Visibility = Visibility.Visible;
            mnuLinks.Visibility = Visibility.Collapsed;
            mnuNewItem.Tag = subjectEntity;
            switch (subjectEntity.EntityType)
            {
                case EntityTypes.Center:
                    header = "رك";
                    break;
                case EntityTypes.Rack:
                    header = "شلف";
                    break;
                case EntityTypes.Shelf:
                    header = "كارت";
                    break;
                case EntityTypes.Card:
                    mnuLinks.Header = MainWindow.Instance.mnuLinks.Header;
                    mnuLinks.Visibility = Visibility.Visible;
                    mnuNewItem.Visibility = Visibility.Collapsed;
                    return;
            }
            if (mnuNewItem.Visibility == Visibility.Visible)
            {
                mnuNewItem.Icon = Entity.GetImage(subjectEntity.EntityType + 1);
                mnuNewItem.Header = string.Format("{0} جديد", header);
            }
        }

        #endregion

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeView1.Items.Count > 0)
            {
                if (prevSelectedCenterID != SelectedCenter.ID)
                {
                    prevSelectedCenterID = SelectedCenter.ID;
                    Center.OnSelectedChanged(SelectedCenter);
                }
                if (treeView1.Items.Count > 0)
                {
                    OnRedrawNeeded(e);
                    SetAbilities();
                }
            }
        }

        private bool CheckHasDeleteCenterPermission()
        {
            return true;

            //List<string> list = User.Current.PermissionNames;
            //foreach (string item in list)
            //{
            //    if ( item.ToString() == "delete_center")
            //        return true;
            //}

            //return false;
        }
        private bool CheckHasEditCenterPermission()
        {
            return true;

            //List<string> list = User.Current.PermissionNames;
            //foreach (string item in list)
            //{
            //    if (item.ToString() == "edit_center" )
            //        return true;
            //}

            //return false;
        }

        private void SetAbilities()
        {

            if (CheckHasDeleteCenterPermission())
            {
                btnDelete.IsEnabled =
                treeView1.SelectedItem != null;

                if (MainWindow.Instance != null)
                    MainWindow.Instance.toolbarTray.IsEnabled = MainWindow.Instance.menu1.IsEnabled = treeView1.Items.Count > 0;
            }

            if (CheckHasEditCenterPermission())
            {
                btnEdit.IsEnabled =
                treeView1.SelectedItem != null;

                if (MainWindow.Instance != null)
                    MainWindow.Instance.toolbarTray.IsEnabled = MainWindow.Instance.menu1.IsEnabled = treeView1.Items.Count > 0;
            }

        }

        internal void OnRedrawNeeded(EventArgs e)
        {
            if (RedrawNeeded != null)
            {
                RedrawNeeded(this, EventArgs.Empty);
            }
        }

        public TreeViewItem SelectedNode
        {
            get
            {
                return treeView1.SelectedItem as TreeViewItem;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            LocalInstancedDB = DB.Instance;
            BuildStructure();
        }

        public Center SelectedCenter
        {
            get
            {
                //if (TMN.Properties.Settings.Default.IsSingleUser)
                //{
                //    return User.Current.Center;
                //}
                //else
                //{
                    if (SelectedNode == null)
                    {
                        return null;
                    }
                    switch ((SelectedNode.DataContext as Entity).EntityType)
                    {
                        case EntityTypes.Center:
                            return SelectedNode.DataContext as Center;
                        case EntityTypes.Rack:
                            return (SelectedNode.DataContext as Rack).Center;
                        case EntityTypes.Shelf:
                            return (SelectedNode.DataContext as Shelf).Rack.Center;
                        case EntityTypes.Card:
                            return (SelectedNode.DataContext as Card).Shelf.Rack.Center;
                        default:
                            throw new NotSupportedException();
                    }
                //}
            }
            set
            {
                if (value == null)
                    return;

                foreach (TreeViewItem item in treeView1.Items.Cast<TreeViewItem>().Where(i => i.DataContext is Center))
                {
                    if ((item.DataContext as Center).ID == value.ID)
                    {
                        item.IsSelected = true;
                        item.EnsureVisible();
                    }
                }
            }
        }
        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTextBox.Background = (SearchTextBox.Text.IsNullOrEmpty() || FindItem(treeView1.Items)) ? Brushes.White : Brushes.Pink;
        }

        private bool FindItem(ItemCollection rootItems)
        {
            foreach (var item in rootItems)
            {
                object foundItemContent = (((item as TreeViewItem).Header as StackPanel).Children[1] as Label).Content;
                if (foundItemContent != null && foundItemContent.ToString().ToLower().Contains(SearchTextBox.Text.ToLower()))
                {
                    CollapseAllItems(treeView1.Items);

                    (item as TreeViewItem).IsSelected = true;
                    (item as TreeViewItem).EnsureVisible();
                    return true;
                }
                else
                {
                    if (FindItem((item as TreeViewItem).Items))
                        return true;
                }
            }
            return false;
        }

        private void CollapseAllItems(ItemCollection items)
        {
            foreach (var item in items)
            {
                if ((item as TreeViewItem).IsExpanded)
                    (item as TreeViewItem).IsExpanded = false;
                CollapseAllItems((item as TreeViewItem).Items);
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Background = Brushes.White;
        }

        private void mnuLinks_Click(object sender, RoutedEventArgs e)
        {
            ShowLinksWindow(sender);
        }

        private static void ShowLinksWindow(object sender)
        {
            MainWindow.Instance.ShowItemsListWindow(sender, EntityTypes.Link);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BuildStructure();
        }

        private void remoteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Center.Selected.Connect();
        }

        private void sensorsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new SensorsWindow().ShowDialog();
        }


    }
}
