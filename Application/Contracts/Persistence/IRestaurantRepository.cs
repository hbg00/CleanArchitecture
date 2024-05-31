using Application.Persistence.Contracts.Common;
using Domain.Entity;

namespace Application.Persistence.Contracts
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
    }
}