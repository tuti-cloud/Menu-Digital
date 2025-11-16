// IRestaurantService.cs
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

        // ⬇️ devuelve true si se eliminó, false si credenciales inválidas o no existe
        bool AutoDelete(CredentialRequestDto dto);

        Restaurant? Authenticate(string email, string password);
        ICollection<SearchProductByRestaurantDto> GetRestaurantsByProductName(string productName);
        
        List<RecommendedProductDto> GetProductsByRestaurantAndCategory(string restaurantName, string categoryName);
        List<MenuDto> GetMenuByRestaurantName(string restaurantName);


    }
}


