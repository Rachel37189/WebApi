using Entities;
using DTOs;
namespace Services
{
    public interface IUserService
    {
        Task<GetUserDTO> GetUserById(int id);
        Task<GetUserDTO> AddUser(UserDTO userDto);
        Task<GetUserDTO> Login(LoginDTO loginDto);
        Task UpdateUser(int id, UserDTO userDto);
    }
}


