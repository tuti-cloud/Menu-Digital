using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Implementation;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Digital.Controllers
{
    [Route("api/categories")]
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

        [HttpGet("{categoryId}")]
        public IActionResult GetCategoryId(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound($"Categoría con ID {categoryId} no fue encontrado.");
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateAndUpdateCategoryDto createCategorytDto)
        {
            var newCategory = _categoryService.Create(createCategorytDto);

            if (newCategory == null)
            {
                return BadRequest("No se pudo crear la categoría.");
            }

            return Ok(newCategory);
        }

        [HttpDelete]
        [Route("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryService.Delete(categoryId);
            return NoContent();
        }

        [HttpPut]
        [Route("{categoryId}")]
        public IActionResult UpdateCategory(CreateAndUpdateCategoryDto dto, int categoryId)
        {
            var updatedCategory = _categoryService.Update(dto, categoryId);
            return Ok(updatedCategory);
        }
    }
}

