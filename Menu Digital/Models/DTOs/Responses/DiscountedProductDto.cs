namespace Menu_Digital.Models.DTOs.Responses
{
    public class DiscountedProductDto
    {
        public string Name { get; set; }
        public double DiscountPercentage { get; set; }   // admite 0–1 o 0–100
        public decimal FinalPrice { get; set; }          // precio con descuento
    }
}
