using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace WeatherApp.Models
{
    public class Units
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string tempUnitCurrent { get; set; }
        public string distanceUnitCurrent { get; set; }
        public string speedUnitCurrent { get; set; }
        public string pressureUnitCurrent { get; set; }
        public string rainUnitCurrent { get; set; }
    }

}
