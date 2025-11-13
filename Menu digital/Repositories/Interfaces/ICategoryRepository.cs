using Menu_Digital.Entities;
using System.Collections.Generic;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? GetCategoryById(int id);
        bool ExistsByName(string name);
        void Add(Category category);
        void Update(Category updatedCategory, int categoryId);
        void Delete(int id);
        void SaveChanges();
    }
}
