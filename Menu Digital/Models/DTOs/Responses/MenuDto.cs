namespace Menu_Digital.Models.DTOs.Responses
{
    public class MenuDto
    {   
        public string CategoryName { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
