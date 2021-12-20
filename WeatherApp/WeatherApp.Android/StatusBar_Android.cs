using Xamarin.Forms;

[assembly: Dependency(typeof(WeatherApp.Droid.StatusBar_Android))]
namespace WeatherApp.Droid
{
    public class StatusBar_Android : Helper.IStatusBar
    {
        public void ChangeStatusBarColor(string color)
        {
            Xamarin.Essentials.Platform.CurrentActivity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));
        }
    }
}