using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Media;
using TMN.Converters;
using System.Windows.Input;

namespace TMN.Diagram
{
    public partial class CircuitItem : Border
    {
        public event EventHandler Removed;
        private bool isThumbnail = true;


        //public int X;
        //public int Y;
        //public CircuitInMap CircuitInMap;

        public CircuitItem(CircuitInMap cim, TMNModelDataContext db) //todo:must change datacontext
        {
            isThumbnail = false;
            DataBase = db;
            //this.Style = FindResource("CircuitItem") as Style;
            
            CircuitInMap = cim;
            DataContext = cim;
            CreateMyFace();
            if (cim == null)
            {
                throw new ArgumentNullException("circuit");
            }
            BindingOperations.SetBinding(this, Canvas.LeftProperty, new Binding("X")
            {
                Mode = BindingMode.TwoWay
            });
            BindingOperations.SetBinding(this, Canvas.TopProperty, new Binding("Y")
            {
                Mode = BindingMode.TwoWay
            });
        }

        public TMNModelDataContext DataBase
        {
            get;
            set;
        }

        private void Remove()
        {
            this.Visibility = Visibility.Hidden;
            foreach (CircuitLink circuitLink in this.CircuitInMap.CircuitLinks)
            {
                DataBase.CircuitLinks.DeleteOnSubmit(circuitLink);
            }
            foreach (CircuitLink circuitLink in this.CircuitInMap.CircuitLinks1)
            {
                DataBase.CircuitLinks.DeleteOnSubmit(circuitLink);
            }
            DataBase.CircuitInMaps.DeleteOnSubmit(CircuitInMap);
            if (Removed != null)
            {
                Removed(this, EventArgs.Empty);
            }
        }

        public CircuitInMap CircuitInMap
        {
            get
            {
                return DataContext as CircuitInMap;
            }
            set
            {
                DataContext = value;
            }
        }


        #region Linking

        public enum LinkingMode
        {
            Creating = 1,
            Loading
        }

        public void StartLinking(Line line, LinkingMode mode)
        {
            if (mode == LinkingMode.Creating)
            {
                this.CircuitInMap.CircuitLinks.Add(line.DataContext as CircuitLink);
                line.X1 = line.X2 = Canvas.GetLeft(this) + (this.Width / 2);
                line.Y1 = line.Y2 = Canvas.GetTop(this) + (this.Height / 2);
            }
            BindingOperations.SetBinding(line, Line.X1Property, new Binding("CircuitInMap.X")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Width / 2
            });
            BindingOperations.SetBinding(line, Line.Y1Property, new Binding("CircuitInMap.Y")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Height / 2
            });
            (this.Parent as Panel).Children.Add(line);
            Canvas.SetZIndex(line, -100);
        }

        public void StartMovingLink(Line line)
        {
            double X, Y;
            // A CircuitLink's primary key consists of CenterA and CenterB; 
            // so, nither of its circuits cannot be changed and the only way of changing a CircuitLink's circuits 
            // is recreating that CircuitLink:
            CircuitLink oldLink = line.DataContext as CircuitLink;
            CircuitInMap otherCircuitInMap = null;
            CircuitLink newLink = new CircuitLink()
             {
                 //E1Count = oldLink.E1Count,
                 //LinkType = oldLink.LinkType
                 ID = Guid.NewGuid(),
                 Color = oldLink.Color,
                  Title = oldLink.Sensor.Title,
                  CircuitID = oldLink.CircuitID,
                  CenterID = oldLink.CenterID
                  
             };
            if (this.CircuitInMap == oldLink.CircuitInMap)
            { // I am CenterA(Center) on X1,Y1
                otherCircuitInMap = oldLink.CircuitInMap1;
                X = line.X1;
                Y = line.Y1;
            }
            else
            { // I am CenterB(Center1) on X2,Y2
                otherCircuitInMap = oldLink.CircuitInMap;
                X = line.X2;
                Y = line.Y2;
            }
            DataBase.CircuitLinks.DeleteOnSubmit(oldLink);
            DataBase.SubmitChanges();
            newLink.CircuitInMap = otherCircuitInMap;
            line.DataContext = newLink;
            line.X2 = X;
            line.Y2 = Y;

        }

        public bool TerminateLinking(Line line, LinkingMode mode)
        {
            if (mode == LinkingMode.Creating)
            {
                CircuitLink cLink = line.DataContext as CircuitLink;
                cLink.CircuitInMap1 = this.CircuitInMap;
                if (cLink.Exists(DataBase))
                {
                    // CancelLinking(line) occures on the calling methods of this function.
                    MessageBox.Show("مسير بين اين دو مركز قبلا تعريف شده است.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            BindingOperations.SetBinding(line, Line.X2Property, new Binding("CircuitInMap1.X")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Width / 2
            });
            BindingOperations.SetBinding(line, Line.Y2Property, new Binding("CircuitInMap1.Y")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Height / 2
            });
            return true;
        }

        public void CancelLinking(Line line)
        {
            CircuitLink cLink = line.DataContext as CircuitLink;
            this.CircuitInMap.CircuitLinks.Remove(cLink);
            this.CircuitInMap.CircuitLinks1.Remove(cLink);
            (Parent as Panel).Children.Remove(line);
        }

        public void CancelLinking(Line line, CircuitItem prevCircuitItem)
        {
            if (!prevCircuitItem.TerminateLinking(line, LinkingMode.Creating))
                CancelLinking(line);
        }

        #endregion

        private void CreateMyFace()
        {
            TMN.UserControls.JellyIcon btn = new UserControls.JellyIcon();
            this.Width = 20;
            this.Height = 20;
            
            btn.DarkColor = Colors.DarkGreen;
            btn.LightColor = Colors.LightGreen;
            
            //btn.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(btn_MouseLeftButtonUp);
            ContextMenu cm = new ContextMenu();
            MenuItem deleteitem = new MenuItem();
            deleteitem.CommandParameter = "delete";
            deleteitem.Header = "حذف";
            deleteitem.Click += new RoutedEventHandler(deleteitem_Click);
            cm.Items.Add(deleteitem);

            //btn.ContextMenu = cm; // FindResource("CircuitMenu") as ContextMenu;
            //btn.ContextMenu.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(ContextMenu_MouseLeftButtonUp);
            //btn.IsEnabled = false;
            this.Child = btn;
            //Canvas.SetTop(btn, CircuitInMap.Y);
            //Canvas.SetLeft(btn, CircuitInMap.X);
            //StackPanel stk = new StackPanel();
            //this.Child = stk;
            //stk.Opacity = .5;
            //// DropshadowEffect causes problem in win7
            ////stk.Effect = new DropShadowEffect()
            ////{
            ////    ShadowDepth = 1
            ////};
            //TextBlock nameTextBlock = new TextBlock()
            //{
            //    HorizontalAlignment = HorizontalAlignment.Center,
            //};
            //TextBlock lblType = new TextBlock()
            //{
            //    HorizontalAlignment = HorizontalAlignment.Center
            //};
            //if (!isThumbnail)
            //{
            //    stk.Children.Add(CreateButton());
            //}
            //if (isThumbnail)
            //{
            //    stk.Children.Add(nameTextBlock);
            //    BindingOperations.SetBinding(nameTextBlock, TextBlock.TextProperty, new Binding("Name"));
            //}
            //else
            //{
            //    stk.Children.Add(new TextBlock()
            //    {
            //        Text = string.Format("{0}\r\n{1}", Center.Name, CenterTypesConverter.Instance.Convert(Center.CenterType, null, null, null)),
            //        TextAlignment = TextAlignment.Center,
            //    });
            //    ToolTip = new TextBlock()
            //    {
            //        Text = string.Format("{0}\nType: {1}\nSwitch: {2}\nPoint Code: {3}\nCode: {4}"
            //                            , Center.Name
            //                            , CenterTypesConverter.Instance.Convert(Center.CenterType, null, null, null)
            //                            , Center.SwitchType.Name
            //                            , Center.PointCode
            //                            , Center.Code
            //                ),
            //        TextAlignment = TextAlignment.Right
            //    };
            //}
        }

        void deleteitem_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Remove();
        }




        //void btn_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    e.Handled = true;
        //    Remove();
        //}

        //private Button CreateButton()
        //{
        //    Button btn =
        //        new Button()
        //        {
        //            HorizontalAlignment = HorizontalAlignment.Left,
        //            VerticalAlignment = VerticalAlignment.Top,
        //            Opacity = .3,
        //            FontSize = 8,
        //            VerticalContentAlignment = VerticalAlignment.Top,
        //            Padding = new Thickness(0),
        //            Content = "X",
        //            Margin = new Thickness(2),
        //            ToolTip = "حذف",
        //            Focusable = false,
        //            Width = 14,
        //            Height = 14,
        //            Background = Brushes.Transparent
        //        };
        //    btn.Click += new RoutedEventHandler(btn_Click);
        //    return btn;
        //}



    }
}
