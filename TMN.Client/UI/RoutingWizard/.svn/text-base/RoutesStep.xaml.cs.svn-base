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
using TMN.Interfaces;

namespace TMN.UI.RoutingWizard
{
    /// <summary>
    /// Interaction logic for RoutesStep.xaml
    /// </summary>
    public partial class RoutesStep : UserControl, IValidator
    {

        public RoutesStep()
        {
            InitializeComponent();
            routsListView.ItemsSource = DB.Instance.Routes.Where(r => r.Center1 == Center.Current);
        }

        public Route SelectedRoute
        {
            get
            {
                return routsListView.SelectedItem as Route;
            }
        }

        #region IValidator Members

        public bool Validate()
        {
            if (routsListView.SelectedItem != null)
            {
                return true;
            }
            else
            {
                MessageBox.ShowError("هيچ مسيری انتخاب نشده. لطفا مسير مورد نظر را انتخاب کنيد.");
                return false;
            }
        }

        #endregion
    }
}
