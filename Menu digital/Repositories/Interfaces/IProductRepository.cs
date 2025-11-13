using Menu_Digital.Entities;

namespace Menu_Digital.Repositories.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
        Product? GetProductById(int id);
        Product? GetProductByName(string Name);
        Product Create(Product product);
        void Update(Product updatedProduct, int productId);
        void Delete(int id);
        public ICollection<Product> GetByCategoryId(int categoryId); //obtener producto por categoria
        public ICollection<Product> GetByrestaurantId(int restaurantId); //obtener productos por restaurante
        public ICollection<Product> GetDiscountedProduct(int restauranteId); //obtener productos con descuento de un restaurante
        public ICollection<Product> GetHaappyHourProduct(int restauranteId); //obtener productos en happy hour de un restaurante

        public ICollection<Product> GetRecommended(); //obtener productos recomendados 

        public ICollection<Product> GetHappyHour(int restaurantId);
        public ICollection<Product> GetDiscounted(int restaurantId);
        public int SetHappyHourForRestaurant(int restaurantId, bool enabled);
        public void UpdateDiscount(int productId, int discountPercentage);
        ICollection<Product> IncreasePricesByRestaurant(int restaurantId, decimal percentage);
        


    }
}

// var hasHappyHour = !product.HappyHour;