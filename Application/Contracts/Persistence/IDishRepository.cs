using Application.Persistence.Contracts.Common;
using Domain.Entity;

namespace Application.Persistence.Contracts
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        Task<IReadOnlyList<Dish>> GetDishesBelongToRestaurant(int restaurantId);
        Task<Dish> GetDishDetails(int restaurantId, int dishId);
    }
}