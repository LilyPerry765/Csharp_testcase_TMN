using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public class Roles
    {
        static TMNModelDataContext db = new TMNModelDataContext();
        public static Role Admin
        {
            get
            {
                return db.Roles.Single(r => r.ID == "Admin");
            }
        }

        public static Role PowerUser
        {
            get
            {
                return db.Roles.Single(r => r.ID == "PowerUser");
            }
        }
    }
}
