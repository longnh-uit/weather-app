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
        public DetailHistoryWeatherPage(Location location, long dt, int title)
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
        public List<HistoryWeather.Hourly> allListHour = new List<HistoryWeather.Hourly>();

        private async void GetHistoryWeather(Location location, long dt)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/timemachine?lon={location.lon}&lat={location.lat}&dt={dt}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {

                    var weatherInfo = JsonConvert.DeserializeObject<HistoryWeather>(result.Response);
                    var date = ConvertUnit.getDateTime(dt);
                    weatherInfo = ConvertUnit.HistoryWeather(weatherInfo);
                    descriptionTxt.Text = weatherInfo.current.weather[0].description;
                    positionText.Text = location.name;
                    dateLabel.Text = date.ToString("ddd", CultureInfo.CreateSpecificCulture("vi-VN")) + ", Th" + date.ToString("MM") + " " + date.ToString("dd");
                    iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.current.weather[0].icon}@2x.png";
                    temperatureTxt.Text = $"{weatherInfo.current.temp.ToString("0")}{App.unit.tempUnitCurrent}";
                    humidityTxt.Text = $"{weatherInfo.current.humidity.ToString("0")}%";
                    pressureTxt.Text = $"{weatherInfo.current.pressure.ToString("0")}{App.unit.pressureUnitCurrent}";
                    visibilityTxt.Text = $"{weatherInfo.current.visibility.ToString("0")}{App.unit.distanceUnitCurrent}";
                    windSpeedTxt.Text = $"{weatherInfo.current.wind_speed.ToString("0")} {App.unit.speedUnitCurrent}";
                    feelLikeText.Text = $"Cảm giác như: {weatherInfo.current.feels_like.ToString("0")}{App.unit.tempUnitCurrent}";
                    uviTxt.Text = $"{weatherInfo.current.uvi.ToString("0")}";
                    cloudinessTxt.Text = $"{weatherInfo.current.cloud.ToString("0")}%";
                    sunriseTxt.Text = ConvertUnit.getDateTime(weatherInfo.current.sunrise).ToString("HH:mm");
                    sunsetTxt.Text = ConvertUnit.getDateTime(weatherInfo.current.sunset).ToString("HH:mm");
                    // Notification part

                    int i = 0;
                    foreach (var list in weatherInfo.hourly)
                    {
                        i++;
                        //ConvertUnit.HourlyWeather(list);
                        var dateHourly = ConvertUnit.getDateTime(list.dt);
                        if (i < 24)
                        {
                            list.time = dateHourly.ToString("HH:mm");
                            list.image = $"http://openweathermap.org/img/wn/{list.weather[0].icon}@2x.png";
                            if (App.unit.tempUnitCurrent == "°F")
                            {
                                list.temp = Math.Round(list.temp * 1.8 + 32);
                            }
                            else
                            {
                                list.temp = Math.Round(list.temp);
                            }

                            allListHour.Add(list);
                        }
                    }
                    listByHour.ItemsSource = allListHour;
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