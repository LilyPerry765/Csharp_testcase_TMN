using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.Views.Lists
{

    public partial class LongRecordsListView : ItemsListBase
    {

        public LongRecordsListView()
        {
            InitializeComponent();
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from l in DB.Instance.LongRecords
                         where l.User.CenterID == Center.CurrentCenterID
                         select l, selectLast);
        }


    }
}