﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ChangeLocationPage();
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
