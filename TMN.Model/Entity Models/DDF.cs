using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public partial class DDF : Entity
    {
        public DDF(Guid centerID, int bay, int position, int number)
            : this()
        {
            this.ID = Guid.NewGuid();
            this.CenterID = centerID;
            this.Bay = bay;
            this.Position = position;
            this.Number = number;
        }

        public static DDF FindOrCreate(Center center, int bay, int position, int number, String description)
        {
            var db = DB.Instance;
            DDF foundDDF = db.DDFs.SingleOrDefault(p => p.Center == center && p.Bay == bay && p.Position == position && p.Number == number);
            if (foundDDF != null)
            {
                foundDDF.Description = description;
                db.SubmitChanges();
                return foundDDF;
            }
            else
            {
                DDF newDDF = new DDF(center.ID, bay, position, number)
                {
                    Description = description,
                };
                db.DDFs.InsertOnSubmit(newDDF);
                db.SubmitChanges();
                return newDDF;
            }
        }

        public Link SingleLink
        {
            get
            {
                if (IsSTM1)
                    return null;

                return this.Links.FirstOrDefault();
            }
        }

        public bool IsSTM1
        {
            get
            {
                return this.Links.Count > 1;
            }
        }
    }
}
