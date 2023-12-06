using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Effects;

namespace TMN.Units
{
    class RackUnit : Unit
    {
        public const double ThumbWidth = 60;
        public const double ThumbHeight = 60;
        public const double NormalWidth = 300;
        public const double NormalHeight = 600;
        public const int NormalBorderThickness = 1;
        public const double SideMargin = 5;
        private const double TopMargin = 20;
        private const double ButtomMargin = 5;

        public RackUnit(TreeViewItem mappedNode, bool isThumbnail)
            : base(mappedNode)
        {
            CornerRadius = new System.Windows.CornerRadius(5);

            BindXY();
            if (this.IsThumbnail = isThumbnail)
            {
                InitializeThumbnailRack();
            }
            else //Is Not Thambnail
            {
                InitializFullRackView();
            }
        }

        private void InitializFullRackView()
        {
            Grid grd = new Grid();
            grd.Margin = new Thickness(2);
            // The extra RowDefinition is for rack label
            for (int i = 0; i < this.GetEntity<Rack>().RackType.Capacity + 1; i++)
            {
                grd.RowDefinitions.Add(new RowDefinition());
            }
            grd.RowDefinitions[0].Height = GridLength.Auto;
            Label RackLabel = new Label()
            {
                FlowDirection = FlowDirection.LeftToRight,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                Opacity = .8,
                Padding = new Thickness(5),
                Margin = new Thickness(2)
            };
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 40, 143, 143));
            grd.Children.Add(RackLabel.Border(new Thickness(1), new CornerRadius(4), brush, new SolidColorBrush(Color.FromArgb(150, brush.Color.R, brush.Color.G, brush.Color.B))));
            Grid.SetRow(RackLabel, 0);
            BindingOperations.SetBinding(RackLabel, Label.ContentProperty, new Binding("Name"));
            this.ChildrenHolder = grd;
            BorderThickness = new Thickness(NormalBorderThickness);
            base.Width = NormalWidth;
            base.Height = NormalHeight;
        }

        private void InitializeThumbnailRack()
        {
            base.ChildrenHolder = new Grid();
            Label lbl = new Label()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Foreground = this.BorderBrush,
                //BitmapEffect = new EmbossBitmapEffect()
                //{
                //    Relief = 1,
                //    LightAngle = 45
                //}
            };
            base.ChildrenHolder.Children.Add(lbl);
            BindingOperations.SetBinding(lbl, Label.ContentProperty, new Binding("Name"));
            base.Width = ((Rack.RackType.IsDouble ?? false) ? (RackUnit.ThumbWidth * 2 + CenterUnit.MinDistance) : RackUnit.ThumbWidth);
            base.Height = ThumbHeight;
            BorderThickness = new Thickness(1);
            ToolTip = null;
        }

        private void BindXY()
        {
            BindingOperations.SetBinding(this, Canvas.LeftProperty, new Binding("X")
            {
                Mode = BindingMode.TwoWay
            });
            BindingOperations.SetBinding(this, Canvas.TopProperty, new Binding("Y")
            {
                Mode = BindingMode.TwoWay
            });
        }

        protected override Panel ChildrenHolder
        {
            set
            {
                if (!(this.Child is Border))
                {
                    this.Child = new Border();
                }
                (this.Child as Border).Child = value;
            }
            get
            {
                return (this.Child as Border).Child as Panel;
            }
        }

        protected override void BindToolTip()
        {
            MultiBinding binding = new MultiBinding();
            binding.Bindings.Add(new Binding("Name"));
            binding.Bindings.Add(new Binding("RackType.Name"));
            binding.Bindings.Add(new Binding("RackType.Capacity"));
            binding.Converter = Converters.StringFormatConverter.Instance;
            binding.ConverterParameter = "Name: {0}\nModel: {1}\nCapacity: {2}";
            BindingOperations.SetBinding(this, ToolTipProperty, binding);
        }

        protected override void Edit()
        {
            if (IsThumbnail)
            {
                GotoRackView();
            }
            else
            {
                base.Edit();
            }
        }

        public Rack Rack
        {
            get
            {
                return DataContext as Rack;
            }
        }

        public bool IsThumbnail
        {
            get;
            set;
        }
        public RackUnit(TreeViewItem mappedNode)
            : this(mappedNode, false)
        {
        }

        public ShelfUnit LoadShelf(ShelfUnit shelfUnit)
        {
            this.Children.Add(shelfUnit);
            Grid.SetRow(shelfUnit, GetEntity<Rack>().RackType.Capacity.Value - (shelfUnit.GetEntity<Shelf>().Position ?? 0) + 1);
            shelfUnit.Height = ((NormalHeight - TopMargin - ButtomMargin - ChildrenHolder.Margin.GetHeight()) / GetEntity<Rack>().RackType.Capacity ?? 1) - shelfUnit.Margin.GetHeight() - 10;
            return shelfUnit;

        }
        public void GotoRackView()
        {
            this.MappedNode.EnsureVisible();
            this.MappedNode.IsSelected = true;
        }

    }
}
