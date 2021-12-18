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
        string[] locations = new string[] { "Hà Nội", "Luân Đôn", "Dubai" };
        public MainCarouselPage()
        {
            InitializeComponent();
            this.Children.Add(new MainPageDetail());
            txtName.Text = "Hà Nội";
            this.CurrentPageChanged += OnPageChanged;
        }
        public MainCarouselPage(Location location,int index)
        {

            InitializeComponent();
            InitMainpageDetail();
            txtName.Text = location.name;
            this.CurrentPage = this.Children[index];
            this.CurrentPageChanged += OnPageChanged;
        }

        public void InitMainpageDetail()
        {
            this.Children.Add(new MainPageDetail(105.8412, 21.0245)); // hà nội
            //foreach (string name in locations)
            //{
                
            //    this.Children.Add(new MainPageDetail(name));
            //}
        }
         public void OnPageChanged(object sender, EventArgs e )
        {
            int index = Children.IndexOf(CurrentPage);
            txtName.Text = locations[index].ToString();
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