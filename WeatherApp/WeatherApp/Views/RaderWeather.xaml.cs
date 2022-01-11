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
    public partial class RaderWeather : ContentPage
    {
        public RaderWeather()
        {
            InitializeComponent();
            initLocation();
            
        }

        void initLocation()
        {
            var url = $"https://www.windy.com/{App.curLocation.lat}/{App.curLocation.lon}";
            browser.Source = url;
        }
    }
}