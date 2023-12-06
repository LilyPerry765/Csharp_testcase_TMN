using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;

namespace TMN
{
    public partial class TaskType : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                bool isUsed = db.Tasks.Where(e => e.TaskType == this).Count() > 0;

                if (!isUsed)
                {
                    db.TaskTypes.DeleteOnSubmit(db.TaskTypes.Where(p => p.ID == this.ID).SingleOrDefault());
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show(MessageTypes.CannotDeleteHasItems);
                }
            }
            return false;
        }

        public bool IsUnique(string name)
        {

            if (DB.Instance.TaskTypes.Where(c => c.ID != this.ID && c.Name == name).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }
    }
}
