using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
using WeatherApp.Models;
using System.Collections.Generic;
using Plugin.LocalNotification;
using WeatherApp.Helper;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class App : Application
    {
        public static int index = 0;
        public static List<string> temperatureOptions = new List<string>() { "°C", "°F" };
        public static List<string> distanceOptions = new List<string>() { "m", "km" };
        public static List<string> speedOptions = new List<string>() {"mph", "km/h", "m/s", "ft/s" };
        public static List<string> pressureOptions = new List<string>() { "psi", "mBar", "inHg", "bar", "mmHg" };
        public static Units unit;
        public static Database db = new Database();
        public static Location curLocation;
        public App()
        {
            InitializeComponent();
            db.CreateDatebase();
            curLocation = db.GetDefaultLocation();
            InitGetBgColor();
            ShowAlertNotification();


            NavigationPage navPage = new NavigationPage(new SplashScreen());
            MainPage = navPage;
        }

        private async void ShowAlertNotification()
        {
            List<Location> locations = db.GetAllLocation();

            foreach (Location location in locations)
            {

                var url = $"http://www.xamarinweatherapi.somee.com/api/alert?lon={location.lon}&lat={location.lat}";

                var result = await ApiCaller.Get(url);

                if (result.Successful)
                {
                    try
                    {
                        Alert alert = JsonConvert.DeserializeObject<Alert>(result.Response);
                        if (alert.alerts == null)
                            return;
                        // Notification part
                        NotificationRequest notification = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Title = alert.alerts[alert.alerts.Length - 1].sender_name,
                            Subtitle = alert.alerts[alert.alerts.Length - 1].event_name,
                            Description = alert.alerts[alert.alerts.Length - 1].description
                        };
                        NotificationCenter.Current.Show(notification);
                    }
                    catch
                    {
                        
                    }

                }
            }
        }

        void InitGetBgColor()
        {
            //Database db = new Database();
            Variable item = db.GetBgColor();
            Current.Resources["PageBackgroundColor"] = Color.FromHex(item.VariableValue);
            //Current.Resources["PageBackgroundColor"] = "Red";
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
