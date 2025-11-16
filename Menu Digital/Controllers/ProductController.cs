using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Menu_Digital.Controllers
{
    [Route("api/products")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        // GET: api/products/id/5
        [HttpGet("id/{productId}")]
        [AllowAnonymous]
        public IActionResult GetProductId(int productId)
        {
            var product = _productService.GetProductById(productId);

            if (product == null)
                return NotFound($"Producto con ID {productId} no fue encontrado.");

            return Ok(product);
        }

        // GET: api/products/name/Coca
        [HttpGet("name/{productName}")]
        [AllowAnonymous]
        public IActionResult GetProductByName(string productName)
        {
            var product = _productService.GetProductByName(productName);

            if (product == null)
                return NotFound($"Producto con nombre {productName} no fue encontrado.");

            return Ok(product);
        }

        // GET: api/products/recommended
        [HttpGet("recommended")]
        [AllowAnonymous]
        public IActionResult GetRecommended()
        {
            var result = _productService.GetRecommended();
            return Ok(result);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult CreateProduct(CreateAndUpdateProductDto dto)
        {
            var newProduct = _productService.Create(dto);

            if (newProduct == null)
                return BadRequest("No se pudo crear el producto.");

            return Ok(newProduct);
        }

        // DELETE: api/products/5
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.Delete(productId);
            return NoContent();
        }

        // PUT: api/products/5
        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(CreateAndUpdateProductDto dto, int productId)
        {
            var updatedProduct = _productService.Update(dto, productId);
            return Ok(updatedProduct);
        }

        // GET: api/products/sushiclub/happyhour
        [HttpGet("{restaurantName}/happyhour")]
        [AllowAnonymous]
        public IActionResult GetHappyHour(string restaurantName)
        {
            var result = _productService.GetHappyHourByName(restaurantName);
            return Ok(result);
        }

        // GET: api/products/sushiclub/discounted
        [HttpGet("{restaurantName}/discounted")]
        [AllowAnonymous]
        public IActionResult GetDiscounted(string restaurantName)
        {
            var result = _productService.GetDiscountedByName(restaurantName);
            return Ok(result);
        }

        // PUT: api/products/5/happyhour/true
        [HttpPut("{restaurantId}/happyhour/{enabled}")]
        public IActionResult SetHappyHourForRestaurant(int restaurantId, bool enabled)
        {
            var count = _productService.SetHappyHourForRestaurant(restaurantId, enabled);
            return Ok(new { affected = count, happyHourEnabled = enabled });
        }

        // PUT: api/products/5/discount/20
        [HttpPut("{productId}/discount/{percentage}")]
        public IActionResult UpdateDiscount(int productId, int percentage)
        {
            _productService.UpdateDiscount(productId, percentage);
            return Ok("Discount updated successfully");
        }

        // PUT: api/products/increase-prices/5?percentage=10
        [HttpPut("increase-prices/{restaurantId}")]
        public IActionResult IncreasePrices(int restaurantId, [FromQuery] decimal percentage)
        {
            try
            {
                if (percentage == 0m)
                    return BadRequest("El porcentaje debe ser distinto de 0.");

                var updated = _productService.IncreasePrices(restaurantId, percentage);

                if (updated == null || !updated.Any())
                    return NotFound($"No se encontraron productos para el restaurantId {restaurantId}.");

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
        // GET: api/products/{restaurantId}/happyhour-status
        [HttpGet("{restaurantId}/happyhour-status")]
        
        public IActionResult GetHappyHourStatus(int restaurantId)
        {
            var enabled = _productService.GetHappyHourStatus(restaurantId);

            return Ok(new { happyHourEnabled = enabled });
        }


    }
}

