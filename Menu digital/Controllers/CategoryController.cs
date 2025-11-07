using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menu_Digital.Services.Interfaces;

namespace Menu_Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAllCategories();
            if (!categories.Any())
                return NoContent();

            return Ok(categories);
        }
    }
}

// CategoriesController
// [ApiController]
// [Route("api/categories")]

// GET /api/categories/restaurantId

