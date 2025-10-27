using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface IRestaurantService
    {

        public Restaurant? Authenticate(string Email, string password);
        List<Restaurant> GetAllRestaurants();
        RestaurantDto GetRestaurantById(int id);
        RestaurantDto Create(CreateAndUpdateRestaurantRequest restaurantDto);
        RestaurantDto Update(int restaurantId, CreateAndUpdateRestaurantRequest restaurantDto);
        bool Delete(int restaurantId);
    }
}
