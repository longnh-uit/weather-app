using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryWeatherPage : ContentPage
    {
        public long Selected_dt;
        //public static DateTime today = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture);

        long unixStart = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        public HistoryWeatherPage()
        {
            InitializeComponent();
            InitPreviousTime();
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

        private void prkSelectedDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            int selectedVehicleIndex = picker.SelectedIndex;
            

            switch (selectedVehicleIndex)
            {
                case 0:
                    Selected_dt = unixStart - 86400;
                    DisplayAlert("Weather Info", Selected_dt.ToString(), "OK");
                    break;
                case 1:
                    Selected_dt = unixStart - 86400*2;
                    DisplayAlert("Weather Info", Selected_dt.ToString(), "OK");
                    break;
                case 2:
                    Selected_dt = unixStart - 86400*3;
                    DisplayAlert("Weather Info", Selected_dt.ToString(), "OK");
                    break;
                default: break;
            }
        }
    }
}