using Menu_Digital.Entities;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
            Restaurant resto = new Restaurant()
            {
                RestaurantId = 1,
                RestaurantName = "Sushi Club",
                Address = "japon 123",
                PhoneNumber = "1111111",
                Email = "sushiclub@gmail.com",
                PasswordHash = "sushi123"
                
            };
            Category Bebidas = new Category()
            { 
                CategoryId= 1,
                CategoryName= "Bebidas",
                
                };
            Product Coca = new Product()
            {
                ProductId  = 1,
                ProductName = "Coca Cola",
                Description= "Bebida gasificada refrescante capaz q te moris joven",
                Price = 3000,
                CategoryId= 1,
                RestaurantId = 1,
                DiscountPercentage= 0.5,
                HappyHour = false,
                IsRecommended = false,
            };
            
            modelBuilder.Entity<Restaurant>().HasData(
                resto);
            modelBuilder.Entity<Category>().HasData(Bebidas);
            modelBuilder.Entity<Product>().HasData(Coca);

            base.OnModelCreating(modelBuilder);
        }
    }
}
