using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM Genres";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(reader["name"].ToString());
                }

                reader.Close();
            }

            return genres;
        }
    }
}