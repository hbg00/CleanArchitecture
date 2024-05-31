using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrucutre.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasData(
                new Address
                {
                    Id = 1,
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
                new Address
                {
                    Id = 2,
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                }
            );
        }
    }
}
