using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAll();
        Category? GetCategoryById(int id);
        Category Create(Category category);
        void Update(Category updatedCategory, int categoryId);
        void Delete(int id);
        public ICollection<Category> GetByrestaurantId(int restaurantId); //obtener categorias por restaurante
        void AssignProducts(int categoryId, IEnumerable<int> productIds);

    }
}
