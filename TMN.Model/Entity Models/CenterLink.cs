using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;

namespace TMN
{
    public partial class CenterLink : Entity
    {
        public bool Exists(TMNModelDataContext db)
        {
            return (from cLink in db.CenterLinks
                    where (cLink.CenterA == this.CenterA && cLink.CenterB == this.CenterB)
                    || (cLink.CenterA == this.CenterB && cLink.CenterB == this.CenterA)
                    select cLink).Count() > 0;

        }
    }
}
