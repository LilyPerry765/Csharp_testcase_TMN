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
    /// Interaction logic for ChannelStep.xaml
    /// </summary>
    public partial class ChannelStep : UserControl, IValidator
    {
        public ChannelStep()
        {
            InitializeComponent();
        }

        #region IValidator Members

        public bool Validate()
        {
            return ChannelUpDown.IsAnswered("کانال شروع")
                    && CICUpDown.IsAnswered("CIC شروع");
        }

        #endregion
    }
}
