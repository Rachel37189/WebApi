using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System.Security.Cryptography;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //public async Task<List<ProductDTO>> GetProducts(int position, int skip, int? Product_Id, string? name, float? minPrice, float? maxPrice, int[]? CategoryIds, string? description)
        //{

        //    List<Product> listProduct = await _productRepository.GetProducts(position, skip, Product_Id, name, minPrice, maxPrice, CategoryIds, description);
        //    List<ProductDTO> listProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(listProduct);
        //    return listProductDTO;
        //}
        public async Task<List<ProductDTO>> GetProducts(int position, int skip, int? Product_Id, string? name, float? minPrice, float? maxPrice, int[]? CategoryIds, string? description)
        {
            var listProduct = await _productRepository.GetProducts(position, skip, Product_Id, name, minPrice, maxPrice, CategoryIds, description);
            var listProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(listProduct);
            return (listProductDTO);
        }

    }
}
