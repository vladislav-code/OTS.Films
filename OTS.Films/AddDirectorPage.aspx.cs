﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BLToolkit.Data;

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
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Создание подключения к базе данных и выполнение операции вставки
            DbManager.AddConnectionString(connectionString);

            using (DbManager db = new DbManager())
            {
                // SQL-запрос для вставки данных
                var query = db.SetCommand("INSERT INTO Directors (name) VALUES (@name)", db.Parameter("@name", directorName)).ExecuteNonQuery();

                ////Создание объекта Director и установка свойства Name
                //Director director = new Director { Name = directorName };

                // // Вставка данных в таблицу с использованием BLToolkit
                // db.Insert(director);
            }

            // Очистка элементов управления после вставки
            txtDirectorName.Text = string.Empty;

            // Закрыть соединение???
            // Сообщение об операции
        }
    }
}