using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;

namespace TMN
{

    public partial class RackType : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
               
                if (!IsInUse)
                {
                    RackType rackType = db.RackTypes.Where(p => p.ID == this.ID).SingleOrDefault();
                    db.RackTypes.DeleteOnSubmit(rackType);

                    // user log
					//UserLog.Log(db, ActionType.RackRemove, string.Format("Name={0}", rackType.Name), string.Format("ID={0} , Name={1} ", rackType.ID, rackType.Name));

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

        public bool IsInUse
        {
            get
            {
                return DB.Instance.Racks.Where(p => p.RackType == this).Count() > 0;
            }
        }

        public bool IsUnique(string name)
        {
            if (DB.Instance.RackTypes.Where(r => r.ID != this.ID && r.Name == name && r.SwitchType.ID == this.SwitchType.ID).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }
    }
}
