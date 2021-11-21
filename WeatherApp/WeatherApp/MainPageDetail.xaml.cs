using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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
            Device.StartTimer(TimeSpan.FromSeconds(1), () => { Device.BeginInvokeOnMainThread(() => {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
                TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                DateLabel.Text = DateTime.Now.ToString("ddd", culture) + ", Th" + DateTime.Now.ToString("MM") + " " + DateTime.Now.ToString("dd");
            }); return true; });
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