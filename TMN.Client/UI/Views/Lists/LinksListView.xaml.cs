using System.Linq;
using TMN.UI.Windows;
using System.Windows.Controls;
using System.Collections;
using System;
using System.Windows;
using TMN.Views.Details;
using System.Windows.Input;

namespace TMN.Views.Lists
{

    public partial class LinksListView : ItemsListBase
    {
        TMNModelDataContext db = DB.Instance;

        public LinksListView()
        {
            InitializeComponent();
            SearchContent.FindName("SearchExpander").As<Expander>().IsExpanded = true;
            // MainWindow.Instance.Tree.RedrawNeeded += new System.EventHandler(Tree_RedrawNeeded);
        }

        protected override void OnHostWindowChanged()
        {
            AddButtonsToToolBar();
        }

        private void AddButtonsToToolBar()
        {
            AddToolbarButton("RouteButton", "کانال ها", "routes.png", 3, RouteButton_Click, OperationMode.SingleItem);
            AddToolbarButton("DDFButton", "اتصال به DDF", "ddf_add.png", 3, DDFButton_Click, OperationMode.MultiItem).IsEnabled = false;
            AddToolbarButton("AssignToCardButton", "تخصيص کارت", "card.png", 3, AssignToCardButton_Click, OperationMode.MultiItem).IsEnabled = false;
            AddToolbarButton("ImportButton", "دريافت ار فايل", "import.png", 3, ImportButton_Click);
        }

        public override void Add(EntityTypes type, object arg)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
            {
                base.Add(type, arg);
            }
            else
            {
                if (new DetailsHolderWindow(new ViewInfo()
                {
                    Title = "لينک",
                    DetailsView = new LinkGroupView()
                }).ShowDialog(this) == true)
                    Refresh();
            }
        }

        public override void Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes)
            {
                Cursor = Cursors.Wait;
                db.Links.DeleteAllOnSubmit(ListView.SelectedItems.Cast<Link>());
                db.SubmitChanges();
                Refresh();
                Cursor = Cursors.Arrow;
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
			//if (new ImportWindow().ShowDialog(this) == true)
			//{
			//    Refresh();
			//}
        }

        private void AssignToCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                SetCardWindow win = new SetCardWindow(ListView.SelectedItems.Count);
                if (ListView.SelectedItems.Cast<Link>().AllTheSame(l => l.Card))
                {
                    win.Card = ListView.SelectedItems.Cast<Link>().First().Card;
                }
                if (win.ShowDialog(this) == true)
                {
                    Card card = db.Cards.SingleOrDefault(p => p == win.Card);

                    foreach (var item in ListView.SelectedItems)
                    {
                        item.As<Link>().Card = card;
                    }
                    db.SubmitChanges();
                }
            }
        }

        void RouteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count == 1)
            {
                new ItemsListHolderWindow(EntityTypes.Channel, ListView.SelectedItem).ShowDialog(this);
                Refresh();
            }
        }

        void DDFButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                SetDDFWindow win = new SetDDFWindow();
                if (ListView.SelectedItems.Cast<Link>().AllTheSame(l => l.DDF))
                {
                    win.DDF = ListView.SelectedItems.Cast<Link>().First().DDF;
                }
                if (win.ShowDialog(this) == true)
                {
                    DDF ddf = db.DDFs.SingleOrDefault(p => p == win.DDF);

                    foreach (var item in ListView.SelectedItems)
                    {
                        item.As<Link>().DDF = ddf;
                    }
                    db.SubmitChanges();
                }
            }
        }

        public override void Refresh(bool selectLast)
        {
            db = DB.Instance;
            var links = from l in db.Links.Where(l => l.CenterID == Center.CurrentCenterID)

                        where (RouteComboBox.SelectedItem == null || l.Channels.Any(c => c.RouteID == (Guid?)RouteComboBox.SelectedValue))
                            && (AddressTextBox.Text.Trim() == string.Empty || l.Address.ToUpper().Contains(AddressTextBox.Text.Trim().ToUpper()))
                        select l;

            base.Refresh(links.ToList().Where(p => TypeComboBox.SelectedItem == null || p.State == (LinkStates)TypeComboBox.SelectedValue).OrderBy(p => p.Address), selectLast);

            LoadRoutes();

        }

        private bool isLoaded;
        private void ItemsListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                LoadRoutes();
                isLoaded = true;
            }
        }

        private void LoadRoutes()
        {
            Guid? selectedID = (Guid?)RouteComboBox.SelectedValue;
            RouteComboBox.ItemsSource = db.Routes.Where(r => r.Center == Center.Current).OrderBy(r => r.TGNO);
            RouteComboBox.SelectedValue = selectedID;
        }

        private void ItemsListBase_SelectionChanged(int selectionCount)
        {
            RouteMenuItem.IsEnabled = selectionCount == 1;
            AssignToCardMenuItem.IsEnabled = DDFMenuItem.IsEnabled = selectionCount > 0;
        }

    }
}
