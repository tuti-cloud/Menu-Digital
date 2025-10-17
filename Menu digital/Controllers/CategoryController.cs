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

    }
}
