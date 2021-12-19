using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WeatherApp.Models
{
    public class Location
    {
        [PrimaryKey]
        public string _id { get; set; }
        public string name { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        
    }
}
