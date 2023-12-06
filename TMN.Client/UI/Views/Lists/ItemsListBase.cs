using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMN.Interfaces;
using TMN.UI.Windows;

namespace TMN.Views.Lists
{
    public delegate void SelectionEventHandler(int selectionCount);
    public class ItemsListBase : UserControl
    {
        public event SelectionEventHandler SelectionChanged;
        public event EventHandler<CancelEventArgs> Adding;
        public event EventHandler Refreshed;
        private bool canRefresh = true;
        public bool IsSearching;
        private Timer timer = new Timer(100);

        #region Constructor

        public ItemsListBase()
        {
            // Some of the following instructions raise error in design time
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.Initialized += new EventHandler(ItemsListView_Initialized); // <- causes designer failure
                this.SizeChanged += new SizeChangedEventHandler(ItemsListView_SizeChanged);
                this.MouseDoubleClick += new MouseButtonEventHandler(ItemsListView_MouseDoubleClick);
                this.Loaded += new RoutedEventHandler(ItemsListView_Loaded); // <- causes designer failure
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                FrameworkElement parentTab = this.GetParent<TabItem>();
            }
        }

        void ItemsListView_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Cursor = Cursors.Wait;
            Refresh();
            MainWindow.Instance.Cursor = Cursors.Arrow;
        }

        #endregion

        private bool CanRefresh
        {
            get
            {
                if (canRefresh)
                {
                    timer.Start();
                    canRefresh = false;
                    return true;
                }
                return false;
            }
            set
            {
                timer.Stop();
                canRefresh = true;
            }
        }

        List<Button> MultiItemButtons = new List<Button>();
        List<Button> SinglItemButtons = new List<Button>();
        protected Button AddToolbarButton(string name, string toolTip, string imagePath, int index, System.Windows.RoutedEventHandler handler, OperationMode opMode = OperationMode.All)
        {
            if (!HostWindow.HasButtonOnToolBar(name))
            {
                Button btn = new Button()
                {
                    ToolTip = toolTip,
                    Name = name,
                    Content = new Image()
                    {
                        Source = ImageSourceHelper.GetImageSource(imagePath)
                    }
                };
                btn.Click += handler;
                HostWindow.toolBar.Items.Insert(index, btn);
                switch (opMode)
                {
                    case OperationMode.SingleItem:
                        SinglItemButtons.Add(btn);
                        btn.IsEnabled = false;
                        break;
                    case OperationMode.MultiItem:
                        MultiItemButtons.Add(btn);
                        btn.IsEnabled = false;
                        break;
                }
                return btn;
            }
            else
                return HostWindow.FindButtonOnToolbar(name);
        }

        private void SetButtonsAbility()
        {
            foreach (var b in SinglItemButtons)
            {
                b.IsEnabled = ListView.SelectedItems.Count == 1;
            }
            foreach (var b in MultiItemButtons)
            {
                b.IsEnabled = ListView.SelectedItems.Count > 0;
            }
        }

        protected void OnSelectionChanged()
        {
            SetButtonsAbility();
            if (SelectionChanged != null)
            {
                SelectionChanged(ListView.SelectedItems.Count);
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets the list view that is defined in the related view
        /// </summary>
        public ListView ListView
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the search panel that is defined in the related view
        /// </summary>
        public Panel SearchContent
        {
            get;
            set;
        }

        private ItemsListHolderWindow _HostWindow;
        /// <summary>
        /// Gets or sets the basic info window that hosts this view.
        /// </summary>
        public ItemsListHolderWindow HostWindow
        {
            get
            {
                return _HostWindow;
            }
            set
            {
                _HostWindow = value;
                OnHostWindowChanged();
            }
        }

        protected virtual void OnHostWindowChanged()
        {
        }

        #endregion

        #region Evnet Handlers

        void ItemsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((this.Content as FrameworkElement).FindName("SearchExpander") as Expander).MaxWidth = e.NewSize.Width;
        }

        void Tree_RedrawNeeded(object sender, EventArgs e)
        {
            if (this.IsVisible && RefreshWhenCenterTreeChanges)
            {
                Refresh();
            }
        }

        void ItemsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView.GetIsSelectionActive(ListView))
                Edit();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            canRefresh = true;
        }

        private void ItemsListView_Initialized(object sender, EventArgs e)
        {
            ListView.SelectionChanged += new SelectionChangedEventHandler(ListView_SelectionChanged);

            ItemsListUI UI = new ItemsListUI();
            this.Content = UI;
            ((this.Content as FrameworkElement).FindName("ListViewHolder") as Border).Child = ListView;
            if (SearchContent == null)
            {
                ((this.Content as FrameworkElement).FindName("SearchExpander") as Expander).Visibility = Visibility.Collapsed;
            }
            else
            {
                UI.SearchQueryRequested += new Action(UI_SearchQueryRequested);
                UI.SearchExpander.Expanded += new RoutedEventHandler(SearchExpander_Expanded);
                ((this.Content as FrameworkElement).FindName("SearchPanel") as Border).Child = SearchContent;
            }
        }

        void SearchExpander_Expanded(object sender, RoutedEventArgs e)
        {
            ResetSearchPanel();
        }

        void ItemsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void UI_SearchQueryRequested()
        {
            IsSearching = true;
            Refresh();
            IsSearching = false;
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }

        #endregion

        #region Add

        private bool CanAdd()
        {
            if (Adding != null)
            {
                CancelEventArgs e = new CancelEventArgs();
                Adding(this, e);
                if (e.Cancel == true)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual void Add(EntityTypes type, object arg)
        {
            if (CanAdd())
            {
                DetailsHolderWindow win = new DetailsHolderWindow(type, arg);
                if (win.ShowDialog(this) ?? false == true)
                {
                    Refresh(true);
                }
            }
        }

        public virtual void AddBasedOn(Entity source, object arg)
        {
            DetailsHolderWindow win = new DetailsHolderWindow(source, arg);
            if (win.ShowDialog(this) ?? false == true)
            {
                Refresh(true);
            }
        }

        #endregion

        #region Edit

        public virtual void Edit()
        {
            if (ListView.SelectedIndex == -1)
            {
                return;
            }
            DetailsHolderWindow win = new DetailsHolderWindow(ListView.SelectedItem as Entity);
            if (win.ShowDialog(this) ?? false == true)
            {
                Refresh();
            }
        }

        #endregion

        #region Delete

        public virtual void Delete()
        {
            if (ListView.SelectedIndex > -1 && ListView.SelectedItem is IDeletable)
            {
                (ListView.SelectedItem as IDeletable).Delete();

                Refresh();
            }
        }

        #endregion
        
        #region Refresh

        public void Refresh()
        {
            if (CanRefresh)
            {
                Refresh(false);
            }
        }

        public void ForceRefresh()
        {
            CanRefresh = true;
            Refresh();
        }

        public virtual void Refresh(bool selectLast)
        {
            throw new NotImplementedException("This method must be overriden in the base class.");
        }

        protected void Refresh<T>(IEnumerable<T> itemSource, bool selectLast)
        {
            if (ListView != null)
            {
                int i = ListView.SelectedIndex;
                ListView.ItemsSource = itemSource;

                foreach (var item in ListView.View.As<GridView>().Columns)
                {
                    item.Width = item.ActualWidth;
                    item.Width = double.NaN;
                }
                if (selectLast)
                {
                    ListView.SelectedIndex = ListView.Items.Count - 1;
                }
                else
                {
                    ListView.SelectedIndex = Math.Min(i, ListView.Items.Count - 1);
                }
                OnRefreshed();
            }
        }

        protected void OnRefreshed()
        {
            if (Refreshed != null)
            {
                Refreshed(this, EventArgs.Empty);
                
            }
        }

        protected bool RefreshWhenCenterTreeChanges
        {
            get;
            set;
        }

        private void ResetSearchPanel()
        {
            foreach (var item in SearchContent.Children)
            {
                if (item is FrameworkElement)
                    (item as FrameworkElement).Reset();
            }
        }
        #endregion

 
    }
}
