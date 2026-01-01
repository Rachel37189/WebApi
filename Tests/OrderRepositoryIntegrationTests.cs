using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Tests
{
    public class OrderRepositoryIntegrationTests : IClassFixture<DbFixture>
    {

        private readonly WebApiShop_215602996Context _context;
        private readonly OrderRepository _repository;

        public OrderRepositoryIntegrationTests(DbFixture fixture)
        {
            _context = fixture.Context;
            _repository = new OrderRepository(_context);
            _context.ChangeTracker.Clear();
        }
        #region HappyTests
        [Fact]
        public async Task AddOrder_ShouldSaveOrderToDatabase()
        {
            // Arrange
            var user = new User { UserName = "test@user.com", Password = "456", FirstName = "Efrat", LastName = "Leibovitz" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var order = new Order { OrderSum = 100, UserId = user.Id };

            // Act
            var result = await _repository.addOrder(order);

            // Assert
            var orderInDb = await _context.Orders.FindAsync(result.OrderId);
            Assert.NotNull(orderInDb);
            Assert.Equal(100, orderInDb.OrderSum);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnCorrectOrder()
        {
            // Arrange
            var user = new User { UserName = "efrat@user.com", Password = "Ee123!@#WWW", FirstName = "A", LastName = "B" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var order = new Order { OrderSum = 200, UserId = user.Id }; // שים לב ל‑UserId
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetOrderById(order.OrderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.OrderSum);
        }
        #endregion
        #region UnHappyTests
        [Fact]
        public async Task GetOrderById_OrderDoesNotExist_ReturnsNull()
        {
            var result = await _repository.GetOrderById(9999);

            Assert.Null(result);
        }
        [Fact]
        public async Task AddOrder_WithoutUser_ThrowsException()
        {
            var order = new Order
            {
                OrderSum = 100,
                UserId = 999 // לא קיים
            };

            await Assert.ThrowsAsync<DbUpdateException>(() =>
                _repository.addOrder(order));
        }
        [Fact]
        public async Task AddOrder_NegativeSum_SavedButInvalidBusinessCase()
        {
            // Arrange
            var user = new User
            {
                UserName = "sum@test.com",
                Password = "123",
                FirstName = "A",
                LastName = "B"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                OrderSum = -50,
                UserId = user.Id
            };

            // Act
            var result = await _repository.addOrder(order);

            // Assert
            Assert.True(result.OrderSum < 0);
            #endregion
        }
    }
}
