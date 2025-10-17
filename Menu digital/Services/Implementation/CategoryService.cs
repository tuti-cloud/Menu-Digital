namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;

public class CategoryServicec : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    public CategoryServicec(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
}
