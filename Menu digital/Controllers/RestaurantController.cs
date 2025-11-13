using Menu_Digital.Models.DTOs.Requests;
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
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            if (!restaurants.Any())
                return NoContent();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        [AllowAnonymous]
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
        [Route("{email}/{passwordHash}")]
        public IActionResult DeleteRestaurant(string email, string passwordHash)
        {
            var dto = new CredentialRequestDto
            {
                Email = email,
                PasswordHash = passwordHash
            };

            _restaurantService.AutoDelete(dto);
            return Ok("Eliminado con éxito");
        }

        [HttpPut]
        [Route("{restaurantId}")]
        public IActionResult UpdateRestaurant(CreateAndUpdateRestaurantDto dto, int restaurantId)
        {
            var updatedRestaurant = _restaurantService.Update(dto, restaurantId);
            return Ok(updatedRestaurant);
        }

        [HttpGet("product/{productName}")]
        [AllowAnonymous]
        public IActionResult GetRestaurantsByProductName([FromRoute] string productName)
        {
            var result = _restaurantService.GetRestaurantsByProductName(productName);

            if (result == null || result.Count == 0)
                return NotFound("No se encontraron restaurantes con ese producto.");

            return Ok(result);
        }

        [HttpGet("{restaurantId:int}/menu")]
        [AllowAnonymous]
        public IActionResult GetMenu(int restaurantId)
        {
            var menu = _restaurantService.GetMenuByRestaurantId(restaurantId);

            if (menu == null || !menu.Any())
                return NotFound(new { message = "No se encontraron categorías o productos para este restaurante." });

            return Ok(menu);
        }
    }
}

