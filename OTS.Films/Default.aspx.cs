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
using System.Data;
using BLToolkit.Data;

namespace OTS.Films
{
    public  partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Название", typeof(string));
                dt.Columns.Add("Режиссер", typeof(string));
                dt.Columns.Add("Жанр", typeof(string));
                dt.Columns.Add("rowspan", typeof(int));
                dt.Columns.Add("rowspan1", typeof(int));
                //dt.Columns.Add("rowspan2", typeof(int));

                using (DbManager db = new DbManager())
                {
                    try
                    {
                        var query = db.SetCommand("SELECT Films.title AS Название, Directors.name AS Режиссер, Genres.name AS Жанр FROM Directors INNER JOIN (Films INNER JOIN Genres ON Films.id = Genres.film_id) ON Directors.film_id = Films.id GROUP BY Films.title, Directors.name, Genres.name;");
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader["Название"].ToString(), reader["Режиссер"].ToString(), reader["Жанр"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured " + ex.Message);
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
                        if (dt.Rows[i]["Название"].ToString() == dt.Rows[j]["Название"].ToString())
                        {
                            rowspan++;
                        }
                        else
                        {
                            break; // Прерываем цикл, если нашли различие
                        }
                    }

                    // Устанавливаем значение rowspan в DataTable
                    if (i != 0 && Convert.ToInt32(dt.Rows[i - 1]["rowspan"]) > rowspan && dt.Rows[i - 1]["Название"].ToString() == dt.Rows[i]["Название"].ToString())
                    {
                        for (int row = 0; row < rowspan; row++)
                            dt.Rows[i + row]["rowspan"] = 0;
                        i += rowspan - 1;
                    }
                    else dt.Rows[i]["rowspan"] = rowspan;
                }

                for (int i = 0; i < rowCount; i++)
                {
                    int rowspan = 1; // Изначально устанавливаем rowspan в 1

                    // Сравниваем текущее значение "Имя" с предыдущими строками
                    for (int j = i + 1; j < rowCount; j++)
                    {
                        // проверить объединение последней
                        if (dt.Rows[i]["Режиссер"].ToString() == dt.Rows[j]["Режиссер"].ToString())
                        {
                            rowspan++;
                        }
                        else
                        {
                            break; // Прерываем цикл, если нашли различие
                        }
                    }

                    // Устанавливаем значение rowspan в DataTable
                    if (i != 0 && Convert.ToInt32(dt.Rows[i - 1]["rowspan1"]) > rowspan && dt.Rows[i - 1]["Режиссер"].ToString() == dt.Rows[i]["Режиссер"].ToString())
                    {
                        for (int row = 0; row < rowspan; row++)
                            dt.Rows[i + row]["rowspan1"] = 0;
                        i += rowspan - 1;
                    }
                    else dt.Rows[i]["rowspan1"] = rowspan;
                }

                //for (int i = 0; i < rowCount; i++)
                //{
                //    int count = 0;
                //    int rowspan = 1; // Изначально устанавливаем rowspan в 1
                //    for (int j = i + 1; j < rowCount; j++)
                //    {
                //        count++;
                //        if (dt.Rows[i]["Название"].ToString() == dt.Rows[j]["Название"].ToString())
                //        {
                //            if (dt.Rows[i]["Жанр"].ToString() == dt.Rows[j]["Жанр"].ToString())
                //            {
                //                rowspan++;
                //                dt.Rows[j]["rowspan2"] = 0;
                //            }
                //        }
                //        else
                //        {
                //            break; // Прерываем цикл, если нашли различие
                //        }
                //    }
                //    if (dt.Rows[i]["rowspan2"] is DBNull || Convert.ToInt32(dt.Rows[i]["rowspan2"]) != 0)
                //        dt.Rows[i]["rowspan2"] = rowspan;
                //}
                MyRepeater.DataSource = dt;
                MyRepeater.DataBind();
            }
        }
    }
}