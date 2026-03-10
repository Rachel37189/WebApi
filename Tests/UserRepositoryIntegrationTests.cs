using Xunit;
using DTOs;
using Entities;
using Repository;
using Microsoft.EntityFrameworkCore;
namespace Tests {

    public class UserRepositoryIntegrationTests : IClassFixture<DbFixture>
    {
        private readonly WebApiShop_215602996Context _context;
        private readonly UserRepository _repository;

        public UserRepositoryIntegrationTests(DbFixture fixture)
        {
            _context = fixture.Context;
            _repository = new UserRepository(_context);

            // ❌ לא סוגרים ולא עושים Dispose כאן
            // מבודד את ה־ChangeTracker כדי למנוע tracked duplicates
            _context.ChangeTracker.Clear();
        }
        #region HappyTests
        [Fact]
        public async Task RegisterAsync_ShouldSaveUserToRealDatabase()
        {
            var user = new User { UserName = "integration@test.com", Password = "123", FirstName = "Test", LastName = "User" };

            var result = await _repository.AddUser(user);

            var userInDb = await _context.Users.FindAsync(result.Id);
            Assert.NotNull(userInDb);
            Assert.Equal("integration@test.com", userInDb.UserName);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsUserFromDb()
        {
            var user = new User { UserName = "login@integration.com", Password = "password123", FirstName = "A", LastName = "B" };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var loginDto = new LoginDTO("login@integration.com","password123" );

            var result = await _repository.Login(loginDto);

            Assert.NotNull(result);
            Assert.Equal("login@integration.com", result.UserName);
        }
        #endregion
        #region UnHappyTests
        [Fact]
        public async Task LoginAsync_WrongPassword_ReturnsNull()
        {
            var user = new User
            {
                UserName = "fail@test.com",
                Password = "123",
                FirstName = "A",
                LastName = "B"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var loginAttempt = new LoginDTO("fail@test.com","wrong");

            var result = await _repository.Login(loginAttempt);

            Assert.Null(result);
        }
        [Fact]
        public async Task RegisterAsync_DuplicateEmail_ThrowsException()
        {
            var user1 = new User { UserName = "dup@test.com", Password = "123" };
            var user2 = new User { UserName = "dup@test.com", Password = "456" };

            await _repository.AddUser(user1);

            await Assert.ThrowsAsync<DbUpdateException>(() =>
                _repository.AddUser(user2));
        }


        #endregion

    } 
}