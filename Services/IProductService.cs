using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion);
    }
}