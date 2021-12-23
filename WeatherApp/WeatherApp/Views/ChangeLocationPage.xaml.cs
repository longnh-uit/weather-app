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
        // init database 
        private static readonly Database db = App.db;
        private List<Location> locations;
        public ChangeLocationPage()
        {
            InitializeComponent();
        }

        public void InitListLocation()
        {
            locations = db.GetAllLocation();
            if (locations == null)
            {
                listPositionSearch.ItemsSource = null;
                return;
            }

            listPositionSearch.ItemsSource = locations;
        }

        //redirect to search page
        private void AddButton_Clicked(object sender, EventArgs e)
        {
            _ = Navigation.PushModalAsync(new SearchLocationPage());
        }
    
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            _ = Navigation.PushModalAsync(new SearchLocationPage());
        }

        //delete location
        private void deleteLocation_Clicked(object sender, EventArgs e)
        {
            var item = (ImageButton)sender;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var choice = await DisplayAlert("Message", "Are you sure to delete this location", "YES", "NO");

                if (choice)
                {
                    if (db.DeleteLocation(item.CommandParameter.ToString()))
                    {
                        App.index = 0;
                        await DisplayAlert("Message", "Remove item successfully", "OK");

                        //update listLocation ...
                        InitListLocation();
                    }
                    else
                    {
                        await DisplayAlert("Message", "Failed ", "OK");
                    }
                }
            });
        }

        //click default location
        private void Handle_DefaultLocation(object sender, System.EventArgs e)
        {
            App.index = 0;
            _ = Navigation.PopToRootAsync();
        }

        private void listPositionSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listPositionSearch.SelectedItem != null)
            {
                if (locations != null)
                {
                    App.index = locations.IndexOf((Location)listPositionSearch.SelectedItem) + 1;
                    _ = Navigation.PopToRootAsync();
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitListLocation();
        }
    }

}