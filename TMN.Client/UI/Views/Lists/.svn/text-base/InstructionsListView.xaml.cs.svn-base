using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TMN.UI.Windows;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for InstructionsListView.xaml
    /// </summary>
    public partial class InstructionsListView : ItemsListBase
    {
        TMNModelDataContext db = null;

        public InstructionsListView()
        {
            InitializeComponent();
            // MainWindow.Instance.Tree.RedrawNeeded += new EventHandler(Tree_RedrawNeeded);
        }

        //void Tree_RedrawNeeded(object sender, EventArgs e)
        //{
        //    Refresh();
        //}

        private void AttachmentButton_Click(object sender, RoutedEventArgs e)
        {
            new InstructionAttachmentsList(ListView.SelectedItem as Instruction).ShowDialog(this);
        }

        public override void Refresh(bool selectLast)
        {
            db = DB.Instance;
            base.Refresh(db.Instructions.Where(p => p.User.CenterID == Center.CurrentCenterID)
                                        .Where(p => !IsSearching || 
                                            ((txtNo.Text.Trim() == "" || p.Number == txtNo.Text.Trim()) 
                                            && (IsDoneCheckBox.IsChecked == null || (p.IsDone ?? false) == IsDoneCheckBox.IsChecked)))
                                        , selectLast);
        }

        void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            new InstructionImageWindow(ListView.SelectedItem as Instruction, db).ShowDialog(this);
        }

        private void ItemsListView_Loaded(object sender, RoutedEventArgs e)
        {
            AddToolbarButton("AttachmentsButton", "اسناد", "attach.png", 3, AttachmentButton_Click, OperationMode.SingleItem);
            AddToolbarButton("ImageButton", "تصوير", "image.png", 3, btnSelectImage_Click, OperationMode.SingleItem);
        }
    }
}
