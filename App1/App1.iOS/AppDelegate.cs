
using Foundation;
using UIKit;

namespace LevantateChevere.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
		    global::Xamarin.Forms.Forms.Init();
		    
		    //test audio in Background
		    NSError error;
		    AVAudioSession instance = AVAudioSession.SharedInstance();
		    instance.SetCategory(new NSString("AVAudioSessionCategoryPlayback"), AVAudioSessionCategoryOptions.MixWithOthers, out error);
		    instance.SetMode(new NSString("AVAudioSessionModeDefault"), out error);
		    instance.SetActive(true, AVAudioSessionSetActiveOptions.NotifyOthersOnDeactivation, out error);
		    //end test audio in Background

		    UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
		    UIApplication.SharedApplication.SetStatusBarHidden(false, false);
		    LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
