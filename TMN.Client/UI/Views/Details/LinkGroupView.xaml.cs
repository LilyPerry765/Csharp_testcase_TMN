using System;
using System.Linq;
using System.Windows.Controls;
using TMN.Interfaces;
using System.Windows;
using Enterprise;
using System.Transactions;

namespace TMN.Views.Details
{
    /// <summary>
    /// Interaction logic for TemplateView.xaml
    /// </summary>
    public partial class LinkGroupView : UserControl, IDetailsView
    {
        public LinkGroupView()
        {
            InitializeComponent();
        }

        private TMNModelDataContext db = DB.Instance;
        public TMNModelDataContext DataSource
        {
            get
            {
                return db;
            }
        }

        public void BeginEdit(Entity entity)
        {
            throw new NotSupportedException("Edit is not supported for Link group.");
        }

        public void BeginInsert()
        {
            // Do nothing
        }

        public Entity SaveData()
        {
            for (decimal i = DIUFromUpDown.Value.Value; i <= DIUTo.Value.Value; i++)
            {
                Link link = new Link()
                {
                    ID = Guid.NewGuid(),
                    LTG = LTGTextBox.Text.Trim(),
                    DIU = (int)i,
                    Address = string.Format("{0}-{1}", LTGTextBox.Text.Trim(), i.ToString()),
                    Sys = (long)(SysFromUpDown.Value + (i - DIUFromUpDown.Value)),
                    CenterID = Center.CurrentCenterID
                };
                if (link.IsUnique())
                    db.Links.InsertOnSubmit(link);
                else
                {
                    db = new TMNModelDataContext();
                    return null;
                }
            }

            try
            {
                db.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);

                // Only wanted to return some Entity other than null, so that the calling dialog box could return true as the dialog result.
                return new Link();
            }
            catch (Exception ex)
            {
                db = new TMNModelDataContext();
                Logger.Write(ex);
                if (ex.Message.Contains("MSDTC"))
                {
                    Logger.WriteDebug("Start \"Distributed Transaction Coordinator\" service.");
                }
                MessageBox.ShowError("اطلاعات وارد شده تکراری است");
            }
            return null;
        }

        public bool Validate()
        {
            return LTGTextBox.IsAnswered("LTG");
        }

        private void DIUFromUpDown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DIUFromUpDown.Value > DIUTo.Value)
                DIUTo.Value = DIUFromUpDown.Value;
            SetToSys();
        }

        private void SetToSys()
        {
            SysTo.Content = SysFromUpDown.Value + (DIUTo.Value - DIUFromUpDown.Value);
        }

        private void SysNumericUpDown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetToSys();
        }

        private void DIUTo_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DIUFromUpDown.Value > DIUTo.Value)
                DIUFromUpDown.Value = DIUTo.Value;
            SetToSys();
        }

    }

}
