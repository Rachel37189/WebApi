using Entities;
using DTOs;
namespace Services
{
    public interface IOrderService
    {
        Task<OrderDTO> addOrder(Order order);
        Task<OrderDTO> GetOrderById(int id);
    }
}