using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;

namespace TMN
{
    public partial class SpareCard : Entity, IDeletable
    {
        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                TMNModelDataContext db = new TMNModelDataContext();
                db.SpareCards.DeleteOnSubmit(db.SpareCards.Where(p => p == this).SingleOrDefault());
                db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
