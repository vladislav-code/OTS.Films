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
                using (DbManager db = new DbManager())
                {
                    try
                    {
                        List<Director> directors = db.GetTable<Director>().ToList(); // Получение всех фильмов
                        // Удалить дубликаты из списка режиссеров
                        directors = directors.GroupBy(d => d.name).Select(g => g.First()).ToList();

                        lbDirectors.DataSource = directors;
                        lbDirectors.DataBind();

                        List<Genre> genres = db.GetTable<Genre>().ToList();
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
            string[] selectedDirectors = lbDirectors.GetSelectedIndices()
                                              .Select(i => lbDirectors.Items[i].Value)
                                              .ToArray();
            string[] selectedGenres = lbGenres.GetSelectedIndices()
                                              .Select(i => lbGenres.Items[i].Value)
                                              .ToArray();

            using (DbManager db = new DbManager())
            {
                try
                {
                    // проверка вставки такого же фильма

                    // Вставка фильма и получение его идентификатора
                    var film = new Film { title = filmTitle };
                    db.Insert(film);
                    int film_id = db.SetCommand("SELECT MAX(id) FROM Films;").ExecuteScalar<int>();

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
                            string genreName = lbGenres.Items.FindByValue(selectedGenre).Text;
                            Genre genre = new Genre
                            {
                                film_id = film_id,
                                name = genreName
                            };

                            db.Insert(genre);

                            //TODO: переделать с id на name поиск
                        }
                    }
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
        }
    }
}