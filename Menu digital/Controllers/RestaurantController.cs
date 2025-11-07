using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Digital.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            if (!restaurants.Any())
                return NoContent();

            return Ok(restaurants);
        
        }


    }


}




