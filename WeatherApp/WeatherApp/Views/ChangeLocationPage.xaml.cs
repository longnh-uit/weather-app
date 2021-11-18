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
    public partial class ChangeLocationPage : ContentPage
    {
        public ChangeLocationPage()
        {
            InitializeComponent();
            ItemsAdded();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchLocationPage());
        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new SearchLocationPage());
        }

        List<string> itemsList = new List<string>();
        void ItemsAdded()
        {
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            itemsList.Add("abc");
            listPositionSearch.ItemsSource = itemsList;
        }
    }
}