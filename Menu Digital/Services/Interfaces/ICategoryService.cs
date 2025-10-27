using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        CategoryDto Create(CreateAndUpdateCategoryRequest request);
        CategoryDto Update(int categoryId, CreateAndUpdateCategoryRequest categoryDto);
        public void Delete(int categoryId);

    }
}
