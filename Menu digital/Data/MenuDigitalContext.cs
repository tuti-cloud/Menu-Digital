using Menu_Digital.Entities;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace MenuDigital.Data
{
    public class MenuDigitalContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public MenuDigitalContext(DbContextOptions<MenuDigitalContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Restaurant luis = new Restaurant()
            {
                Id = 1,
                Name = "Regina",
                Address = "Paraguay 1950",
                PhoneNumber = "1111111",
                Email = "aaa@gmail.com",
                PasswordHash = "1235"
            };

            modelBuilder.Entity<Restaurant>().HasData(
                luis);

            base.OnModelCreating(modelBuilder);
        }
    }
}
