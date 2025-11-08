using System;
using System.Collections.Generic;
using System.Linq;
using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;

namespace Menu_Digital.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto Create(CreateAndUpdateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                RestaurantId = productDto.RestaurantId,
                DiscountPercentage = productDto.DiscountPercentage,
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
                    CategoryName = p.Category?.Name,
                    RestaurantName = p.Restaurant?.Name
                })
                .ToList();

            return products;
        }

        public ProductDto GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                throw new Exception("product not found");

            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                HappyHour = product.HappyHour,
                IsRecommended = product.IsRecommended,
                CategoryName = product.Category?.Name,
                RestaurantName = product.Restaurant?.Name
            };
        }

        // Obtener un producto por Id validando que pertenezca a ese restaurante
        public ProductDto GetProductByIdForRestaurant(int restaurantId, int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null || product.RestaurantId != restaurantId)
                throw new Exception("product not found for this restaurant");

            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                HappyHour = product.HappyHour,
                IsRecommended = product.IsRecommended,
                CategoryName = product.Category?.Name,
                RestaurantName = product.Restaurant?.Name
            };
        }

        // Obtener todos los productos marcados como recomendados (favoritos)
        public ICollection<ProductDto> GetRecommended()
        {
            var products = _productRepository
                .GetAll()
                .Where(p => p.IsRecommended)
                .ToList();

            return products.Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DiscountPercentage = p.DiscountPercentage,
                HappyHour = p.HappyHour,
                IsRecommended = p.IsRecommended,
                CategoryName = p.Category?.Name,
                RestaurantName = p.Restaurant?.Name
            }).ToList();
        }

        public ICollection<ProductDto> GetHappyHour(int restaurantId)
        {
            // Si nadie tiene HH activo, devuelve vacío
            return _productRepository.GetHappyHour(restaurantId)
                        .Select(MapToDto)
                        .ToList();
        }

        public ICollection<ProductDto> GetDiscounted(int restaurantId)
        {
            return _productRepository.GetDiscounted(restaurantId)
                        .Select(MapToDto)
                        .ToList();
        }

        public int SetHappyHourForRestaurant(int restaurantId, bool enabled)
        {
            // Cambia HH = enabled para TODOS los productos del restaurante
            return _productRepository.SetHappyHourForRestaurant(restaurantId, enabled);
        }

       
        public ProductDto Update(CreateAndUpdateProductDto dto, int productId)
        {
            var updated = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DiscountPercentage = dto.DiscountPercentage,
                HappyHour = dto.HappyHour,
                IsRecommended = dto.Favorite
            };

            _productRepository.Update(updated, productId);
            var p = _productRepository.GetProductById(productId);

            if (p == null)
                throw new Exception("product not found after update");

            return MapToDto(p);
        }

        // Helper
        private static ProductDto MapToDto(Product p) => new ProductDto
        {
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            DiscountPercentage = p.DiscountPercentage,
            HappyHour = p.HappyHour,
            IsRecommended = p.IsRecommended,
            CategoryName = p.Category?.Name,
            RestaurantName = p.Restaurant?.Name
        };
    }
}
