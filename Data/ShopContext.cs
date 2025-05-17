using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class ShopContext : DbContext
    {
        // Entities
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);   // server=(localdb)\\mssqllocaldb
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=App1;Trusted_Connection=True;TrustServerCertificate=True;");
                //.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
