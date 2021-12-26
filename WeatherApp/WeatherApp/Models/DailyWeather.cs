using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class DailyWeather
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public Current current { get; set; }
        public Daily[] daily { get; set; }
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
            public int win_deg { get; set; }
            public double win_gust { get; set; }
            public Weather[] weather { get; set; }
        }
        
        
        
    }

    public class Daily
    {
        public int dt { get; set; }
        public string datetime { get; set; }
        public string dateUTC { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }
        // create new variable 
        public Unit unit { get; set; }
        public string image { get; set; }
        public string sunsetText { get; set; }
        public string sunriseText { get; set; }
        public string wind_degText { get; set; }

        //
        public class Temp
        {
            public double day { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }
        public Temp temp { get; set; }
        public class Feels_like
        {
            public double day { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }
        public Feels_like feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public int clouds { get; set; }
        public double pop { get; set; }
        public double snow { get; set; }
        public double uvi { get; set; }
    }
}
