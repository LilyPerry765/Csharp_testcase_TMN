using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;

namespace TMN
{
    public partial class CenterContact : Entity, IDeletable
    {

        public bool Delete()
        {
            using (TMNModelDataContext db = new TMNModelDataContext())
            {
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();

                db.CenterContacts.DeleteOnSubmit(db.CenterContacts.Where(p => p == this).SingleOrDefault());

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
            }
        }
    }
}
