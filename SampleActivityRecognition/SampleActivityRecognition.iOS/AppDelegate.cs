
using System;
using Foundation;
using UIKit;

namespace SampleActivityRecognition.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            try
            {
                global::Xamarin.Forms.Forms.Init();
                LoadApplication(new App());

                return base.FinishedLaunching(app, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
