using System.Collections.Generic;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;

namespace Menu_Digital.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(int id);
        ProductDto Create(CreateAndUpdateProductDto productDto);
        ProductDto Update(CreateAndUpdateProductDto updatedProductDto, int productId);
        public void Delete(int productId);
    }
}

