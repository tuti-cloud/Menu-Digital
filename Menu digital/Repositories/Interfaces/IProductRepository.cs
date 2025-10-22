using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetProductById(int id);
        Product Create(Product product);
        void Update(Product product, int productId);
        void Delete(int id);
        public IEnumerable<Product> GetByCategoryId(int categoryId); //obtener producto por categoria
        public IEnumerable<Product> GetByrestaurantId(int restaurantId); //obtener productos por restaurante
        public IEnumerable<Product> GetDiscountedProduct(int restauranteId); //obtener productos con descuento de un restaurante
        public IEnumerable<Product> GetHaappyHourProduct(int restauranteId); //obtener productos en happy hour de un restaurante

    }
}
