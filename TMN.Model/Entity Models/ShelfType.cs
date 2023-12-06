using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;

namespace TMN
{

    public partial class ShelfType : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;

                if (!IsInUse)
                {
                    ShelfType shelfType = db.ShelfTypes.Where(p => p.ID == this.ID).SingleOrDefault();
                    db.ShelfTypes.DeleteOnSubmit(shelfType);

                    // user log
					//UserLog.Log(db, ActionType.ShelfRemove, string.Format("Name={0}", shelfType.Name), string.Format("ID={0} , Name={1} ", shelfType.ID, shelfType.Name));


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
                return DB.Instance.Shelfs.Where(p => p.ShelfType == this).Count() > 0;
            }
        }

        public bool IsUnique(string name)
        {
            if (DB.Instance.ShelfTypes.Where(s => s.ID != this.ID && s.Name == name && s.SwitchType.ID == this.SwitchType.ID).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }
    }
}
