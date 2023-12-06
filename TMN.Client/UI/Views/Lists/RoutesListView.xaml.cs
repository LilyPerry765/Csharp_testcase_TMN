using System;
using System.Linq;
using TMN.UI.Windows;
using System.Windows;
using System.Windows.Input;
using Enterprise;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for RoutesListView.xaml
    /// </summary>
    public partial class RoutesListView : ItemsListBase
    {

        public RoutesListView()
        {
            InitializeComponent();
            MainWindow.Instance.Tree.RedrawNeeded += new EventHandler(Tree_RedrawNeeded);
            DestCenterComboBox.ItemsSource = DB.Instance.Centers;
        }

        void Tree_RedrawNeeded(object sender, EventArgs e)
        {
            Refresh();
        }


        public override void Delete()
        {
            var db = DB.Instance;
            int unDeletables = 0;
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes)
            {
                Cursor = Cursors.Wait;
                foreach (var item in ListView.SelectedItems)
                {
                    try
                    {
                        db.Routes.DeleteOnSubmit(db.Routes.Single(r => r == item as Route));
                        db.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        unDeletables++;
                        Logger.Write(ex, "Unable to delete " + item.As<Route>().TGNO);
                        db = new TMNModelDataContext();
                    }
                }
                Refresh();
                Cursor = Cursors.Arrow;
                if (unDeletables > 0)
                    MessageBox.ShowError("{0} مسير به دليل مورد استفاده بودن در ساير بخش های نرم افزار قابل حذف نمی باشد.", unDeletables);
            }
        }

        public override void Refresh(bool selectLast)
        {
            if (Center.Current == null)
            {
                return;
            }
            base.Refresh(from route in DB.Instance.Routes.Where(route => route.SourceCenter.Value == Center.CurrentCenterID).ToArray()
                         where
                                (!IsSearching || ((DestCenterComboBox.SelectedIndex == -1) || (route.Dest != null && (DestCenterComboBox.SelectedItem.As<Center>() == route.Dest.Center)))
                             && (route.Instruction == null || route.Instruction.Number == null || route.Instruction.Number.Contains(InstructionIDTextBox.Text))
                             && (route.TGNO == null || route.TGNO.ToLower().Contains(TGNOTextBox.Text.ToLower()))
                             && (route.RouteName == null || route.RouteName.ToLower().Contains(RouteNameTextBox.Text.ToLower())))
                         select route, selectLast);
        }
    }
}
