using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TMN.UI.Windows;

namespace TMN.Units
{
    public abstract class Unit : Border
    {
        public Unit(TreeViewItem mappedNode)
        {
            this.MappedNode = mappedNode;
            this.DataContext = mappedNode.DataContext;
            this.ContextMenu = MappedNode.ContextMenu;
            this.ContextMenuOpening += new ContextMenuEventHandler(Unit_ContextMenuOpening);
            BindToolTip();
            EntityTypes unitType = (DataContext as Entity).EntityType;
            Style = FindResource(Enum.GetName(typeof(EntityTypes), unitType)) as Style;
            CornerRadius = new CornerRadius(5);
            this.MouseDown += new MouseButtonEventHandler(Unit_MouseDown);
            this.PreviewMouseDown += new MouseButtonEventHandler(Unit_PreviewMouseDown);
            SnapsToDevicePixels = true;
        }

        void Unit_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (this is RackUnit && this.As<RackUnit>().IsThumbnail)
            {
                e.Handled = true;
            }
        }

        protected virtual void BindToolTip()
        {
            BindingOperations.SetBinding(this, Unit.ToolTipProperty, new Binding((DataContext as Entity).InferDisplayPath()));
        }

        void Unit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(this is RackUnit) || !(this as RackUnit).IsThumbnail)
            {
                MappedNode.IsSelected = true;
                MappedNode.EnsureVisible();
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    MainWindow.Instance.Tree.SetupContextMenu(DataContext as Entity);
                }
            }
        }

        void Unit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                e.Handled = true;
                Edit();
            }
        }

        protected virtual void Edit()
        {
            var win = new DetailsHolderWindow(this.Entity);
            if (win.ShowDialog(this) ?? false)
            {
                MainWindow.Instance.Tree.SelectedNode.DataContext = win.Entity;
                MainWindow.Instance.Tree.OnRedrawNeeded(EventArgs.Empty);
            }
        }

        public TreeViewItem MappedNode
        {
            get;
            set;
        }
        protected virtual Panel ChildrenHolder
        {
            get
            {
                return base.Child as Panel;
            }
            set
            {
                base.Child = value;
            }
        }

        protected UIElementCollection Children
        {
            get
            {
                return this.ChildrenHolder.Children;
            }
        }

        public Entity Entity
        {
            get
            {
                return DataContext as Entity;
            }
            set
            {
                DataContext = value;
            }
        }

        public T GetEntity<T>() where T : Entity
        {
            return DataContext as T;
        }
    }
}
