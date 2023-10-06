﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.EntityFrameworkCore;
using OTS.Films.Models;
using Microsoft.SqlServer;
using System.Data;
using BLToolkit.Data;

namespace OTS.Films
{
    public partial class DirectorFilms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


                DataTable dt = new DataTable();
                dt.Columns.Add("Имя", typeof(string));
                dt.Columns.Add("Жанр", typeof(string));
                dt.Columns.Add("Количество", typeof(int));
                dt.Columns.Add("rowspan", typeof(int));
                DbManager.AddConnectionString(connectionString);

                using (DbManager db = new DbManager())
                {

                    var query = db.SetCommand("SELECT Directors.name AS Имя, Genres.name AS Жанр, COUNT(Films.id) AS Количество FROM Directors INNER JOIN (Films INNER JOIN Genres ON Films.id = Genres.film_id) ON Directors.film_id = Films.id GROUP BY Directors.name, Genres.name;");
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader["Имя"].ToString(), reader["Жанр"].ToString(), Convert.ToInt32(reader["Количество"]));
                        }
                    }
                }

                int rowCount = dt.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    int rowspan = 1; // Изначально устанавливаем rowspan в 1

                    // Сравниваем текущее значение "Имя" с предыдущими строками
                    for (int j = i + 1; j < rowCount; j++)
                    {
                        // проверить объединение последней
                        if (dt.Rows[i]["Имя"].ToString() == dt.Rows[j]["Имя"].ToString())
                        {
                            rowspan++;
                        }
                        else
                        {
                            break; // Прерываем цикл, если нашли различие
                        }
                    }

                    // Устанавливаем значение rowspan в DataTable
                    if (i != 0 && Convert.ToInt32(dt.Rows[i - 1]["rowspan"]) > rowspan && dt.Rows[i-1]["Имя"].ToString() == dt.Rows[i]["Имя"].ToString())
                    {
                        for (int row = 0; row < rowspan; row++)
                            dt.Rows[i + row]["rowspan"] = 0;
                        i += rowspan - 1;
                    }
                    else dt.Rows[i]["rowspan"] = rowspan;
                }
                // менять значения сравнивая с предыдущим 1-2 -> 2-1-3 -> 3-1-2
                MyRepeater.DataSource = dt;
                MyRepeater.DataBind();
            }
        }
    }
}

//using System;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Web.UI;
//using BLToolkit.Data;
//using BLToolkit.Data.Linq;
//using OTS.Films.Models;

//namespace OTS.Films
//{
//    public partial class DirectorFilms : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

//                using (DbManager db = new DbManager(connectionString))
//                {
//                    var query = from director in db.GetTable<Director>()
//                                join film in db.GetTable<Film>() on director.FilmID equals film.ID
//                                join genre in db.GetTable<Genre>() on film.ID equals genre.FilmID
//                                group new { director, genre } by new { director.Name, genre.Name } into grouped
//                                select new
//                                {
//                                    Name = grouped.Key.Name,
//                                    Genre = grouped.Key.Name,
//                                    Count = grouped.Count()
//                                };

//                    DataTable dt = new DataTable();
//                    dt.Columns.Add("Имя", typeof(string));
//                    dt.Columns.Add("Жанр", typeof(string));
//                    dt.Columns.Add("Количество", typeof(int));
//                    dt.Columns.Add("rowspan", typeof(int));

//                    foreach (var item in query)
//                    {
//                        dt.Rows.Add(item.Name, item.Genre, item.Count);
//                    }

//                    int rowCount = dt.Rows.Count;
//                    for (int i = 0; i < rowCount; i++)
//                    {
//                        int rowspan = 1; // Изначально устанавливаем rowspan в 1

//                        // Сравниваем текущее значение "Имя" с предыдущими строками
//                        for (int j = i + 1; j < rowCount; j++)
//                        {
//                            // проверить объединение последней
//                            if (dt.Rows[i]["Имя"].ToString() == dt.Rows[j]["Имя"].ToString())
//                            {
//                                rowspan++;
//                            }
//                            else
//                            {
//                                break; // Прерываем цикл, если нашли различие
//                            }
//                        }

//                        // Устанавливаем значение rowspan в DataTable
//                        if (i != 0 && Convert.ToInt32(dt.Rows[i - 1]["rowspan"]) > rowspan && dt.Rows[i - 1]["Имя"].ToString() == dt.Rows[i]["Имя"].ToString())
//                        {
//                            for (int row = 0; row < rowspan; row++)
//                                dt.Rows[i + row]["rowspan"] = 0;
//                            i += rowspan - 1;
//                        }
//                        else dt.Rows[i]["rowspan"] = rowspan;
//                    }

//                    MyRepeater.DataSource = dt;
//                    MyRepeater.DataBind();
//                }
//            }
//        }
//    }
//}
