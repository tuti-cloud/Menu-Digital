using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Digital.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpGet("{productId}")]
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
    }
}

