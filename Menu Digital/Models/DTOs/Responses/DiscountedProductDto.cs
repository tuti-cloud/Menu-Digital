namespace Menu_Digital.Models.DTOs.Responses
{
    public class DiscountedProductDto
    {
        public string Name { get; set; }
        public double DiscountPercentage { get; set; }   // así admitimo 0–1 o 0–100 de disc
        public decimal FinalPrice { get; set; }          // ciopre con descuento
    }
}
