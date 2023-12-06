using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN.Interfaces;
using System.Data.SqlClient;
using System.Windows;
using System.Data.Linq.Mapping;

namespace TMN.Reports.Model
{

    public class Cards : TMN.Card
    {
        public string ShelfName
        {
            get;
            set;
        }

        public string RackName
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }

        public string IsControlCard
        {
            get;
            set;
        }
        
    }
}
