using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace OTS.Films.Models
//{
//    public class Films
//    {
//        public int id { get; set; }
//        public string title { get; set; }

//    }
//}

using BLToolkit.Mapping;
using BLToolkit.DataAccess;

namespace OTS.Films.Models
{
    [TableName("Films")] // Указываем имя таблицы в базе данных
    public class Film
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [MapField("Title")] // Указываем соответствие между свойством и столбцом в таблице
        public string Title { get; set; }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.SqlServer;


//namespace OTS.Films.Models
//{
//    public class FilmsDBContext : DbContext
//    {
//        public DbSet<Films> Films { get; set; }
//        public FilmsDBContext(DbContextOptions<FilmsDBContext> options)
//            : base(options)
//        {
//            //Database.EnsureCreated();   // создаем базу данных при первом обращении
//        }
//    }
//}