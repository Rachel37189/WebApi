using Entities;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion);
    }
}