using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;
namespace WeatherApp.Helper
{
    public class ApiCaller
    {
        public static async Task<ApiResponse> Get(string url)
        {
            using (var client = new HttpClient())
            {
                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                    return new ApiResponse { ErrorMessage = request.ReasonPhrase };
            }
        }

        public static async Task<ApiResponse> PostContact(Contact info)
        {
            using (var client = new HttpClient())
            {
                string url = "http://www.xamarinweatherapi.somee.com/api/contact/";
                StringContent content = new StringContent(JsonConvert.SerializeObject(info), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await response.Content.ReadAsStringAsync() };
                }
                else
                    return new ApiResponse { ErrorMessage = response.ReasonPhrase };
            }

        }
    }




    public class ApiResponse
    {
        public bool Successful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
