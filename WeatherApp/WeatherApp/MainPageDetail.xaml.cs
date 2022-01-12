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
using Plugin.LocalNotification;
using Acr.UserDialogs;
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public Location locationGlobal;
        private readonly int index;
        private int timezone = 0;
        public int Index { get => index; }
        public Action getLocation;
        List<Daily> allList = new List<Daily>();
        List<Hourly> allListHour = new List<Hourly>();
        public MainPageDetail(int index)
        {
            InitializeComponent();
            this.index = index;
            LoadingData(App.curLocation);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                    var now = DateTime.UtcNow.AddSeconds(timezone);
                    TimeLabel.Text = now.ToString("HH:mm");
                    dateLabel.Text = now.ToString("ddd", culture) + ", Th" + now.ToString("MM") + " " + now.ToString("dd");
                }); return true;
            });

        }

        public MainPageDetail(Location location, int index)
        {
            InitializeComponent();
            locationGlobal = location;
            this.index = index;
            LoadingData(locationGlobal);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                    var now = DateTime.UtcNow.AddSeconds(timezone);
                    TimeLabel.Text = now.ToString("HH:mm");
                    dateLabel.Text = now.ToString("ddd", culture) + ", Th" + now.ToString("MM") + " " + now.ToString("dd");
                }); return true;
            });
        }

        async void LoadingData(Location location)
        {
            UserDialogs.Instance.ShowLoading("Đang tải", MaskType.Black);
            await GetWeatherInfo(location);
            UserDialogs.Instance.HideLoading();
        }
        void getBgImage(string desc)
        {
            if(!(App.db.GetBgColor().VariableValue == "#7097DA"))
            {
                BackgroundImageSource = null;
            }
            else
            {
                if (desc.Contains("mây"))
                {
                    BackgroundImageSource = "lc.jpg";
                }
                else if(desc.Contains("bầu trời"))
                {
                    BackgroundImageSource = "c.png";
                }

                if (desc.Contains("u ám"))
                {
                    BackgroundImageSource = "s.jpg";
                }
                else if (desc.Contains("mưa"))
                {
                    BackgroundImageSource = "lr.png";
                }
                else if (desc.Contains("tuyết"))
                {
                    BackgroundImageSource = "sn.png";
                }

            }
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

        private async Task GetWeatherInfo(Location location)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/currentweather?lon={location.lon}&lat={location.lat}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    weatherInfo = ConvertUnit.CurrentWeather(weatherInfo);
                    timezone = weatherInfo.timezone;
                    descriptionTxt.Text = ConvertUnit.FirstCharToUpper(weatherInfo.weather[0].description);
                    getBgImage(weatherInfo.weather[0].description);
                    iconImg.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    iconPrimary.Source = $"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    temperatureTxt.Text = $"{weatherInfo.main.temp.ToString("0")}{App.unit.tempUnitCurrent}";
                    humidityTxt.Text = $"{weatherInfo.main.humidity.ToString("0")}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure.ToString("0")}{App.unit.pressureUnitCurrent}";
                    visibilityTxt.Text = $"{weatherInfo.visibility.ToString("0")}{App.unit.distanceUnitCurrent}";
                    windTxt.Text = $"{weatherInfo.wind.speed.ToString("0")} {App.unit.speedUnitCurrent}";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all.ToString("0")}%";
                    maxMinTempText.Text = $"Cao: {weatherInfo.main.temp_max.ToString("0")}° ~ Thấp: {weatherInfo.main.temp_min.ToString("0")}°";

                    if (location == App.curLocation)
                    {

                        // Notification part
                        NotificationRequest notification = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Silent = true,
                            NotificationId = 1337,
                            Subtitle = DateTime.Now.ToString("HH:mm"),
                            Description = $"{descriptionTxt.Text} {temperatureTxt.Text}\n{location.name}"
                        };
                        NotificationCenter.Current.Show(notification);
                    }
                    GetHourlyWeather(location);
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

                        ConvertUnit.DailyWeather(list);
                        var date = ConvertUnit.getDateTime(list.dt);

                        if (date > DateTime.Now)
                        {
                            list.dateUTC = date.ToString("ddd", CultureInfo.CreateSpecificCulture("vi-VN")) + ", Th" + date.ToString("MM") + " " + date.ToString("dd");
                            list.datetime = getDayOfWeek(date.DayOfWeek.ToString());
                            list.image = $"http://openweathermap.org/img/wn/{list.weather[0].icon}@2x.png";
                            
                            list.temp.min = Math.Round(list.temp.min);
                            list.temp.max = Math.Round(list.temp.max);
                            list.sunriseText = ConvertUnit.getDateTime(list.sunrise).ToString("HH:mm");
                            list.sunsetText = ConvertUnit.getDateTime(list.sunset).ToString("HH:mm");
                            list.unit = new Unit()
                            {
                                tempUnitCurrent = App.unit.tempUnitCurrent,
                                distanceUnitCurrent = App.unit.distanceUnitCurrent,
                                speedUnitCurrent = App.unit.speedUnitCurrent,
                                
                                rainUnitCurrent = App.unit.rainUnitCurrent,
                                pressureUnitCurrent = App.unit.pressureUnitCurrent,

                            };
                            list.pop *= 100;
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

        private async void GetHourlyWeather(Location location)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/hourlyweather?lon={location.lon}&lat={location.lat}";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<HourlyWeather>(result.Response);
                    int i = 0;
                    foreach (var list in forcastInfo.hourly)
                    {
                        i++;
                        ConvertUnit.HourlyWeather(list);
                        var date = ConvertUnit.getDateTime(list.dt);
                        if (i < 24)
                        {
                            if (date > DateTime.Now)
                            {
                                list.dateUTC = date.ToString("ddd", CultureInfo.CreateSpecificCulture("vi-VN")) + ", Th" + date.ToString("MM") + " " + date.ToString("dd");
                                list.time = date.ToString("HH:mm");
                                list.image = $"http://openweathermap.org/img/wn/{list.weather[0].icon}@2x.png";
                                list.temp = Math.Round(list.temp);
                                list.wind_speed = Math.Round(list.wind_speed);
                                list.dew_point = Math.Round(list.dew_point);
                                list.weather[0].description = ConvertUnit.FirstCharToUpper(list.weather[0].description);
                                list.unit = new Unit()
                                {
                                    tempUnitCurrent = App.unit.tempUnitCurrent,
                                    distanceUnitCurrent = App.unit.distanceUnitCurrent,
                                    speedUnitCurrent = App.unit.speedUnitCurrent,
                                    rainUnitCurrent = App.unit.rainUnitCurrent,
                                    pressureUnitCurrent = App.unit.pressureUnitCurrent,

                                };
                                list.pop *= 100;
      
                                allListHour.Add(list);
                            }
                        }
                    }
                    
                    listByHour.ItemsSource = allListHour;
                    GetDailyWeather(location);

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

        private void btnDetailHour_Clicked(object sender, EventArgs e)
        {
            App.index = index;
            Navigation.PushAsync(new DetailByHour(allListHour));
        }

        private void btnDetailDay_Clicked(object sender, EventArgs e)
        {
            App.index = index;
            Navigation.PushAsync(new DetailByDay(allList));
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/updateweather";
            await ApiCaller.Get(url);
            GetDailyWeather(locationGlobal);
            refreshPage.IsRefreshing = false;
        }

        private void GetLocationButton_Clicked(object sender, EventArgs e)
        {
            getLocation?.Invoke();
        }
    }
}