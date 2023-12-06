using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Windows;

namespace TMN
{

    public partial class Task : Entity, IDeletable
    {
        public bool Delete()
        {

            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Tasks.DeleteOnSubmit(db.Tasks.Where(p => p.ID == this.ID).SingleOrDefault());
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

        public override object Tag
        {
            get
            {
                return DueDate;
            }
            set
            {
                base.Tag = value;
            }
        }


    }
}
