using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMN.Interfaces;

namespace TMN.UI.RoutingWizard
{
    /// <summary>
    /// Interaction logic for RoutingWizardWindow.xaml
    /// </summary>
    public partial class RoutingWizardWindow : Window
    {
        private List<Link> links = new List<Link>();
        private LinkedList<IValidator> steps = new LinkedList<IValidator>();
        RoutesStep routesStep = new RoutesStep();
        ChannelStep channelStep = new ChannelStep();

        public RoutingWizardWindow(List<Link> initLinks)
        {
            InitializeComponent();
            links.AddRange(initLinks);
            InitSteps();
        }

        private void InitSteps()
        {
            steps.AddFirst(new LinksStep(links));
            steps.AddLast(channelStep);
            steps.AddLast(routesStep);
            // Remember: Cannot be merged with the first line
            CurrentStep = steps.First;
        }

        private void Next()
        {
            if (CurrentStep.Value.Validate())
                CurrentStep = CurrentStep.Next;
        }

        private void Back()
        {
            CurrentStep = CurrentStep.Previous;
        }

        private void Finish()
        {
            AddLinksToRoute();
            DialogResult = true;
        }

        private void AddLinksToRoute()
        {
            throw new NotImplementedException();
            // ToDo:AddLinksToRoute
            //var db = DB.Instance;
            //int i = 0;
            //foreach (Link link in links)
            //{
            //    db.Channels.DeleteAllOnSubmit(db.Channels.Where(p => p.Link == link));
            //    db.Channels.InsertOnSubmit(new Channel()
            //    {
            //        LinkID = link.ID,
            //        RouteID = routesStep.SelectedRoute.ID,
            //        ToTimeSlot = 31,
            //        FromTimeSlot = 0,
            //        ID = Guid.NewGuid(),
            //        ChannelOffset = (int)channelStep.ChannelUpDown.Value + i * 32,
            //    });
            //}
            //db.SubmitChanges();
        }

        private LinkedListNode<IValidator> _CurrentStep;
        private LinkedListNode<IValidator> CurrentStep
        {
            get
            {
                return _CurrentStep;
            }
            set
            {
                if (value == null)
                {
                    // We ran out of steps, the wizard ends here.
                    Finish();
                }
                else
                {
                    _CurrentStep = value;
                    backButton.IsEnabled = value != steps.First;
                    SetupNextbutton();
                    stepViewer.Child = (UserControl)CurrentStep.Value;
                }
            }
        }

        private void SetupNextbutton()
        {
            if (CurrentStep == steps.Last)
            {
                nextButton.Text = "پايان";
                nextButton.ImageSource = Assets.ImageSourceExtension.GetImageSource("check.png");
            }
            else
            {
                nextButton.ImageSource = Assets.ImageSourceExtension.GetImageSource("right.png");
                nextButton.Text = "ادامه";
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            Next();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
