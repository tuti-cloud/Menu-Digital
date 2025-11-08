using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu_Digital.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } //un producto se relaciona con varias categorias. 
        public int CategoryId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; } //un producto se relaciona con varios rest.
        public int RestaurantId { get; set; }
        public double DiscountPercentage { get; set; } // null si no tiene descuento // 10, 20, 50
        public bool HappyHour { get; set; }
        public bool IsRecommended { get; set; }
    }
}
