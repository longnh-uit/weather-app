using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Helper;
using Newtonsoft.Json;
using WeatherApp.Models;
namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        async void submmitBtn_Clicked(object sender, EventArgs e)
        {
            var result = await ApiCaller.PostContact(emailTxt.Text, nameTxt.Text, msgTxt.Text);
            if (result.Successful)
            {
                Notify notify = JsonConvert.DeserializeObject<Notify>(result.Response);
                await DisplayAlert("Thông báo", notify.msg, "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Đã có lỗi xảy ra. Vui lòng thử lại", "OK");
            }
        }
    }
}