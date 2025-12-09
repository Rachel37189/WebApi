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
            return await _webApiShopContext.Users.FindAsync(id);
        }
                                                                                                                     
        public  async Task<User> AddUser(User user)
        {
            await _webApiShopContext.Users.AddAsync(user);
            await _webApiShopContext.SaveChangesAsync();
            return  user;

        }
        public async Task UpdateUser(int id, User user)
        {
             _webApiShopContext.Users.Update(user);
            //_webApiShopContext.Users.Update(user);
            await _webApiShopContext.SaveChangesAsync();
        }
        public async Task<User> Login(User user)
        {
            return await _webApiShopContext.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
        }
    }
}
