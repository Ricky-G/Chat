using Android.App;
using Android.Content.PM;

namespace Chat.Mobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        [Java.Interop.Export(nameof(BackdoorTest))]
        public void BackdoorTest()
            => Shell.Current.GoToAsync("//Tabs/Profile");
    }
}