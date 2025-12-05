using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> addUser(User user);
        Task<User> GetUserById(int id);
        Task<User> login(User user);
        Task updateUser(int id, User user);
    }
}