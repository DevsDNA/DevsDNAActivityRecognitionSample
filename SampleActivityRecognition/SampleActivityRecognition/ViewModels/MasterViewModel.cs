namespace SampleActivityRecognition.ViewModels
{
    using System.Windows.Input;
    using SampleActivityRecognition.Services.NavigationService;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using SampleActivityRecognition.Views;
    using Xamarin.Forms;

    /// <summary>
    /// Master view model.
    /// </summary>
    public class MasterViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IRecognitionActivityService recognitionActivityService;
        private ICommand runningAppCommand;
        private ICommand trackerAppCommand;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SampleActivityRecognition.ViewModels.MasterViewModel"/> class.
        /// </summary>
        public MasterViewModel()
        {
            this.navigationService = DependencyService.Get<INavigationService>();
            this.recognitionActivityService = DependencyService.Get<IRecognitionActivityService>();
            this.recognitionActivityService.StartService();

            this.runningAppCommand = new Command(() => this.navigationService.NavigateTo<RunningAppView>());
            this.trackerAppCommand = new Command(() => this.navigationService.NavigateTo<TrackerAppView>());
        }

        /// <summary>
        /// Gets the running app command.
        /// </summary>
        public ICommand RunningAppCommand => this.runningAppCommand;
        
        /// <summary>
        /// Gets the tracker app command.
        /// </summary>
        public ICommand TrackerAppCommand => this.trackerAppCommand;
    }
}
