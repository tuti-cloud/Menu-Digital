namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Models.DTOs.Responses;
using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;
using System.Collections.Generic;

public class ProductService : IProductService
{   
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public ProductDto Create(CreateProductRequest productDto)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int productId)
    {
        throw new NotImplementedException();
    }

    public List<ProductDto> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public ProductDto GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public ProductDto Update(int productId, CreateProductRequest productDto)
    {
        throw new NotImplementedException();
    }
}

