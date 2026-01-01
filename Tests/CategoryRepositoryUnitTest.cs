using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository;


namespace Tests
{
    public class CategoryRepositoryUnitTest:TestBase
    {
        #region HappyTests
        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Cakes" },
                new Category { CategoryId = 2, CategoryName = "Cookies" }
            };

            var mockContext =
                GetMockContext<WebApiShop_215602996Context, Category>(categories, c => c.Categories);

            var repo = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repo.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Cakes", result[0].CategoryName);
        }
        #endregion
        #region UnHappyTests
        [Fact]
        public async Task GetCategories_EmptyList_ReturnsEmpty()
        {
            // Arrange
            var mockContext =
                GetMockContext<WebApiShop_215602996Context, Category>(
                    new List<Category>(), c => c.Categories);

            var repo = new CategoryRepository(mockContext.Object);

            // Act
            var result = await repo.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        #endregion
    }
}
