using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Menu_Digital.Controllers
{
    [Route("api/[restaurants]")]
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

        [HttpGet("{restaurantId}")]
        public IActionResult GetRestaurantId(int restaurantId)
        {
            var restaurant = _restaurantService.GetByRestaurantId(restaurantId);

            if (restaurant == null)
            {
                return NotFound($"Restaurante con ID {restaurantId} no fue encontrado.");
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public IActionResult CreateRestaurant(CreateAndUpdateRestaurantDto createRestaurantDto)
        {
            var newRestaurant = _restaurantService.Create(createRestaurantDto);

            if (newRestaurant == null)
            {
                return BadRequest("No se pudo crear el restaurante.");
            }

            return Ok(newRestaurant);
        }

        [HttpDelete]
        [Route("{restaurantId}")]
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            _restaurantService.Delete(restaurantId);
            return NoContent();
        }

        [HttpPut]
        [Route("{restaurantId}")]
        public IActionResult UpdateRestaurant(CreateAndUpdateRestaurantDto dto, int restaurantId)
        {
            var updatedRestaurant = _restaurantService.Update(dto, restaurantId);
            return Ok(updatedRestaurant);
        }
    }
}

