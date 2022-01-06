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
                    page = new MainPageDetail(App.curLocation, Children.Count);
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
            locations = db.GetAllLocation();
            InitMainpageDetail();
            txtName.Text = App.index > 0 ? locations[App.index - 1].name : App.curLocation.name;
            CurrentPage = Children[App.index];

        }

        public async void GetLocationCurrent()
        {
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