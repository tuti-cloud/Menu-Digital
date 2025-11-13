using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu_Digital.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        //  grup0o cometné la relación con Restaurant
        // porque las categorías son globales.
        // public int RestaurantId { get; set; }
        // public Restaurant Restaurant { get; set; }

        // Relación: una categoría tiene varios productos
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}


