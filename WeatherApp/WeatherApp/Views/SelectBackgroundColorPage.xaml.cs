using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBackgroundColorPage : ContentPage
    {
        //readonly Database db = new App().;
        public SelectBackgroundColorPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            Application.Current.Resources["PageBackgroundColor"] = button.BackgroundColor;
            Variable color = App.db.GetBgColor();
            color.VariableValue = button.BackgroundColor.ToHex();
            App.db.UpdateBgColor(color);
            DependencyService.Get<Helper.IStatusBar>().ChangeStatusBarColor(color.VariableValue);
        }

        private void BtnDefault_Clicked(object sender, EventArgs e)
        {
            Application.Current.Resources["PageBackgroundColor"] = "#7097DA";
            Variable color = App.db.GetBgColor();
            color.VariableValue = "#7097DA";
            App.db.UpdateBgColor(color);
            DependencyService.Get<Helper.IStatusBar>().ChangeStatusBarColor(color.VariableValue);
        }
    }
}