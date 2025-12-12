using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System.Security.Cryptography;
namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> GetProducts(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion)
        {
            //return await _productRepository.GetProducts(Product_Id, name, price, CategoryId, descripion);
            List<Product> listProduct = await _productRepository.GetProducts(Product_Id, name, price, CategoryId, descripion);
            List<ProductDTO> listProductDTO = _mapper.Map<List<Product>,List<ProductDTO>>(listProduct);
            return listProductDTO;
        }
    }
}
