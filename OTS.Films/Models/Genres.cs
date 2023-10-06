using BLToolkit.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace OTS.Films.Models
{
    public class GenreRepository
    {
        private string connectionString;

        public GenreRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<string> GetGenres()
        {
            List<string> genres = new List<string>();

            DbManager.AddConnectionString(connectionString);
            using (DbManager db = new DbManager())
            {
                var query = db.SetCommand("SELECT name FROM Genres");
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(reader["name"].ToString());
                    }
                }
            }

            return genres;
        }
    }
}