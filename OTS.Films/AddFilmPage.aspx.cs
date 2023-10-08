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
using BLToolkit.Data.Linq;

namespace OTS.Films
{
    public partial class AddFilmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Director director = new Director();
                //Genre genre = new Genre();
                using (DbManager db = new DbManager())
                {
                    try
                    {
                        List<Director> directors = db.GetTable<Director>().ToList(); // Получение всех фильмов
                                                                                     //List<Director> directors = director.GetDirectors();
                                                                                     // Удалить дубликаты из списка режиссеров
                        directors = directors.GroupBy(d => d.name).Select(g => g.First()).ToList();

                        lbDirectors.DataSource = directors; // здесь ошибка
                        lbDirectors.DataBind();

                        List<Genre> genres = db.GetTable<Genre>().ToList();
                        //List<Genre> genres = genre.GetGenres();
                        // Удалить дубликаты из списка жанров
                        genres = genres.GroupBy(g => g.name).Select(g => g.First()).ToList();

                        lbGenres.DataSource = genres;
                        lbGenres.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured " + ex.Message);
                    }
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
                try
                {
                    // try catch обработка
                    // проверка вставки такого же фильма

                    // Вставка фильма и получение его идентификатора
                    var film = new Film { title = filmTitle };
                    // int film_id = (int)db.InsertWithIdentity(film);
                    db.Insert(film);
                    int film_id = db.SetCommand("SELECT MAX(id) FROM Films;").ExecuteScalar<int>(); // получение id вставленного фильма

                    // SQL-запрос для вставки qданных
                    //var query = db.SetCommand("INSERT INTO Films (title) OUTPUT INSERTED.id VALUES (@title)", db.Parameter("@title", filmTitle));


                    //int film_id = (int)query.ExecuteScalar();
                    foreach (string selectedDirector in selectedDirectors)
                    {
                        // Обновление записи режиссера, если film_id равен null
                        string updateDirectorQuery = "UPDATE Directors SET film_id = @film_id WHERE id = @selectedDirector AND film_id IS NULL";
                        int updatedRows = db.SetCommand(updateDirectorQuery,
                            db.Parameter("@film_id", film_id),
                            db.Parameter("@selectedDirector", selectedDirector)
                        ).ExecuteNonQuery();

                        // Если обновление не затронуло ни одну запись, выполнение вставки новой записи
                        if (updatedRows == 0)
                        {
                            string directorName = lbDirectors.Items.FindByValue(selectedDirector).Text;
                            Director director = new Director
                            {
                                film_id = film_id,
                                name = directorName
                            };
                            db.Insert(director);
                        }
                    }
                    foreach (string selectedGenre in selectedGenres)
                    {
                        // Обновить запись жанра, если film_id равен null
                        string updateGenreQuery = "UPDATE Genres SET film_id = @film_id WHERE id = @selectedGenre AND film_id IS NULL";
                        int updatedRows = db.SetCommand(updateGenreQuery,
                            db.Parameter("@film_id", film_id),
                            db.Parameter("@selectedGenre", selectedGenre)
                        ).ExecuteNonQuery();

                        // Если обновление не затронуло ни одну запись, выполнить вставку новой записи
                        if (updatedRows == 0)
                        {
                            // string genreName = lbGenres.Items[Convert.ToInt32(selectedGenre)].Text;
                            string genreName = lbGenres.Items.FindByValue(selectedGenre).Text;
                            // Создайте новый объект Genre и заполните его свойства.
                            Genre genre = new Genre
                            {
                                film_id = film_id,
                                name = genreName
                            };

                            // Вставьте новую запись в таблицу "Genres".
                            db.Insert(genre);

                            //TODO: переделать с id на name поиск
                            //string genreName = lbGenres.Items[Convert.ToInt32(selectedGenre)].Text;
                            //string insertGenreQuery = "INSERT INTO Genres (film_id, name) VALUES (@film_id, @genreName)";
                            //db.SetCommand(insertGenreQuery,
                            //    db.Parameter("@film_id", film_id),
                            //    db.Parameter("@genreName", genreName)
                            //).ExecuteNonQuery();
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
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured " + ex.Message);
                }
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