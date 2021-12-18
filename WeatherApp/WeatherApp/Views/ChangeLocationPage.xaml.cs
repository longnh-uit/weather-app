using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeLocationPage : ContentPage
    {
        Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };

        public ChangeLocationPage()
        {
            InitializeComponent();
            //ItemsAdded();
            listPositionSearch.ItemsSource = itemsList;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchLocationPage());
        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new SearchLocationPage());
        }

        private void deleteLocation_Clicked(object sender, EventArgs e)
        {

        }

        List<Location> itemsList = new List<Location>();

        void ItemsAdded()
        {

            itemsList.Add(new Location
            {
                name = "abc"
            });
        }


        void Handle_DefaultLocation(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage(Hanoi, 0));
        }

        private void listPositionSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listPositionSearch.SelectedItem != null)
            {
                int index = itemsList.IndexOf((Location)listPositionSearch.SelectedItem);
                Location position = (Location)listPositionSearch.SelectedItem;
                Navigation.PushAsync(new MainPage(position, index));
            }
        }

       
    }

}