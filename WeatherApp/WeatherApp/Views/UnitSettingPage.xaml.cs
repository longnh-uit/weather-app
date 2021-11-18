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
    public partial class UnitSettingPage : ContentPage
    {
        List<string> temperatureOptions = new List<string>() { "°C", "°F" };
        List<string> timeFormatOptions = new List<string>() { "12:00", "24:00" };
        List<string> distanceOptions = new List<string>() { "km", "mi" };
        List<string> speedOptions = new List<string>() { "kph", "mph", "km/h", "m/s", "knots", "ft/s" };
        List<string> pressureOptions = new List<string>() { "mBar", "inHg", "psi", "bar", "mmHg" };
        List<string> rainOptions = new List<string>() { "mm", "in" };

        public UnitSettingPage()
        {
            InitializeComponent();
            TemperaturePicker.ItemsSource = temperatureOptions;
            TemperaturePicker.SelectedItem = temperatureOptions[0];

            TimeFormatPicker.ItemsSource = timeFormatOptions;
            TimeFormatPicker.SelectedItem = timeFormatOptions[1];

            DistancePicker.ItemsSource = distanceOptions;
            DistancePicker.SelectedItem = distanceOptions[0];

            SpeedPicker.ItemsSource = speedOptions;
            SpeedPicker.SelectedItem = speedOptions[2];

            PressurePicker.ItemsSource = pressureOptions;
            PressurePicker.SelectedItem = pressureOptions[0];

            RainPicker.ItemsSource = rainOptions;
            RainPicker.SelectedItem = rainOptions[1];
        }
    }
}