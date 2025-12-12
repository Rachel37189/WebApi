using Entities;
using DTOs;
namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion);
    }
}