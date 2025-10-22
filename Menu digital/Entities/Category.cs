namespace Menu_Digital.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Restaurant Restaurant { get; set; } //una categoria se relaciona con varios restaurantes.
        public int RestaurantId { get; set; }

    }
}
