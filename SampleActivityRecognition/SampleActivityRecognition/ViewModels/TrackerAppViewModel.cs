namespace SampleActivityRecognition.ViewModels
{
    using System;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using SampleActivityRecognition.ViewModels.Base;
    using Xamarin.Forms;


    /// <summary>
    /// Viewmodel of tracker app view.
    /// </summary>
    /// <seealso cref="SampleActivityRecognition.ViewModels.Base.BaseViewModel" />
    public class TrackerAppViewModel : BaseViewModel
    {
        private readonly IRecognitionActivityService recognitionActivityService;
        private ActivityRecognized activity;

        public event EventHandler<ActivityChangedEventArgs> ActivityChanged;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:SampleActivityRecognition.ViewModels.TrackerAppViewModel"/> class.
        /// </summary>
        public TrackerAppViewModel()
        {
            this.recognitionActivityService = DependencyService.Get<IRecognitionActivityService>();
        }

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        public void OnAppearing()
        {
            this.recognitionActivityService.ActivityChanged += RecognitionActivityService_ActivityChanged;
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
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
