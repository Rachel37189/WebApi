using Entities;
using Repository;
namespace Services
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();
        PasswordService passwordService = new PasswordService();
        public User GetUserById(int id)
        {
            return userRepository.GetUserById(id);
        }
        public User addUser(User user) {
            if (passwordService.Level(user.passWord).Strength <= 2)
                return null;
                return null;
            return userRepository.addUser(user);
        }
        public void updateUser(int id, User user)
        {
            userRepository.updateUser(id, user);

        }
        public User login(User user) { 
            return userRepository.login(user);
        }
    }
}
