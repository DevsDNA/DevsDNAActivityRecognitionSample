namespace SampleActivityRecognition.Views
{
    using System;
    using System.ComponentModel;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using SampleActivityRecognition.ViewModels;
    using Xamarin.Forms;


    /// <summary>
    /// View to simulate running app.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class RunningAppView
    {
        private bool isAnimationRealized;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningAppView"/> class.
        /// </summary>
        public RunningAppView()
        {
            InitializeComponent();
            BindingContext = new RunningAppViewModel();
        }

        public RunningAppViewModel ViewModel => (BindingContext as RunningAppViewModel);

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.PropertyChanged+= ViewModel_PropertyChanged;
            ViewModel.OnAppearing();
        }
        
        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.PropertyChanged-= ViewModel_PropertyChanged;
            ViewModel.OnDisappearing();
        }
        

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Activity):
                    Title = ViewModel.Activity.ActivityType.ToString();
                    if (ViewModel.Activity.ActivityType == ActivityTypes.Running || ViewModel.Activity.ActivityType == ActivityTypes.Walking)
                        ExecuteAnimation(true);
                    else
                        ExecuteAnimation(false);
                    break;
            }
        }


        private void ExecuteAnimation(bool makeButtonBigger)
        {
            if (makeButtonBigger && !isAnimationRealized)
            {
                Button1.IsVisible = false;
                Button2.IsVisible = false;
                Button3.IsVisible = false;
                Button4.IsVisible = false;

                Animation animationHeight = new Animation(x => { this.StartButton.HeightRequest = x; this.StopButton.HeightRequest = x; }, this.Button1.Height, this.Height / 2);
                Animation animationWidth = new Animation(x => { this.StartButton.WidthRequest = x; this.StopButton.WidthRequest = x; }, this.Button1.Height, this.Width / 2);
                animationWidth.Commit(this, nameof(animationWidth));
                animationHeight.Commit(this, nameof(animationHeight));
                isAnimationRealized = true;
            }
            else if (!makeButtonBigger && isAnimationRealized)
            {
                Button1.IsVisible = true;
                Button2.IsVisible = true;
                Button3.IsVisible = true;
                Button4.IsVisible = true;

                Animation animationHeight = new Animation(x => { this.StartButton.HeightRequest = x; this.StopButton.HeightRequest = x; }, this.Height / 2, this.Button1.Height);
                Animation animationWidth = new Animation(x => { this.StartButton.WidthRequest = x; this.StopButton.WidthRequest = x; }, this.Width / 2, this.Button1.Height);
                animationWidth.Commit(this, nameof(animationWidth));
                animationHeight.Commit(this, nameof(animationHeight));
                isAnimationRealized = false;
            }
        }

        private void Handle_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Option clicked", "This is an option from app.", "OK");
        }
    }
}