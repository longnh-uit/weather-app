using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
using WeatherApp.Helper;
using Newtonsoft.Json;
using WeatherApp.Models;
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            ItemsAdded();
            GetWeatherInfo();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => { Device.BeginInvokeOnMainThread(() => {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                dateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
            }); return true; });
        }

        public MainPageDetail(string location)
        {
            InitializeComponent();
            ItemsAdded();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                    TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                    dateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
                }); return true;
            });
        }

        private async void GetWeatherInfo()
        {
            var url = $"http://192.168.1.2:80/weather/api/currentweather?name=Lọndon";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description;
                    iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = $"{weatherInfo.main.temp.ToString("0")}°C";
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure}mb";
                    visibilityTxt.Text = $"{weatherInfo.visibility / 1000} km";
                    windTxt.Text = $"{weatherInfo.wind.speed * 3.6} km/h";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";
                    maxMinTempText.Text = $"Cao: {weatherInfo.main.temp_max.ToString("0")}°C ~ Thap: {weatherInfo.main.temp_min.ToString("0")}°C";
                   
                    //var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    //dateLabel.Text = dt.ToString("dddd, MMM dd").ToUpper();

                    //GetForecast();
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

        List<string> itemsList = new List<string>();
        void ItemsAdded()
        {
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            listByHour.ItemsSource = itemsList;
            listDatailDay.ItemsSource = itemsList;
        }

        private void btnDetailHour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByHour());
        }

        private void btnDetailDay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByDay());
        }
    }
}