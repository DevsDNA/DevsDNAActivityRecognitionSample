namespace SampleActivityRecognition.Droid.Services.RecognitionActivityService
{
    using System;
    using Android.Content;

    /// <summary>
    /// Activity recognizer broadcast receiver.
    /// </summary>
    public class ActivityRecognizerBroadcastReceiver : BroadcastReceiver
    {
        /// <summary>
        /// Gets or sets the on receive impl.
        /// </summary>
        public Action<Context, Intent> OnReceiveImpl { get; set; }

        /// <summary>
        /// Ons the receive.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="intent">Intent.</param>
        public override void OnReceive(Context context, Intent intent)
        {
            OnReceiveImpl(context, intent);
        }
    }
}
