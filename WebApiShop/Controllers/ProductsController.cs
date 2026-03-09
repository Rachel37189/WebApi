using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System.Security.Cryptography;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get(
            int position,
            int skip,
            int? productId,
            string? name,
            float? minPrice,
            float? maxPrice,
            int[]? categoryIds,
            string? descripion)
        {
            var product = await _productService.GetProducts(position, skip, productId, name, minPrice, maxPrice, categoryIds, descripion);
            if (product == null)
                return NoContent();
            return Ok(product);
        }
    }
}
