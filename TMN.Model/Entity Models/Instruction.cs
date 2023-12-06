using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Reflection;
using Enterprise;

namespace TMN
{

    public partial class Instruction : Entity, IDeletable
    {

        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;

                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                db.Instructions.DeleteOnSubmit(db.Instructions.Where(p => p.ID == this.ID).SingleOrDefault());
                try
                {
                    System.IO.Directory.Delete(StoragePath, true);
                }
                catch (Exception ex)
                {
                    Logger.Write(ex, "Could not delete directory \"{0}\"", StoragePath);
                }
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

        public string StoragePath
        {
            get
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path = System.IO.Path.Combine(path, @"Files\Instructions\" + this.ID);
                return path;
            }
        }

        public bool IsUnique()
        {
            if (DB.Instance.Instructions.Count(i => i.ID != this.ID && (i.Ineffect == Ineffect || i.Number == Number) && i.User.Center == Center.Current) > 0)
            {
                MessageBox.ShowError("اين دستورمداری تکراری است.");
                return false;
            }
            return true;
        }

    }
}
