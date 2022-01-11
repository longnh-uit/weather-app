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
                var result = error.Where(item => item.Key == "email").ToList();
                if (result.Count == 0)
                {
                    //error.Insert(0, new KeyValuePair<string, string>("email", "Nhập email"));
                    error.Add(new KeyValuePair<string, string>("email", "Nhập email"));
                }
                else
                {
                    error.RemoveAll(x => x.Key.Equals("email"));
                    //error.Insert(0, new KeyValuePair<string, string>("email", "Nhập email"));
                    error.Add(new KeyValuePair<string, string>("email", "Nhập email"));
                }
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                var result = error.Where(item => item.Key == "email").ToList();
                if (result.Count == 0)
                {
                    error.Add(new KeyValuePair<string, string>("email", "Email không hợp lệ"));
                    //error.Add(new KeyValuePair<string, string>("email", "Nhập email"));
                }
                else
                {
                    error.RemoveAll(x => x.Key.Equals("email"));
                    error.Add(new KeyValuePair<string, string>("email", "Email không hợp lệ"));
                }
            }
            else
            {
                error.RemoveAll(x => x.Key.Equals("email"));
            }
            if (name.Length <= 0)
            {
                var result = error.Where(item => item.Key == "name").ToList();
                if (result.Count == 0)
                {
                    //error.Insert(0, new KeyValuePair<string, string>("email", "Nhập email"));
                    error.Add(new KeyValuePair<string, string>("name", "Nhập họ và tên"));
                    //error.Add(new KeyValuePair<string, string>("email", "Nhập email"));
                }

            }
            else
            {
                error.RemoveAll(x => x.Key.Equals("name"));
            }
            if (msg.Length <= 0)
            {
                var result = error.Where(item => item.Key == "message").ToList();
                if (result.Count == 0)
                {
                    //error.Insert(0, new KeyValuePair<string, string>("email", "Nhập email"));
                    //error.Insert(1, new KeyValuePair<string, string>("name", "Nhập họ và tên"));
                    error.Add(new KeyValuePair<string, string>("message", "Nhập nội dung"));
                    //error.Add(new KeyValuePair<string, string>("email", "Nhập email"));
                }
            }
            else
            {
                error.RemoveAll(x => x.Key.Equals("message"));

            }
            if (error.Count > 0) return false;
            return true;
        }
        async void submmitBtn_Clicked(object sender, EventArgs e)
        {
            bool check = validate(emailTxt.Text, nameTxt.Text, msgTxt.Text);
            if (check == false)
            {
                var resultEmail = error.Where(item => item.Key == "email").ToList();
                var resultName = error.Where(item => item.Key == "name").ToList();
                var resultMsg = error.Where(item => item.Key == "message").ToList();
                if (resultEmail.Count == 1)
                {
                    var v = error.FirstOrDefault(x => x.Key == "email");
                    errorEmail.Text = v.Value;
                }
                else
                {
                    errorEmail.Text = "";
                }
                if (resultName.Count == 1)
                {
                    var v = error.FirstOrDefault(x => x.Key == "name");
                    errorName.Text = v.Value;
                }
                else
                {
                    errorName.Text = "";
                }
                if (resultMsg.Count == 1)
                {
                    var v = error.FirstOrDefault(x => x.Key == "message");
                    errorMsg.Text = v.Value;
                }
                else
                {
                    errorMsg.Text = "";
                }
                return;
            }
            else
            {
                errorEmail.Text = "";
                errorName.Text = "";
                errorMsg.Text = "";
                Contact info = new Contact
                {
                    email = emailTxt.Text,
                    name = nameTxt.Text,
                    message = msgTxt.Text,
                };
                var result = await ApiCaller.PostContact(info);
                if (result.Successful)
                {
                    string notify = result.Response;
                    await DisplayAlert("Thông báo", notify, "OK");
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