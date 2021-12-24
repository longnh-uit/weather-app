using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;
namespace WeatherApp.Helper
{
    public class ConvertUnit
    {

        public static WeatherInfo CurrentWeather(WeatherInfo data)
        {
            if (App.unit.tempUnitCurrent == "°F")
            {
                data.main.temp = data.main.temp * 1.8 + 32;
                data.main.temp_max = data.main.temp_max * 1.8 + 32;
                data.main.temp_min = data.main.temp_min * 1.8 + 32;
            }

            if (App.unit.distanceUnitCurrent == "km")
            {
                data.visibility = data.visibility / 1000;
            }

            if (App.unit.rainUnitCurrent == "mm")
            {
                data.main.temp = Math.Round(data.wind.speed * 0.0393700787);
            }

            switch (App.unit.speedUnitCurrent)
            {
                case "km/h":
                    data.wind.speed = data.wind.speed * 3.6;
                    break;
                case "ft/s":
                    data.wind.speed = Math.Round(data.wind.speed * 3.2808398950131);
                    break;
                case "mph":
                    data.wind.speed = Math.Round(data.wind.speed * 2.23693629);
                    break;
                default: data.wind.speed = data.wind.speed; break;
            }
            switch (App.unit.pressureUnitCurrent)
            {
                case "inHg":
                    data.main.pressure = (int)Math.Round(data.main.pressure * 0.02953);
                    break;
                case "psi":
                    data.main.pressure = (int)Math.Round(data.main.pressure * 0.0145037738);
                    break;
                case "bar":
                    data.main.pressure = data.main.pressure / 1000;
                    break;
                case "mmHg":
                    data.main.pressure = (int)Math.Round(data.main.pressure * 0.750061683);
                    break;
                default: data.wind.speed = data.wind.speed; break;
            }
            return data;
        }

        public static void DailyWeather(Daily data)
        {
            if (App.unit.tempUnitCurrent == "°F")
            {
                data.temp.max = data.temp.max * 1.8 + 32;
                data.temp.min = data.temp.min * 1.8 + 32;
            }

            //if (App.unit.distanceUnitCurrent == "km")
            //{
            //    data.visibility = data.visibility / 1000;
            //}

            //if (App.unit.rainUnitCurrent == "mm")
            //{
            //    data.main.temp = Math.Round(data.wind.speed * 0.0393700787);
            //}

            switch (App.unit.speedUnitCurrent)
            {
                case "km/h":
                    data.wind_speed = data.wind_speed * 3.6;
                    break;
                case "ft/s":
                    data.wind_speed = Math.Round(data.wind_speed * 3.2808398950131);
                    break;
                case "mph":
                    data.wind_speed = Math.Round(data.wind_speed * 2.23693629);
                    break;
                default: data.wind_speed = data.wind_speed; break;
            }
            switch (App.unit.pressureUnitCurrent)
            {
                case "inHg":
                    data.pressure = (int)Math.Round(data.pressure * 0.02953);
                    break;
                case "psi":
                    data.pressure = (int)Math.Round(data.pressure * 0.0145037738);
                    break;
                case "bar":
                    data.pressure = data.pressure / 1000;
                    break;
                case "mmHg":
                    data.pressure = (int)Math.Round(data.pressure * 0.750061683);
                    break;
                default: data.pressure = data.pressure; break;
            }
           
        }

        public static void HourlyWeather(Hourly data)
        {
            if (App.unit.tempUnitCurrent == "°F")
            {
                data.temp = data.temp * 1.8 + 32;  
            }

            //if (App.unit.distanceUnitCurrent == "km")
            //{
            //    data.visibility = data.visibility / 1000;
            //}

            //if (App.unit.rainUnitCurrent == "mm")
            //{
            //    data.main.temp = Math.Round(data.wind.speed * 0.0393700787);
            //}

            switch (App.unit.speedUnitCurrent)
            {
                case "km/h":
                    data.wind_speed = data.wind_speed * 3.6;
                    break;
                case "ft/s":
                    data.wind_speed = Math.Round(data.wind_speed * 3.2808398950131);
                    break;
                case "mph":
                    data.wind_speed = Math.Round(data.wind_speed * 2.23693629);
                    break;
                default: data.wind_speed = data.wind_speed; break;
            }
            switch (App.unit.pressureUnitCurrent)
            {
                case "inHg":
                    data.pressure = (int)Math.Round(data.pressure * 0.02953);
                    break;
                case "psi":
                    data.pressure = (int)Math.Round(data.pressure * 0.0145037738);
                    break;
                case "bar":
                    data.pressure = data.pressure / 1000;
                    break;
                case "mmHg":
                    data.pressure = (int)Math.Round(data.pressure * 0.750061683);
                    break;
                default: data.pressure = data.pressure; break;
            }

        }
    }
}
