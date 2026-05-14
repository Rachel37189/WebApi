using AutoMapper;
using DTOs;
using Entities;
using Repository;
using BCrypt.Net;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordService passwordService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<GetUserDTO> GetUserById(int id)
        {

            User? user = await _userRepository.GetUserById(id);
            if (user == null)
                return null;
            GetUserDTO userDTO = _mapper.Map<GetUserDTO>(user);
            return userDTO;
        }

        public async Task<GetUserDTO> AddUser(UserDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            if ((await _passwordService.CheckPasswordStrength(user.Password)).Strength <= 2)
                return null;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User user1 = await _userRepository.AddUser(user);
            GetUserDTO userDTO = _mapper.Map<GetUserDTO>(user1);
            return userDTO;
        }

        public async Task<GetUserDTO> Login(LoginDTO loginDto)
        {

            User user3 = await _userRepository.Login(loginDto);
            //if (user == null)
            //    return null;
            GetUserDTO userDTO = _mapper.Map<GetUserDTO>(user3);
            return userDTO;
        }
        public async Task UpdateUser(int id, UserDTO userDto)
        {
            await _userRepository.UpdateUser(id, userDto);

        }
      



    }
}







     


  