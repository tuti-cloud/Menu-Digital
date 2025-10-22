using System.ComponentModel.DataAnnotations;

namespace Menu_Digital.Models.DTOs.Requests
{
        public class CreateAndUpdateRestaurantRequest
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
        
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
        }

      

    
}
