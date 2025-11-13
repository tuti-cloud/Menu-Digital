namespace Menu_Digital.Models.DTOs.Responses
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<int> ProductIds { get; set; } = new();
        //public int RestaurantId { get; set; }
        //public List<int> ProductIds { get; set; } = new();
    }
}
