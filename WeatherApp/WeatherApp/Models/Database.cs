using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WeatherApp.Models
{
    public class Database
    {
        private readonly string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private string path;

        public bool CreateDatebase()
        {
            try
            {
                // Create Database
                path = System.IO.Path.Combine(folder, "database.db");
                using (SQLiteConnection connection = new SQLiteConnection(path))
                {
                    //create table
                    _ = connection.CreateTable<Variable>();
                    //_ = connection.Update(new Variable { VariableName = "backgroundColor", VariableValue = "#7097DA" });
                    _ = connection.Insert(new Variable { VariableName = "backgroundColor", VariableValue = "#7097DA" });
                    return true;
                }
            }
            catch (SQLiteException)
            {
                return false;
                throw;
            }
        }

        public Variable GetBgColor()
        {
            try
            {
                // Create Database
                using (SQLiteConnection connection = new SQLiteConnection(path))
                    return connection.Table<Variable>().FirstOrDefault(x => x.VariableName == "backgroundColor");
                //return connection.Query<Variable>("select * from Variable where bgColorName=" + bgColor);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public bool UpdateBgColor(Variable color)
        {
            try
            {
                // Create Database
                using (SQLiteConnection connection = new SQLiteConnection(path))
                {
                    connection.Update(color);
                    return true;
                }
                //return connection.Query<Variable>("select * from Variable where bgColorName=" + bgColor);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
