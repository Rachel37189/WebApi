using Entities;
using Repository;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UserService (IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        public async Task<User> AddUser(User user)
        {
            if (_passwordService.CheckPasswordStrength(user.Password).Strength <= 2)
                return null;
            
            return await _userRepository.AddUser(user);
        }
        public async Task UpdateUser(int id, User user)
        {
            await _userRepository.UpdateUser(id, user);

        }
        public async Task<User> Login(User user)
        {
            return await _userRepository.Login(user);
        }
    }
}
