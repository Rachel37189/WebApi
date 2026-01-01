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
    public class OrderRepositoryUnitTest:TestBase
    {
        #region HappyTests
        [Fact]
        public async Task GetOrderById_ExistingId_ReturnsOrder()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { OrderId = 1, UserId = 3 }
            };

            var mockContext =
                GetMockContext<WebApiShop_215602996Context, Order>(orders, c => c.Orders);

            var repo = new OrderRepository(mockContext.Object);

            // Act
            var result = await repo.GetOrderById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.OrderId);
        }

        [Fact]
        public async Task AddOrder_ValidOrder_ReturnsOrder()
        {
            // Arrange
            var orders = new List<Order>();

            var mockContext = GetMockContext<WebApiShop_215602996Context, Order>(orders, c => c.Orders);

            var repo = new OrderRepository(mockContext.Object);

            var order = new Order { OrderId = 1, UserId = 2 };

            // Act
            var result = await repo.addOrder(order);

            // Assert
            Assert.Equal(1, result.OrderId);
            Assert.NotNull(result);
        }
        #endregion
        #region UnHappyTests
        [Fact]
        public async Task GetOrderById_NotExistingId_ReturnsNull()
        {
            // Arrange
            var orders = new List<Order>
             {
                new Order { OrderId = 1, UserId = 3 }
             };

            var mockContext =
                GetMockContext<WebApiShop_215602996Context, Order>(orders, c => c.Orders);

            var repo = new OrderRepository(mockContext.Object);

            // Act
            var result = await repo.GetOrderById(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task AddOrder_NullOrder_ThrowsException()
        {
            // Arrange
            var mockContext =
                GetMockContext<WebApiShop_215602996Context, Order>(
                    new List<Order>(), c => c.Orders);

            var repo = new OrderRepository(mockContext.Object);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => repo.addOrder(null)
            );
        }

        #endregion

    }

}

