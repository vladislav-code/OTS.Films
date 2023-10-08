using BLToolkit.Data;
using OTS.Films.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OTS.Films
{
    public partial class GenreReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (DbManager db = new DbManager())
                {
                    try
                    {
                        List<Genre> genres = db.GetTable<Genre>().ToList();
                        genres = genres.GroupBy(g => g.name).Select(g => g.First()).ToList();

                        ddlGenre.DataSource = genres;
                        ddlGenre.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occured " + ex.Message);
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string genre = ddlGenre.SelectedItem.Text;
            using (DbManager db = new DbManager())
            {
                try
                {
                    List<Film> films = db.SetCommand("SELECT Title FROM Films INNER JOIN Genres ON Films.id = Genres.film_id WHERE Genres.name = @genre", db.Parameter("@genre", genre)).ExecuteList<Film>();

                    MyRepeater.DataSource = films;
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