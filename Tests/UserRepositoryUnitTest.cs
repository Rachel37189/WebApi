using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Moq;
using Repository;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Tests
{
    public class UserRepositoryUnitTest:TestBase
    {
        #region HappyTests
        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            // Arrange
            var user = new User {Id = 1, UserName = "test@test.com", Password = "1234" };
            var users = new List<User> { user };

            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async Task addUser_AddsUser()
        {
            // Arrange
            var users = new List<User>();
            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);
           

            var repo = new UserRepository(mockContext.Object);
            var user = new User { UserName = "new@test.com",FirstName="aaa",LastName="bbb", Password = "123" };

            // Act
            var result = await repo.addUser(user);

            // Assert

            Assert.Equal("new@test.com", result.UserName);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task updateUser_UpdatesUser()
        {
            // Arrange
            var user = new User { Id = 1, UserName = "old@test.com", Password = "1" };
            var users = new List<User> { user };

            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var repo = new UserRepository(mockContext.Object);
            user.UserName = "updated@test.com";

            // Act
            await repo.updateUser(1, user);

            // Assert
            Assert.Equal("updated@test.com", users[0].UserName);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new User { UserName = "login@test.com", Password = "pass" };
            var users = new List<User> { user };

            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.login(new User { UserName = "login@test.com", Password = "pass" });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async Task login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var users = new List<User>
        {
            new User { UserName = "real@test.com", Password = "1234" }
        };

            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.login(new User { UserName= "fake@test.com", Password = "0000" });

            // Assert
            Assert.Null(result);
        }
        #endregion
        #region UnHappyTsts
        [Fact]
        public async Task GetUserById_NotExistingId_ReturnsNull()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = 1, UserName = "a@a.com" }
    };

            var mockContext =
                GetMockContext<WebApiShop_215602996Context, User>(users, c => c.Users);

            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.GetUserById(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Login_WrongPassword_ReturnsNull()
        {
            // Arrange
            var users = new List<User>
    {
        new User { UserName = "test@test.com", Password = "1234" }
    };

            var mockContext =
                GetMockContext<WebApiShop_215602996Context, User>(users, c => c.Users);

            var repo = new UserRepository(mockContext.Object);

            // Act
            var result = await repo.login(new User
            {
                UserName = "test@test.com",
                Password = "WRONG"
            });

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Register_NullUser_ThrowsException()
        {
            // Arrange
            var mockContext = new Mock<WebApiShop_215602996Context>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            var repo = new UserRepository(mockContext.Object);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => repo.addUser(null)
            );
        }

        #endregion
    }
}
