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
                DateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
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
                    DateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
                }); return true;
            });
        }

        private async void GetWeatherInfo()
        {
            var url = $"http://localhost/weather/api/currentweather?name=Ha%20Noi";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    //descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    //iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    //cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    //humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    //pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    //windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    //cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    //var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    //dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();

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