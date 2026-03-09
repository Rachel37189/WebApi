//using Entities;

//namespace Repository
//{
//    public interface IUserRepository
//    {
//        Task<User> AddUser(User user);
//        Task<User> GetUserById(int id);
//        Task<User> Login(User user);
//        Task UpdateUser(int id, User user);
//    }
//}
using DTOs;
using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User?> GetUserById(int id);
        Task<User?> Login(LoginDTO loginDto);
        Task UpdateUser(int id, UserDTO userDto);
    }
}