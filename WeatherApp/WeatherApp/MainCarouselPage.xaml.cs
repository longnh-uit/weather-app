using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCarouselPage : CarouselPage
    {

        public MainCarouselPage()
        {
            InitializeComponent();
            InitMainpageDetail();
            txtName.Text = "0";
            //OnCurrentPageChanged();
            this.CurrentPageChanged += OnPageChanged;
        }
        public MainCarouselPage(int index)
        {

            InitializeComponent();
            txtName.Text = index.ToString();
            this.Children.Add(new MainPageDetail());
            this.Children.Add(new MainPageDetail());
            this.Children.Add(new MainPageDetail());
            this.CurrentPage = this.Children[index];
            this.CurrentPageChanged += OnPageChanged;
        }

        public void InitMainpageDetail()
        {
            for (int i = 0; i < 5; i++)
            {
                this.Children.Add(new MainPageDetail());
            }
        }
         public void OnPageChanged(object sender, EventArgs e )
        {
            int index = Children.IndexOf(CurrentPage);
            txtName.Text = index.ToString();
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