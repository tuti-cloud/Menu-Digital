using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Digital.Controllers
{
    [Route("api/products")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public IActionResult GetProductId(int productId)
        {
            var product = _productService.GetProductById(productId);

            if (product == null)
            {
                return NotFound($"Producto con ID {productId} no fue encontrado.");
            }

            return Ok(product);
        }
        [HttpGet("recommended")]
        [AllowAnonymous]
        public IActionResult GetRecommended()
        {
            var result = _productService.GetRecommended(); // ICollection<ProductDto>
            return Ok(result);
        }



        [HttpPost]
        public IActionResult CreateProduct(CreateAndUpdateProductDto createProductDto)
        {
            var newProduct = _productService.Create(createProductDto);

            if (newProduct == null)
            {
                return BadRequest("No se pudo crear el producto.");
            }

            return Ok(newProduct);
        }

        [HttpDelete]
        [Route("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.Delete(productId);
            return NoContent();
        }

        [HttpPut]
        [Route("{productId}")]
        public IActionResult UpdateProduct(CreateAndUpdateProductDto dto, int productId)
        {
            var updatedProduct = _productService.Update(dto, productId);
            return Ok(updatedProduct);
        }
        [HttpGet("{restaurantName}/happyhour")]
        [AllowAnonymous]
        public IActionResult GetHappyHour(string restaurantName)
        {
            var result = _productService.GetHappyHourByName(restaurantName);
            return Ok(result);
        }

        [HttpGet("{restaurantName}/discounted")]
        [AllowAnonymous]
        public IActionResult GetDiscounted(string restaurantName)
        {
            var result = _productService.GetDiscountedByName(restaurantName);
            return Ok(result);
        }



        // PUT: api/products/{restaurantId}/happyhour/{enabled}
        // Habilita/Deshabilita el Happy Hour de TODOS los productos del restaurante
        [HttpPut("{restaurantId}/happyhour/{enabled}")]
        public IActionResult SetHappyHourForRestaurant(int restaurantId, bool enabled)
        {
            var count = _productService.SetHappyHourForRestaurant(restaurantId, enabled);
            return Ok(new { affected = count, happyHourEnabled = enabled });
        }

        [HttpPut("{productId:int}/discount")]
        public IActionResult UpdateDiscount(int productId, [FromBody] CreateAndUpdateProductDto dto)
        {
            _productService.UpdateDiscount(productId, dto.DiscountPercentage);
            return Ok(new { message = $"Discount updated to {dto.DiscountPercentage}%" });
        }

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
                // Para producción podrías loguear el error y devolver un mensaje más genérico.
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

        }
    }

}
