using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata;
using System.Text.Json;
namespace Repository
{
    public class OrderRepository : IOrderRepository
    {

        WebApiShop_215602996Context _webApiShopContext;
        public OrderRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _webApiShopContext.Orders.FindAsync(id);
        }

        public async Task<Order> addOrder(Order order)
        {
            await _webApiShopContext.Orders.AddAsync(order);
            await _webApiShopContext.SaveChangesAsync();
            return order;

        }

    }
}
