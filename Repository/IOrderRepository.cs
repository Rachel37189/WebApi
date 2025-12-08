using Entities;

namespace Repository
{
    public interface IOrderRepository
    {
        Task<Order> addOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}