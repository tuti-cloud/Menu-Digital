using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Menu_Digital.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MenuDigitalContext _context;

        public CategoryRepository(MenuDigitalContext context)
        {
            _context = context;
        }

        // 🔹 Obtener todas las categorías globales
        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        // 🔹 Obtener una categoría por ID
        public Category? GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        // 🔹 Verificar si ya existe una categoría con el mismo nombre
        public bool ExistsByName(string name)
        {
            return _context.Categories.Any(c => c.CategoryName == name);
        }

        // 🔹 Agregar nueva categoría global
        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        // 🔹 Actualizar nombre de categoría
        public void Update(Category updatedCategory, int categoryId)
        {
            var existing = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existing != null)
            {
                existing.CategoryName = updatedCategory.CategoryName;
            }
        }

        // 🔹 Eliminar categoría por ID
        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        // 🔹 Guardar cambios
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}




