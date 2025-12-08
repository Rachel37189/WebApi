using Entities;
using Repository;
namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;


        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _categoryRepository.GetCategories();
        }
    }
}
