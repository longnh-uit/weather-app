using Xamarin.Forms;

[assembly: Dependency(typeof(WeatherApp.Droid.SettingService_Android))]
namespace WeatherApp.Droid
{
    public class SettingService_Android : Helper.ISettingService
    {
        public void OpenSettings()
        {
            Xamarin.Essentials.Platform.CurrentActivity.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocationSourceSettings));
        }

        public bool IsGPSAvailable()
        {
            Android.Locations.LocationManager manager = (Android.Locations.LocationManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.LocationService);
            return manager.IsProviderEnabled(Android.Locations.LocationManager.GpsProvider);
        }

        public void OpenPrivacySetting()
        {
            var intent = new Android.Content.Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(Android.Content.ActivityFlags.NewTask);
            var uri = Android.Net.Uri.FromParts("package", "com.companyname.weatherapp", null);
            intent.SetData(uri);
            Xamarin.Essentials.Platform.CurrentActivity.StartActivity(intent);
        }
    }
}