namespace SampleActivityRecognition.Services.RecognitionActivityService
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Service to recognition activity.
    /// </summary>
    public interface IRecognitionActivityService
    {
        /// <summary>
        /// Occurs when [activity changed].
        /// </summary>
        event EventHandler<ActivityChangedEventArgs> ActivityChanged;

        /// <summary>
        /// Gets the last activity.
        /// </summary>
        ActivityRecognized LastActivity { get; }

        /// <summary>
        /// Starts the service.
        /// </summary>
        void StartService();

        /// <summary>
        /// Stops the service.
        /// </summary>
        void StopService();
    }
}
