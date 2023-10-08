using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLToolkit.Mapping;
using BLToolkit.DataAccess;

namespace OTS.Films.Models
{
    [TableName("Films")] // Указываем имя таблицы в базе данных
    public class Film
    {
        [PrimaryKey, Identity] // Первичный ключ
        public int Id { get; set; }

        [MapField("title")] // Указываем соответствие между свойством и столбцом в таблице
        public string title { get; set; }
        [Association(ThisKey = "id", OtherKey = "film_id", CanBeNull = true)]
        public List<Director> Directors;
        [Association(ThisKey = "id", OtherKey = "film_id", CanBeNull = true)]
        public List<Genre> Genres;
    }
}