using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;

namespace TMN
{
    public partial class CircuitLink : Entity
    {
        public bool Exists(TMNModelDataContext db)
        {
            return (from cLink in db.CircuitLinks
                    where (cLink.AMapID == this.AMapID && cLink.BMapID == this.BMapID)
                    || (cLink.AMapID == this.BMapID && cLink.BMapID== this.AMapID)
                    select cLink).Count() > 0;

        }
    }
}
