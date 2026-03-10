using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static WebApiShop.Controllers.UsersController;
using Entities;
using Repository;
using Services;
using DTOs;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

       IOrderService _orderService;
        IMapper _mapper;
        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        //// GET: api/<UsersController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
           
            OrderDTO order= await _orderService.GetOrderById(id);
            if (order == null)
                   return NoContent();
            return Ok(order);
        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO order)
        {
            Order orderEntity = _mapper.Map<Order>(order);

            
            OrderDTO _order = await _orderService.addOrder(orderEntity);
            if (_order == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = _order.OrderId },
     new { Message = $"הזמנה מספר {_order.OrderId} בוצעה בהצלחה!", Data = _order });
            //return CreatedAtAction(nameof(Get), new {id= _order.OrderId }, _order);
            //return Ok(_order);

        }

       
    }
}
