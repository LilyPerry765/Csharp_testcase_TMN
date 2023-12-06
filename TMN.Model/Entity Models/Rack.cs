using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;

namespace TMN
{

    public partial class Rack : Entity, IDeletable, IChild, ICapacity
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                bool isUsed = (from s in db.Shelfs
                               where s.RackID == this.ID
                               select s).Count() > 0;
                if (!isUsed)
                {
                    db.Racks.DeleteOnSubmit(db.Racks.Where(p => p.ID == this.ID).SingleOrDefault());
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
                return this.Center;
            }
        }

        public int FreeSpace
        {
            get
            {
                return (this.RackType.Capacity ?? 0) 
                    - DB.Instance.Racks.Where(r => r == this).Single().Shelfs.Count;
            }
        }

        public bool IsUnique(string name)
        {

            if (DB.Instance.Racks.Where(r => r.ID != this.ID && r.Name == name && r.Center == Center.Selected).Count() > 0)
            {
                MessageBox.Show( MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }

        public bool HasChild
        {
            get
            {
                return this.Shelfs.Count > 0;
            }
        }


    }
}
