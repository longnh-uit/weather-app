using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace WeatherApp.Models
{
    class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        
        
        public bool CreateDatebase()
        {
            try
            {
        
                
                // create db
                string path = System.IO.Path.Combine(folder, "Database.db");
                var connection = new SQLiteConnection(path);
                
                //create table
                connection.CreateTable<Variable>();

                connection.Insert(new Variable { bgColorValue= "#7097DA", bgColorID=100,bgColorName="backgroundColor"});

                return true;

            }
            catch (SQLiteException ex)
            {
                return false;
                throw;
            }
        }

        public List<Variable> GetBgColor(string bgColor)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "Database.db");
                var connection = new SQLiteConnection(path);
                return connection.Table<Variable>().ToList();
                //return connection.Query<Variable>("select * from Variable where bgColorName=" + bgColor);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
