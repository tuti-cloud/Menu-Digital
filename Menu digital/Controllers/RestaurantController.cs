using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menu_Digital.Services.Interfaces;

namespace Menu_Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
    }
}
