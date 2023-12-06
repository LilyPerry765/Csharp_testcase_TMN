using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for Schema.xaml
    /// </summary>
    public partial class SchemaWindow : Window
    {
        private ToolBar toolbar;

        public SchemaWindow()
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
            toolbar = CreateToolbar();
        }

        private ToolBar CreateToolbar()
        {
            const int imgSize = 25;
            ToolBar tb = new ToolBar();

            Button ziButton = new Button()
            {
                ToolTip = "بزرگ نمايی",
                Content = new Image()
                {
                    Width = imgSize,
                    Height = imgSize,
                    Source = ImageSourceHelper.GetImageSource("zoomin.png")
                },
                ClickMode = ClickMode.Press
            };
            ziButton.Click += new RoutedEventHandler(ziButton_Click);

            Button zoButton = new Button()
            {
                ToolTip = "کوچک نمايی",
                Content = new Image()
                {
                    Width = imgSize,
                    Height = imgSize,
                    Source = ImageSourceHelper.GetImageSource("zoomout.png")
                },
                ClickMode = ClickMode.Press
            };
            zoButton.Click += new RoutedEventHandler(zoButton_Click);

            tb.Items.Add(ziButton);
            tb.Items.Add(zoButton);
            return tb;
        }

        private void ziButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn();
        }

        private void zoButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.toolbarTray.ToolBars.Add(toolbar.Extract());
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            toolbar.Extract();
        }

        private double zoomFactor = 1;

        private void ZoomOut()
        {

            Root.Zoom(zoomFactor /= 1.3);
        }

        private void ZoomIn()
        {

            Root.Zoom(zoomFactor *= 1.3);
        }


    }
}
