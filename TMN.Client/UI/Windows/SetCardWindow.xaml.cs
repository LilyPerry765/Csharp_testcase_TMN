using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TMN.UI.Windows
{

    public partial class SetCardWindow : Window
    {
        TMNModelDataContext db = new TMNModelDataContext();

        private int linksCount;
        public SetCardWindow(int linksCount)
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
            this.linksCount = linksCount;
            LoadData();
        }


        private void LoadData()
        {
            RackComboBox.ItemsSource = db.Racks.Where(p => p.Center == Center.Current);
            ShelfComboBox.ItemsSource = db.Shelfs.Where(p => p.Rack.Center == Center.Current);
            CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf.Rack.Center == Center.Current);
        }

        public Card Card
        {
            get
            {
                return CardComboBox.SelectedItem as Card;
            }
            set
            {
                if (value != null)
                {
                    RackComboBox.SelectedValue = value.Shelf.Rack.ID;
                    ShelfComboBox.SelectedValue = value.Shelf.ID;
                    CardComboBox.SelectedValue = value.ID;
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
                DialogResult = true;
        }

        private bool Validate()
        {
            return CardComboBox.IsAnswered("کارت")
                && CheckCard();
        }

        private bool CheckCard()
        {

            if (Card.CardType.IsControlCard == true)
            {
                MessageBox.ShowError("اين کارت کنترلی است.");
                return false;
            }
            else if (Card.FreeSpace < linksCount)
            {
                MessageBox.ShowError("ظرفيت اين کارت تکميل است. لطفا کارت ديگری انتخاب نماييد يا تعداد لينک های انتخاب شده را کاهش دهيد.");
                return false;
            }

            return true;
        }

        private void RackComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RackComboBox.SelectedItem != null)
            {
                ShelfComboBox.ItemsSource = db.Shelfs.Where(p => p.Rack == RackComboBox.SelectedItem);
                CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf.Rack == RackComboBox.SelectedItem);
            }
            else
            {
                ShelfComboBox.ItemsSource = db.Shelfs.Where(p => p.Rack.Center == Center.Current);
                CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf.Rack.Center == Center.Current);
            }
        }

        private void ShelfComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShelfComboBox.SelectedItem != null)
            {
                CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf == ShelfComboBox.SelectedItem);
            }
            else if (RackComboBox.SelectedItem != null)
            {
                CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf.Rack == RackComboBox.SelectedItem);
            }
            else
            {
                CardComboBox.ItemsSource = db.Cards.Where(p => p.Shelf.Rack.Center == Center.Current);
            }
        }
    }

}
