using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Views;
namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            ItemsAdded();
        }
        List<string> itemsList = new List<string>();
        void ItemsAdded()
        {
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            listByHour.ItemsSource = itemsList;
            listDatailDay.ItemsSource = itemsList;
        }

        private void btnDetailHour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByHour());
        }

        private void btnDetailDay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailByDay());
        }
    }
}