using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TMN.Views.Lists;
using System.Windows.Controls;
using System;

namespace TMN.UI.Windows
{
    public partial class ItemsListHolderWindow : Window
    {
        public ItemsListBase View;
        private readonly EntityTypes Type;
        private object arg;

        #region Constructor

        public ItemsListHolderWindow(EntityTypes type)
            : this(type, null)
        {
        }

        public object Arg
        {
            get
            {
                return arg;
            }
            set
            {
                arg = value;
                AttachView();
            }
        }

        public ItemsListHolderWindow(EntityTypes type, object arg)
        {
            InitializeComponent();
            Type = type;
            this.Arg = arg;
            Icon = MainWindow.Instance.Icon;
            View.Refreshed += new System.EventHandler(View_Refreshed);
        }

        #endregion

        #region Singleton

        private static List<ItemsListHolderWindow> instances = new List<ItemsListHolderWindow>();
        public static ItemsListHolderWindow GetSingleInstance(EntityTypes type, string title, object arg)
        {
            ItemsListHolderWindow win = instances.SingleOrDefault(p => p.Type == type);
            if (win == null)
            {
                win = new ItemsListHolderWindow(type, arg);
                if (win.Title.IsNullOrEmpty())
                {
                    win.Title = title;
                }
                instances.Add(win);
            }
            else
                win.Arg = arg;
            return win;
        }

        public static ItemsListHolderWindow GetSingleInstance(EntityTypes type, string title)
        {
            return GetSingleInstance(type, title, null);
        }

        public static ItemsListHolderWindow GetSingleInstance(EntityTypes type, object arg)
        {
            return GetSingleInstance(type, null, arg);
        }

        public static ItemsListHolderWindow GetSingleInstance(EntityTypes type)
        {
            return GetSingleInstance(type, null, null);
        }


        #endregion

        #region Event Handlers

        void view_SelectionChanged(int selectionCount)
        {
            btnEdit.IsEnabled = selectionCount == 1;
            btnDelete.IsEnabled = selectionCount > 0;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            View.Delete();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            View.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            View.Add(Type, Arg);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            View.Edit();
        }

        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (View.ListView.IsKeyboardFocusWithin)
            {
                if (e.Key == Key.Enter)
                {
                    if (btnEdit.IsVisible && btnEdit.IsEnabled)
                        View.Edit();
                }
                else if ((e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control) || e.Key == Key.Insert)
                {
                    if (btnAdd.IsVisible && btnAdd.IsEnabled)
                        View.Add(Type, Arg);
                }
                else if (e.Key == Key.Delete)
                {
                    if (btnDelete.IsVisible && btnDelete.IsEnabled)
                        View.Delete();
                }
                else if (e.Key == Key.F5 || (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control))
                {
                    if (btnRefresh.IsVisible && btnRefresh.IsEnabled)
                        View.Refresh();
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // If the window is not shown independently and is opened as a child of another parent container (eg: a tabcontrol)
            if (RootGrid.Parent != this && sender.As<UIElement>().IsVisible)
                MainWindow.Instance.toolbarTray.ToolBars.Add(toolBar.Extract());
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            // If the window is not shown independently and is opened as a child of another parent container (eg: a tabcontrol)
            if (RootGrid.Parent != this)
                RootGrid.Children.Insert(0, toolBar.Extract());

            if (MainWindow.Instance.tabControl.SelectedItem == null || ((TabItem)MainWindow.Instance.tabControl.SelectedItem).Content == RootGrid)
                MainWindow.Instance.CountText.Text = string.Empty;

        }

        void View_Refreshed(object sender, EventArgs e)
        {
            MainWindow.Instance.CountText.Text = "تعداد: " + View.ListView.Items.Count.ToString();
        }

        #endregion

        private void AttachView()
        {
            var viewInfo = ViewInfo.GetByEntityType(ViewType.List, Type, Arg);
            Title = viewInfo.Title;
            View = viewInfo.ListView;
            View.HostWindow = this;
            View.Refresh();
            Root.Child = View as UIElement;
            View.SelectionChanged += new SelectionEventHandler(view_SelectionChanged);
        }

        public Button FindButtonOnToolbar(string buttonName)
        {
            return toolBar.Items.Cast<FrameworkElement>().SingleOrDefault(item => item.Name == buttonName) as Button;
        }

        public bool HasButtonOnToolBar(string buttonName)
        {
            return FindButtonOnToolbar(buttonName) != null;
        }

    }
}
