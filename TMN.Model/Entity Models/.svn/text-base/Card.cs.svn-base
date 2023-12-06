using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Windows;

namespace TMN
{

    public partial class Card : Entity, IDeletable, IChild, ICapacity
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Cards.DeleteOnSubmit(db.Cards.Where(p => p.ID == this.ID).SingleOrDefault());
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

        public virtual object Parent
        {
            get
            {
                return this.Shelf;
            }
        }

        public static IEnumerable<Card> FromDDF(Center center, int bay, int position, int number)
        {
            TMNModelDataContext db = new TMNModelDataContext();
            return from link in db.Links
                   where link.DDF.Center == center
                         && link.DDF.Bay == bay
                         && link.DDF.Position == position
                         && link.DDF.Number == number
                   select link.Card;
        }

        public bool IsOnBusySlot
        {
            get
            {
                return DB.Instance.Cards.Where(c => c.ShelfID == this.ShelfID && c.SlotNo == this.SlotNo && c != this).Count() > 0;
            }
        }

        partial void OnSlotNoChanged()
        {
            SendPropertyChanged("IsOnBusySlot");
        }

        public bool ValidateSlot()
        {
            if (IsOnBusySlot)
            {
                MessageBox.Show("اين اسلات توسط کارت دیگری اشغال شده است." + "\n" + "لطفا اسلات دیگری را انتخاب نمایید.", "اسلات اشغال", MessageBoxImage.Error);
            }
            return !IsOnBusySlot;
        }

        public bool IsUnique(string name)
        {

            if (DB.Instance.Cards.Any(c => c.ID != this.ID && c.Name == name && c.Shelf.Rack.Center == Center.Selected) )
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }

        public int FreeSpace
        {
            get
            {
                if (this.CardType.IsControlCard.Value)
                {
                    return -1;
                }

                return this.CardType.E1Count.Value - DB.Instance.Links.Where(l => l.Card == this).Count();
            }
        }

        public bool HasChild
        {
            get
            {
                return this.Links.Count > 0;
            }
        }
    }
}
