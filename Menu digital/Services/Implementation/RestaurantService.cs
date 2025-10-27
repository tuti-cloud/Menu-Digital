namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

public class RestaurantService : IRestaurantService
{
    private IRestaurantRepository _restaurantRepository;
    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public Restaurant? Authenticate(string Name, string passwordHash)
    {
        throw new NotImplementedException();
    }

    public RestaurantDto Create(CreateAndUpdateRestaurantDto restaurantDto)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int restaurantid)
    {
        throw new NotImplementedException();
    }

    public List<RestaurantDto> GetAllRestaurants()
    {
        var restaurants = _restaurantRepository.GetAll()
      .Select(u => new RestaurantDto
      {
        Name = u.Name,
        Address = u.Address,
        Email = u.Email,
        PhoneNumber = u.PhoneNumber,
      })
      .ToList();

        return restaurants;
    }

    public RestaurantDto GetByRestaurantId(int restaurantId)
    { 
    
        var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
        if (restaurant == null)
        {
            throw new Exception("restaurat not found");
        }

        return new RestaurantDto
        {
            Name = restaurant.Name,
            Address = restaurant.Address,
            Email = restaurant.Email,
            PhoneNumber = restaurant.PhoneNumber,
        };
    }

    public RestaurantDto Update(int userId, CreateAndUpdateRestaurantDto restaurantDto)
    {
        throw new NotImplementedException();
    }
}

      
    


