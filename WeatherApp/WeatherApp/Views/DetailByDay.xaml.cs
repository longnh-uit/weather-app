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
    public partial class DetailByDay : ContentPage
    {
        public DetailByDay()
        {
            InitializeComponent();
            
        }

        public DetailByDay(List<Daily> allList)
        {
            InitializeComponent();
            //ItemsAdded();
            listItemDay.ItemsSource = allList;
        }
        
        
        
        private void listItemDay_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            list.SelectedItem = null;
        }
    }
}