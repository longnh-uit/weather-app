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
    public partial class SelectBackgroundColorPage : ContentPage
    {
        public SelectBackgroundColorPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Application.Current.Resources["PageBackgroundColor"] =  button.BackgroundColor;
        }

        private void BtnDefault_Clicked(object sender, EventArgs e)
        {
            Application.Current.Resources["PageBackgroundColor"] = "#7097DA";
        }
    }
}