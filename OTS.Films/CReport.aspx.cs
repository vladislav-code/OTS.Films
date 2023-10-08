using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTS.Films.Models;
using System.Data;
using BLToolkit.Data;

namespace OTS.Films
{
    public partial class CReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                DataTable dt = new DataTable();
                dt.Columns.Add("Название", typeof(string));
                dt.Columns.Add("Количество", typeof(int));

                using (DbManager db = new DbManager())
                {
                    try
                    {
                        List<Genre> genres = db.GetTable<Genre>().ToList();

                        var query = db.SetCommand("SELECT name AS Название, COUNT(film_id) AS Количество FROM Genres GROUP BY name");
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader["Название"].ToString(), Convert.ToInt32(reader["Количество"]));
                            }
                            reader.Close();
                        }

                        MyRepeater.DataSource = dt;
                        MyRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured " + ex.Message);
                    }
                }
            }
        }
    }
}