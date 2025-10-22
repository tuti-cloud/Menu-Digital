namespace Menu_Digital.Models.DTOs.Responses
{
    public class ProductDto
    {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public bool HappyHour { get; set; }
            public bool Favorite { get; set; }
            public string CategoryName { get; set; }
            public string RestaurantName { get; set; }
    }
}
