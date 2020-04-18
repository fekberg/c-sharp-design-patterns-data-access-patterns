using Microsoft.EntityFrameworkCore;
using MyShop.Business;

namespace MyShop.Data
{

    public class ShoppingContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orders.db");
        }
    }
}
