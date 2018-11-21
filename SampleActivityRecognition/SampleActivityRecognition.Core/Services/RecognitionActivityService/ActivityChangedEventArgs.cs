namespace SampleActivityRecognition.Services.RecognitionActivityService
{
    using System;

    public class ActivityChangedEventArgs:EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityChangedEventArgs"/> class.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public ActivityChangedEventArgs(ActivityRecognized activity)
        {
            Activity = activity;
        }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public ActivityRecognized Activity { get; set; }
    }
}
