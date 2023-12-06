using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;

namespace TMN
{

    public partial class Dest : Entity, IDeletable
    {

        partial void OnCreated()
        {
            ID = Guid.NewGuid();
        }

        public bool CanBeDeleted
        {
            get
            {
                return !DB.Instance.Routes.Any(r => r.Dest == this);
            }
        }

        public bool Delete()
        {
            var db = DB.Instance;
            db.Connection.Open();
            db.Transaction = db.Connection.BeginTransaction();
            db.Dests.DeleteOnSubmit(db.Dests.Where(p => p == this).SingleOrDefault());
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
                // MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                return false;
            }
        }
    }
}
