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
    public partial class MainCarouselPage : CarouselPage
    {
        private static Database db = new Database();
        List<Location> locations = db.GetAllLocation();

        Location Hanoi = new Location
        {
            _id = "123",
            name = "Hà Nội",
            lon = 105.8412,
            lat = 21.0245

        };

        public MainCarouselPage()
        {
            InitializeComponent();
            this.Children.Add(new MainPageDetail());
            txtName.Text = "Hà Nội";
            this.CurrentPageChanged += OnPageChanged;
        }
        public MainCarouselPage(Location location, int index)
        {

            InitializeComponent();
            InitMainpageDetail();
            txtName.Text = location.name;
            this.CurrentPage = this.Children[index];
            this.CurrentPageChanged += OnPageChanged;
        }

        public void InitMainpageDetail()
        {
            this.Children.Add(new MainPageDetail(Hanoi)); // hà nội
            if (locations != null)
            { 
                foreach (Location position in locations)
                {
                    this.Children.Add(new MainPageDetail(position));
                }
            }
        }
        public void OnPageChanged(object sender, EventArgs e)
        {
            int index = Children.IndexOf(CurrentPage);
            if (index > 0)
            {

            txtName.Text = locations[index-1].name.ToString();
            }
            else { txtName.Text = Hanoi.name; }
            this.CurrentPage = this.Children[index];

        }


        //public void PageRight(this CarouselPage carouselPage)
        //{
        //    var pageCount = carouselPage.Children.Count;
        //    if (pageCount < 2)
        //        return;

        //    var index = carouselPage.Children.IndexOf(carouselPage.CurrentPage);
        //    index++;
        //    if (index >= pageCount)
        //        index = 0;

        //    carouselPage.CurrentPage = carouselPage.Children[index];
        //}

    }
}