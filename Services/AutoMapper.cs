using AutoMapper;
using DTOs;
using Entities;

namespace Services
{
    public class AutoMapper:Profile
    {
       public AutoMapper() { 
        
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

       }
    }
}
