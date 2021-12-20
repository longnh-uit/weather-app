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
        Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };
        List<Daily> allList = new List<Daily>();
        public MainPageDetail()
        {
            InitializeComponent();
            //ItemsAdded();
            GetWeatherInfo(Hanoi);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => { Device.BeginInvokeOnMainThread(() => {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                dateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
            }); return true; });
        }

        public MainPageDetail(Location location)
        {
            InitializeComponent();
            //ItemsAdded();
            GetWeatherInfo(location);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                    TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                    dateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
                }); return true;
            });
        }

        public DateTime getDateTime(long input)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(input);
            return dateTimeOffset.ToOffset(new TimeSpan(7, 0, 0)).DateTime;
        }

        //convert day in week to vietnamese
        public string getDayOfWeek(string day)
        {
            switch (day)
            {
                case "Monday": return "Thứ 2";
                case "Tuesday": return "Thứ 3";
                case "Wednesday": return "Thứ 4";
                case "Thursday": return "Thứ 5";
                case "Friday": return "Thứ 6";
                case "Saturday": return "Thứ 7";
                case "Sunday": return "Chủ nhật";
                default: return "";

            }
        }

        private async void GetWeatherInfo(Location location)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/currentweather?lon={location.lon}&lat={location.lat}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description;
                    iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    iconPrimary.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    //cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = $"{weatherInfo.main.temp.ToString("0")}°C";
                    humidityTxt.Text = $"{weatherInfo.main.humidity.ToString("0")}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure.ToString("0")}mb";
                    visibilityTxt.Text = $"{(weatherInfo.visibility / 1000).ToString("0")} km";
                    windTxt.Text = $"Gió: {(weatherInfo.wind.speed * 3.6).ToString("0")} km/h";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all.ToString("0")}%";
                    maxMinTempText.Text = $"Cao: {weatherInfo.main.temp_max.ToString("0")}°C ~ Thap: {weatherInfo.main.temp_min.ToString("0")}°C";

                    //var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    //dateLabel.Text = dt.ToString("dddd, MMM dd").ToUpper();

                    GetDailyWeather(location);
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

        private async void GetDailyWeather(Location location)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/dailyweather?lon={location.lon}&lat={location.lat}";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<DailyWeather>(result.Response);

                    foreach (var list in forcastInfo.daily)
                    {
                        var date = getDateTime(list.dt);

                        if (date > DateTime.Now)
                        {
                            list.dateUTC = date.ToString("ddd", CultureInfo.CreateSpecificCulture("vi-VN")) + ", Th" + date.ToString("MM") + " " + date.ToString("dd");
                            list.datetime = getDayOfWeek(date.DayOfWeek.ToString());
                            list.image = $"http://openweathermap.org/img/wn/{list.weather[0].icon}@2x.png";
                            list.temperature = $"{list.temp.min.ToString("0")}° ~ {list.temp.max.ToString("0")}°";
                            list.rainability = $"{list.pop}%";
                            allList.Add(list);
                        }
                    }
                    listDatailDay.ItemsSource = allList;

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }
        List<string> itemsList = new List<string>();
      

        private void btnDetailHour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByHour());
        }

        private void btnDetailDay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByDay(allList));
        }
    }
}