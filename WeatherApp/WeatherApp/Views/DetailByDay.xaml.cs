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
    public partial class DetailByDay : ContentPage
    {
        public DetailByDay()
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
            listItemDay.ItemsSource = itemsList;
        }

        private void listItemDay_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            list.SelectedItem = null;
        }
    }
}