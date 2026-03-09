using Entities;
using DTOs;
namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(int position, int skip, int? Product_Id, string? name, float? minPrice, float? maxPrice, int[]? CategoryIds, string? description);
    }
}