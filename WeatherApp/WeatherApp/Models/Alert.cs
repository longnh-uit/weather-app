using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WeatherApp.Models
{
    public class Alert
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public class AlertClass
        {
            public string sender_name { get; set; }
            [JsonProperty("event")]
            public string event_name { get; set; }
            public int start { get; set; }
            public int end { get; set; }
            public string description { get; set; }
            public string[] tags { get; set; }
        }
        public AlertClass[] alerts;
    }
}
