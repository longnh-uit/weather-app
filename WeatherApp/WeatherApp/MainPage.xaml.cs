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
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        Models.Location Hanoi = new Models.Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            NavigationPage navPage = new NavigationPage(new MainCarouselPage());
            Detail = navPage;
            GetLocationCurrent();
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
                    await DisplayAlert("abc", "No GPS", "ok");
                }
                else
                {
                    await DisplayAlert("abc", position.ToString(), "ok");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}