using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public ProductService(IProductRepository productRepository,  IDistributedCache cache ,IMapper mapper)
        {
            _productRepository = productRepository;
            _cache = cache;
            _mapper = mapper;
        }
        //public async Task<List<ProductDTO>> GetProducts(int position, int skip, int? Product_Id, string? name, float? minPrice, float? maxPrice, int[]? CategoryIds, string? description)
        //{

        //    List<Product> listProduct = await _productRepository.GetProducts(position, skip, Product_Id, name, minPrice, maxPrice, CategoryIds, description);
        //    List<ProductDTO> listProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(listProduct);
        //    return listProductDTO;
        //}

        public async Task<List<ProductDTO>> GetProducts(
    int position,
    int skip,
    int? Product_Id,
    string? name,
    float? minPrice,
    float? maxPrice,
    int[]? CategoryIds,
    string? description)
        {
            
            // 🔑 יצירת cache key ייחודי
            string cacheKey = $"products:{position}:{skip}:{Product_Id}:{name}:{minPrice}:{maxPrice}:{description}";

            // אם יש קטגוריות – מוסיפים גם אותן
            if (CategoryIds != null && CategoryIds.Length > 0)
            {
                cacheKey += ":" + string.Join(",", CategoryIds);
            }

            // 1. ניסיון לשלוף מה-Redis

            string? cachedData = null;

            try
            {
                cachedData = await _cache.GetStringAsync(cacheKey);
            }
            catch
            {
                Console.WriteLine("❌ Redis not available");
            }

            if (cachedData != null)
            {
                Console.WriteLine("⚡ FROM CACHE");
                return JsonSerializer.Deserialize<List<ProductDTO>>(cachedData);
            }

            // 2. אם לא נמצא – מביא מה-DB
            var listProduct = await _productRepository.GetProducts(
                position, skip, Product_Id, name, minPrice, maxPrice, CategoryIds, description);

            var listProductDTO = _mapper.Map<List<Product>, List<ProductDTO>>(listProduct);

            // 3. שמירה ב-Redis
          
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            try
            {
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(listProductDTO),
                    options
                );
            }
            catch
            {
                Console.WriteLine("❌ Failed to write to Redis");
            };

            return listProductDTO;
        }

    }
}
