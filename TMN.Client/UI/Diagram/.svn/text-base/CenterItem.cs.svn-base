using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;
using System.Windows.Media;
using TMN.Converters;

namespace TMN.Diagram
{
    public partial class CenterItem : Border
    {
        public event EventHandler Removed;
        private bool isThumbnail = true;

        public CenterItem()
        {
            this.Style = FindResource("CenterItem") as Style;
            CreateMyFace();
        }

        public CenterItem(Center center, TMNModelDataContext db) //todo:must change datacontext
        {
            isThumbnail = false;
            DataBase = db;
            this.Style = FindResource("CenterItem") as Style;
            Center = center;
            CreateMyFace();
            if (center == null)
            {
                throw new ArgumentNullException("center");
            }
            DataContext = center;
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
            foreach (CenterLink centerLink in this.Center.CenterLinks)
            {
                DataBase.CenterLinks.DeleteOnSubmit(centerLink);
            }
            foreach (CenterLink centerLink in this.Center.CenterLinks1)
            {
                DataBase.CenterLinks.DeleteOnSubmit(centerLink);
            }
            this.Center.RemoveFromDiagram();
            if (Removed != null)
            {
                Removed(this, EventArgs.Empty);
            }
        }

        public Center Center
        {
            get
            {
                return DataContext as Center;
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
                this.Center.CenterLinks.Add(line.DataContext as CenterLink);
                line.X1 = line.X2 = Canvas.GetLeft(this) + (this.Width / 2);
                line.Y1 = line.Y2 = Canvas.GetTop(this) + (this.Height / 2);
            }
            BindingOperations.SetBinding(line, Line.X1Property, new Binding("Center.X")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Width / 2
            });
            BindingOperations.SetBinding(line, Line.Y1Property, new Binding("Center.Y")
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
            // A CenterLink's primary key consists of CenterA and CenterB; 
            // so, nither of its centers cannot be changed and the only way of changing a CenterLink's centers 
            // is recreating that CenterLink:
            CenterLink oldLink = line.DataContext as CenterLink;
            Center otherCenter = null;
            CenterLink newLink = new CenterLink()
             {
                 E1Count = oldLink.E1Count,
                 LinkType = oldLink.LinkType
             };
            if (this.Center == oldLink.Center)
            { // I am CenterA(Center) on X1,Y1
                otherCenter = oldLink.Center1;
                X = line.X1;
                Y = line.Y1;
            }
            else
            { // I am CenterB(Center1) on X2,Y2
                otherCenter = oldLink.Center;
                X = line.X2;
                Y = line.Y2;
            }
            DataBase.CenterLinks.DeleteOnSubmit(oldLink);
            DataBase.SubmitChanges();
            newLink.Center = otherCenter;
            line.DataContext = newLink;
            line.X2 = X;
            line.Y2 = Y;

        }

        public bool TerminateLinking(Line line, LinkingMode mode)
        {
            if (mode == LinkingMode.Creating)
            {
                CenterLink cLink = line.DataContext as CenterLink;
                cLink.Center1 = this.Center;
                if (cLink.Exists(DataBase))
                {
                 // CancelLinking(line) occures on the calling methods of this function.
                    MessageBox.Show("مسير بين اين دو مركز قبلا تعريف شده است.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            BindingOperations.SetBinding(line, Line.X2Property, new Binding("Center1.X")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Width / 2
            });
            BindingOperations.SetBinding(line, Line.Y2Property, new Binding("Center1.Y")
            {
                Converter = DoubleAddConverter.Instance,
                ConverterParameter = Height / 2
            });
            return true;
        }

        public void CancelLinking(Line line)
        {
            CenterLink cLink = line.DataContext as CenterLink;
            this.Center.CenterLinks.Remove(cLink);
            this.Center.CenterLinks1.Remove(cLink);
            (Parent as Panel).Children.Remove(line);
        }

        public void CancelLinking(Line line, CenterItem prevCenterItem)
        {
            if (!prevCenterItem.TerminateLinking(line, LinkingMode.Creating))
                CancelLinking(line);
        }

        #endregion

        private void CreateMyFace()
        {
            StackPanel stk = new StackPanel();
            this.Child = stk;
            stk.Opacity = .5;
            // DropshadowEffect causes problem in win7
            //stk.Effect = new DropShadowEffect()
            //{
            //    ShadowDepth = 1
            //};
            TextBlock nameTextBlock = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            TextBlock lblType = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center
            };
            if (!isThumbnail)
            {
                stk.Children.Add(CreateButton());
            }
            if (isThumbnail)
            {
                stk.Children.Add(nameTextBlock);
                BindingOperations.SetBinding(nameTextBlock, TextBlock.TextProperty, new Binding("Name"));
            }
            else
            {
                stk.Children.Add(new TextBlock()
                {
                    Text = string.Format("{0}\r\n{1}", Center.Name, CenterTypesConverter.Instance.Convert(Center.CenterType, null, null, null)),
                    TextAlignment = TextAlignment.Center,
                });
                ToolTip = new TextBlock()
                {
                    Text = string.Format("{0}\nType: {1}\nSwitch: {2}\nPoint Code: {3}\nCode: {4}"
                                        , Center.Name
                                        , CenterTypesConverter.Instance.Convert(Center.CenterType, null, null, null)
                                        , Center.SwitchType.Name
                                        , Center.PointCode
                                        , Center.Code
                            ),
                    TextAlignment = TextAlignment.Right
                };
            }
        }

        private Button CreateButton()
        {
            Button btn =
                new Button()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Opacity = .3,
                    FontSize = 8,
                    VerticalContentAlignment = VerticalAlignment.Top,
                    Padding = new Thickness(0),
                    Content = "X",
                    Margin = new Thickness(2),
                    ToolTip = "حذف",
                    Focusable = false,
                    Width = 14,
                    Height = 14,
                    Background = Brushes.Transparent
                };
            btn.Click += new RoutedEventHandler(btn_Click);
            return btn;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Remove();
        }

    }
}
