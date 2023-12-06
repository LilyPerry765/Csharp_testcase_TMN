using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;
using System.Windows.Media;

namespace TMN.Units
{
    class CardUnit : Unit
    {
        public CardUnit(TreeViewItem mappedNode)
            : base(mappedNode)
        {
            CornerRadius = new System.Windows.CornerRadius(3);
            Margin = new Thickness(2);
            this.ChildrenHolder = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            
            TextBlock displayTextBlock = new TextBlock
            {
                Foreground = Brushes.LimeGreen,
                TextAlignment = TextAlignment.Center,
                //Effect = new DropShadowEffect
                //{
                //    ShadowDepth = 1
                //},
                //RenderTransform = new TransformGroup()
            };
            //(displayTextBlock.RenderTransform as TransformGroup).Children.Add(new RotateTransform(-270));
            BindingOperations.SetBinding(displayTextBlock, TextBlock.TextProperty, new Binding((this.DataContext as Entity).InferDisplayPath())
            {
                Converter = TMN.Converters.StringToLinesConverter.Instance
            });
            this.ChildrenHolder = new Grid();
            this.ChildrenHolder.Children.Add(displayTextBlock);
            
        }

        protected override void BindToolTip()
        {
            MultiBinding binding = new MultiBinding();
            binding.Bindings.Add(new Binding("Name"));
            binding.Bindings.Add(new Binding("CardType.Name"));
            binding.Bindings.Add(new Binding("SerialNo"));
            binding.Bindings.Add(new Binding("SlotNo"));
            binding.Converter = Converters.StringFormatConverter.Instance;
            binding.ConverterParameter = "Name: {0}\nModel: {1}\nSerial No: {2}\nSlot: {3}";
            BindingOperations.SetBinding(this, ToolTipProperty, binding);
        }

    }
}
