using Entities;
using Repository;
namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task<Order> addOrder(Order order)
        {
            return await _orderRepository.addOrder(order);

        }
    }
}
