using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace NewPongCity
{
    [Activity(Label = "PongCity"
        , MainLauncher = true
        , Icon = "@drawable/pong_city"
        , Theme = "@style/Theme.pong_city"
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new MainMenu();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

