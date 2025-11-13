namespace Menu_Digital.Models.DTOs.Responses
{
    public class MenuDto
    {
        public int CategoryId { get; set; }           // 🔹 ID de la categoría
        public string CategoryName { get; set; }      // 🔹 Nombre de la categoría
        public List<ProductDto> Products { get; set; } = new(); // 🔹 Lista de productos
    }
}

