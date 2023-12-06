using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TMN.Diagram;
using TMN.Converters;

namespace TMN.UI.Windows
{

    public partial class CenterLinkWindow : Window
    {
        # region Private Variables

        private const int minDistance = 5;
        private Point startPosition;
        private CenterItem selectedCenterItem;
        private CenterItem prevCenterItem;
        private TMNModelDataContext db;
        private Line currentLine;
        private bool isDrawingLine;
        private bool isMovingLine;
        private int itemOffset = 0;
        private Point prevScrollPosition;

        #endregion

        #region Constructor

        public CenterLinkWindow()
        {
            InitializeComponent();
            Root.MouseMove += new System.Windows.Input.MouseEventHandler(Root_MouseMove);
            Root.PreviewMouseUp += new MouseButtonEventHandler(Root_PreviewMouseUp);
            LoadLinkTypes();
            RefreshDB();
            RefreshDiagram();
            RefreshTree();
            Icon = MainWindow.Instance.Icon;
        }

        #endregion

        #region Properties

        private Point ItemPosition
        {
            get
            {
                Point currentScrollPosition = new Point(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset);
                if (currentScrollPosition != prevScrollPosition)
                {
                    itemOffset = 0;
                    prevScrollPosition = currentScrollPosition;
                }
                if (itemOffset == 100)
                {
                    itemOffset = 0;
                }
                else
                {
                    itemOffset += 2;
                }
                return currentScrollPosition + new Vector(itemOffset, itemOffset);
            }
        }

        /// <summary>
        /// Gets whether we are in line mode
        /// </summary>
        private bool IsInLineMode
        {
            get
            {
                return btnLine.IsChecked ?? false;
            }
        }

        #endregion

        #region Refresh Methods

        private void RefreshDiagram()
        {
            CenterItem centerItem;
            Root.Children.Clear();
            foreach (CenterLink centerLink in db.CenterLinks.Where(cl =>
                    cmbCenterType.SelectedValue == null
                    || (CenterTypes)cmbCenterType.SelectedValue == CenterTypes.Null
                    || (((CenterTypes?)cl.Center.CenterType == (CenterTypes?)cmbCenterType.SelectedValue || cl.Center == Center.Current)
                        && ((CenterTypes?)cl.Center1.CenterType == (CenterTypes?)cmbCenterType.SelectedValue || cl.Center1 == Center.Current))))
            {
                Line line = CreateLinkLine(centerLink);
                if ((centerItem = FindCenterItem(centerLink.Center)) == null)
                {
                    centerItem = AddCenterItem(new CenterItem(centerLink.Center, db));
                }
                centerItem.StartLinking(line, CenterItem.LinkingMode.Loading);

                if ((centerItem = FindCenterItem(centerLink.Center1)) == null)
                {
                    centerItem = AddCenterItem(new CenterItem(centerLink.Center1, db));
                }
                if (!centerItem.TerminateLinking(line, CenterItem.LinkingMode.Loading))
                    centerItem.CancelLinking(line);
            }
            foreach (Center c in FilteredCenters().Where(c => c.X.HasValue && c.Y.HasValue))
            {
                AddCenterItem(new CenterItem(c, db));
            }
        }

        private void RefreshDB()
        {
            if (Tree != null)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db = DB.Instance;
            }
        }

        private void RefreshTree()
        {
            Tree.Items.Clear();
            foreach (Center c in FilteredCenters())
            {
                if (FindCenterItem(c) == null)
                {
                    Tree.Items.Add(c);
                }
            }
        }

        #endregion

        private void LoadLinkTypes()
        {
            foreach (var item in Enum.GetValues(typeof(LinkTypes)))
            {
                CheckBox chk = new CheckBox()
                {
                    Content = item,
                    Foreground = (Brush)Converters.LinkTypesColorConverter.Instance.Convert(item, null, null, null)
                };
                chk.Checked += new RoutedEventHandler(chk_Checked);
                LinkTypesListBox.Items.Add(chk);
            }
        }

        void chk_Checked(object sender, RoutedEventArgs e)
        {
            btnLine.IsChecked = true;
        }

        private List<Center> FilteredCenters()
        {
            return db.Centers.Where(c =>
                    (cmbCenterType.SelectedValue == null
                    || (CenterTypes)cmbCenterType.SelectedValue == CenterTypes.Null
                    || ((CenterTypes?)c.CenterType == (CenterTypes?)cmbCenterType.SelectedValue)
                    || (c == Center.Current))).ToList();
        }

        private CenterItem AddCenterItem(CenterItem ci)
        {
            if (!Root.Children.Contains(ci))
            {
                ci.MouseDown += new MouseButtonEventHandler(CenterItem_MouseDown);
                ci.Removed += (s, ee) =>
                {
                    db.SubmitChanges();
                    RefreshDiagram();
                    RefreshTree();
                };
                Root.Children.Add(ci);
                if (ci.Center.X == null && ci.Center.Y == null)
                {
                    Canvas.SetTop(ci, ItemPosition.Y);
                    Canvas.SetLeft(ci, ItemPosition.X);
                }
            }
            return ci;
        }

        private Line CreateLinkLine(CenterLink dataContext)
        {
            Line l = new Line()
                {
                    Stroke = LinkTypesColorConverter.Instance.Convert((CenterTypes)dataContext.LinkType.Value, null, null, null) as Brush,
                    DataContext = dataContext,
                    StrokeThickness = 6
                };
            BindToolTip(l);
            l.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed && CanMoveLine(l))
                {
                    StartMovingLine(l, e.GetPosition(l));
                }
            };
            return l;
        }

        private void BindToolTip(DependencyObject objectToBind)
        {
            MultiBinding binding = new MultiBinding()
            {
                Converter = StringFormatConverter.Instance,
                ConverterParameter = "ظرفيت:" + " {0}" + "\n" + "نوع ارتباط:" + " {1}"
            };
            binding.Bindings.Add(new Binding("E1Count"));
            binding.Bindings.Add(new Binding("LinkType")
            {
                Converter = LinkTypesConverter.Instance
            });
            BindingOperations.SetBinding(objectToBind, Line.ToolTipProperty, binding);
        }

        private void StartMovingLine(Line l, Point startPoint)
        {
            isMovingLine = true;
            currentLine = l;
            CenterItem ci1 = prevCenterItem = FindNearestCenterItemFromLine(l, startPoint);
            selectedCenterItem = FindOtherCenterItemOfLine(l, ci1);
            ci1.StartMovingLink(l);
        }


        #region Find Methods

        /// <summary>
        /// Finds the CenterItem whose Center is given.
        /// </summary>
        /// <param x:Name="center">The Center to find its associated CenterItem</param>
        /// <returns>The associated CenterItem with the given center</returns>
        private CenterItem FindCenterItem(Center center)
        {
            foreach (var item in Root.Children)
            {
                if (item is CenterItem && (item as CenterItem).Center.ID == center.ID)
                {
                    return item as CenterItem;
                }
            }
            return null;
        }

        private CenterItem FindOtherCenterItemOfLine(Line l, CenterItem thisCenterItem)
        {
            if ((l.DataContext as CenterLink).Center == thisCenterItem.Center)
            {
                return FindCenterItem((l.DataContext as CenterLink).Center1);
            }
            else
            {
                return FindCenterItem((l.DataContext as CenterLink).Center);
            }
        }

        /// <summary>
        /// Gets the nearest CenterItem to the specified Point on the given Line
        /// </summary>
        private CenterItem FindNearestCenterItemFromLine(Line line, Point p)
        {
            if (Math.Abs(p.Y - line.Y1) < Math.Abs(p.Y - line.Y2) || Math.Abs(p.X - line.X1) < Math.Abs(p.X - line.X2))
            {

                return FindCenterItem((line.DataContext as CenterLink).Center);
            }
            else
            {
                return FindCenterItem((line.DataContext as CenterLink).Center1);
            }
        }

        #endregion

        #region Mouse Event Handlers

        void CenterItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (new DetailsHolderWindow((sender as CenterItem).Center).ShowDialog(Root) ?? false)
                {
                    RefreshDB();
                    RefreshDiagram();
                }
            }
            else
            {
                startPosition = e.GetPosition(sender as IInputElement);
                selectedCenterItem = sender as CenterItem;
                if (IsInLineMode)
                {
                    isDrawingLine = true;
                    currentLine = CreateLinkLine(
                     new CenterLink()
                        {
                            Center = selectedCenterItem.Center,
                            E1Count = (int?)E1CountNumericUpdown.Value,
                            LinkType = CollectLinkTypes()
                        });
                    selectedCenterItem.StartLinking(currentLine, CenterItem.LinkingMode.Creating);
                }
            }
        }

        private int CollectLinkTypes()
        {
            int result = 0;
            foreach (var item in LinkTypesListBox.Items.Cast<CheckBox>())
            {
                if (item.IsChecked == true)
                {
                    result |= (int)item.Content;
                }
            }
            return result;
        }

        void Root_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && selectedCenterItem != null)
            {
                if (isDrawingLine || isMovingLine)
                {
                    // Decreasing one pixel is for preventing the line from dropping under the cursor and resulting in passing the line instead of the CenterItem to the MouseUp event
                    currentLine.X2 = e.GetPosition(Root).X - 1;
                    currentLine.Y2 = e.GetPosition(Root).Y - 1;
                }
                else
                {
                    double w = 1;
                    double h = 1;
                    double left = minDistance + Math.Round((e.GetPosition(Root).X - startPosition.X) / w) * w;
                    double top = minDistance + Math.Round((e.GetPosition(Root).Y - startPosition.Y) / h) * h;
                    if (left >= minDistance & left <= Root.ActualWidth - minDistance - selectedCenterItem.Width)
                    {
                        Canvas.SetLeft(selectedCenterItem, left);
                    }
                    if (top >= minDistance & top <= Root.ActualHeight - minDistance - selectedCenterItem.Height)
                    {
                        Canvas.SetTop(selectedCenterItem, top);
                    }
                }
            }
        }

        private bool CanMoveLine(Line line)
        {
            // Currently there is no relation between centerlink and other parts of system, so there is no restriction for deleting.
            return true;

            //if (Route.ExistsOn(line.DataContext.As<CenterLink>().Center, line.DataContext.As<CenterLink>().Center1))
            //{
            //    MessageBox.ShowError("اين ارتباط قابل تغيير نيست.");
            //    return false;
            //}
            //return true;
        }

        private bool CanDeleteCurrentCenterLink()
        {
            // Currently there is no relation between centerlink and other parts of system, so there is no restriction for deleting.
            return true;

            //if (!Route.ExistsOn(selectedCenterItem.Center, prevCenterItem.Center))
            //    return true;
            //MessageBox.Show(MessageTypes.CannotDelete);
            //return false;
        }

        void Root_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            if (isDrawingLine || isMovingLine)
            {
                if (e.Source is Canvas || e.Source is Line) // Canceling: Line released in a free space on canvas or on another line
                {
                    if (!isMovingLine || (MessageBox.Show(MessageTypes.ConfirmDelete) == MessageBoxResult.Yes && CanDeleteCurrentCenterLink()))
                    {
                        selectedCenterItem.CancelLinking(currentLine);
                    }
                    else
                    {
                        selectedCenterItem.CancelLinking(currentLine, prevCenterItem);
                    }
                }
                else // Finishing: Line released on a CenterItem or its child elements
                {
                    CenterItem targetCenterItem = (e.Source as FrameworkElement).GetParent<CenterItem>();
                    if (targetCenterItem == selectedCenterItem)
                    {
                        selectedCenterItem.CancelLinking(currentLine);
                    }
                    else
                    {
                        if (!targetCenterItem.TerminateLinking(currentLine, CenterItem.LinkingMode.Creating))
                            selectedCenterItem.CancelLinking(currentLine, prevCenterItem);
                    }
                }
                isDrawingLine = false;
                isMovingLine = false;
            }
            db.SubmitChanges();
            selectedCenterItem = null;
            prevCenterItem = null;
        }

        private void TreeViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                AddCenterItem(new CenterItem(Tree.SelectedItem as Center, db));
                btnLine.IsChecked = false;
                RefreshTree();
            }
        }
        #endregion

        #region UI Event Handlers

        private void btnLine_Checked(object sender, RoutedEventArgs e)
        {
            if (LinkTypesListBox.Items.Cast<CheckBox>().All(c => c.IsChecked == false))
            {
                btnLine.IsChecked = false;
                MessageBox.ShowWarning("حداقل يکی از انواع لينک انتخاب شود.", MessageBoxButton.OK);
                LinkTypesListBox.Focus();
            }
            else
                Root.Cursor = Cursors.Pen;
        }

        private void btnLine_Unchecked(object sender, RoutedEventArgs e)
        {
            Root.Cursor = Cursors.Arrow;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnLine.IsChecked = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDB();
            RefreshDiagram();
            RefreshTree();
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value += (ZoomSlider.Maximum - ZoomSlider.Minimum) / 5;
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value -= (ZoomSlider.Maximum - ZoomSlider.Minimum) / 5;
        }
        private void cmbCenterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDiagram();
            RefreshTree();
        }

        #endregion

    }
}
