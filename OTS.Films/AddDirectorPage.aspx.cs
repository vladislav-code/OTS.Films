using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLToolkit.Data;
using OTS.Films.Models;
using BLToolkit.Data.Linq;

namespace OTS.Films
{
    public partial class AddDirectorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Получение данных из элементов управления
            string directorName = txtDirectorName.Text;

            using (DbManager db = new DbManager())
            {
                try
                {
                    //Создание объекта Director и установка свойства name
                    Director director = new Director { name = directorName };

                    // Вставка данных в таблицу с использованием BLToolkit
                    db.Insert(director);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured " + ex.Message);
                }
            }
            // Очистка элементов управления после вставки
            txtDirectorName.Text = string.Empty;
        }
    }
}