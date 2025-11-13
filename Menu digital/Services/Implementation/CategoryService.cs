namespace Menu_Digital.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Menu_Digital.Entities;
    using Menu_Digital.Models.DTOs.Requests;
    using Menu_Digital.Models.DTOs.Responses;
    using Menu_Digital.Repositories.Interfaces;
    using Menu_Digital.Services.Interfaces;

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryDto Create(CreateAndUpdateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre de la categoría es requerido.", nameof(dto.Name));

            if (_categoryRepository.ExistsByName(dto.Name))
                return null; // o lanzar una excepción controlada

            var cat = new Category { CategoryName = dto.Name };

            _categoryRepository.Add(cat);
            _categoryRepository.SaveChanges();

            return new CategoryDto
            {
                CategoryId = cat.CategoryId,
                Name = cat.CategoryName,
                ProductIds = new List<int>()
            };
        }

        public void Delete(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
            _categoryRepository.SaveChanges();
        }

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();

            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.CategoryName,
                ProductIds = new List<int>()
            }).ToList();
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id)
                           ?? throw new KeyNotFoundException("Categoría no encontrada.");

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.CategoryName,
                ProductIds = new List<int>()
            };
        }

        public CategoryDto Update(CreateAndUpdateCategoryDto updatedCategoryDto, int categoryId)
        {
            var existing = _categoryRepository.GetCategoryById(categoryId)
                           ?? throw new KeyNotFoundException("Categoría no encontrada.");

            existing.CategoryName = updatedCategoryDto.Name;

            _categoryRepository.Update(existing, categoryId);
            _categoryRepository.SaveChanges();

            return new CategoryDto
            {
                CategoryId = existing.CategoryId,
                Name = existing.CategoryName,
                ProductIds = new List<int>()
            };
        }
    }
}

