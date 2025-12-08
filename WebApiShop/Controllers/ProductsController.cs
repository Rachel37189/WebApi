using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;
using Entities;
using Repository;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        IProductService _productService;
        public ProductsController (IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async  Task<ActionResult<List<Product>>> Get(int? Product_Id, string? name, float? price, int? CategoryId, string? descripion)
        {
            return await _productService.GetProducts(Product_Id, name, price, CategoryId, descripion);
        }

   
    }
}
