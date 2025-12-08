using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata;
using System.Text.Json;
namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        WebApiShop_215602996Context _webApiShopContext;
        public CategoryRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _webApiShopContext.Categories.ToListAsync();
        }

    }
}
