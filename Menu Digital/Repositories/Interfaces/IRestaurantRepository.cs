using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetAll();
        Restaurant? GetRestaurantById(int id);
        Restaurant Create(Restaurant restaurant);
        void Update(Restaurant updatedRestaurant, int restaurantId);
        void DeleteByEmail(string email);
        public Restaurant? GetByEmail(string email); //para autenticación
        Restaurant GetByName(string name);

        ICollection<Restaurant> GetByProductName(string Name);

    }
}
