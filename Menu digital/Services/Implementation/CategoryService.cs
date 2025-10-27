namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System.Collections.Generic;

public class CategoryServicec : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    public CategoryServicec(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryDto Create(CreateAndUpdateCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public void Delete(int categoryId)
    {
        throw new NotImplementedException();
    }

    public List<CategoryDto> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public CategoryDto GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public CategoryDto Update(int categoryId, CreateAndUpdateCategoryRequest categoryDto)
    {
        throw new NotImplementedException();
    }
}
