﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public static async Task<ApiResponse> PostContact(string email, string name, string msg)
        {
            using (var client = new HttpClient())
            {
                var info = new
                {
                    email = email,
                    name = name,
                    message = msg,
                };

                string url = "https://chito-stationery.herokuapp.com/contact/add";
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
