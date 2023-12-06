using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using TMN.UserControls;
using System.Windows.Data;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for DDFWindow.xaml
    /// </summary>
    public partial class DDFWindow : Window
    {
        private TMNModelDataContext db = DB.Instance;

        public DDFWindow()
        {
            InitializeComponent();
            MainWindow.Instance.Tree.RedrawNeeded += new EventHandler(Tree_RedrawNeeded);
            Icon = MainWindow.Instance.Icon;
        }

        void Tree_RedrawNeeded(object sender, EventArgs e)
        {
            if (DDFPanel.IsVisible)
                PutItemsOnDDF();
        }

        private void PutItemsOnDDF()
        {
            // Window is not yet loaded
            if (DDFPanel != null)
            {
                var DDFCache = db.DDFs.Where(d => d.CenterID == Center.CurrentCenterID && d.Links.Count > 0);
                ChannelsPanel.Children.Clear();
                DDFPanel.Children.Clear();
                FrameworkElement ddfItem = null;
                int bay = (int)BayUpDown.Value;
                int position = (int)PositionUpdown.Value;
                for (int number = 1; number <= 20; number++)
                {
                    DDF ddf = DDFCache.FirstOrDefault(d => d.Bay == bay
                                                        && d.Position == position
                                                        && d.Number == number);
                    if (ddf == null)
                        ddfItem = DrawFreeLink(number);
                    else
                        ddfItem = DrawBusyLink(ddf);
                    ddfItem.Margin = new Thickness(10, 5, 10, 5);
                    Grid.SetColumn(ddfItem, 0);
                    Grid.SetRow(ddfItem, number - 1);
                    BindingOperations.SetBinding(ddfItem, FrameworkElement.HeightProperty, new Binding("ActualWidth")
                    {
                        RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                        Converter = Converters.DoubleMultiplyConverter.Instance,
                        ConverterParameter = .6
                    });
                }
            }
        }

        private FrameworkElement DrawBusyLink(DDF ddf)
        {
            // ToDo: Instead of "false" pass an argumant that shows wheather the link has any problem
            DDFDisplay ddfDispley = new DDFDisplay(false);
            ToggleButton btn = new ToggleButton()
            {
                Background = Brushes.Transparent, //FindResource("Connected") as Brush,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                DataContext = ddf,
                Content = ddfDispley,
            };
            Block.SetTextAlignment(btn, TextAlignment.Center);
            ddfDispley.CommitEdit += new EventHandler(ddfDispley_CommitEdit);
            btn.Checked += new RoutedEventHandler(btn_Checked);
            btn.Unchecked += new RoutedEventHandler(btn_Unchecked);
            DDFPanel.Children.Add(btn);
            return btn;
        }

        private FrameworkElement DrawFreeLink(int number)
        {
            Border ddfItem = new Border()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(5),
                Child = new Label()
                {
                    Content = number,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right,
                    FlowDirection = System.Windows.FlowDirection.LeftToRight
                },
                Opacity = .7,
                Background = new SolidColorBrush(Colors.White)
                {
                    Opacity = .3
                }
            };
            DDFPanel.Children.Add(ddfItem);
            return ddfItem;
        }

        void btn_Unchecked(object sender, RoutedEventArgs e)
        {
            ChannelsPanel.Children.Clear();
        }

        void btn_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in DDFPanel.Children)
            {
                if (item != sender && item is ToggleButton && item.As<ToggleButton>().IsChecked.Value)
                {
                    (item as ToggleButton).IsChecked = false;
                }
            }
            new ItemsListHolderWindow(EntityTypes.DDFRoute, (sender as ToggleButton).DataContext as DDF).ShowAsChildOf(ChannelsPanel);
        }

        void ddfDispley_CommitEdit(object sender, EventArgs e)
        {
            db.SubmitChanges();
        }


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            try
            {
                PrintDialog print = new PrintDialog();
                if (print.ShowDialog() == true)
                {
                    p.PrintVisual(DDFPanel, "DDF");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            db = new TMNModelDataContext();
            PutItemsOnDDF();
        }

        private void PositionUpdown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PutItemsOnDDF();
        }

        private void BayUpDown_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PutItemsOnDDF();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PutItemsOnDDF();
        }

    }
}
