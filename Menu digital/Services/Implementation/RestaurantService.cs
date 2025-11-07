namespace Menu_Digital.Services.Implementation;
using System;
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

    public Restaurant? Authenticate(string email, string password)
    {
        var restaurant = _restaurantRepository.GetByEmail(email);
        if (restaurant is null)
        
            return null;
        if (restaurant.Password == password)
            return restaurant;
        return null;
    }

    public RestaurantDto Create(CreateAndUpdateRestaurantDto restaurantDto)
    {
        Restaurant restaurant = new Restaurant()
        {
            RestaurantName = restaurantDto.Name,
            Address = restaurantDto.Address,
            PhoneNumber = restaurantDto.PhoneNumber,
            Email = restaurantDto.Email,
            Password = restaurantDto.Password,
        };

        var newRestaurant = _restaurantRepository.Create(restaurant);
        return new RestaurantDto
        {
            Name = newRestaurant.RestaurantName,
            Address = newRestaurant.Address,
            Email = newRestaurant.Email,
            PhoneNumber = newRestaurant.PhoneNumber,
        };
    }

    public void Delete(int restaurantid)
    {
        _restaurantRepository.Delete(restaurantid);
    }

    public List<RestaurantDto> GetAllRestaurants()
    {
        var restaurants = _restaurantRepository.GetAll()
      .Select(u => new RestaurantDto
      {
          Name = u.RestaurantName,
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
            Name = restaurant.  RestaurantName,
            Address = restaurant.Address,
            Email = restaurant.Email,
            PhoneNumber = restaurant.PhoneNumber,
        };
    }

    public RestaurantDto Update(CreateAndUpdateRestaurantDto updatedRestaurantDto, int restaurantId)
    {
        // Convertir DTO → Entidad
        var updatedRestaurant = new Restaurant
        {
            RestaurantName = updatedRestaurantDto.Name,
            Address = updatedRestaurantDto.Address,
            PhoneNumber = updatedRestaurantDto.PhoneNumber,
            Email = updatedRestaurantDto.Email,
            Password = updatedRestaurantDto.Password
        };

        _restaurantRepository.Update(updatedRestaurant, restaurantId);

        // Obtener la entidad actualizada (opcional si el repo la devuelve)
        var restaurant = _restaurantRepository.GetRestaurantById(restaurantId);

        // Convertir Entidad → DTO
        return new RestaurantDto
        {
            Name = restaurant.RestaurantName,
            Address = restaurant.Address,
            PhoneNumber = restaurant.PhoneNumber,
            Email = restaurant.Email
        };
    }
}
