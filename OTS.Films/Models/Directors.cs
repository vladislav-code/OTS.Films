using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM Directors";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    directors.Add(reader["name"].ToString());
                }

                reader.Close();
            }

            return directors;
        }
    }
}