using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu_Digital.Entities
{
    public class Restaurant
    {
        private int restaurantId;

        //public Restaurant(int restaurantId, string name, string address, string phoneNumber, string email, string password)
        //{
        //    this.restaurantId = restaurantId;
        //    Name = name;
        //    Address = address;
        //    PhoneNumber = phoneNumber;
        //    Email = email;
        //    Password = password;
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        ICollection<Category> Categories { get; set; } // un restaurante tiene varias categorias.

    }
}
