using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;

namespace TMN
{

    public partial class Alarm : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;

                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Alarms.DeleteOnSubmit(db.Alarms.Where(p => p.ID == this.ID).SingleOrDefault());
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

        public string Duration
        {
            get
            {
                TimeSpan ts = DisconnectTimespan;
                StringBuilder outputBuilder = new StringBuilder();
                if (ts.Days > 0)
                    outputBuilder.Append(string.Format("{0}d ", ts.Days));
                if (ts.Hours > 0)
                    outputBuilder.Append(string.Format("{0}h ", ts.Hours));
                if (ts.Minutes > 0)
                    outputBuilder.Append(string.Format("{0}m ", ts.Minutes));
                return outputBuilder.ToString();
            }
        }

        public TimeSpan DisconnectTimespan
        {
            get
            {
                TimeSpan ts;
                if (!ConnectTime.HasValue)
                    ts = DateTime.Now - DisconnectTime.Value;
                else
                    ts = ConnectTime.Value - DisconnectTime.Value;
                return ts;
            }
        }


    }
}
