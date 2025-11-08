using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetAll();
        Restaurant? GetRestaurantById(int id);
        Restaurant Create(Restaurant restaurant);
        void Update(Restaurant updatedRestaurant, int restaurantId);
       
        public Restaurant? GetByEmail(string email); //para autenticación
        void DeleteByEmail(string email);
        
    }
}
