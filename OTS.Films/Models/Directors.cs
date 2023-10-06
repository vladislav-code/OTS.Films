using BLToolkit.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace OTS.Films.Models
{
    public class DirectorRepository
    {
        private string connectionString;

        public DirectorRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<string> GetDirectors()
        {
            List<string> directors = new List<string>();

            DbManager.AddConnectionString(connectionString);
            using (DbManager db = new DbManager())
            {
                var query = db.SetCommand("SELECT name FROM Directors");

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        directors.Add(reader["name"].ToString());
                    }
                }
            }
            return directors;
        }
    }
}