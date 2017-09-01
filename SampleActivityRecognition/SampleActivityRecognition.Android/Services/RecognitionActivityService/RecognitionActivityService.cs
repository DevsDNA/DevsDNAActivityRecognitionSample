[assembly: Xamarin.Forms.Dependency(typeof(SampleActivityRecognition.Droid.Services.RecognitionActivityService.RecognitionActivityService))]
namespace SampleActivityRecognition.Droid.Services.RecognitionActivityService
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Android.Gms.Common;
    using Android.Gms.Common.Apis;
    using Android.Gms.Location;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V4.Content;
    using SampleActivityRecognition.Commons;
    using SampleActivityRecognition.Services.RecognitionActivityService;

    public class RecognitionActivityService : Java.Lang.Object, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener, IRecognitionActivityService
    {
        private ActivityRecognizerBroadcastReceiver broadcastReceiver;
        private ActivityRecognized lastActivity;
        private GoogleApiClient client;

        public event EventHandler<ActivityChangedEventArgs> ActivityChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:SampleActivityRecognition.Droid.Services.RecognitionActivityService.RecognitionActivityService"/> class.
        /// </summary>
        public RecognitionActivityService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:SampleActivityRecognition.Droid.Services.RecognitionActivityService.RecognitionActivityService"/> class.
        /// </summary>
        /// <param name="handle">Handle.</param>
        /// <param name="transfer">Transfer.</param>
        public RecognitionActivityService(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        /// <value>The last activity.</value>
        public ActivityRecognized LastActivity
        {
            get { return this.lastActivity; }
            set
            {
                this.lastActivity = value;
                ActivityChanged?.Invoke(this, new ActivityChangedEventArgs(this.lastActivity));
            }
        }

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>The service.</returns>
        public void StartService()
        {
            if (this.client?.IsConnected ?? false)
                return;

            GoogleApiConnect();
        }

        /// <summary>
        /// Stops the service.
        /// </summary>
        /// <returns>The service.</returns>
        public void StopService()
        {
            if (this.broadcastReceiver != null)
            {
                LocalBroadcastManager.GetInstance(Application.Context).UnregisterReceiver(this.broadcastReceiver);
                this.broadcastReceiver.OnReceiveImpl -= BroadcastReceiver_OnReceiveImpl;
                this.broadcastReceiver.Dispose();
                this.broadcastReceiver = null;
            }
            this.client?.Disconnect();
            this.client?.Dispose();
            this.client = null;
        }

        /// <summary>
        /// Ons the connected.
        /// </summary>
        /// <param name="connectionHint">Connection hint.</param>
        public async void OnConnected(Bundle connectionHint)
        {
            Console.WriteLine("Google Api Client connected.");
            await RequestActivityUpdateAsync();
        }

        /// <summary>
        /// Ons the connection suspended.
        /// </summary>
        /// <param name="cause">Cause.</param>
        public void OnConnectionSuspended(int cause)
        {
            Console.WriteLine("Google Api Client connection suspended.");
        }

        /// <summary>
        /// Ons the connection failed.
        /// </summary>
        /// <param name="result">Result.</param>
        public void OnConnectionFailed(ConnectionResult result)
        {
            Console.WriteLine("Google Api Client failed connection.");
        }



        private void GoogleApiConnect()
        {
            try
            {
                Console.WriteLine($"Connecting api google for activity recognition.");

                this.client = new GoogleApiClient.Builder(Application.Context)
                    .AddApi(ActivityRecognition.API)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .Build();

                this.broadcastReceiver = new ActivityRecognizerBroadcastReceiver();
                this.broadcastReceiver.OnReceiveImpl += BroadcastReceiver_OnReceiveImpl;
                LocalBroadcastManager.GetInstance(Application.Context).RegisterReceiver(this.broadcastReceiver, new IntentFilter("DetectedActivityBroadcast"));
                this.client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BroadcastReceiver_OnReceiveImpl(Context context, Intent intent)
        {
            DetectedActivity activity = (DetectedActivity)intent.GetParcelableExtra("DetectedActivities");
            ActivityRecognized activityRecognized = new ActivityRecognized { Confidence = activity.Confidence };
            switch (activity.Type)
            {
                case DetectedActivity.InVehicle:
                    activityRecognized.ActivityType = ActivityTypes.OnVehicle;
                    LastActivity = activityRecognized;
                    break;
                case DetectedActivity.OnBicycle:
                    activityRecognized.ActivityType = ActivityTypes.OnBicycle;
                    LastActivity = activityRecognized;
                    break;
                case DetectedActivity.Walking:
                    activityRecognized.ActivityType = ActivityTypes.Walking;
                    LastActivity = activityRecognized;
                    break;
                case DetectedActivity.OnFoot:
                case DetectedActivity.Running:
                    activityRecognized.ActivityType = ActivityTypes.Running;
                    LastActivity = activityRecognized;
                    break;
                case DetectedActivity.Still:
                    activityRecognized.ActivityType = ActivityTypes.Stopped;
                    LastActivity = activityRecognized;
                    break;
                case DetectedActivity.Unknown:
                case DetectedActivity.Tilting:
                default:
                    break;
            }
        }

        private async Task RequestActivityUpdateAsync()
        {
            Console.WriteLine("Requestion activity updates recognitions.");
            Intent intent = new Intent(Application.Context, typeof(ActivityRecognizerIntentService));
            PendingIntent pendintIntent = PendingIntent.GetService(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);

            Android.Gms.Common.Apis.Statuses detectedActivity = await ActivityRecognition.ActivityRecognitionApi.RequestActivityUpdatesAsync(client, AppConfiguration.DetectionIntervalMillis, pendintIntent);
        }
    }
}