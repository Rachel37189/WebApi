using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly WebApiShop_215602996Context _webApiShopContext;
        public UserRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _webApiShopContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddUser(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            await _webApiShopContext.Users.AddAsync(user);
            await _webApiShopContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(int id, UserDTO userDto)
        {
            User? user = await _webApiShopContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return;

            user.UserName = userDto.UserName;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Password = userDto.Password;

            await _webApiShopContext.SaveChangesAsync();
        }
        //public async Task<User> Login(LoginDTO loginDto)
        //{
        //    return await _webApiShopContext.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
        //}
        public async Task<User?> Login(LoginDTO loginDto)
        {
            User? user = await _webApiShopContext.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == loginDto.UserName);

            if (user == null)
                return null;

            bool isPasswordCorrect = false;

            try
            {
                isPasswordCorrect =
                    BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            }
            catch
            {
                return null;
            }

            if (!isPasswordCorrect)
                return null;

            return user;
        }
    }
}







    //    public async Task<bool> UpdateUser(int id, UserDTO userDto)
    //    {
    //        User? user = await _webApiShopContext.Users.FirstOrDefaultAsync(u => u.Id == id);

    //        if (user == null)
    //            return false;

    //        user.UserName = userDto.UserName;
    //        user.FirstName = userDto.FirstName;
    //        user.LastName = userDto.LastName;
    //        user.Password = userDto.Password;

    //        await _webApiShopContext.SaveChangesAsync();
    //        return true;
    //    }

    //    public async Task<User?> Login(LoginDTO loginDto)
    //    {
    //        return await _webApiShopContext.Users.FirstOrDefaultAsync(x =>
    //            x.UserName == loginDto.UserName &&
    //            x.Password == loginDto.Password);
    //    }
    //}
