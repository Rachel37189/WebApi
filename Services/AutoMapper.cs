using AutoMapper;
using DTOs;
using Entities;

namespace Services
{
    public class AutoMapper:Profile
    {
       public AutoMapper() {
            CreateMap<UserDTO, User>();            
            CreateMap<User,GetUserDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

       }
    }
}
