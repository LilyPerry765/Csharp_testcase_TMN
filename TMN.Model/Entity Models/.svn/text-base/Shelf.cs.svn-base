using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;

namespace TMN
{

    public partial class Shelf : Entity, IDeletable, IChild, ICapacity
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                bool isUsed = (from c in db.Cards
                               where c.ShelfID == this.ID
                               select c).Count() > 0;
                if (!isUsed)
                {
                    db.Shelfs.DeleteOnSubmit(db.Shelfs.Where(p => p.ID == this.ID).SingleOrDefault());

                    // user log
                    db.UserLogs.InsertOnSubmit(new UserLog
                    {
                        ID = Guid.NewGuid(),
                        CenterID = Center.Current.ID,
                        UserID = User.Current.ID,
                        Date = DateTime.Now,
                        Description = ""
                    });

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

        public object Parent
        {
            get
            {
                return this.Rack;
            }
        }

        public int FreeSpace
        {
            get
            {
                return (this.ShelfType.Capacity ?? 0)
                    - DB.Instance.Cards.Where(c => c.Shelf == this).Count();
            }
        }

        public bool IsUnique(string name)
        {

            if (DB.Instance.Shelfs.Where(s => s.ID != this.ID && s.Name == name && s.Rack.Center == Center.Selected).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }

        public bool HasChild
        {
            get
            {
                return this.Cards.Count > 0;
            }
        }


        public bool IsOnBusyPosition
        {
            get
            {
                return DB.Instance.Shelfs.Where(sh => sh.RackID == this.RackID && sh.Position == this.Position && sh != this).Count() > 0;
            }
        }

    }
}
