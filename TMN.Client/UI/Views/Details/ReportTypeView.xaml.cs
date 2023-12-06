using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;


namespace TMN.Views.Details
{

    public partial class ReportTypeView : UserControl, IDetailsView
    {
        private bool isNew;
     //   ReportType r;

        public ReportTypeView()
        {
            InitializeComponent();

            cmbSwitchType.ItemsSource = DataSource.SwitchTypes.OrderBy(c => c.Name);
          //  cmbSwitchType.DisplayMemberPath = "Name";
          //  cmbSwitchType.SelectedValuePath = "ID";
          
            
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public void BeginEdit(Entity entity)
        {
            //  DataContext = DataSource.ReportTypes.Where(p => p == entity as ReportType).SingleOrDefault();

            //this.DataContext = (from r in DataSource.ReportTypes
            //                    join s in DataSource.SwitchTypes on r.SwitchTypeID equals s.ID
            //                    where r == entity as ReportType
            //                    select r).SingleOrDefault();

            cmbSwitchType.IsEnabled = false;
            this.DataContext = (from r in DataSource.ReportTypes
                                join s in DataSource.SwitchTypes on r.SwitchTypeID equals s.ID into rs_join
                                from j in rs_join.DefaultIfEmpty()
                                where r == entity as ReportType
                                select r).SingleOrDefault();

        }

        public void BeginInsert()
        {
            //isNew = true;
            //DataContext = new ReportType()
            //{
            //    ID = Guid.NewGuid(),
            //    Name = NameTextBox.Text,
            //    SwitchTypeID = (Guid?)cmbSwitchType.SelectedValue,
            //    DefaultValue = DefaultValueTextBox.Text
            //};


            isNew = true;

        }

        public Entity SaveData()
        {
            //if (isNew)
            //    DataSource.ReportTypes.InsertOnSubmit(DataContext as ReportType);

            if (isNew)
            {
                if (cmbSwitchType.SelectedValue == null)
                {

                    var maxRank1 =
                                (
                                    from r in db.ReportTypes
                                    join s in db.SwitchTypes on r.SwitchTypeID equals s.ID into rs_join
                                    from j in rs_join.DefaultIfEmpty()
                                    join c in db.Centers on j.ID equals c.Switch into jc_join
                                    from k in jc_join.DefaultIfEmpty()
                                    where (k.SwitchType.ID == null)
                                    select r.Rank
                                ).Max();

                    DataContext = new ReportType()
                    {
                        ID = Guid.NewGuid(),
                        Name = NameTextBox.Text,
                        SwitchTypeID = (Guid?)cmbSwitchType.SelectedValue,
                        DefaultValue = DefaultValueTextBox.Text,
                        Rank = ++maxRank1
                    };
                    DataSource.ReportTypes.InsertOnSubmit(DataContext as ReportType);

                }

                else
                {
                    var maxRank2 =
                                (
                                    from r in db.ReportTypes
                                    join s in db.SwitchTypes on r.SwitchTypeID equals s.ID into rs_join
                                    from j in rs_join.DefaultIfEmpty()
                                    join c in db.Centers on j.ID equals c.Switch into jc_join
                                    from k in jc_join.DefaultIfEmpty()
                                    where (k.ID == Center.Selected.ID)
                                    select r.Rank
                                ).Max();

                    DataContext = new ReportType()
                    {
                        ID = Guid.NewGuid(),
                        Name = NameTextBox.Text,
                        SwitchTypeID = (Guid?)cmbSwitchType.SelectedValue,
                        DefaultValue = DefaultValueTextBox.Text,
                        Rank = ++maxRank2
                    };
                    DataSource.ReportTypes.InsertOnSubmit(DataContext as ReportType);

                }


                //DataContext = new ReportType()
                //{
                //    ID = Guid.NewGuid(),
                //    Name = NameTextBox.Text,
                //    SwitchTypeID = (Guid?)cmbSwitchType.SelectedValue,
                //    DefaultValue = DefaultValueTextBox.Text,
                //};
                //DataSource.ReportTypes.InsertOnSubmit(DataContext as ReportType);
            }

            // user log
			//UserLog.Log(db, ActionType.ReportItemsInsert, string.Format("Name={0}", (DataContext as ReportType).Name), 
				//string.Format("ID={0} , Name={1}", (DataContext as ReportType).ID, (DataContext as ReportType).Name));


            this.EndEdit();
            DataSource.SubmitChanges();
            return DataContext as Entity;
        }

        public bool Validate()
        {
            return NameTextBox.IsAnswered("عنوان");
        }

    }
}
