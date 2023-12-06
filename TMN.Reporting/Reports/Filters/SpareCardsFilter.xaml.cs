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
using Microsoft.Reporting.WinForms;
using TMN.Reports.Model;

namespace TMN.Reports.Filters
{
    /// <summary>
    /// Interaction logic for RoutesReport.xaml
    /// </summary>
    public partial class SpareCardsFilter : UserControl, IReportFilter
    {
        public SpareCardsFilter()
        {
            InitializeComponent();
        }

        TMNModelDataContext db = new TMNModelDataContext();
        public ReportDataSource GetDataSource()
        {
            ReportDataSource src = new ReportDataSource("SpareCards");
            src.Value = (from sp in db.SpareCards
                         where sp.Center == Center.Current
                         && (cmbType.SelectedItem == null || sp.CardType == (cmbType.SelectedItem as CardType))
                         select new Reports.Model.SpareCards
                         {
                             IsControlCard = (sp.CardType.IsControlCard ?? false) ? "*" : "",
                             Count = sp.Count,
                             TypeName = sp.CardType.Name,
                             Description = sp.Description

                         }).ToList();
            return src;
        }

        public Control GetControl()
        {
            return this;
        }

        private void cmbType_DropDownOpened(object sender, EventArgs e)
        {
            cmbType.ItemsSource = db.Centers.Where(c => c == Center.Current).Single().SwitchType.CardTypes;
        }

        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cmbType.SelectedIndex = -1;
        }

        public bool Validate()
        {
            return true;
        }
    }
}
