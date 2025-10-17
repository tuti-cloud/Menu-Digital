namespace Menu_Digital.Services.Implementation;

using Menu_Digital.Repositories.Interfaces;
using Menu_Digital.Services.Interfaces;

public class ProductService : IProductService
{   
    private IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
}

