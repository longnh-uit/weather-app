using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace WeatherApp.Models
{
    public class HistoryWeather
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public class Current
        {
            public long dt { get; set; }
            public long sunrise { get; set; }
            public long sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public int cloud { get; set; }
            public double uvi { get; set; }
            public int visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_gust { get; set; }
            public int wind_deg { get; set; }
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
            public Weather[] weather { get; set; }
        }
        public Current current { get; set; }
        public class Hourly
        {
            public long dt { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public int cloud { get; set; }
            public int visibility { get; set; }
            public double wind_speed { get; set; }
            public double wind_gust { get; set; }
            public int wind_deg { get; set; }
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
            public Weather[] weather { get; set; }
            //add
            public string time { get; set; }
            public string image { get; set; }
        }
        public Hourly[] hourly { get; set; }
    }
}
