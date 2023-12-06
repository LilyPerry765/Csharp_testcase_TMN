using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;
using System.Data.SqlClient;

namespace TMN
{
    public partial class FailureReason : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.FailureReasons.DeleteOnSubmit(db.FailureReasons.Where(p => p == this).SingleOrDefault());
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
            return false;
        }

        public bool IsUnique(string name)
        {

            if (DB.Instance.FailureReasons.Where(c => c.ID != this.ID && c.Name == name).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }
    }
}
