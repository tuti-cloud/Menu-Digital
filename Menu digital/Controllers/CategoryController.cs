using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Menu_Digital.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // 🔹 GET api/categories
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAllCategories();

            if (categories == null || !categories.Any())
                return NoContent();

            return Ok(categories);
        }

        // 🔹 GET api/categories/{categoryId}
        [HttpGet("{categoryId:int}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {
                var category = _categoryService.GetCategoryById(categoryId);
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la categoría con ID {categoryId}.");
            }
        }

        // 🔹 POST api/categories
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateAndUpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCategory = _categoryService.Create(dto);

            if (newCategory == null)
                return Conflict("Ya existe una categoría con ese nombre.");

            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = newCategory.CategoryId }, newCategory);
        }

        // 🔹 PUT api/categories/{categoryId}
        [HttpPut("{categoryId:int}")]
        public IActionResult UpdateCategory([FromBody] CreateAndUpdateCategoryDto dto, int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCategory = _categoryService.Update(dto, categoryId);
            return Ok(updatedCategory);
        }

        // 🔹 DELETE api/categories/{categoryId}
        [HttpDelete("{categoryId:int}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryService.Delete(categoryId);
            return Ok($"Categoría con ID {categoryId} eliminada correctamente.");
        }
    }
}


