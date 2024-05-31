using Application.Persistence.Contracts;
using Domain.Entity;
using Infrastrucutre.Data;
using Infrastrucutre.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Repository
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public DishRepository(RestaurantDbContext restaurantDbContext) : base(restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }

        public async Task<IReadOnlyList<Dish>> GetDishesBelongToRestaurant(int restaurantId)
        {
            return await _restaurantDbContext.Dishes
                .Where(d => d.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Dish> GetDishDetails(int restaurantId, int dishId)
        {
            return await _restaurantDbContext.Dishes
                .Where(d => d.RestaurantId == restaurantId && d.Id == dishId)
                .FirstOrDefaultAsync();
        }
    }
}
