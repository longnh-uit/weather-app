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
        public static int index = 0;
        public App()
        {
            InitializeComponent();
            db.CreateDatebase();
            initGetBgColor();
            NavigationPage navPage = new NavigationPage(new SplashScreen());
            MainPage = navPage;
        }

        void initGetBgColor()
        {
            //Database db = new Database();
            Variable item = db.GetBgColor();
            Current.Resources["PageBackgroundColor"] = Color.FromHex(item.VariableValue);
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
