namespace SampleActivityRecognition.Views.Menu
{
    using SampleActivityRecognition.ViewModels;

    /// <summary>
    /// View of menu.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class MasterView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterView" /> class.
        /// </summary>
        public MasterView()
        {
            InitializeComponent();
            BindingContext = new MasterViewModel();
        }
    }
}