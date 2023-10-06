using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTS.Films.Models;
using BLToolkit.Data;
using System.Data;
using BLToolkit.DataAccess;

namespace OTS.Films
{
    public partial class AddFilmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectorRepository directorRepo = new DirectorRepository(); //Замена
            List<string> directors = directorRepo.GetDirectors();

            ddlDirectors.DataSource = directors; // здесь ошибка
            ddlDirectors.DataBind();

            GenreRepository genreRepo = new GenreRepository();  //Замена
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
            string selectedDirector = ddlDirectors.SelectedValue;
            string[] selectedGenres = lbGenres.GetSelectedIndices()
                                              .Select(i => lbGenres.Items[i].Value)
                                              .ToArray();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Создание подключения к базе данных и выполнение операции вставки
            DbManager.AddConnectionString(connectionString);

            using (DbManager db = new DbManager())
            {
                // Вставка фильма и получение его идентификатора
                //var film = new Film { Title = filmTitle };
                //db.Insert(film);

                // SQL-запрос для вставки данных
                var query = db.SetCommand("INSERT INTO Films (title) OUTPUT INSERTED.id VALUES (@title)", db.Parameter("@title", filmTitle));
                int film_id = (int)query.ExecuteScalar();
                var query1 = db.SetCommand("UPDATE Directors SET film_id = @film_id WHERE name = @selectedDirector", db.Parameter("@film_id", film_id), db.Parameter("@selectedDirector", selectedDirector)).ExecuteNonQuery();
                var query2 = db.SetCommand("INSERT INTO Genres (film_id) VALUES (@film_id)", db.Parameter("@film_id", film_id)).ExecuteNonQuery();

                //using (SqlCommand cmd = new SqlCommand(query, connection))
                //{
                //    // параметры для предотвращения SQL-инъекций
                //    cmd.Parameters.AddWithValue("@Title", filmTitle);

                //    // Выполнение запроса
                //    cmd.ExecuteNonQuery();
                //}
            }

            // Очистка элементов управления после вставки
            txtFilmTitle.Text = string.Empty;
            ddlDirectors.ClearSelection();
            lbGenres.ClearSelection();

            // Закрыть соединение???
            // Сообщение об операции
        }
    }
}