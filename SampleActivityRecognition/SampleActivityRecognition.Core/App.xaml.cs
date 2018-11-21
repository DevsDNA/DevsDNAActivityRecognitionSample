[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace SampleActivityRecognition
{
    using SampleActivityRecognition.Views.Menu;
    using Xamarin.Forms;

    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            Current.MainPage = new MasterDetailView();
        }
    }
}
