using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp;
using WeatherApp.Helper;
using Newtonsoft.Json;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailHistoryWeatherPage : ContentPage
    {
        public DetailHistoryWeatherPage()
        {
            InitializeComponent();
        }
        public DetailHistoryWeatherPage(Location location, long dt)
        {
            InitializeComponent();
            GetHistoryWeather(location, dt);

        }

        
        private async void GetHistoryWeather(Location location,long dt)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/timemachine?lon={location.lon}&lat={location.lat}&dt={dt}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<HistoryWeather>(result.Response);
                    
                    await DisplayAlert("dfd", location.name, weatherInfo.current.weather[0].description, weatherInfo.current.temp.ToString());
                    //weatherInfo = ConvertUnit.CurrentWeather(weatherInfo);
                    //descriptionTxt.Text = weatherInfo.weather[0].description;
                    //iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    //iconPrimary.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    //temperatureTxt.Text = $"{weatherInfo.main.temp.ToString("0")}{App.unit.tempUnitCurrent}";
                    //humidityTxt.Text = $"{weatherInfo.main.humidity.ToString("0")}%";
                    //pressureTxt.Text = $"{weatherInfo.main.pressure.ToString("0")}{App.unit.pressureUnitCurrent}";
                    //visibilityTxt.Text = $"{weatherInfo.visibility.ToString("0")}{App.unit.distanceUnitCurrent}";
                    //windTxt.Text = $"Gió: {weatherInfo.wind.speed.ToString("0")} {App.unit.speedUnitCurrent}";
                    //cloudinessTxt.Text = $"{weatherInfo.clouds.all.ToString("0")}%";
                    //maxMinTempText.Text = $"Cao: {weatherInfo.main.temp_max.ToString("0")}° ~ Thap: {weatherInfo.main.temp_min.ToString("0")}°";

                    // Notification part




                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }

    }
}