using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WeatherApp.Models
{
     public class HourlyWeather
     {
        public double lat { get; set; }
        public double lon { get; set; }
        public Current current { get; set; }
        public Hourly[] hourly { get; set; }
        public class Current
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public double uvi { get; set; }
            public int cloud { get; set; }
            public int visibility { get; set; }
            public int wind_deg { get; set; }
            public double wind_gust { get; set; }
            public Weather[] weather { get; set; }
        }
        
        public class Alert
        {
            public string sender_name { get; set; }
            [JsonProperty("event")]
            public string event_name { get; set; }
            public int start { get; set; }
            public int end { get; set; }
            public string description { get; set; }
            public string[] tags { get; set; }
        }
     }

    public class Hourly
    {
        public int dt { get; set; }
        public string dateUTC { get; set; }
        public string time { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public int clouds { get; set; }
        public double pop { get; set; }
        // create  new variable
        public Unit unit { get; set; }
        public string image { get; set; }
        public string temperature { get; set; }
        public string rainability { get; set; }
        public string rainAmountText { get; set; }
        public string wind_degText { get; set; }
        public string visibilityText { get; set; }
        public string humidityText { get; set; }
        //
        public class Rain
        {
            [JsonProperty("1h")]
            public double _1h { get; set; }
        }
        public Rain rain { get; set; }
        public class Snow
        {
            [JsonProperty("1h")]
            public double _1h { get; set; }
        }
        public Snow snow { get; set; }
        public double uvi { get; set; }
    }
}
