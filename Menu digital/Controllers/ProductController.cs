using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menu_Digital.Services.Interfaces;

namespace Menu_Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}
