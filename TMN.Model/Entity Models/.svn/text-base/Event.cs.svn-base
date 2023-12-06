using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Windows;

namespace TMN
{

    public partial class Event : Entity, IDeletable
    {
        public bool Delete()
        {
            if (IsLocked??false)
            {
                MessageBox.Show(MessageTypes.CannotDelete);
                return false;
            }

            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Events.DeleteOnSubmit(db.Events.Where(p => p.ID == this.ID).SingleOrDefault());
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
                return Time;
            }
            set
            {
                base.Tag = value;
            }
        }


    }
}
