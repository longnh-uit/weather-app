using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            Detail = new NavigationPage(new MainCarouselPage());
            
        }
        public MainPage(Location location,int index)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            Detail = new NavigationPage(new MainCarouselPage(location, index));

        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail.Navigation.PushAsync(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }
}