namespace Menu_Digital.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; } //un producto se relaciona con varias categorias. 
        public int CategoryId { get; set; }
        public Restaurant Restaurant { get; set; } //un producto se relaciona con varios rest.
        public int RestaurantId { get; set; }
        public decimal? DiscountPercentage { get; set; } // null si no tiene descuento
        public bool HappyHour { get; set; }
        public bool Favorite { get; set; }
    }
}
