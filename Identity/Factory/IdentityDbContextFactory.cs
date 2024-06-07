using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Identity.Data;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    private readonly IConfiguration _configuration;

    public IdentityDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbIdentity"));
        return new IdentityDbContext(optionsBuilder.Options);
    }
}
/*
 *  var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.Development.json", optional: true)
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbIdentity"));

            return new RestaurantDbContext(optionsBuilder.Options);
 */