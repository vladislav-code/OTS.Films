using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTS.Films.Models;

namespace OTS.Films
{
    public partial class AddFilmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectorRepository directorRepo = new DirectorRepository();
            List<string> directors = directorRepo.GetDirectors();

            ddlDirectors.DataSource = directors; // здесь ошибка
            ddlDirectors.DataBind();

            GenreRepository genreRepo = new GenreRepository();
            List<string> genres = genreRepo.GetGenres();

            lbGenres.DataSource = genres;
            lbGenres.DataBind();
        }
        protected void btnAddFilm_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Получение данных из элементов управления
            string filmTitle = txtFilmTitle.Text;
            //string genreTitle = txtGenreTitle.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Создание подключения к базе данных и выполнение операции вставки
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для вставки данных
                string query = "INSERT INTO Films (Title) VALUES (@Title)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // параметры для предотвращения SQL-инъекций
                    cmd.Parameters.AddWithValue("@Title", filmTitle);

                    // Выполнение запроса
                    cmd.ExecuteNonQuery();
                }
            }

            // Очистка элементов управления после вставки
            txtFilmTitle.Text = string.Empty;

            // Закрыть соединение???
            // Сообщение об операции
        }
    }
}