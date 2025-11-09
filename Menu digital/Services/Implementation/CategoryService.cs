namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Xml.Linq;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryDto Create(CreateAndUpdateCategoryDto categoryDto)
    {

        Category category = new Category()
        {
          CategoryName = categoryDto.Name,
          RestaurantId = categoryDto.RestaurantId, //debe estar?
        };

        var newCategory = _categoryRepository.Create(category);
        return new CategoryDto
        {
            Name = newCategory.CategoryName,
            RestaurantId = newCategory.RestaurantId, //debe estar?
        };
    }

    public void Delete(int categoryId)
    {
        _categoryRepository.Delete(categoryId);
    }

    public List<CategoryDto> GetAllCategories()
    {
        var categories = _categoryRepository.GetAll();

        return categories.Select(c => new CategoryDto
        {

            Name = c.CategoryName,
            RestaurantId = c.RestaurantId,


            ProductIds = new List<int>()
        })
        .ToList();
    }

    public CategoryDto GetCategoryById(int id)
    {
        var category = _categoryRepository.GetCategoryById(id);
        if (category == null)
        {
            throw new Exception("category not found");
        }

        return new CategoryDto
        {
            Name = category.CategoryName,
            RestaurantId = category.RestaurantId,
            ProductIds = new List<int>()

        };
    }

    public CategoryDto Update(CreateAndUpdateCategoryDto updatedCategoryDto, int categoryId)
    {
        // Convertir DTO → Entidad
        var updatedCategory = new Category
        {
            CategoryName = updatedCategoryDto.Name,
    
        };

        _categoryRepository.Update(updatedCategory, categoryId);

        // Obtener la entidad actualizada (opcional si el repo la devuelve)
        var category = _categoryRepository.GetCategoryById(categoryId);

        // Convertir Entidad → DTO
        return new CategoryDto
        {
            Name = category.CategoryName,
            
        };
    }
}
