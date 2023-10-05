using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.EntityFrameworkCore;
using OTS.Films.Models;
using Microsoft.SqlServer;
using System.Data.SqlClient;
using System.Data;

namespace OTS.Films
{
    public partial class CReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GenreRepository genreRepo = new GenreRepository();

                List<string> genres = new List<string>();
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                DataTable dt = new DataTable();
                dt.Columns.Add("Название", typeof(string));
                dt.Columns.Add("Количество", typeof(int));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT name AS Название, COUNT(film_id) AS Количество FROM Genres GROUP BY name";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        dt.Rows.Add(reader["Название"].ToString(), Convert.ToInt32(reader["Количество"]));
                    }

                    reader.Close();
                }

                MyRepeater.DataSource = dt;
                MyRepeater.DataBind();
            }
        }
    }
}