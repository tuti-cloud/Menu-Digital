namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;

public class RestaurantService : IRestaurantService
{
    private IRestaurantRepository _restaurantRepository;
    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public Restaurant? Authenticate(string Name, string passwordHash)
    {
        throw new NotImplementedException();
    }
}

