using Microsoft.EntityFrameworkCore;
using WebApplication5.Model;

namespace WebApplication5.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderCategory>().HasKey(x => new
            {
                x.OrderId,
                x.CategoryId
            });
            modelBuilder.Entity<OrderCategory>()
                    .HasOne(x => x.orders)
                    .WithMany(y => y.OrderCategories)
                    .HasForeignKey(x => x.OrderId);
            modelBuilder.Entity<OrderCategory>()
                    .HasOne(x => x.Category)
                    .WithMany(c => c.OrderCategories)
                    .HasForeignKey(y => y.CategoryId);


        }


    }
}
