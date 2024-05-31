using Application.Persistence.Contracts;
using Domain.Entity;
using Infrastrucutre.Data;
using Infrastrucutre.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Repository
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public RestaurantRepository(RestaurantDbContext restaurantDbContext) : base(restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }

        public override async Task<IReadOnlyList<Restaurant>> GetAll()
        {
            return await _restaurantDbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToListAsync();
        }
        public override async Task<Restaurant> GetById(int id)
        {
            return await _restaurantDbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
