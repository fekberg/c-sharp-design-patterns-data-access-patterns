using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;

namespace MyShop.Infrastructure
{

    public class ShoppingContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                // .UseLazyLoadingProxies()
                .UseSqlite("Data Source=orders.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Ignore(c => c.ProfilePictureLazy);
            modelBuilder.Entity<Customer>().Ignore(c => c.ProfilePictureValueHolder);
            modelBuilder.Entity<Customer>().Ignore(c => c.ProfilePicture2);
            modelBuilder.Entity<Customer>().Ignore(c => c.ProfilePicture3);

            base.OnModelCreating(modelBuilder);
        }
    }
}
