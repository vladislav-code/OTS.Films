//using BLToolkit.Data;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;

//namespace OTS.Films.Models
//{
//    public class GenreRepository
//    {
//        private string connectionString;

//        public GenreRepository()
//        {
//            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//        }

//        public List<string> GetGenres()
//        {
//            List<string> genres = new List<string>();

//            DbManager.AddConnectionString(connectionString);
//            using (DbManager db = new DbManager())
//            {
//                var query = db.SetCommand("SELECT name FROM Genres");
//                using (var reader = query.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        genres.Add(reader["name"].ToString());
//                    }
//                }
//            }

//            return genres;
//        }
//    }
//}

using BLToolkit.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace OTS.Films.Models
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            using (DbManager db = new DbManager())
            {
                var query = db.SetCommand("SELECT id, name FROM Genres");

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            id = (int)reader["id"],
                            name = reader["name"].ToString()
                        });
                    }
                }
            }
            return genres;
        }

    }
}

//    public class Director
//    {
//        // private string connectionString;
//        private int id { get; set; }
//        private string name { get; set; }
//        private int film_id { get; set; }
//        public Director()
//        {
//            // connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//        }

//        public List<string> GetDirectors()
//        {
//            List<Director> directors = new List<Director>();

//            // DbManager.AddConnectionString(connectionString);
//            using (DbManager db = new DbManager())
//            {
//                var query = db.SetCommand("SELECT name FROM Directors");

//                using (var reader = query.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        directors.id
//                    }
//                }
//            }
//            return directors;
//        }
//    }
//}