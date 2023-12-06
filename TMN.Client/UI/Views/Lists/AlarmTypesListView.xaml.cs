using System.Linq;

namespace TMN.Views.Lists
{

    public partial class AlarmTypesListView : ItemsListBase
    {

        public AlarmTypesListView()
        {
            InitializeComponent();
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from at in DB.Instance.AlarmTypes
                         select at, selectLast);
        }


    }
}
