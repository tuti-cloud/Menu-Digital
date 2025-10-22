using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        IEnumerable <Restaurant> GetAll();
        Restaurant? GetRestaurantById(int id);
        Restaurant Create(Restaurant restaurant);
        void Update(Restaurant updatedRestaurant, int restaurantId);
        void Delete(int id);
        public Restaurant? GetByEmail(string email); //para autenticación
    }
}
