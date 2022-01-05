using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp.Views;
using Xamarin.Essentials;
using WeatherApp.Helper;
using Newtonsoft.Json;
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        private Models.Location curLocation = App.curLocation; 
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            GetLocationCurrent();
            NavigationPage navPage = new NavigationPage(new MainCarouselPage());
            Detail = navPage;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            MainCarouselPage detailPage = ((NavigationPage)Detail).RootPage as MainCarouselPage;


            App.index = detailPage.Children.IndexOf(detailPage.CurrentPage);
            Detail.Navigation.PushAsync(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }

        // lấy tọa độ hiện tại
        public async void GetLocationCurrent()
        {
            try
            {
                var position = await Geolocation.GetLastKnownLocationAsync();
                if (position == null)
                {
                    position = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (position == null)
                {
                    curLocation = null;
                }
                else
                {
                    var url = $"http://www.xamarinweatherapi.somee.com/api/getLocation?lon={position.Longitude}&lat={position.Latitude}";

                    var result = await ApiCaller.Get(url);
                    if (result.Successful)
                    {
                        try
                        {
                            var locationInfo = JsonConvert.DeserializeObject<Models.Location>(result.Response);
                            curLocation = locationInfo;
                            
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
            }
            catch (Exception ex)
            {
                await DisplayAlert("sdsd", "error", "ok");
            }
        }
    }
}