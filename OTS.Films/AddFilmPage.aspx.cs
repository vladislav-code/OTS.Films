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
            if (!IsPostBack)
            {
                Director director = new Director();
                using (DbManager db = new DbManager())
                {
                    List<Director> directors = db.GetTable<Director>().ToList(); // Получение всех фильмов
                    //List<Director> directors = director.GetDirectors();
                    // Удалить дубликаты из списка режиссеров
                    directors = directors.GroupBy(d => d.name).Select(g => g.First()).ToList();

                    lbDirectors.DataSource = directors; // здесь ошибка
                    lbDirectors.DataBind();

                    Genre genre = new Genre();
                    List<Genre> genres = genre.GetGenres();
                    // Удалить дубликаты из списка жанров
                    genres = genres.GroupBy(g => g.name).Select(g => g.First()).ToList();

                    lbGenres.DataSource = genres;
                    lbGenres.DataBind();
                }
            }
        }
        protected void btnAddFilm_Click(object sender, EventArgs e)
        {
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Получение данных из элементов управления
            string filmTitle = txtFilmTitle.Text;
            //List<Director> selectedDirectors = new List<Director>();
            //foreach (ListItem item in lbDirectors.Items)
            //{
            //    if (item.Selected)
            //    {
            //        selectedDirectors.Add(item.Value);
            //    }
            //}
            // Value = id
            string[] selectedDirectors = lbDirectors.GetSelectedIndices()
                                              .Select(i => lbDirectors.Items[i].Value)
                                              .ToArray();
            // Получение выбранных режиссеров
            //List<int> selectedDirectors = new List<int>();
            //foreach (ListItem item in lbDirectors.Items)
            //{
            //    if (item.Selected)
            //    {
            //        int directorID;
            //        if (int.TryParse(item.Value, out directorID))
            //        {
            //            selectedDirectors.Add(directorID);
            //        }
            //    }
            //}
            string[] selectedGenres = lbGenres.GetSelectedIndices()
                                              .Select(i => lbGenres.Items[i].Value)
                                              .ToArray();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Создание подключения к базе данных и выполнение операции вставки
            DbManager.AddConnectionString(connectionString);

            using (DbManager db = new DbManager())
            {
                // try catch обработка
                // проверка вставки такого же фильма

                // Вставка фильма и получение его идентификатора
                //var film = new Film { Title = filmTitle };
                //db.Insert(film);

                // SQL-запрос для вставки данных
                var query = db.SetCommand("INSERT INTO Films (title) OUTPUT INSERTED.id VALUES (@title)", db.Parameter("@title", filmTitle));


                int film_id = (int)query.ExecuteScalar();
                foreach (string selectedDirector in selectedDirectors)
                {
                    // Обновить запись режиссера, если film_id равен null
                    string updateDirectorQuery = "UPDATE Directors SET film_id = @film_id WHERE id = @selectedDirector AND film_id IS NULL";
                    int updatedRows = db.SetCommand(updateDirectorQuery,
                        db.Parameter("@film_id", film_id),
                        db.Parameter("@selectedDirector", selectedDirector)
                    ).ExecuteNonQuery();

                    // Если обновление не затронуло ни одну запись, выполнить вставку новой записи
                    if (updatedRows == 0)
                    {
                        string insertGenreQuery = "INSERT INTO Directors (film_id, name) VALUES (@film_id, @selectedDirector)";
                        db.SetCommand(insertGenreQuery,
                            db.Parameter("@film_id", film_id),
                            db.Parameter("@selectedDirector", selectedDirector)
                        ).ExecuteNonQuery();
                    }
                }
                foreach (string selectedGenre in selectedGenres)
                {
                    // Обновить запись жанра, если film_id равен null
                    string updateDirectorQuery = "UPDATE Genres SET film_id = @film_id WHERE id = @selectedGenre AND film_id IS NULL";
                    int updatedRows = db.SetCommand(updateDirectorQuery,
                        db.Parameter("@film_id", film_id),
                        db.Parameter("@selectedGenre", selectedGenre)
                    ).ExecuteNonQuery();

                    // Если обновление не затронуло ни одну запись, выполнить вставку новой записи
                    if (updatedRows == 0)
                    {
                        //TODO: переделать с id на name поиск
                        string genreName = lbGenres.Items[Convert.ToInt32(selectedGenre)].Text;
                        string insertGenreQuery = "INSERT INTO Genres (film_id, name) VALUES (@film_id, @genreName)";
                        db.SetCommand(insertGenreQuery,
                            db.Parameter("@film_id", film_id),
                            db.Parameter("@genreName", genreName)
                        ).ExecuteNonQuery();
                    }
                }
               
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
            lbDirectors.ClearSelection();
            lbGenres.ClearSelection();

            // Закрыть соединение???
            // Сообщение об операции
        }
    }
}