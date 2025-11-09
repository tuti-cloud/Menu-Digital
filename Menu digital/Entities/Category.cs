using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu_Digital.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; } //una categoria se relaciona con varios restaurantes.
        public int RestaurantId { get; set; }
        public ICollection<Product> Products { get; set; } // una categoria tiene varios productos.
    }
}
