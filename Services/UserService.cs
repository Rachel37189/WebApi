using AutoMapper;
using DTOs;
using Entities;
using Repository;
namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IPasswordService _passwordService;
        IMapper _mapper;

        public UserService (IUserRepository userRepository, IPasswordService passwordService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper=mapper;
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            //return await _userRepository.GetUserById(id);
            User user = await _userRepository.GetUserById(id);
            UserDTO userDTO = _mapper.Map<User,UserDTO>(user);
            return userDTO;
        }
        public async Task<UserDTO> addUser(User user)
        {
            if (_passwordService.Level(user.Password).Strength <= 2)
                return null;

            // return await _userRepository.addUser(user);
            User userCreated = await _userRepository.addUser(user);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(userCreated);
            return userDTO;
        }
        public async Task updateUser(int id, User user)
        {
            await _userRepository.updateUser(id, user);

        }
        public async Task<UserDTO> login(User user)
        {
          
            User userLoggedIn = await _userRepository.login(user);
            UserDTO userDTO = _mapper.Map<User,UserDTO>(userLoggedIn);
            return userDTO;
        }
    }
}
