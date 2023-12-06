using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Collections;

namespace TMN
{

    public partial class ReportItem : Entity, IDeletable
    {


        

        public bool Delete()
        {
            
            //if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            //{
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.ReportItems.DeleteOnSubmit(db.ReportItems.Where(p => p == this).SingleOrDefault());
                try
                {
                    db.SubmitChanges();
                    db.Transaction.Commit();
                    return true;
                }
                catch (SqlException)
                {
                    db.Transaction.Rollback();
                    db.Connection.Close();
                    MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                    return false;
                }
          //  }
            
          //  return false;
        }

        public int? Rank
        {
            get
            {
                return ReportType.Rank;
            }
        }
    }
}
