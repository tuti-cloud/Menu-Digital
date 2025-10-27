using Menu_Digital.Entities;
using System.ComponentModel.DataAnnotations;

namespace Menu_Digital.Models.DTOs.Requests
{
        public class CreateAndUpdateRestaurantDto
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
        
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
              ICollection<Category> Categories { get; set; }

    }

      

    
}
