using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Data;

namespace TMN
{

    public partial class Report : Entity, IDeletable
    {

        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                try
                {
                    Report report = db.Reports.Where(p => p == this).SingleOrDefault();
                    db.Reports.DeleteOnSubmit(report);


                    db.SubmitChanges();
                    db.Transaction.Commit();
                    db.Connection.Close();
                    return true;
                }
                catch (SqlException)
                {
                    db.Transaction.Rollback();
                    db.Connection.Close();
                    MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                    return false;
                }
            }
            return false;
        }

        public static Report Find(TMNModelDataContext db, DateTime date, Shifts shift, Guid centerid)
        {

             //return db.Reports.FirstOrDefault(r => r.Shift.Value == (int)shift && r.Date.Value == date  && r.CenterID == centerid);


            // baraye in ke har user ke vared mishavad be sorate default report an ra biavarad . 
             return db.Reports.FirstOrDefault(r => r.Shift.Value == (int)shift && r.Date.Value == date && r.CenterID == centerid && r.UserID == User.Current.ID );




            //return (from r in db.Reports
            //        where (r.Date.Value == date) && (r.Shift.Value == (int)User.Current.Shift) && (r.CenterID == Center.Selected.ID)
            //        select r).FirstOrDefault();
            //join ri in db.ReportItems on r.ID equals ri.ReportID
                    //join rt in db.ReportTypes on ri.ReportTypeID equals rt.ID into q_join
                    //from q in q_join.DefaultIfEmpty()
                    //join st in db.SwitchTypes on q.SwitchTypeID equals st.ID into j_join
                    //from j in j_join.DefaultIfEmpty()
                    //join c in db.Centers on j.ID equals c.Switch into k_join
                    //from k in k_join.DefaultIfEmpty()
        }

        public static Report Find(TMNModelDataContext db, DateTime date, Shifts shift, Guid centerid, Guid userid)
        {
            return db.Reports.FirstOrDefault(r => r.Shift.Value == (int)shift && r.Date.Value == date && r.CenterID == centerid && r.UserID == userid);
        }

        public ICollectionView ReportItemsView
        {
            get
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(ReportItems);

                //ListCollectionView dataView = CollectionViewSource.GetDefaultView(ReportItems) as ListCollectionView;
                //dataView.CustomSort = new RPISorter();
                if (dataView != null)
                    dataView.SortDescriptions.Add(new SortDescription("Rank", ListSortDirection.Ascending));

                return dataView;
            }
        }
        //public class RPISorter : IComparer
        //{
        //    public int Compare(object x, object y)
        //    {
        //        ReportItem rpiX = x as ReportItem;
        //        ReportItem rpiY = y as ReportItem;
        //        return rpiX.ReportType.Rank.Value.CompareTo(rpiY.ReportType.Rank.Value);
        //    }
        //}
    }
}
