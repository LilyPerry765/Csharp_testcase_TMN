using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using TMN.UI.Windows;

namespace TMN.Units
{
    class CenterUnit : Unit
    {
        public const int MinDistance = 5;
        private Point startPosition;
        private FrameworkElement selectedElement;
        private int verticalRackCapacity;
        private int horizontalRackCapacity;

        public CenterUnit(TreeViewItem mappedNode)
            : base(mappedNode)
        {
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            BindingOperations.SetBinding(this, Unit.WidthProperty, new Binding("Width")
            {
                Mode = BindingMode.TwoWay
            });
            BindingOperations.SetBinding(this, Unit.HeightProperty, new Binding("Height")
            {
                Mode = BindingMode.TwoWay
            });
            if (double.IsNaN(Height))
            {
                VerticalRackCapacity = 4;
            }
            if (double.IsNaN(Width))
            {
                HorizontalRackCapacity = 4;
            }
            this.ChildrenHolder = new Canvas();
            this.MouseMove += new System.Windows.Input.MouseEventHandler(Child_MouseMove);
            this.MouseUp += new MouseButtonEventHandler(Child_MouseUp);
            CornerRadius = new CornerRadius(5);
            Margin = new Thickness(5);
            MinWidth = RackUnit.ThumbWidth + MinDistance;
            MinHeight = RackUnit.ThumbHeight + MinDistance;
            ResizeGrip grip = new ResizeGrip();
            grip.Cursor = Cursors.SizeNESW;
            grip.MouseDown += new MouseButtonEventHandler(grip_MouseDown);
            Canvas.SetBottom(grip, 0);
            Canvas.SetRight(grip, 0);
            this.Children.Add(grip);
        }


        protected override void BindToolTip()
        {
            MultiBinding binding = new MultiBinding();
            binding.Bindings.Add(new Binding("Name"));
            binding.Bindings.Add(new Binding("SwitchType.Name"));
            binding.Bindings.Add(new Binding("PointCode"));
            binding.Bindings.Add(new Binding("Code"));
            binding.Converter = Converters.StringFormatConverter.Instance;
            binding.ConverterParameter = "Name: {0}\nSwitch: {1}\nPoint Code: {2}\nCode:{3}";
            BindingOperations.SetBinding(this, ToolTipProperty, binding);
        }

        void grip_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StartResizeing();
            }
        }

        private void StartResizeing()
        {
            IsResizing = true;
            (this.Parent as FrameworkElement).Cursor = Cursors.SizeNESW;
            MinWidth = GetMaxRackX() + MinDistance * 2;
            MinHeight = GetMaxRackY() + MinDistance * 2 + RackUnit.ThumbHeight;
        }

        private double GetMaxRackX()
        {
            double max = 0;
            foreach (UIElement ru in this.Children)
            {
                if (ru is RackUnit)
                {
                    double newLeft = Canvas.GetLeft(ru) + (((ru as RackUnit).Rack.RackType.IsDouble ?? false) ? 2 * RackUnit.ThumbWidth + MinDistance : RackUnit.ThumbWidth);
                    if (newLeft > max)
                    {
                        max = newLeft;
                    }
                }
            }
            return max;
        }

        private double GetMaxRackY()
        {
            double max = 0;
            foreach (UIElement ru in this.Children)
            {
                if (ru is RackUnit && Canvas.GetTop(ru) > max)
                {
                    max = Canvas.GetTop(ru);
                }
            }
            return max;
        }

        public int VerticalRackCapacity
        {
            set
            {
                verticalRackCapacity = value;
                base.Height = (RackUnit.ThumbHeight + MinDistance) * value + MinDistance;
            }
            get
            {
                return verticalRackCapacity;
            }
        }

        public int HorizontalRackCapacity
        {
            set
            {
                horizontalRackCapacity = value;
                base.Width = (RackUnit.ThumbWidth + MinDistance) * value + MinDistance;
            }
            get
            {
                return horizontalRackCapacity;
            }
        }

        public bool IsResizing
        {
            get;
            set;
        }

        public RackUnit AddRack(RackUnit rack)
        {
            this.Children.Add(rack);
            if (rack.IsThumbnail)
            {
                rack.MouseDown += (sender, e) =>
                {
                    startPosition = e.GetPosition(sender as IInputElement);
                    selectedElement = sender as FrameworkElement;
                    if (e.ClickCount == 2)
                    {
                        rack.GotoRackView();
                    }
                };
            }
            return rack;
        }

        void Child_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (selectedElement != null)
                {
                    double w = RackUnit.ThumbWidth + MinDistance;
                    double h = RackUnit.ThumbHeight + MinDistance;
                    double left = MinDistance + Math.Round((e.GetPosition(this).X - startPosition.X) / w) * w;
                    double top = MinDistance + Math.Round((e.GetPosition(this).Y - startPosition.Y) / h) * h;
                    if (left >= MinDistance & left <= Width - MinDistance - (RackUnit.ThumbWidth * (((selectedElement as RackUnit).Rack.RackType.IsDouble ?? false) ? 2 : 1)))
                    {
                        Canvas.SetLeft(selectedElement, left);
                    }
                    if (top >= MinDistance & top <= Height - MinDistance - RackUnit.ThumbHeight)
                    {
                        Canvas.SetTop(selectedElement, top);
                    }
                }
            }
            selectedElement = null;
            // SubmitChanges will be called on mouseUp of parent container
        }

        // Moving Racks
        void Child_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (selectedElement != null)
                {
                    double left = MinDistance + e.GetPosition(this).X - startPosition.X;
                    double top = MinDistance + e.GetPosition(this).Y - startPosition.Y;
                    if (left >= MinDistance & left <= Width - MinDistance - (RackUnit.ThumbWidth * (((selectedElement as RackUnit).Rack.RackType.IsDouble ?? false) ? 2 : 1)))
                    {
                        Canvas.SetLeft(selectedElement, left);
                    }
                    if (top >= MinDistance & top <= Height - MinDistance - RackUnit.ThumbHeight)
                    {
                        Canvas.SetTop(selectedElement, top);
                    }
                }
            }
        }
    }
}
