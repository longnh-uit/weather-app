using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection.Emit;

using Xamarin.Forms;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp
{
     
     public class SplashScreen : ContentPage
    {
        Image splashImage;

        Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };

        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                WidthRequest = 200,
                HeightRequest = 200
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);
            this.BackgroundColor = Color.FromHex("#3694E4");
            this.Content = sub;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 4000); 
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(150, 1200, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }

    }

    
}
