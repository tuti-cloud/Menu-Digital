using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetCategoryById(int id);
        Category Create(Category category);
        void Update(Category category, int categoryId);
        void Delete(int id);
        public IEnumerable<Category> GetByrestaurantId(int restaurantId); //obtener categorias por restaurante

    }
}
