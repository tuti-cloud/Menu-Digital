using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        List<CategoryDto> GetByRestaurantId(int restaurantId);
        CategoryDto Create(CreateAndUpdateCategoryDto request);
        CategoryDto Update(CreateAndUpdateCategoryDto updatedCategoryDto, int categoryId);
        public void Delete(int categoryId);

    }
}
