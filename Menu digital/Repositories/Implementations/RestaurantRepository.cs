namespace Menu_Digital.Repositories.Implementations;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
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
        var RestaurantToDelete = _context.Restaurants.FirstOrDefault(r => r.Id == id); //busca el restaurante con el id especificado
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
        return _context.Restaurants.FirstOrDefault(r => r.Id == id);
    }

    public void Update(Restaurant updatedRestaurant, int restaurantId)
    {
        Restaurant? restaurant = _context.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
        if (restaurant is not null)
        {
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;
            restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
            restaurant.Email = updatedRestaurant.Email;
            restaurant.Password = updatedRestaurant.Password;

            _context.SaveChanges();
        }
    }
}
