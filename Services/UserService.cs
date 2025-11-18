using Entities;
using Repository;
namespace Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly PasswordService _passwordService = new PasswordService();
        
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        
        public User AddUser(User user)
        {
            if (_passwordService.CheckPasswordStrength(user.Password).Strength <= 2)
                return null;
            return _userRepository.AddUser(user);
        }
        
        public void UpdateUser(int id, User user)
        {
            _userRepository.UpdateUser(id, user);
        }
        
        public User Login(User user)
        {
            return _userRepository.Login(user);
        }
    }
}
