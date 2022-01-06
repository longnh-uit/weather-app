using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitSettingPage : ContentPage
    {
        //List<string> temperatureOptions = new List<string>() { "°C", "°F" };
        List<string> timeFormatOptions = new List<string>() { "12:00", "24:00" };
        //List<string> distanceOptions = new List<string>() { "km", "mi" };
        //List<string> speedOptions = new List<string>() { "kph", "mph", "km/h", "m/s", "knots", "ft/s" };
        List<string> pressureOptions = new List<string>() { "psi", "mBar", "inHg", "bar", "mmHg" };
        List<string> rainOptions = new List<string>() { "mm", "in" };

        public UnitSettingPage()
        {
            InitializeComponent();
            TemperaturePicker.ItemsSource = App.temperatureOptions;
            TemperaturePicker.SelectedItem = App.temperatureOptions[App.temperatureOptions.IndexOf(App.unit.tempUnitCurrent)];

            DistancePicker.ItemsSource = App.distanceOptions;
            DistancePicker.SelectedItem = App.distanceOptions[App.distanceOptions.IndexOf(App.unit.distanceUnitCurrent)];

            SpeedPicker.ItemsSource = App.speedOptions;
            SpeedPicker.SelectedItem = App.speedOptions[App.speedOptions.IndexOf(App.unit.speedUnitCurrent)];

            PressurePicker.ItemsSource = App.pressureOptions;
            PressurePicker.SelectedItem = App.pressureOptions[App.pressureOptions.IndexOf(App.unit.pressureUnitCurrent)];

            RainPicker.ItemsSource = rainOptions;
            RainPicker.SelectedItem = rainOptions[1];
        }

        private void TemperaturePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            App.unit.tempUnitCurrent = e.CurrentSelection[0].ToString();
            App.db.UpdateUnit(App.unit);

        }

        private void TimeFormatPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DisplayAlert("sd", e.CurrentSelection[0].ToString(), "ok");
            //App.unit.distanceUnitCurrent = e.CurrentSelection[0].ToString();
            //App.db.UpdateUnit(App.unit);
        }

        private void DistancePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DisplayAlert("sd", e.CurrentSelection[0].ToString(), "ok");
            App.unit.distanceUnitCurrent = e.CurrentSelection[0].ToString();
            App.db.UpdateUnit(App.unit);
        }

        private void SpeedPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DisplayAlert("sd", e.CurrentSelection[0].ToString(), "ok");
            App.unit.speedUnitCurrent = e.CurrentSelection[0].ToString();
            App.db.UpdateUnit(App.unit);
        }

        private void PressurePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DisplayAlert("sd", e.CurrentSelection[0].ToString(), "ok");
            App.unit.pressureUnitCurrent = e.CurrentSelection[0].ToString();
            App.db.UpdateUnit(App.unit);
        }
    }
}