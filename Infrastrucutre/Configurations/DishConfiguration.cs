using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucutre.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Price)
                    .HasColumnType("decimal(18,2)");

            builder.HasOne(d => d.Restaurant)
                   .WithMany(r => r.Dishes)
                   .HasForeignKey(d => d.RestaurantId);

            builder.HasData(
                new Dish
                {
                    Id = 1,
                    Name = "Nashville Hot Chicken",
                    Description = "Spicy fried chicken inspired by the flavors of Nashville.",
                    Price = 10.30M,
                    RestaurantId = 1
                },
                new Dish
                {
                    Id = 2,
                    Name = "Chicken Nuggets",
                    Description = "Crispy and juicy chicken nuggets, perfect for snacking.",
                    Price = 5.30M,
                    RestaurantId = 1
                },
                new Dish
                {
                    Id = 3,
                    Name = "Nashville Hot Chicken",
                    Description = "Spicy fried chicken inspired by the flavors of Nashville.",
                    Price = 10.30M,
                    RestaurantId = 2
                },
                new Dish
                {
                    Id = 4,
                    Name = "Chicken Nuggets",
                    Description = "Crispy and juicy chicken nuggets, perfect for snacking.",
                    Price = 5.30M,
                    RestaurantId = 2
                }
            );
        }
    }
}
