namespace Menu_Digital.Models.DTOs.Requests
{
        public class CreateProductRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
            public int RestaurantId { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public bool HappyHour { get; set; }
            public bool Favorite { get; set; }
        }
    
}
