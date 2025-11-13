namespace Menu_Digital.Repositories.Implementations;

using Menu_Digital.Entities;
using Menu_Digital.Repositories.Interfaces;
using MenuDigital.Data;
using System.Collections.Generic;

public class ProductRepository : IProductRepository
{

    private readonly MenuDigitalContext _context; //contexto de la base de datos    
    public ProductRepository(MenuDigitalContext context)
    {
        _context = context;
    }
    public Product Create(Product product)
    {
        Product newproduct = _context.Products.Add(product).Entity;
        _context.SaveChanges();
        return newproduct;
    }

    public void Delete(int id)
    {
        var ProductToDelete = _context.Products.FirstOrDefault(p => p.ProductId == id); //busca el producto con el id especificado
        if (ProductToDelete != null)
        {
            _context.Products.Remove(ProductToDelete); //si lo encuentra, lo elimina de la lista
            _context.SaveChanges();
        }
    }
    public ICollection<Product> GetAll()
    {
        return _context.Products.ToList();
    }
    public Product? GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.ProductId == id);
    }
    public Product? GetProductByName(string Name)
    {
        return _context.Products.FirstOrDefault(p => p.ProductName == Name);
    }
    public ICollection<Product> GetRecommendedProducts(int restaurantId, bool isRecommended)
    {
        return _context.Products // Acceder a la colección de Productos
        .Where(p => p.RestaurantId == restaurantId) // Filtrar por el ID del restaurante
        .Where(p => p.IsRecommended == isRecommended) // Filtrar por el estado de recomendación
        .ToList(); // Ejecutar la consulta y devolver una lista
    }
    ICollection<Product> IProductRepository.GetByCategoryId(int categoryId)
    {
        return _context.Products
        .Where(p => p.CategoryId == categoryId) // Filtrar: Traer solo los productos (p) cuyo CategoryId coincida con el que se busca.
        .ToList(); // Ejecutar la consulta en la base de datos y devolver como lista.
    }

    ICollection<Product> IProductRepository.GetByrestaurantId(int restaurantId)
    {
        return _context.Products
        .Where(p => p.RestaurantId == restaurantId) // Filtrar: Traer solo los restaurants (r) cuyo RestaurantId coincida con el que se busca.
        .ToList(); 
    }

    ICollection<Product> IProductRepository.GetDiscountedProduct(int restaurantId)
    {
        return _context.Products
        .Where(p => p.RestaurantId == restaurantId) // Filtra primero por el ID del restaurante
        .Where(p => p.DiscountPercentage > 0) // Filtra luego por la condición de descuento. Se asume que cualquier valor > 0 indica un descuento.
        .ToList(); // Ejecuta la consulta y devuelve los resultados como una lista
    }

    ICollection<Product> IProductRepository.GetHaappyHourProduct(int restaurantId)
    {
        return _context.Products
            .Where(p=> p.RestaurantId == restaurantId) 
            .Where(p => p.HappyHour == true)
            .ToList();
    }

    public void Update(Product updatedProduct, int productId)
    {
        Product? product = _context.Products.SingleOrDefault(p => p.ProductId == productId);
        if (product is not null)
        {
            product.ProductName = updatedProduct.ProductName;
            product.IsRecommended = updatedProduct.IsRecommended;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.CategoryId = updatedProduct.CategoryId;
            product.RestaurantId = updatedProduct.RestaurantId;
            product.DiscountPercentage = updatedProduct.DiscountPercentage;
            product.HappyHour = updatedProduct.HappyHour;

            _context.SaveChanges();
        }
        // Claves Foráneas: Cambiar CategoryId y RestaurantId si el producto se mueve
        // (Suele ser raro mover el restaurante, pero CategoryId es más común)
    }

    public ICollection<Product> GetRecommended()
    {
        return _context.Products
            .Where(p => p.IsRecommended)
            .ToList();
    }



    public ICollection<Product> GetHappyHour(int restaurantId)
    {
        return _context.Products
            .Where(p => p.RestaurantId == restaurantId && p.HappyHour)
            .ToList();
    }

    public ICollection<Product> GetDiscounted(int restaurantId)
    {
        return _context.Products
            .Where(p => p.RestaurantId == restaurantId && p.DiscountPercentage > 0)
            .ToList();
    }

    public int SetHappyHourForRestaurant(int restaurantId, bool enabled)
    {
        var items = _context.Products
            .Where(p => p.RestaurantId == restaurantId)
            .ToList();

        foreach (var p in items)
            p.HappyHour = enabled;

        _context.SaveChanges();
        return items.Count;
    }

    public void UpdateDiscount(int productId, int discountPercentage)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
            throw new Exception("product not found");

        product.DiscountPercentage = discountPercentage; // int -> double OK (conversión implícita)
        _context.SaveChanges();
    }

    public ICollection<Product> IncreasePricesByRestaurant(int restaurantId, decimal percentage)
    {
        var products = _context.Products  // cargar productos del restaurante
            .Where(p => p.ProductId == restaurantId)
            .ToList();

        if (!products.Any())
            return products;

        if (percentage <= -100m) //  límite a -100 para que no de negativos
            throw new ArgumentException("El porcentaje debe ser mayor a -100.");

        decimal multiplier = 1 + (percentage / 100m);

        foreach (var p in products)
        {
            p.Price = Math.Round(p.Price * multiplier, 2, MidpointRounding.AwayFromZero);  // actualizar precio (redondea a 2 dec)
        }
        _context.SaveChanges();

        return products;
    }

}
