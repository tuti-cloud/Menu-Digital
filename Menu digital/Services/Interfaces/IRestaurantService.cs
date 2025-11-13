using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface IRestaurantService
    {
        List<RestaurantDto> GetAllRestaurants();
        RestaurantDto GetByRestaurantName(string restaurantName);
        RestaurantDto Create(CreateAndUpdateRestaurantDto restaurantDto);
        RestaurantDto Update(CreateAndUpdateRestaurantDto updatedRestaurantDto, int restaurantId);

        // ⬇️ Cambiado: ahora devuelve bool
        bool AutoDelete(CredentialRequestDto dto);

        Restaurant? Authenticate(string email, string password);

        ICollection<SearchProductByRestaurantDto> GetRestaurantsByProductName(string productName);

        List<MenuDto> GetMenuByRestaurantId(int restaurantId);
    }
}

