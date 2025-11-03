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
        var createdProduct = _productRepository.Create(product);
        return new ProductDto
        {
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            DiscountPercentage = createdProduct.DiscountPercentage,
            HappyHour = createdProduct.HappyHour,
            IsRecommended = createdProduct.IsRecommended,
            CategoryName = createdProduct.Category?.Name,
            RestaurantName = createdProduct.Restaurant?.Name
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

    public ProductDto Update(int productId, CreateAndUpdateProductDto productDto)
    {
        Product? product = _productRepository.GetProductById(productId);
        if (product is not null)
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.DiscountPercentage = productDto.DiscountPercentage; //porq toma dec en vez de int (?
            product.HappyHour = productDto.HappyHour;
            product.IsRecommended = productDto.Favorite;
            _productRepository.Update(product);
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
        else
        {
            throw new Exception("restaurant not found");
        }
    }
}

