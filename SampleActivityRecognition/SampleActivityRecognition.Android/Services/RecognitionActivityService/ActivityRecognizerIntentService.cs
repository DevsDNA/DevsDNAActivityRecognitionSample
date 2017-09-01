namespace SampleActivityRecognition.Droid.Services.RecognitionActivityService
{
using System;
    using Android.App;
    using Android.Content;
    using Android.Gms.Location;
    using Android.Support.V4.Content;

    /// <summary>
    /// Activity recognizer intent service.
    /// </summary>
    [Service(Exported = false)]
    public class ActivityRecognizerIntentService:IntentService
    {
        public ActivityRecognizerIntentService()
            : base(nameof(ActivityRecognizerIntentService))
        {
        }

        protected override void OnHandleIntent(Intent intent)
        {
            ActivityRecognitionResult result = ActivityRecognitionResult.ExtractResult(intent);
            Intent localIntent = new Intent("DetectedActivityBroadcast");
            localIntent.PutExtra("DetectedActivities", result.MostProbableActivity);
            LocalBroadcastManager.GetInstance(this).SendBroadcast(localIntent);
        }
    }
}
