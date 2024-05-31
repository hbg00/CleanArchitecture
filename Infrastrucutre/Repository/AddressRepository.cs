using Application.Contracts.Persistence;
using Domain.Entity;
using Infrastrucutre.Data;
using Infrastrucutre.Repository.Common;

namespace Infrastrucutre.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public AddressRepository(RestaurantDbContext restaurantDbContext) : base(restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }
    }
}
