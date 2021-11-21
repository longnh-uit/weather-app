﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
using WeatherApp.Models;
using System.Collections.Generic;

namespace WeatherApp
{
    public partial class App : Application
    {
        readonly Database db = new Database();
        public App()
        {
            InitializeComponent();
            db.CreateDatebase();
            Application.Current.Resources["PageBackgroundColor"] = Color.FromHex("#aaaaaa");
            //initGetBgColor(db, "backgroundColor");
            MainPage = new NavigationPage(new MainPage());
        }

        void initGetBgColor(Database db, string color)
        {
            //Database db = new Database();
            //List<Variable> item = db.GetBgColor(color);

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
