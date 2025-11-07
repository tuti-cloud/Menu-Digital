namespace Menu_Digital.Services.Implementation;
using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System.Collections.Generic;

public class ProductService : IProductService
{   
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductDto Create(CreateAndUpdateProductDto productDto)
    {
        Product product = new Product()
            {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            RestaurantId = productDto.RestaurantId,
            DiscountPercentage = productDto.DiscountPercentage, //porq lo toma como dec por algun motivo(?
            HappyHour = productDto.HappyHour,
            IsRecommended = productDto.Favorite
        };
        var newProduct = _productRepository.Create(product);
        return new ProductDto
        {
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            DiscountPercentage = newProduct.DiscountPercentage,
            HappyHour = newProduct.HappyHour,
            IsRecommended = newProduct.IsRecommended,
            CategoryName = newProduct.Category?.Name,
            RestaurantName = newProduct.Restaurant?.Name
        };
    }

    public void Delete(int productId)
    {
        _productRepository.Delete(productId);
    }

    public List<ProductDto> GetAllProducts()
    {
        var products = _productRepository.GetAll()
      .Select(p => new ProductDto
      {
          Name = p.Name,
          Description = p.Description,
          Price = p.Price,
          DiscountPercentage = p.DiscountPercentage,
          HappyHour = p.HappyHour,
          IsRecommended = p.IsRecommended,
      })
      .ToList();

        return products;
    }

    //implementar un get by restaurant id?

    public ProductDto GetProductById(int id)
    {
        var product = _productRepository.GetProductById(id);
        if (product == null)
        {
            throw new Exception("product not found");
        }

        return new ProductDto
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountPercentage = product.DiscountPercentage,
            HappyHour = product.HappyHour,
            IsRecommended = product.IsRecommended,


        };
    }

    public ProductDto Update(CreateAndUpdateProductDto updatedProductDto, int productId)
    {
        var updatedProduct = new Product
        {
            Name = updatedProductDto.Name,
            Description = updatedProductDto.Description,
            Price = updatedProductDto.Price,
            DiscountPercentage = updatedProductDto.DiscountPercentage,
            HappyHour = updatedProductDto.HappyHour,
            IsRecommended = updatedProductDto.Favorite,
         };

        _productRepository.Update(updatedProduct, productId);

        var product = _productRepository.GetProductById(productId);

        return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                HappyHour = product.HappyHour,
                IsRecommended = product.IsRecommended,
           
            };
        
    }
}

