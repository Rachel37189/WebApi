using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Tests
{
    public class DbFixture : IDisposable
    {
        public WebApiShop_215602996Context Context { get; }

        public DbFixture()
        {
            var options = new DbContextOptionsBuilder<WebApiShop_215602996Context>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Shop_Test;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            Context = new WebApiShop_215602996Context(options);

            // יצירת DB נקי
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            // סוגרים בסוף כל מחלקת טסטים
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
    }
