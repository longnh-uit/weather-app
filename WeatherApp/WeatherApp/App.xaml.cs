using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
using WeatherApp.Models;
using System.Collections.Generic;

namespace WeatherApp
{
    public partial class App : Application
    {
        public static Database db = new Database();
        public App()
        {
            InitializeComponent();
            db.CreateDatebase();
            initGetBgColor();
            MainPage = new NavigationPage(new MainPage());
        }

        void initGetBgColor()
        {
            //Database db = new Database();
            Variable item = db.GetBgColor();
            Application.Current.Resources["PageBackgroundColor"] = Color.FromHex(item.VariableValue);
            //Current.Resources["PageBackgroundColor"] = "Red";
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
