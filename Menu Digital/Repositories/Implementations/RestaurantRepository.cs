using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly MenuDigitalContext _context;

    public RestaurantRepository(MenuDigitalContext context)
    {
        _context = context;
    }

    public Restaurant Create(Restaurant restaurant)
    {
        var newrestaurant = _context.Restaurants.Add(restaurant).Entity;
        _context.SaveChanges();
        return newrestaurant;
    }

    public ICollection<Restaurant> GetAll()
    {
        return _context.Restaurants.ToList();
    }

    public Restaurant? GetByEmail(string email)
    {
        return _context.Restaurants.FirstOrDefault(r => r.Email == email);
    }

    public Restaurant? GetRestaurantById(int id)
    {
        return _context.Restaurants.FirstOrDefault(r => r.RestaurantId == id);
    }

    public bool UpdateByEmail(string email, CreateAndUpdateRestaurantDto updatedData)
    {
        var entity = GetByEmail(email);
        if (entity == null)
            return false;

        try
        {

            entity.RestaurantName = updatedData.RestaurantName ?? entity.RestaurantName;
            entity.Address = updatedData.Address ?? entity.Address;
            entity.PhoneNumber = updatedData.PhoneNumber ?? entity.PhoneNumber;
            entity.Email = updatedData.Email ?? entity.Email;
            entity.PasswordHash = updatedData.PasswordHash ?? entity.PasswordHash;

            _context.Restaurants.Update(entity);
            _context.SaveChanges();

            return true;
        }
        catch
        {
            return false;
        }
    }

    // ✅ Implementación correcta y estable del DeleteByEmail
    public bool DeleteByEmail(string email)
    {
        var entity = GetByEmail(email);
        if (entity == null)
            return false;

        using var tx = _context.Database.BeginTransaction();
        try
        {
            // 1️⃣ Eliminar los productos del restaurante (evita error FK)
            var products = _context.Products.Where(p => p.RestaurantId == entity.RestaurantId);
            _context.Products.RemoveRange(products);

            // 2️⃣ Eliminar el restaurante
            _context.Restaurants.Remove(entity);

            // 3️⃣ Guardar y confirmar
            _context.SaveChanges();
            tx.Commit();
            return true;
        }
        catch
        {
            tx.Rollback();
            return false;
        }
    }

    public Restaurant GetByName(string name)
    {
        return _context.Restaurants
            .FirstOrDefault(r => r.RestaurantName.ToLower() == name.ToLower());
    }

    public ICollection<Product> GetProductsByName(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            return new List<Product>();

        return _context.Products
            .Include(p => p.Restaurant)
            .Where(p => EF.Functions.Like(p.ProductName, $"%{productName}%"))
            .ToList();
    }

    public ICollection<Category> GetMenuByRestaurantName(string restaurantName)
    {
        if (string.IsNullOrWhiteSpace(restaurantName))
            return new List<Category>();

        restaurantName = restaurantName.Trim().ToLower();

        var restaurant = _context.Restaurants
            .FirstOrDefault(r => r.RestaurantName.ToLower() == restaurantName);

        if (restaurant == null)
            return new List<Category>();

        var categories = _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.CategoryId)
            .ToList();

        var products = _context.Products
            .AsNoTracking()
            .Where(p => p.RestaurantId == restaurant.RestaurantId)
            .ToList();

        foreach (var c in categories)
            c.Products = products.Where(p => p.CategoryId == c.CategoryId).ToList();

        return categories;
    }

    // RestaurantRepository.cs
    public ICollection<Product> GetProductsByRestaurantAndCategory(string restaurantName, string categoryName)
    {
        if (string.IsNullOrWhiteSpace(restaurantName) || string.IsNullOrWhiteSpace(categoryName))
            return new List<Product>();

        restaurantName = restaurantName.Trim().ToLower();
        categoryName = categoryName.Trim().ToLower();

        return _context.Products
            .Include(p => p.Restaurant)
            .Include(p => p.Category)
            .Where(p =>
                p.Restaurant.RestaurantName.ToLower() == restaurantName &&
                p.Category.CategoryName.ToLower() == categoryName
            )
            .ToList();
    }

}



