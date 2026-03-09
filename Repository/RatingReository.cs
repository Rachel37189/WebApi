using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RatingRepository : IRatingRepository
    {

        WebApiShop_215602996Context _webApiShopContext;
        public RatingRepository(WebApiShop_215602996Context webApiShopContext)
        {
            _webApiShopContext = webApiShopContext;
        }



        public async Task<Rating> AddRating(Rating newRating)
        {
            await _webApiShopContext.Ratings.AddAsync(newRating);
            await _webApiShopContext.SaveChangesAsync();
            return newRating;
        }
    }
}