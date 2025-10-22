using Menu_Digital.Entities;

namespace Menu_Digital.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Restaurant? Authenticate(string Name, string passwordHash);
    }
}
