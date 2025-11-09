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
        RestaurantDto Update(CreateAndUpdateRestaurantDto updatedRestaurantDto, int restaurantId);
        public void AutoDelete(CredentialRequestDto dto);
        public Restaurant? Authenticate(string email, string password);
        ICollection<SearchProductByRestaurantDto> GetRestaurantsByProductName(string productName);

    }
}
