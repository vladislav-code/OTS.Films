using BLToolkit.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using BLToolkit.Mapping;
using BLToolkit.DataAccess;

namespace OTS.Films.Models
{
    [TableName("Genres")] // Указываем имя таблицы в базе данных
    public class Genre
    {
        [PrimaryKey, Identity] // Первичный ключ
        public int id { get; set; }
        [MapField("name")] // Указываем соответствие между свойством и столбцом в таблице
        public string name { get; set; }
        [MapField("film_id")] // Указываем соответствие между свойством и столбцом в таблице
        public int? film_id { get; set; }
        [Association(ThisKey = "film_id", OtherKey = "id", CanBeNull = true)]
        public Film Film;
    }
}