using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata;
using System.Text.Json;
namespace Repository
{
    public class ProductRepository : IProductRepository
    {

        WebApiShop_215602996Context _webApiShopContext;
        public ProductRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<List<Product>> GetProducts(int? Product_Id,string? name,float? price, int? CategoryId, string? descripion)
        {
            return await _webApiShopContext.Products.ToListAsync();
        }


    }
}
