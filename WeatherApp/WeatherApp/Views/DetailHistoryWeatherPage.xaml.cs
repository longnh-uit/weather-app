using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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
        public DetailHistoryWeatherPage(Location location, long dt,int title)
        {
            InitializeComponent();
            GetHistoryWeather(location, dt);
            setTitle(title);
        }

        void setTitle(int index)
        {
            switch (index)
            {
                case 0:
                    Title = "1 ngày trước";
                    break;
                case 1:
                    Title = "2 ngày trước";
                    break;
                case 2:
                    Title = "3 ngày trước";
                    break;
                default: break;
            }
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
                    var date = ConvertUnit.getDateTime(dt);
                    //await DisplayAlert("dfd", location.name, weatherInfo.current.weather[0].description, weatherInfo.current.temp.ToString());
                    //weatherInfo = ConvertUnit.CurrentWeather(weatherInfo);
                    //descriptionTxt.Text = weatherInfo.weather[0].description;
                    positionText.Text = location.name;
                    dateLabel.Text = date.ToString("ddd", CultureInfo.CreateSpecificCulture("vi-VN")) + ", Th" + date.ToString("MM") + " " + date.ToString("dd");
                    iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.current.weather[0].icon}@2x.png";
                    //iconPrimary.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    temperatureTxt.Text = $"{weatherInfo.current.temp.ToString("0")}{App.unit.tempUnitCurrent}";
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