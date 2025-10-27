namespace Menu_Digital.Repositories.Implementations;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CategoryRepository : ICategoryRepository
{
    private readonly MenuDigitalContext _context; //contexto de la base de datos    
    public CategoryRepository(MenuDigitalContext context)
    {
        _context = context;
    }

    public Category Create(Category category)
    {
        Category newcategory = _context.Categories.Add(category).Entity;
        _context.SaveChanges();
        return newcategory;
    }

    public void Delete(int id)
    {
        var CategoryToDelete = _context.Categories.FirstOrDefault(c => c.Id == id); //busca la category con el id especificado
        if (CategoryToDelete != null)
        {
            _context.Categories.Remove(CategoryToDelete); //si la encuentra, la elimina de la lista
            _context.SaveChanges();
        }
    }

    public Category? GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public ICollection<Category> GetAll()
    {
        return _context.Categories.ToList();
    }
    ICollection<Category> ICategoryRepository.GetByrestaurantId(int restaurantId)
    {
        return _context.Categories
        .Where(c => c.RestaurantId == restaurantId)
        .ToList();
    }

    public void Update(Category updatedCategory, int categoryId)
    {
        Category? category = _context.Categories.SingleOrDefault(c => c.Id == categoryId);
        if (category is not null)
        {
            category.Name = updatedCategory.Name;        

            _context.SaveChanges();
        }
    }

  
}



