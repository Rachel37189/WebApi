using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Tests
{
    public class TestBase
    {
        protected Mock<TContext> GetMockContext<TContext, TEntity>(
        List<TEntity> data,
        Expression<Func<TContext, DbSet<TEntity>>> dbSetSelector
    ) where TContext : DbContext where TEntity : class
        {
            var mockContext = new Mock<TContext>();
            mockContext.Setup(dbSetSelector).ReturnsDbSet(data);
            return mockContext;
        }
    }
}
