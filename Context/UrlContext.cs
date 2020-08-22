
using MVCShort.Models;
using System.Data.Entity;

namespace Context
{
    public class UrlContext : DbContext
    {
        public UrlContext(): base("name=UrlContext")
        {
        }
        public virtual DbSet<UrlModel> Urls { get; set; }

     
           protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlModel>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<UrlModel>()
                .Property(e => e.ShortUrl)
                .IsUnicode(false);
        }

        }
}
