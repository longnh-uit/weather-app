using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

    using WeatherApp;
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
                    _ = connection.CreateTable<Location>();
                    _ = connection.CreateTable<Units>();
                    if (GetUnit().Count == 0)
                    {
                        _ = connection.Insert(new Units { tempUnitCurrent = "°C", distanceUnitCurrent = "m", speedUnitCurrent = "m/s", pressureUnitCurrent = "mBar", rainUnitCurrent = "mm" });

                    }
                    App.unit= GetUnit()[0];
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
        public List<Units> GetUnit()
        {
            try
            {
                // Create Database
                using (SQLiteConnection connection = new SQLiteConnection(path))
                    return connection.Table<Units>().ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public bool UpdateUnit(Units unit)
        {
            try
            {
                // Create Database
                using (SQLiteConnection connection = new SQLiteConnection(path))
                {
                    connection.Update(unit);
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

        public bool AddNewLocation(Location position)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "database.db");
                var connection = new SQLiteConnection(path);

                connection.Insert(position);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteLocation(string id)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "database.db");
                var connection = new SQLiteConnection(path);

                connection.Delete<Location>(id);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public List<Location> GetAllLocation()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "database.db");
                var connection = new SQLiteConnection(path);

                return connection.Table<Location>().ToList();

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<Location> GetOneLocation(string id)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "database.db");
                var connection = new SQLiteConnection(path);

                return connection.Query<Location>("select * from Hotel where _id=" + id);

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


    }
}
