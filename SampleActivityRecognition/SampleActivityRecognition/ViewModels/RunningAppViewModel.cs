namespace SampleActivityRecognition.ViewModels
{
    using System;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using SampleActivityRecognition.ViewModels.Base;
    using Xamarin.Forms;

    /// <summary>
    /// Viewmodel of running app view.
    /// </summary>
    /// <seealso cref="SampleActivityRecognition.ViewModels.Base.BaseViewModel" />
    public class RunningAppViewModel : BaseViewModel
    {
        private readonly IRecognitionActivityService recognitionActivityService;
        private ActivityRecognized activity;

        public event EventHandler<ActivityChangedEventArgs> ActivityChanged;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:SampleActivityRecognition.ViewModels.RunningAppViewModel"/> class.
        /// </summary>
        public RunningAppViewModel()
        {
            this.recognitionActivityService = DependencyService.Get<IRecognitionActivityService>();
        }

        public void OnAppearing()
        {
            this.recognitionActivityService.ActivityChanged += RecognitionActivityService_ActivityChanged;
        }

        public void OnDisappearing()
        {
            this.recognitionActivityService.ActivityChanged -= RecognitionActivityService_ActivityChanged;
        }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public ActivityRecognized Activity
        {
            get { return this.activity; }
            set
            {
                this.activity = value;
                OnPropertyChanged();
            }
        }


        private void RecognitionActivityService_ActivityChanged(object sender, ActivityChangedEventArgs e)
        {
            Activity = e.Activity;
            ActivityChanged?.Invoke(sender, e);
        }
    }
}
