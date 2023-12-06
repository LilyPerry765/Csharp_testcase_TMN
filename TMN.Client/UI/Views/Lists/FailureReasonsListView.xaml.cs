using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMN.UI.Windows;

namespace TMN.Views.Lists
{
    public partial class FailureReasonsListView : ItemsListBase
    {

        public FailureReasonsListView()
        {
            InitializeComponent();
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(DB.Instance.FailureReasons, selectLast);
        }
    }
}
