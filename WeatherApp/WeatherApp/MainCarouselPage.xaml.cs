using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp.Helper;
using Newtonsoft.Json;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCarouselPage : CarouselPage
    {
        private static readonly Database db = App.db;
        private List<Location> locations;
        private PauseTokenSource pts = new PauseTokenSource();
        private bool locationFlag1 = false, locationFlag2 = false;

        private readonly Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };

        public MainCarouselPage()
        {
            InitializeComponent();
            CurrentPageChanged += OnPageChanged;
            GetLocationCurrent();
        }

        public void InitMainpageDetail()
        {

            Children.Clear();
            if (App.curLocation == null)
            {
                App.curLocation = Hanoi;
            }
            var page = new MainPageDetail(App.curLocation, Children.Count);
            page.getLocation += GetLocationCurrent;
            Children.Add(page);

            if (locations != null)
            {
                foreach (Location position in locations)
                {
                    page = new MainPageDetail(position, Children.Count);
                    page.getLocation += GetLocationCurrent;
                    Children.Add(page);
                }
            }
        }

        public void OnPageChanged(object sender, EventArgs e)
        {
            int index = Children.IndexOf(CurrentPage) == -1 ? App.index : Children.IndexOf(CurrentPage);
            if (index > 0)
            {
                txtName.Text = locations[index - 1].name.ToString();
            }
            else { txtName.Text = App.curLocation.name; }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (locationFlag1)
                GetLocationCurrent1();
            else if (locationFlag2)
                GetLocationCurrent2();
            locations = db.GetAllLocation();
            InitMainpageDetail();
            txtName.Text = App.index > 0 ? locations[App.index - 1].name : App.curLocation.name;
            CurrentPage = Children[App.index];

        }

        public async void GetLocationCurrent()
        {
            var settingService = DependencyService.Get<ISettingService>();

            var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();

            if (status == Xamarin.Essentials.PermissionStatus.Denied)
            {
                var permissionOption = await DisplayAlert("Thông báo", "Ứng dụng này cần quyền sử dụng tính năng này. Bạn có thể cấp chúng trong cài đặt ứng dụng", "CÀI ĐẶT", "HUỶ");
                if (permissionOption)
                {
                    settingService.OpenPrivacySetting();
                    locationFlag1 = true;
                    return;
                }
                else return;
            }
            GetLocationCurrent1();

        }

        public async void GetLocationCurrent1()
        {
            locationFlag1 = false;
            var settingService = DependencyService.Get<ISettingService>();

            var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            if (status == Xamarin.Essentials.PermissionStatus.Denied) return;

            if (!settingService.IsGPSAvailable())
            {
                var gpsOption = await DisplayAlert("Thông báo", "GPS của bạn dương như bị tắt, bạn có muốn bật tính năng này không", "OK", "HUỶ");
                if (gpsOption)
                {
                    settingService.OpenSettings();
                    locationFlag2 = true;
                    return;
                }
                else return;
            }
            GetLocationCurrent2();
        }

        public async void GetLocationCurrent2()
        {
            locationFlag2 = false;
            var settingService = DependencyService.Get<ISettingService>();
            var gpsAvailable = settingService.IsGPSAvailable();
            if (!gpsAvailable) return;

            try
            {
                var position = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
                if (position == null)
                {
                    position = await Xamarin.Essentials.Geolocation.GetLocationAsync(new Xamarin.Essentials.GeolocationRequest
                    {
                        DesiredAccuracy = Xamarin.Essentials.GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (position == null)
                {
                    App.curLocation = null;
                }
                else
                {
                    var url = $"http://www.xamarinweatherapi.somee.com/api/getlocation?lon={position.Longitude}&lat={position.Latitude}";

                    var result = await ApiCaller.Get(url);
                    if (result.Successful)
                    {
                        try
                        {
                            var locationInfo = JsonConvert.DeserializeObject<Models.Location>(result.Response);
                            App.curLocation = locationInfo;

                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Weather Info", ex.Message, "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Weather Info", "No information found", "OK");
                    }
                }
                App.index = 0;
                InitMainpageDetail();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ok");
            }
        }


        //public void PageRight(this CarouselPage carouselPage)
        //{
        //    var pageCount = carouselPage.Children.Count;
        //    if (pageCount < 2)
        //        return;

        //    var index = carouselPage.Children.IndexOf(carouselPage.CurrentPage);
        //    index++;
        //    if (index >= pageCount)
        //        index = 0;

        //    carouselPage.CurrentPage = carouselPage.Children[index];
        //}

    }
}