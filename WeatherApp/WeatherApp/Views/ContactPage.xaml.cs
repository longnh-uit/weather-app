using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
        
        public List<KeyValuePair<string, string>> error = new List<KeyValuePair<string, string>>()
        {

        };
        public bool validate(string email, string name, string msg)
        {

            var emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
            if (email.Length <= 0)
            {
                error.Insert(0, new KeyValuePair<string, string>("email", "Nhập họ và tên"));              
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                error.Insert(0, new KeyValuePair<string, string>("email", "Email không hợp lệ"));              
            }
            if (name.Length <= 0)
            {
                error.Insert(1, new KeyValuePair<string, string>("name", "Nhập họ và tên"));
          
            }
            if (msg.Length <= 0)
            {
                error.Insert(2, new KeyValuePair<string, string>("message", "Nhập nội dung"));        
            }
            if (error.Count > 0) return false;
            return true;
        }
        async void submmitBtn_Clicked(object sender, EventArgs e)
        {
            bool check = validate(emailTxt.Text, nameTxt.Text, msgTxt.Text);
            if (!check)
            {
                errorEmail.Text = error[0].Value;
                errorName.Text = error[1].Value;
                errorMsg.Text = error[2].Value;
                return;
            }
            else
            {
                Contact info = new Contact
                {
                    email = emailTxt.Text,
                    name = nameTxt.Text,
                    message = msgTxt.Text,
                };
                var result = await ApiCaller.PostContact(info);
                if (result.Successful)
                {
                    Notify notify = JsonConvert.DeserializeObject<Notify>(result.Response);
                    await DisplayAlert("Thông báo", notify.msg, "OK");
                    emailTxt.Text = "";
                    nameTxt.Text = "";
                    msgTxt.Text = "";
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đã có lỗi xảy ra. Vui lòng thử lại", "OK");
                }
            }
        }
    }
}