using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;

namespace TMN
{

    public partial class CardType : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;

                if (!IsInUse)
                {
                    CardType cardType = db.CardTypes.Where(p => p.ID == this.ID).SingleOrDefault();
                    db.CardTypes.DeleteOnSubmit(cardType);

                    // user log
					//UserLog.Log(db, ActionType.CardRemove, string.Format("Name={0}", cardType.Name), string.Format("ID={0} , Name={1} ", cardType.ID, cardType.Name));


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
            if (DB.Instance.CardTypes.Where(r => r.ID != this.ID && r.Name == name && r.SwitchType.ID == this.SwitchType.ID).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }

        public bool IsInUse
        {
            get
            {
                return (from r in DB.Instance.Cards
                        where r.CardType == this
                        select r).Count() > 0;
            }
        }



    }
}
