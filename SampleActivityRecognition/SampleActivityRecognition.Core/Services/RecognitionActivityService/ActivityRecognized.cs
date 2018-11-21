namespace SampleActivityRecognition.Services.RecognitionActivityService
{
    public class ActivityRecognized
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public ActivityTypes ActivityType { get; set; }

        /// <summary>
        /// Gets or sets the confidence.
        /// </summary>
        public int Confidence { get; set; }
    }
}
