using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata;
using System.Text.Json;
namespace Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly WebApiShop_215602996Context _webApiShopContext;
        public ProductRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<List<Product>> GetProducts(int position, int skip, int? Product_Id, string? name, float? minPrice, float? maxPrice, int[]? CategoryIds, string? description)
        {
            var query = _webApiShopContext.Products
         .Where(product =>
             (description == null ? true : product.Description.Contains(description))
             && (name == null ? true : product.ProductName.Contains(name))
             && (minPrice == null ? true : product.Price >= minPrice)
             && (maxPrice == null ? true : product.Price <= maxPrice)
             && ((CategoryIds == null || CategoryIds.Length == 0) ? true : CategoryIds.Contains(product.CategoryId))
         )
         .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip((position - 1) * skip)
            .Take(skip).Include(product => product.Category).ToListAsync();
            // var total = await query.CountAsync();
            return (products);
        }
 


    
     public async Task<Product> GetProductById(int id)
        {
            return await _webApiShopContext.Products.FindAsync(id);
        }
    }}
