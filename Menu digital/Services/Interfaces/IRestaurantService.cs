using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface IRestaurantService
    {
        List<RestaurantDto> GetAllRestaurants();
        RestaurantDto GetByRestaurantId(int restaurantId);
        RestaurantDto Create(CreateAndUpdateRestaurantDto restaurantDto);
        RestaurantDto Update(Restaurant updatedRestaurant);
        public void Delete(int restaurantid);
        public Restaurant? Authenticate(string Name, string passwordHash);

    }
}
