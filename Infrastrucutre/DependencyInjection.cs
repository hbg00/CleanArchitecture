using Application.Contracts.Persistence;
using Application.Persistence.Contracts;
using Application.Persistence.Contracts.Common;
using Infrastrucutre.Data;
using Infrastrucutre.Repository;
using Infrastrucutre.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastrucutre
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Db")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }

        
    }
}
