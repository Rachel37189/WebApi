using AutoMapper;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging;
using Repository;
namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IKafkaProducerService _kafkaProducerService;
        public OrderService(IOrderRepository orderRepository,
            IMapper mapper, IProductRepository productRepository, 
            ILogger<OrderService> logger, IKafkaProducerService kafkaProducerService)
        {
            _orderRepository = orderRepository;
             _mapper=mapper;
            _productRepository = productRepository;
            _logger = logger;
            _kafkaProducerService = kafkaProducerService;

        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            //return await _orderRepository.GetOrderById(id);
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

        public async Task<OrderDTO> addOrder(Order order)
        {
            double calculatedSum = 0;
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetProductById(item.ProductId);
                if (product != null)
                {
                    calculatedSum += product.Price * (item.Quantity ?? 0);
                }
            }
            if (calculatedSum != order.OrderSum)
            {
                _logger.LogWarning("Security Alert: Order sum mismatch for User {UserId}. " +
                                   "Received: {ReceivedSum}, Calculated: {CalculatedSum}",
                                   order.UserId, order.OrderSum, calculatedSum);

                order.OrderSum = calculatedSum;
            }

            Order savedOrder = await _orderRepository.addOrder(order);
            string kafkaMessage =
                $"New Order Created | OrderId: {savedOrder.OrderId} | UserId: {savedOrder.UserId} | Sum: {savedOrder.OrderSum}";

            await _kafkaProducerService.ProduceAsync(kafkaMessage);
            return _mapper.Map<OrderDTO>(savedOrder);
        }
    }
}
