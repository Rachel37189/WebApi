using Entities;
using Repository;
using DTOs;
using AutoMapper;
namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper= mapper;
        }
        public async Task<List<CategoryDTO>> GetCategories()
        {
            List<Category> list = await _categoryRepository.GetCategories();
            List<CategoryDTO> listDTO = _mapper.Map<List<Category>,List<CategoryDTO>>(list);
            return listDTO;
            // return await _categoryRepository.GetCategories();
        }
    }
}
