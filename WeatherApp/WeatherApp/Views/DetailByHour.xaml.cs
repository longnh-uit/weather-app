using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailByHour : ContentPage
    {
        public DetailByHour()
        {
            InitializeComponent();
            
        }
        public DetailByHour(List<Hourly> allList)
        {
            InitializeComponent();
            listItemHour.ItemsSource = allList;
        }
       

        private void listItemHour_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            list.SelectedItem = null;
        }
    }
}