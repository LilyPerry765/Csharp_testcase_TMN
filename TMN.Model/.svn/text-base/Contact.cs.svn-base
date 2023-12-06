using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;

namespace TMN
{
    public partial class Contact : Entity, IDeletable
    {
        TMNModelDataContext db = new TMNModelDataContext();

        public bool Delete()
        {



         //   using (TMNModelDataContext db = new TMNModelDataContext())
          //  {



                if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
                {
                    List<CenterContact> list = db.CenterContacts.Where(c => c.ContactID == this.ID).ToList();
                    if (list.Count > 0)
                    {
                        if (MessageBox.Show(MessageTypes.HasChilds) == System.Windows.MessageBoxResult.Yes)
                        {
                            db.Connection.Open();
                            db.Transaction = db.Connection.BeginTransaction();

                            foreach (CenterContact item in list)
                            {
                                item.Delete();
                            }

                            Contact contact = db.Contacts.Where(p => p == this).SingleOrDefault();
                            db.Contacts.DeleteOnSubmit(contact);
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
                    }
                    else
                    {
                        Contact contact = db.Contacts.Where(p => p == this).SingleOrDefault();
                        db.Contacts.DeleteOnSubmit(contact);
                        db.SubmitChanges();
                    }

                }
                
                //Contact contact = db.Contacts.Where(p => p.ID == this.ID).SingleOrDefault();
                //db.Contacts.DeleteOnSubmit(contact);
                //db.SubmitChanges();
      //      }

            return true;
        }

        public  bool IsInCenter(Guid CenterID)
        {


            return db.CenterContacts.Any(c => c.ContactID == this.ID && c.CenterID == CenterID);


           // return DB.Instance.CenterContacts.Any (c => c.ContactID == throw 


           // return DB.Instance.UserRoleRelations.Any(p => p.User == this && p.RoleID == roleID);
        }
    }
}
