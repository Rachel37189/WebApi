using AutoMapper;
using DTOs;
using Entities;
using Repository;
namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
             _mapper=mapper;
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            //return await _orderRepository.GetOrderById(id);
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

        public async Task<OrderDTO> addOrder(OrderDTO order)
        {
           // Order order2 = await _orderRepository.addOrder(order);
           Order order2 = await _orderRepository.addOrder(_mapper.Map<OrderDTO, Order>(order));
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order2);
            return orderDTO;
        }
    }
}
