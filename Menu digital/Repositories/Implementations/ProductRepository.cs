namespace Menu_Digital.Repositories.Implementations;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using System.Collections.Generic;

public class ProductRepository : IProductRepository
{
    public Product Create(Product product)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetByrestaurantId(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetDiscountedProduct(int restauranteId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetHaappyHourProduct(int restauranteId)
    {
        throw new NotImplementedException();
    }

    public Product? GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product, int productId)
    {
        throw new NotImplementedException();
    }
}
