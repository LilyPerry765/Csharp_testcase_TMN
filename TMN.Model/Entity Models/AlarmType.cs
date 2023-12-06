using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;

namespace TMN
{
    public partial class AlarmType : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                AlarmType alarmType = db.AlarmTypes.SingleOrDefault(p => p == this);
                db.AlarmTypes.DeleteOnSubmit(alarmType);
                try
                {

                    // user log
					//UserLog.Log(db, ActionType.AlarmTypeRemove, string.Format("Name={0}", alarmType.Name), string.Format("ID={0} , Name={1}", alarmType.ID, alarmType.Name));

                    db.SubmitChanges();
                    db.Transaction.Commit();
                    return true;
                }
                catch
                {
                    db.Transaction.Rollback();
                    MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                    return false;
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            return false;
        }

        public bool IsUnique(string name)
        {
            if (DB.Instance.AlarmTypes.Count(c => c.ID != this.ID && c.Name == name) > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }
    }
}
