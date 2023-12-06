using System.Windows;
using System.Windows.Controls;
using TMN.Interfaces;

namespace TMN.UI.Windows
{
    /// <summary>
    /// Interaction logic for Shelf.xaml
    /// </summary>
    public partial class DetailsHolderWindow : Window
    {
        private IDetailsView view;

        #region Constructors

        private DetailsHolderWindow()
        {
            InitializeComponent();
            Icon = MainWindow.Instance.Icon;
        }

        /// <summary>
        /// Initializes the window for EDITING an existing entity.
        /// </summary>
        /// <param Name="entityToEdit">The entity to be edited</param>
        public DetailsHolderWindow(Entity entityToEdit)
            : this()
        {
            if (entityToEdit is IChild)
            {
                AttachView(entityToEdit.EntityType, (entityToEdit as IChild).Parent);
            }
            else if (entityToEdit.Tag != null)
            {
                AttachView(entityToEdit.EntityType, entityToEdit.Tag);
            }
            else
            {
                AttachView(entityToEdit.EntityType);
            }
            view.BeginEdit(entityToEdit);
        }


        /// <summary>
        /// Initializes the window for creating a NEW entity of the given type which can logically be a child of another entity.
        /// </summary>
        /// <param Name="arg">Any additional data for example the object to which the new entity must belong</param>
        /// <param Name="type">The type of the entity being created</param>
        public DetailsHolderWindow(EntityTypes type, object arg)
            : this()
        {
            AttachView(type, arg);
            view.BeginInsert();
        }

        public DetailsHolderWindow(ViewInfo viewInfo)
            : this()
        {
            AttachView(viewInfo);
            view.BeginInsert();
        }

        /// <summary>
        /// Initializes the window for creating a NEW entity based on another entity.
        /// </summary>
        /// <param Name="arg">Any additional data for example the object to which the new entity must belong</param>
        /// <param Name="sourceEntity">The entity which the new object is being created based on.</param>
        public DetailsHolderWindow(Entity sourceEntity, object arg)
            : this()
        {
            AttachView(sourceEntity.EntityType, arg);
            if (view is IInsertableBasedOn)
                view.As<IInsertableBasedOn>().BeginInsert(sourceEntity);
        }

        #endregion

        private void AttachView(EntityTypes type)
        {
            AttachView(type, null);
        }

        private void AttachView(EntityTypes type, object arg)
        {
            AttachView(ViewInfo.GetByEntityType(ViewType.Detail, type, arg));
        }

        private void AttachView(ViewInfo viewInfo)
        {
            view = viewInfo.DetailsView;
            Title = viewInfo.Title;
            (view as FrameworkElement).Margin = new Thickness(20);
            Root.Child = (view as FrameworkElement).Extract();
        }

        public Entity Entity
        {
            get;
            private set;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (view.Validate())
            {
                if ((Entity = view.SaveData()) != null)
                {
                    DialogResult = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = (view as ContentControl).ActualHeight + ControlPanel.Height + 80;
            this.Width = (view as ContentControl).ActualWidth + 70;
        }

    }
}
