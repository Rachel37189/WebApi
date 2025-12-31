using Entities;
using DTOs;
namespace Services
{
    public interface IUserService
    {
        Task<UserDTO> addUser(User user);
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO> login(User user);
        Task updateUser(int id, User user);
    }
}