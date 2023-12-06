using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Units;
using TMN.UI.Windows;
using Enterprise;
using System.ComponentModel;

namespace TMN.UserControls
{
    /// <summary>
    /// Interaction logic for CenterSchema.xaml
    /// </summary>
    public partial class CenterSchema : UserControl
    {
        public event EventHandler SchemaChanged;
        private Displays View;

        public CenterSchema()
        {
            InitializeComponent();

            Root.Background = this.Background;
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DrawNode(MainWindow.Instance.Tree.SelectedNode);
                MainWindow.Instance.Tree.RedrawNeeded -= new EventHandler(Tree_RedrawNeeded);
                MainWindow.Instance.Tree.RedrawNeeded += new EventHandler(Tree_RedrawNeeded);
            }
        }

        void Tree_RedrawNeeded(object sender, EventArgs e)
        {
            if (this.IsVisible)
                DrawNode(MainWindow.Instance.Tree.SelectedNode);
        }

        protected void OnSchemaChanged()
        {
            MainWindow.Instance.Tree.LocalInstancedDB.SubmitChanges();
            if (SchemaChanged != null)
            {
                SchemaChanged(this, EventArgs.Empty);
            }
        }

        public void DrawNode(TreeViewItem node)
        {
            if (node == null)
            {
                Root.Content = lblSelectCenter;
                View = Displays.None;
            }
            else if ((node.DataContext as Entity).EntityType == EntityTypes.Center)
            {
                DrawCenter(node);
                View = Displays.CenterView;
            }
            else
            {
                TreeViewItem selectedRack = FindRackNode(node);
                DrawRack(selectedRack);
                View = Displays.RackView;
            }
        }

        private void DrawCenter(TreeViewItem node)
        {
            CenterUnit center = new CenterUnit(node);
            RackUnit ru;
            foreach (var item in node.Items)
            {
                ru = new RackUnit(item as TreeViewItem, true);
                center.AddRack(ru);
            }
            if (Root.Content != null)
            {
                if (View != Displays.CenterView)
                {
                    (Root.Content as UIElement).FadeOut((s, e) =>
                    {
                        Root.Content = center;
                        center.FadeIn();
                    });
                }
                else
                {
                    Root.Content = center;
                }
            }
        }

        private void DrawRack(TreeViewItem rackNode)
        {
            RackUnit ru = new RackUnit(rackNode);
            foreach (TreeViewItem shelfNode in rackNode.Items)
            {
                ShelfUnit shelf = new ShelfUnit(shelfNode);
                ru.LoadShelf(shelf);
                foreach (TreeViewItem cardNode in shelfNode.Items)
                {
                    CardUnit cu = new CardUnit(cardNode);
                    shelf.AddCard(cu, cu.GetEntity<Card>().SlotNo.Value);
                }
            }
            if (Root.Content != null)
            {
                if (View != Displays.RackView)
                {
                    (Root.Content as UIElement).FadeOut((s, e) =>
                    {
                        Root.Content = ru;
                        ru.FadeIn();
                    });
                }
                else
                {
                    Root.Content = ru;
                }
            }
        }

        private TreeViewItem FindRackNode(TreeViewItem node)
        {
            TreeViewItem thisNode = node;
            while (thisNode != null && (thisNode.DataContext as Entity).EntityType != EntityTypes.Rack)
            {
                thisNode = thisNode.Parent as TreeViewItem;
            }
            return thisNode;
        }

        private void Root_MouseMove(object sender, MouseEventArgs e)
        {
            if (Root.Content is CenterUnit)
            {
                CenterUnit u = Root.Content as CenterUnit;
                if (u.IsResizing)
                {
                    if (e.GetPosition(u).X >= u.MinWidth)
                        u.Width = e.GetPosition(u).X + u.Margin.GetWidth();
                    if (e.GetPosition(u).Y >= u.MinHeight)
                        u.Height = e.GetPosition(u).Y + u.Margin.GetHeight();
                }
            }
        }

        private void Root_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Root.Content is CenterUnit)
            {
                CenterUnit u = Root.Content as CenterUnit;
                if (u.IsResizing)
                {
                    if (e.GetPosition(u).X >= u.MinWidth)
                    {
                        double w = RackUnit.ThumbWidth + CenterUnit.MinDistance;
                        u.Width = Math.Round(e.GetPosition(u).X / w) * w + u.Margin.GetWidth();
                    }
                    if (e.GetPosition(u).Y >= u.MinHeight)
                    {
                        double h = RackUnit.ThumbHeight + CenterUnit.MinDistance;
                        u.Height = Math.Round(e.GetPosition(u).Y / h) * h + u.Margin.GetHeight();
                    }

                }
                (Root.Content as CenterUnit).IsResizing = false;
                OnSchemaChanged();
                Root.Cursor = Cursors.Arrow;
            }
        }


    }
}
