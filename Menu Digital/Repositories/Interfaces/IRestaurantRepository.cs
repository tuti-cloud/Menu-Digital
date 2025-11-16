using Menu_Digital.Entities;
using System.Collections.Generic;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetAll();
        Restaurant? GetRestaurantById(int id);
        Restaurant Create(Restaurant restaurant);
        void Update(Restaurant updatedRestaurant, int restaurantId);

        // ⬇️ Ahora devuelve true si se eliminó, false si no existe o falló.
        bool DeleteByEmail(string email);

        // Autenticación / búsquedas
        Restaurant? GetByEmail(string email);
        Restaurant GetByName(string name);

        // Consultas auxiliares
        ICollection<Product> GetProductsByName(string productName);
        
        // IRestaurantRepository.cs
        ICollection<Product> GetProductsByRestaurantAndCategory(string restaurantName, string categoryName);
        ICollection<Category> GetMenuByRestaurantName(string restaurantName);

    }
}

