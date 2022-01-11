using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Helper;
using Newtonsoft.Json;
using WeatherApp.Models;
using Plugin.LocalNotification;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchLocationPage : ContentPage
    {
        public SearchLocationPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            SearchEntry.Text = "";
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (SearchEntry.Text != "")
            {
                GetListRecommemdLocation(SearchEntry.Text);
                ClearButton.IsVisible = true;
            }
            else {
                listPositionSearch.ItemsSource = null;
                ClearButton.IsVisible = false; 
            }


        }

        
        private async void GetListRecommemdLocation(string search)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/searchlocation?q={search}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    List<Location> listLocation = JsonConvert.DeserializeObject<List<Location>>(result.Response);
                    //await DisplayAlert("kq", listLocation.Count.ToString(), "ok");

                    listPositionSearch.ItemsSource = listLocation;


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

        private void listPositionSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Database db = new Database();
            if (listPositionSearch.SelectedItem != null)
            {
                Location pos = (Location)listPositionSearch.SelectedItem;
                if(pos != App.curLocation)
                {

                    //DisplayAlert("Message",pos.name, "OK");
                    if (db.AddNewLocation(pos))
                    {
                        ShowAlertNotification(pos);
                        Navigation.PopModalAsync();
                    }
                    else
                    {
                        DisplayAlert("error", "try again", "ok");
                    }
                }
                else
                {
                    Navigation.PopModalAsync();
                }
            }
        }

        private async void ShowAlertNotification(Location location)
        {
            var url = $"http://www.xamarinweatherapi.somee.com/api/alert?lon={location.lon}&lat={location.lat}";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    Alert alert = JsonConvert.DeserializeObject<Alert>(result.Response);
                    if (alert.alerts == null)
                        return;
                    // Notification part
                    NotificationRequest notification = new NotificationRequest
                    {
                        BadgeNumber = 1,
                        Title = alert.alerts[alert.alerts.Length - 1].sender_name,
                        Subtitle = alert.alerts[alert.alerts.Length - 1].event_name,
                        Description = alert.alerts[alert.alerts.Length - 1].description
                    };
                    NotificationCenter.Current.Show(notification);
                }
                catch
                {

                }

            }
        }
    }
}