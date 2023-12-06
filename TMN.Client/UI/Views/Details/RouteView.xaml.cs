using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for RoutesView.xaml
    /// </summary>
    public partial class RoutesView : UserControl, IDetailsView
    {
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public RoutesView()
        {
            InitializeComponent();
            //var q = (from cl in DataSource.CenterLinks
            //         where cl.Center == Center.Current
            //         select cl.Center1)
            //    .Union(
            //        from cl in DataSource.CenterLinks
            //        where cl.Center1 == Center.Current
            //        select cl.Center
            //            );
            var centers = db.Centers.Where(c => c.ID != Center.CurrentCenterID);

            cmbDestCenter.ItemsSource = db.Dests.Where(d => d.CenterID != Center.CurrentCenterID);
            OPMComboBox.ItemsSource = db.OPMs;

            cmbInstruction.ItemsSource = DataSource.Instructions.Where(p => !(p.IsDone ?? false)
                                                                    && p.User.Center == Center.Current
                                                                    && centers.ToList().Contains(p.Center1));
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Routes.Where(p => p.ID == (entity as Route).ID).SingleOrDefault();
        }

        public void BeginInsert()
        {
            DataContext = new Route()
            {
                IsSignaling = false
            };

        }

        public Entity SaveData()
        {
            if ((DataContext as Route).ID == Guid.Empty)
            {
                (DataContext as Route).ID = Guid.NewGuid();
                (DataContext as Route).SourceCenter = Center.CurrentCenterID;
                DataSource.Routes.InsertOnSubmit(DataContext as Route);
            }

            // user log
			//UserLog.Log(db, ActionType.RouteInsert, string.Format("Name={0}", (DataContext as Route).RouteName), string.Format("ID={0} , Name={1}", (DataContext as Route).ID, (DataContext as Route).RouteName));

            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;

        }

        public bool Validate()
        {
            this.EndEdit();
            return cmbDestCenter.IsAnswered("مركز مقصد")
                && cmbProtocol.IsAnswered("پروتكل")
                && OPMComboBox.IsAnswered("OPM")
                && txtRouteName.IsAnswered("نام مسير")
                && txtTGNO.IsAnswered("TGNO")
                && (DataContext as Route).IsUnique();
        }

        private void cmbDestCenter_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbDestCenter.SelectedItem == null)
            {
                return;
            }
            try
            {
                txtRouteName.Text = cmbDestCenter.SelectedItem.As<Dest>().Center.Name;
            }
            catch (Exception)
            {
            }
            // The instruction may not exist or there may be more than one, so FirstOrDefault() is suitable
            cmbInstruction.SelectedItem = (from i in cmbInstruction.Items.Cast<Instruction>()
                                           where i.Destination == (cmbDestCenter.SelectedItem as Dest).CenterID
                                           select i).FirstOrDefault();
        }

        private void cmbInstruction_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbInstruction.SelectedItem == null)
            {
                return;
            }

            // TODO: Check wheather DEST is mentioned in the Instruction. 
            // If so, we can use dest here and have the following lines, otherwise we have to delete the following lines:
            //// The center definitly exists, so Single() is suitable
            //cmbDestCenter.SelectedItem = (from c in cmbDestCenter.Items.Cast<Center>()
            //                              where c.ID == (cmbInstruction.SelectedItem as Instruction).Destination.Value
            //                              select c).Single();
        }




    }
}
