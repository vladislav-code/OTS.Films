using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OTS.Films.Models;
using BLToolkit.Data;
using BLToolkit.Data.Linq;

namespace OTS.Films
{
    public partial class AddGenrePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Получение данных из элементов управления
            string genreName = txtGenreName.Text;

            using (DbManager db = new DbManager())
            {
                try
                {
                    Genre genre = new Genre { name = genreName };

                    // Вставка данных в таблицу с использованием BLToolkit
                    db.Insert(genre);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured " + ex.Message);
                }
            }

            // Очистка элементов управления после вставки
            txtGenreName.Text = string.Empty;
        }
    }
}