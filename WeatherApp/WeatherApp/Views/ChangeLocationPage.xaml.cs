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

        List<String> itemsList = new List<String>();
        void ItemsAdded()
        {
            itemsList.Add("abc");
            itemsList.Add("xyz");
            itemsList.Add("alv");
            
            listPositionSearch.ItemsSource = itemsList;
        }

        private void listPositionSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listPositionSearch.SelectedItem != null)
            {
                int index = itemsList.IndexOf(listPositionSearch.SelectedItem.ToString());
                Navigation.PushAsync(new MainPage(index));
            }
        }
    }
       
}