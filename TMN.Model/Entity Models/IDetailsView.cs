using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace TMN.Interfaces
{
    public interface IDetailsView
    {
        void BeginEdit(Entity entity);

        void BeginInsert();

        Entity SaveData();

        bool Validate();


        TMNModelDataContext DataSource
        {
            get;
        }

        // bool IsNew;


    }
}
