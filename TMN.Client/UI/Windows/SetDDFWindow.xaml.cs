using System.Linq;
using System.Windows;

namespace TMN.UI.Windows
{

    public partial class SetDDFWindow : Window
    {
        public SetDDFWindow()
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
        }

        public DDF DDF
        {
            get
            {
                return DDF.FindOrCreate(Center.Current, (int)BayUpDown.Value, (int)PositionUpDown.Value, (int)NumberUpDown.Value, DescriptionTextBox.Text);
            }
            set
            {
                if (value != null)
                {
                    PositionUpDown.Value = value.Position.Value;
                    NumberUpDown.Value = value.Number.Value;
                    BayUpDown.Value = value.Bay.Value;
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
            return BayUpDown.IsAnswered("Bay")
                    && PositionUpDown.IsAnswered("Position")
                    && NumberUpDown.IsAnswered("Number");
        }

        private void UpDown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DDF foundDDF = DB.Instance.DDFs.SingleOrDefault(d => d.Center == Center.Current
                                                        && d.Bay == BayUpDown.Value
                                                        && d.Position == PositionUpDown.Value
                                                        && d.Number == NumberUpDown.Value);
            if (foundDDF != null)
            {
                DescriptionTextBox.Text = foundDDF.Description;
            }
            else
            {
                DescriptionTextBox.Text = string.Empty;
            }
        }

        private int CalculateSys()
        {
            if (NumberUpDown.Value > 0 && BayUpDown.Value > 0 && PositionUpDown.Value > 0)
                return (int)(NumberUpDown.Value + (PositionUpDown.Value - 1) * 20 + (BayUpDown.Value - 1) * 40);
            return 0;
        }

     }
}
