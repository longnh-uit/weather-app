using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    internal interface IWeather
    {
        double TempMin { get; set; }
        double TempMax { get; set; }
        double CurrentTemp { get; set; }
        double WindSpeed { get; set; }
        double Humidity { get; set; }
        double Pressure { get; set; }
        DateTime SunRise { get; set; }
        DateTime SunSet { get; set; }
        DateTime Date { get; set; }
        double Visibility { get; set; }
        string Description { get; set; }
        string Icon { get; set; }
    }
}
