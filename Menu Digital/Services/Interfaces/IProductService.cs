using System.Collections.Generic;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductByIdForRestaurant(int restaurantId, int productId);

        ProductDto GetProductById(int id);
        ProductDto Create(CreateAndUpdateProductDto productDto);
        ProductDto Update(CreateAndUpdateProductDto updatedProductDto, int productId);
        public void Delete(int productId);
        // ICollection<ProductDto> GetRecommended();
        IEnumerable<RecommendedProductDto> GetRecommended();
        ICollection<DiscountedProductDto> GetHappyHourByName(string restaurantName);
        ICollection<DiscountedProductDto> GetDiscountedByName(string restaurantName);

        public int SetHappyHourForRestaurant(int restaurantId, bool enabled);
        public void UpdateDiscount(int productId, int discountPercentage);
        List<ProductDto> IncreasePrices(int restaurantId, decimal percentage);

    }
}

