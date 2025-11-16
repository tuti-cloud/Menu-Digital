using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Menu_Digital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 👈 mantenido tal cual
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        // 🔹 GET api/restaurant
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            //if (restaurants == null || !restaurants.Any())
            //    return NoContent();
            if (restaurants == null || !restaurants.Any())
                return Ok(new List<RestaurantDto>());


            return Ok(restaurants);
        }

        // 🔹 GET api/restaurant/{restaurantName}
        [HttpGet("{restaurantName}")]
        [AllowAnonymous]
        public IActionResult GetByName(string restaurantName)
        {
            var restaurant = _restaurantService.GetByRestaurantName(restaurantName);
            if (restaurant == null)
                return NotFound($"Restaurante con nombre '{restaurantName}' no fue encontrado.");

            return Ok(restaurant);
        }

        // 🔹 POST api/restaurant  (se permite anónimo para registro)
        
        [HttpPost]
        public IActionResult CreateRestaurant([FromBody] CreateAndUpdateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newRestaurant = _restaurantService.Create(dto);
            if (newRestaurant == null)
                return BadRequest("No se pudo crear el restaurante.");

            return CreatedAtAction(nameof(GetByName), new { restaurantName = newRestaurant.Name }, newRestaurant);
        }
        [HttpDelete("{email}/{passwordHash}")]
        public IActionResult DeleteRestaurant(string email, string passwordHash)
        {
            var dto = new CredentialRequestDto
            {
                Email = email,
                PasswordHash = passwordHash
            };

            var deleted = _restaurantService.AutoDelete(dto);

            if (!deleted)
                return Unauthorized("Credenciales inválidas o el restaurante no existe.");

            return Ok("Restaurante eliminado con éxito.");
        }


        // 🔹 PUT api/restaurant/{restaurantId}
        [HttpPut("{restaurantId:int}")]
        public IActionResult UpdateRestaurant([FromBody] CreateAndUpdateRestaurantDto dto, int restaurantId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = _restaurantService.Update(dto, restaurantId);
            if (updated == null)
                return NotFound($"No se encontró restaurante con ID {restaurantId}.");

            return Ok(updated);
        }

        // 🔹 GET api/restaurant/product/{productName}
        [HttpGet("product/{productName}")]
        [AllowAnonymous]
        public IActionResult GetRestaurantsByProductName([FromRoute] string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return BadRequest("Debe ingresar un nombre de producto.");

            var result = _restaurantService.GetRestaurantsByProductName(productName);
            if (result == null || !result.Any())
                return NotFound("No se encontraron restaurantes con ese producto.");

            return Ok(result);
        }
        [HttpGet("{restaurantName}/menu")]
        [AllowAnonymous]
        public IActionResult GetMenuByName(string restaurantName)
        {
            if (string.IsNullOrWhiteSpace(restaurantName))
                return BadRequest("Debe indicar el nombre del restaurante.");

            var menu = _restaurantService.GetMenuByRestaurantName(restaurantName);

            if (menu == null || !menu.Any() || menu.All(m => m.Products == null || m.Products.Count == 0))
                return NotFound(new { message = $"No se encontraron productos para '{restaurantName}'." });

            return Ok(menu);
        }

        // RestaurantController.cs
        [HttpGet("{restaurantName}/category/{categoryName}/products")]
        [AllowAnonymous]
        public IActionResult GetProductsByRestaurantAndCategory(string restaurantName, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(restaurantName) || string.IsNullOrWhiteSpace(categoryName))
                return BadRequest("Debe indicar el nombre del restaurante y de la categoría.");

            var products = _restaurantService.GetProductsByRestaurantAndCategory(restaurantName, categoryName);

            if (products == null || products.Count == 0)
                return NotFound($"No se encontraron productos de '{categoryName}' en '{restaurantName}'.");

            return Ok(products);
        }




    }
}

