using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace TMN.Views.Lists
{

    public partial class ReportTypesListView : ItemsListBase
    {
        private Button moveupButton;
        private Button movedownButton;
        SortableObservableCollection<ReportType> sortedData;
        TMNModelDataContext db = DB.Instance;

        public ReportTypesListView()
        {
            InitializeComponent();
            Center.SelectedChanged += new Action(Center_SelectedChanged);
        }

        void Center_SelectedChanged()
        {
            Refresh(true);
        }

        public override void Refresh(bool selectLast)
        {
            //base.Refresh(from repType in DB.Instance.ReportTypes
            //             where repType.SwitchType == null || repType.SwitchType == Center.Current.SwitchType
            //             select repType, selectLast);

            

            ObservableCollection<ReportType> data = new ObservableCollection<ReportType>();

            var result =
                (
                    from r in db.ReportTypes
                    join s in db.SwitchTypes on r.SwitchTypeID equals s.ID into rs_join
                    from j in rs_join.DefaultIfEmpty()
                    join c in db.Centers on j.ID equals c.Switch into jc_join
                    from k in jc_join.DefaultIfEmpty()
                    where (k.ID == Center.Selected.ID) || (k.SwitchType.ID == null)
                    orderby r.Rank 
                    select new
                    {
                        ReportTypeID = r.ID,
                        Rank = r.Rank,
                        Name = r.Name,
                        DefaultValue = r.DefaultValue,
                        SwitchName = j.Name
                    }
                );

            base.Refresh(result, selectLast);

            foreach (var item in result)
            {
                data.Add(new ReportType { ID = item.ReportTypeID, Rank = item.Rank, Name = item.Name, DefaultValue = item.DefaultValue, SwitchName = item.SwitchName });
            }

            sortedData = new SortableObservableCollection<ReportType>(data);
            sortedData.Sort(x => x.Rank, ListSortDirection.Ascending);
            listView.ItemsSource = sortedData;
        }

        protected override void OnHostWindowChanged()
        {
            moveupButton = AddToolbarButton("moveupButton", "بالا", "moveup.png", HostWindow.toolBar.Items.Count - 1, moveupButton_Click);
            movedownButton = AddToolbarButton("movedownButton", "پایین", "movedown.png", HostWindow.toolBar.Items.Count - 1, movedownButton_Click);
        }

        void moveupButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                ReportType rt = listView.SelectedItem as ReportType;
                if (rt.Rank != 1)
                {
                    MoveUp(rt);
                    listView.ItemsSource = null;
                    sortedData.Sort(x => x.Rank, ListSortDirection.Ascending);
                    listView.ItemsSource = sortedData;
                    listView.SelectedItem = rt;
                }
            }
            
        }

        void movedownButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                ReportType rt = listView.SelectedItem as ReportType;
                if (rt.Rank != listView.Items.Count)
                {
                    MoveDown(rt);
                    listView.ItemsSource = null;
                    sortedData.Sort(x => x.Rank, ListSortDirection.Ascending);
                    listView.ItemsSource = sortedData;
                    listView.SelectedItem = rt;
                }
            }
        }

        void MoveUp(ReportType rt)
        {
            foreach (ReportType item in listView.Items)
            {
                if (item.Rank == rt.Rank - 1)
                {
                    item.Rank = rt.Rank;
                    break;
                }
            }
            rt.Rank--;
        }

        void MoveDown(ReportType rt)
        {
            foreach (ReportType item in listView.Items)
            {
                if (item.Rank == rt.Rank + 1)
                {
                    item.Rank = rt.Rank;
                    break;
                }
            }
            rt.Rank++;
        }

        private void listView_Unloaded(object sender, RoutedEventArgs e)
        {
            int i = 100, j = 1;

            foreach (ReportType item in listView.Items)
            {
                if (item.Rank < 100)
                {
                    db.ReportTypes.Where(r => r.ID == item.ID).Single().Rank = j;
                    j++;
                }
                else
                {
                    db.ReportTypes.Where(r => r.ID == item.ID).Single().Rank = i;
                    i++;
                }
            }

            db.SubmitChanges();

        }
    }
}