namespace SampleActivityRecognition.Views
{
    using System;
    using System.ComponentModel;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using SampleActivityRecognition.ViewModels;
    using Xamarin.Forms;


    /// <summary>
    /// View to simulate tracker app.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class TrackerAppView : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackerAppView"/> class.
        /// </summary>
        public TrackerAppView()
        {
            InitializeComponent();
            BindingContext = new TrackerAppViewModel();
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        public TrackerAppViewModel ViewModel => (BindingContext as TrackerAppViewModel);

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.OnAppearing();
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.OnDisappearing();
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Activity):
                    Title = ViewModel.Activity.ActivityType.ToString();
                    if (ViewModel.Activity.ActivityType == ActivityTypes.OnVehicle)
                        ThrowAlert();
                    break;
            }
        }

        private async void ThrowAlert()
        {
            await DisplayAlert("Danger", "Please, don't use the app when you're driving.", "OK");
        }
    }
}