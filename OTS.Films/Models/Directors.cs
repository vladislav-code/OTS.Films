using BLToolkit.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using BLToolkit.Mapping;
using BLToolkit.DataAccess;

namespace OTS.Films.Models
{
    [TableName("Directors")] // Указываем имя таблицы в базе данных
    public class Director
    {
        [PrimaryKey, Identity] // поля
        public int id { get; set; }
        [MapField("name")] // поля
        public string name { get; set; }
        [MapField("film_id")] // поля
        public int film_id { get; set; }
        //public List<Director> GetDirectors()
        //{
        //    List<Director> directors = new List<Director>();

        //    using (DbManager db = new DbManager())
        //    {
        //        var query = db.SetCommand("SELECT id, name FROM Directors");

        //        using (var reader = query.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                directors.Add(new Director
        //                {
        //                    id = (int)reader["id"],
        //                    name = reader["name"].ToString()
        //                });
        //            }
        //        }
        //    }
        //    return directors;
        //}

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