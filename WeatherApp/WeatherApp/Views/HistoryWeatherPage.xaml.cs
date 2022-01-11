using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryWeatherPage : ContentPage
    {
        private static readonly Database db = App.db;
        private List<Location> locations;
        public long Selected_dt;
        public int indexItem;
        //public static DateTime today = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture);

        long unixStart = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };

        public HistoryWeatherPage()
        {
            InitializeComponent();
            InitPreviousTime();
            InitListLocation();
            defaultLocation.Text = App.curLocation.name;
        }

        void InitPreviousTime()
        {
            string[] previousTime = new string[]
            {
                "Một ngày trước",
                "Hai ngày trước",
                "Ba ngày trước",
            };

            prkSelectedDay.ItemsSource = previousTime;
            prkSelectedDay.SelectedIndex = 0;
        }

        public void InitListLocation()
        {
            locations = db.GetAllLocation();
            if (locations == null)
            {
                listPosition.ItemsSource = null;
                return;
            }

            listPosition.ItemsSource = locations;
        }
        private void prkSelectedDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            int selectedVehicleIndex = picker.SelectedIndex;


            switch (selectedVehicleIndex)
            {
                case 0:
                    Selected_dt = unixStart - 86400;
                    indexItem = selectedVehicleIndex;
                    break;
                case 1:
                    Selected_dt = unixStart - 86400 * 2;
                    indexItem = selectedVehicleIndex;
                    break;
                case 2:
                    Selected_dt = unixStart - 86400 * 3;
                    indexItem = selectedVehicleIndex;
                    break;
                default: break;
            }
        }

        private void Handle_DefaultLocation(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailHistoryWeatherPage(App.curLocation, Selected_dt,indexItem));
        }

        private void listPosition_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (listPosition.SelectedItem != null)
            {
                if (locations != null)
                {
                    Location pos = (Location)listPosition.SelectedItem;
                    Navigation.PushAsync(new DetailHistoryWeatherPage(pos, Selected_dt, indexItem));
                }
            }
        }
    }
}