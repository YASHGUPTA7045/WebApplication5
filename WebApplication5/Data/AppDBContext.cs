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

    }
}
