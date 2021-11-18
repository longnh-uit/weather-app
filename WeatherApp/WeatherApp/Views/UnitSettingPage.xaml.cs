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
        class test
        {
            public string Title { get; set; }
        }
        public UnitSettingPage()
        {
            InitializeComponent();
            List<test> list = new List<test>()
            {
                new test {Title = "°C"},
                new test {Title = "°F"}
            };
            TemperaturePicker.ItemsSource = list;
        }
    }
}