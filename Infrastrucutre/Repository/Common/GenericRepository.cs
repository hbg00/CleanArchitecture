using Application.Persistence.Contracts.Common;
using Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RestaurantDbContext _restaurantDbContext;

        public GenericRepository(RestaurantDbContext restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _restaurantDbContext.AddAsync(entity);
            await _restaurantDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _restaurantDbContext.Set<T>().Remove(entity);
            await _restaurantDbContext.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetAll()
        {
            return await _restaurantDbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _restaurantDbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _restaurantDbContext.Entry(entity).State = EntityState.Modified;
            await _restaurantDbContext.SaveChangesAsync();
        }
    }
}
