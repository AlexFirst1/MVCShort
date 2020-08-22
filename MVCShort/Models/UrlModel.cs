using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCShort.Controllers;


namespace MVCShort.Models
{
    public class UrlContext : DbContext
    {
        public UrlContext() : base("name=UrlContext")
        {
        }
        public virtual DbSet<UrlModel> Urls { get; set; }
    }



    public class UrlModel
    {
        public int Id { get; set; }
        [DisplayName("Короткий URL")]
        public string Surl { get; set; }
        [DisplayName("Длинный URL")]
        public string Url { get; set; }
        public  DateTime Date { get; set; }
        public int Count { get; set; }

    }
}