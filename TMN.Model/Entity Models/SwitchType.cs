﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Windows;
using System.Text.RegularExpressions;

namespace TMN
{
    public partial class SwitchType : Entity, IDeletable
    {

        public bool IsUnique(string name)
        {

            if (DB.Instance.SwitchTypes.Where(c => c.ID != this.ID && c.Name == name).Count() > 0)
            {
                MessageBox.Show(MessageTypes.RepeatedName);
                return false;
            }
            return true;
        }

        public bool Delete()
        {
            if (MessageBox.Show(MessageTypes.ConfirmDelete) == System.Windows.MessageBoxResult.Yes)
            {
                var db = DB.Instance;
                bool isUsed = db.Centers.Where(c => c.SwitchType == this).Count() > 0 ||
                              db.RackTypes.Where(c => c.SwitchType == this).Count() > 0 ||
                              db.ShelfTypes.Where(c => c.SwitchType == this).Count() > 0 ||
                              db.CardTypes.Where(c => c.SwitchType == this).Count() > 0;

                if (!isUsed)
                {
                    SwitchType switchType = db.SwitchTypes.Where(p => p.ID == this.ID).SingleOrDefault();
                    db.SwitchTypes.DeleteOnSubmit(switchType );
                    

                    // user log
					//UserLog.Log(db, ActionType.SwitchRemove, string.Format("Name={1}", switchType.Name), string.Format("ID={0} , Name={1} , Company={2}", switchType.ID, switchType.Name, switchType.Company));


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


    }
}
