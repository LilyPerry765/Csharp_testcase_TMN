using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;
using System;

namespace TMN.UserControls
{
    public class ImageButton : Button
    {
        private Image image = new Image();
        private ColumnDefinition freeSpaceColDef = new ColumnDefinition();
        private TextBlock textBlock = new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        public ImageButton()
        {
            CreateContent();
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            image.SizeChanged += new SizeChangedEventHandler(image_SizeChanged);
            IsEnabledChanged += new DependencyPropertyChangedEventHandler(ImageButton_IsEnabledChanged);
        }

        void ImageButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnabled)
            {
                image.Opacity = 1;
            }
            else
            {
                image.Opacity = .3;
            }
        }

        void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            freeSpaceColDef.Width = new GridLength(image.ActualWidth);
        }

        public Visibility ImageVisibility
        {
            get
            {
                return this.image.Visibility;
            }
            set
            {
                this.image.Visibility = value;
            }
        }

        private void CreateContent()
        {
            Grid rootGrid = (Grid)(base.Content = new Grid());

            rootGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = GridLength.Auto,
            });
            rootGrid.ColumnDefinitions.Add(new ColumnDefinition());

            rootGrid.ColumnDefinitions.Add(freeSpaceColDef);
            rootGrid.Children.Add(image);
            rootGrid.Children.Add(textBlock);
            Grid.SetColumn(image, 0);
            Grid.SetColumn(textBlock, 1);
        }

        private HorizontalAlignment _ImageAlignment = HorizontalAlignment.Right;
        public HorizontalAlignment ImageAlignment
        {
            get
            {
                return _ImageAlignment;
            }
            set
            {
                _ImageAlignment = value;
            }
        }

        public string Text
        {
            get
            {
                return textBlock.Text;
            }
            set
            {
                textBlock.Text = value;
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropertyChanged)));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ImageSourceProperty)
            {
                ((ImageButton)d).image.Source = e.NewValue as ImageSource;
            }
        }
    }
}
