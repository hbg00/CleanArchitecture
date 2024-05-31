using Application.Persistence.Contracts.Common;
using Domain.Entity;

namespace Application.Contracts.Persistence
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
    }
}