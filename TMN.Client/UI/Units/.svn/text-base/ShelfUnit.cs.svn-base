using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace TMN.Units
{
    class ShelfUnit : Unit
    {
        public ShelfUnit(TreeViewItem mappedNode)
            : base(mappedNode)
        {

            Grid grd = new Grid();
            for (int i = 0; i < this.GetEntity<Shelf>().ShelfType.Capacity; i++)
            {
                grd.ColumnDefinitions.Add(new ColumnDefinition());
            }
            this.ChildrenHolder = grd;
            this.Margin = new System.Windows.Thickness(1);
            FlowDirection = FlowDirection.LeftToRight;
        }

        public CardUnit AddCard(CardUnit cardUnit, int soltNo)
        {
            cardUnit.SetValue(Grid.ColumnProperty, soltNo - 1);
            this.Children.Add(cardUnit);
            return cardUnit;
        }

        protected override void BindToolTip()
        {
            MultiBinding binding = new MultiBinding();
            binding.Bindings.Add(new Binding("Name"));
            binding.Bindings.Add(new Binding("ShelfType.Name"));
            binding.Bindings.Add(new Binding("ShelfType.Capacity"));
            binding.Converter = Converters.StringFormatConverter.Instance;
            binding.ConverterParameter = "Name: {0}\nModel: {1}\nCapacity: {2}";
            BindingOperations.SetBinding(this, ToolTipProperty, binding);
        }

    }
}
