using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucutre.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Dishes)
                   .WithOne(d => d.Restaurant)
                   .HasForeignKey(d => d.RestaurantId);

            builder.HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    ContactName = "John Doe",
                    HasDelivery = true,
                    AddressId = 1
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    ContactName = "Jane Smith",
                    HasDelivery = true,
                    AddressId = 2
                }
            );
        }
    }
}
