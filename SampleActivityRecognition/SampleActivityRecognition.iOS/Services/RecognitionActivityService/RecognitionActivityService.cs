[assembly: Xamarin.Forms.Dependency(typeof(SampleActivityRecognition.iOS.Services.RecognitionActivityService.RecognitionActivityService))]
namespace SampleActivityRecognition.iOS.Services.RecognitionActivityService
{
    using CoreMotion;
    using Foundation;
    using SampleActivityRecognition.Services.RecognitionActivityService;
    using System;

    /// <summary>
    /// Recognition activity service.
    /// </summary>
    public class RecognitionActivityService : IRecognitionActivityService
    {
        private ActivityRecognized lastActivity;
        private CMMotionActivityManager motionActivityManager;

        public event EventHandler<ActivityChangedEventArgs> ActivityChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SampleActivityRecognition.iOS.Services.RecognitionActivityService.RecognitionActivityService"/> class.
        /// </summary>
        public RecognitionActivityService()
        {
            this.motionActivityManager = new CMMotionActivityManager();
        }

        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        public ActivityRecognized LastActivity
        {
            get { return this.lastActivity; }
            set { this.lastActivity = value; }
        }


        /// <summary>
        /// Starts the service.
        /// </summary>
        public void StartService()
        {
            try
            {
                if (CMMotionActivityManager.IsActivityAvailable)
                    this.motionActivityManager.StartActivityUpdates(NSOperationQueue.CurrentQueue, CMMotionActivity_Update);
                else
                    Console.WriteLine("This device not supports motion activity recognition.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        public void StopService()
        {
            try
            {
                this.motionActivityManager?.StopActivityUpdates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CMMotionActivity_Update(CMMotionActivity activity)
        {
            if (activity.Automotive)
            {
                LastActivity = new ActivityRecognized() { ActivityType = ActivityTypes.OnVehicle, Confidence = 25 + (int)activity.Confidence * 25 };
            }
            else if (activity.Cycling)
            {
                LastActivity = new ActivityRecognized() { ActivityType = ActivityTypes.OnBicycle, Confidence = 25 + (int)activity.Confidence * 25 };
            }
            else if (activity.Running)
            {
                LastActivity = new ActivityRecognized() { ActivityType = ActivityTypes.Running, Confidence = 25 + (int)activity.Confidence * 25 };
            }
            else if (activity.Walking)
            {
                LastActivity = new ActivityRecognized() { ActivityType = ActivityTypes.Walking, Confidence = 25 + (int)activity.Confidence * 25 };
            }
            else if (activity.Stationary)
            {
                LastActivity = new ActivityRecognized() { ActivityType = ActivityTypes.Stopped, Confidence = 25 + (int)activity.Confidence * 25 };
            }

            ActivityChanged?.Invoke(this, new ActivityChangedEventArgs(LastActivity));
        }
    }
}