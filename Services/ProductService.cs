using Entities;
using Repository;
namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion)
        {
            return await _productRepository.GetProducts(Product_Id, name, price, CategoryId, descripion);
        }
    }
}
