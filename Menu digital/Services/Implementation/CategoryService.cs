namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System.Collections.Generic;
using System.Xml.Linq;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryDto Create(CreateAndUpdateCategoryDto request)
    {
       
        var category = new Category
        {
            Name = request.Name,
            RestaurantId = request.RestaurantId
        };

      
        var created = _categoryRepository.Create(category);

        
        var createdDto = new CategoryDto
        {
            
            Name = created.Name,
            RestaurantId = created.RestaurantId,
            ProductIds = new List<int>() // por ahora no se asignan productos desde aquí
        };

        return createdDto;
    }

    public void Delete(int categoryId)
    {
        throw new NotImplementedException();
    }

    public List<CategoryDto> GetAllCategories()
    {
        var categories = _categoryRepository.GetAll();

        return categories.Select(c => new CategoryDto
        {

            Name = c.Name,
            RestaurantId = c.RestaurantId,


            ProductIds = new List<int>()
        })
        .ToList();
    }

    public List<CategoryDto> GetByRestaurantId(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public CategoryDto GetCategoryById(int id)
    {
        throw new NotImplementedException();
    }

    public CategoryDto Update(CreateAndUpdateCategoryDto updatedCategoryDto, int categoryId)
    {
        // Convertir DTO → Entidad
        var updatedCategory = new Category
        {
            Name = updatedCategoryDto.Name,
    
        };

        _categoryRepository.Update(updatedCategory, categoryId);

        // Obtener la entidad actualizada (opcional si el repo la devuelve)
        var category = _categoryRepository.GetCategoryById(categoryId);

        // Convertir Entidad → DTO
        return new CategoryDto
        {
            Name = category.Name,
            
        };
    }
}
