using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Implementations;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Menu_Digital.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        
        public ProductService(IProductRepository productRepository, IRestaurantRepository restaurantRepository)
        {
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
        }

        public ProductDto Create(CreateAndUpdateProductDto productDto)
        {
            var product = new Product
            {
                ProductName = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                RestaurantId = productDto.RestaurantId,
                DiscountPercentage = productDto.DiscountPercentage,
                HappyHour = productDto.HappyHour,
                IsRecommended = productDto.Favorite
            };

            var newProduct = _productRepository.Create(product);

            return MapToDto(newProduct);
        }

        public void Delete(int productId)
        {
            _productRepository.Delete(productId);
        }

        public List<ProductDto> GetAllProducts()
        {
            return _productRepository.GetAll()
                .Select(MapToDto)
                .ToList();
        }

        public ProductDto GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                throw new Exception("product not found");

            return MapToDto(product);
        }
        public ProductDto GetProductByName(string Name)
        {
            var product = _productRepository.GetProductByName(Name);
            if (product == null)
                throw new Exception("no existe ese producto");

            return MapToDto(product);
        }

        public ProductDto GetProductByIdForRestaurant(int restaurantId, int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null || product.RestaurantId != restaurantId)
                throw new Exception("product not found for this restaurant");

            return MapToDto(product);
        }


        public ICollection<DiscountedProductDto> GetHappyHourByName(string restaurantName)
        {
            var restaurant = _restaurantRepository.GetByName(restaurantName);
            if (restaurant == null) throw new Exception("Restaurant not found");

            return _productRepository.GetHappyHour(restaurant.RestaurantId)   // solo HH = true
                .Select(p =>
                {
                    // Si querés mostrar precio con descuento solo cuando DiscountPercentage>0:
                    var rate = p.DiscountPercentage > 1 ? p.DiscountPercentage / 100.0 : p.DiscountPercentage;
                    var final = p.Price * (decimal)(1 - rate);

                    return new DiscountedProductDto
                    {
                        Name = p.ProductName,
                        DiscountPercentage = p.DiscountPercentage,
                        FinalPrice = Math.Round(final, 2)
                    };
                })
                .ToList();
        }
        public ICollection<DiscountedProductDto> GetDiscountedByName(string restaurantName)
        {
            var restaurant = _restaurantRepository.GetByName(restaurantName);
            if (restaurant == null)
                throw new Exception("Restaurant not found");

            return _productRepository.GetDiscounted(restaurant.RestaurantId)
                .Select(p =>
                {
                    var rate = p.DiscountPercentage > 1 ? p.DiscountPercentage / 100.0 : p.DiscountPercentage;
                    var final = p.Price * (decimal)(1 - rate);

                    return new DiscountedProductDto
                    {
                        Name = p.ProductName,
                        DiscountPercentage = p.DiscountPercentage,
                        FinalPrice = Math.Round(final, 2)
                    };
                })
                .ToList();
        }

        public int SetHappyHourForRestaurant(int restaurantId, bool enabled)
        {
            return _productRepository.SetHappyHourForRestaurant(restaurantId, enabled);
        }

        public ProductDto Update(CreateAndUpdateProductDto dto, int productId)
        {
            var updated = new Product
            {
                ProductName = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                RestaurantId = dto.RestaurantId,
                DiscountPercentage = dto.DiscountPercentage,
                HappyHour = dto.HappyHour,
                IsRecommended = dto.Favorite
            };

            _productRepository.Update(updated, productId);

            var product = _productRepository.GetProductById(productId);
            if (product == null)
                throw new Exception("product not found after update");

            return MapToDto(product);
        }

        public void UpdateDiscount(int productId, int discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new Exception("discount must be between 0 and 100");

            _productRepository.UpdateDiscount(productId, discountPercentage);
        }


        public IEnumerable<RecommendedProductDto> GetRecommended()
        {
            return _productRepository.GetRecommended()
                .Select(p => new RecommendedProductDto
                {
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price
                })
                .ToList();
        }

        public List<ProductDto> IncreasePrices(int restaurantId, decimal percentage)
        {
            if (percentage <= -100m) // validación para que no sea neg
                throw new ArgumentException("El porcentaje debe ser mayor a -100.");

            var updatedProducts = _productRepository.IncreasePricesByRestaurant(restaurantId, percentage);
            var dtos = updatedProducts //mapea dtos
                .Select(p => new ProductDto
                {
                    Name = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    DiscountPercentage = p.DiscountPercentage,
                    HappyHour = p.HappyHour,
                    IsRecommended = p.IsRecommended,
                    CategoryName = p.Category?.CategoryName,
                    RestaurantName = p.Restaurant?.RestaurantName
                })
                .ToList();

            return dtos;

            }
            // Helper
        private static ProductDto MapToDto(Product p) => new ProductDto
        {
            Name = p.ProductName,
            Description = p.Description,
            Price = p.Price,
            DiscountPercentage = p.DiscountPercentage,
            HappyHour = p.HappyHour,
            IsRecommended = p.IsRecommended,
            CategoryName = p.Category?.CategoryName,
            RestaurantName = p.Restaurant?.RestaurantName
        };
    }

 
    }
    
