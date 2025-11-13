namespace Menu_Digital.Repositories.Implementations;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class RestaurantRepository : IRestaurantRepository
{

    private readonly MenuDigitalContext _context; //contexto de la base de datos    
    public RestaurantRepository(MenuDigitalContext context)
    {
        _context = context;
    }
    public Restaurant Create(Restaurant restaurant)
    {
        Restaurant newrestaurant = _context.Restaurants.Add(restaurant).Entity;
        _context.SaveChanges();
        return newrestaurant;
    }

    public void Delete(int id)
    {
        var RestaurantToDelete = _context.Restaurants.FirstOrDefault(r => r.RestaurantId == id); //busca el restaurante con el id especificado
        if (RestaurantToDelete != null)
        {
            _context.Restaurants.Remove(RestaurantToDelete); //si lo encuentra, lo elimina de la lista
            _context.SaveChanges();
        }
    }
    ICollection<Restaurant> IRestaurantRepository.GetAll()
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

    public void Update(Restaurant updatedRestaurant, int restaurantId)
    {
        Restaurant? restaurant = _context.Restaurants.SingleOrDefault(r => r.RestaurantId == restaurantId);
        if (restaurant is not null)
        {
            restaurant.RestaurantName = updatedRestaurant.RestaurantName;
            restaurant.Address = updatedRestaurant.Address;
            restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
            restaurant.Email = updatedRestaurant.Email;
            restaurant.PasswordHash = updatedRestaurant.PasswordHash;

            _context.SaveChanges();
        }
    }
    public void DeleteByEmail(string email)
    {
        var entity = GetByEmail(email);
        if (entity != null)
        {
            _context.Restaurants.Remove(entity);
            _context.SaveChanges();
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

    public ICollection<Category> GetMenuByRestaurantId(int restaurantId)
    {
        return _context.Categories
            .Include(c => c.Products)
            //.Where(c => c.RestaurantId == restaurantId)
            .ToList();
    }


}


