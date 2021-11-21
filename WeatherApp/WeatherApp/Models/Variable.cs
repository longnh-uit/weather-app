using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace WeatherApp.Models
{
    public class Variable
    {
        [PrimaryKey]
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
    }
}
