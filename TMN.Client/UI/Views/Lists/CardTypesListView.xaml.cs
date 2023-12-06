using System.Linq;

namespace TMN.Views.Lists
{
    /// <summary>
    /// Interaction logic for CardTypesListView.xaml
    /// </summary>
    public partial class CardTypesListView : ItemsListBase
    {

        public CardTypesListView()
        {
            InitializeComponent();
            SupportingSwitchComboBox.ItemsSource = DB.Instance.SwitchTypes;
        }

        public override void Refresh(bool selectLast)
        {
            base.Refresh(from cardType in DB.Instance.CardTypes
                         where (!IsSearching ||
                                 (SupportingSwitchComboBox.SelectedValue == null || cardType.SwitchType == (SupportingSwitchComboBox.SelectedItem as SwitchType))
                                 && cardType.Name.Contains(NameTextBox.Text)
                                 && (!IsControlCardCheckBox.IsChecked.HasValue || cardType.IsControlCard == IsControlCardCheckBox.IsChecked)
                                 && (FromE1CountNumericUpDown.Value == 0 || cardType.E1Count >= FromE1CountNumericUpDown.Value)
                                 && (ToE1CountNumericUpDown.Value == 0 || cardType.E1Count <= ToE1CountNumericUpDown.Value))
                         orderby cardType.SwitchType.Name
                         select cardType, selectLast);
        }

    }
}
