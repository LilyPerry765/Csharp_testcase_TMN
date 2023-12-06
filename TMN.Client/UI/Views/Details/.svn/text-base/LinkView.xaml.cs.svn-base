using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;
using TMN.UI.Windows;
using System.Data.SqlClient;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for LinkView.xaml
    /// </summary>
    public partial class LinkView : UserControl, IDetailsView
    {

        private Center SourceCenter;
        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public LinkView(Center parent)
        {
            InitializeComponent();
            SourceCenter = parent;
        }

        public void BeginEdit(Entity entity)
        {
            DataContext = DataSource.Links.SingleOrDefault(p => p == (entity as Link));
        }

        public void BeginInsert()
        {
            DataContext = new Link()
            {
                CenterID = Center.CurrentCenterID
            };
        }

        public Entity SaveData()
        {
            if (Link.ID == Guid.Empty)
            {
                Link.ID = Guid.NewGuid();
                DataSource.Links.InsertOnSubmit(DataContext as Link);
            }
            if (Link.DDF != null && Link.DDF.Description.IsNullOrEmpty())
            {
                Link.DDF.Description = string.Format("{0}", Link.Address);
            }
            this.EndEdit();
            try
            {
                DataSource.SubmitChanges();
                return DataContext as Entity;
            }
            catch (SqlException)
            {
                MessageBox.ShowError("اين لينک قبلا تعريف شده");
            }
            return null;
        }

        private Link Link
        {
            get
            {
                return DataContext as Link;
            }
        }

        public bool Validate()
        {
            this.EndEdit();
            return txtAddress.IsAnswered("آدرس لينک")
                && DataContext.As<Link>().IsUnique();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetDDFWindow win = new SetDDFWindow();
            win.DDF = Link.DDF;

            if (win.ShowDialog(this) ?? false)
            {
                Link.DDF = DataSource.DDFs.SingleOrDefault(p => p == win.DDF);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetAddress();
        }

        private void DIUNumericUpDown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetAddress();
        }

        private void SetAddress()
        {
            txtAddress.Text = string.Format("{0}-{1}", txtLTG.Text.Trim(), DIUNumericUpDown.Value);
        }

    }
}

