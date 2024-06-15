using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Booking.Domain;

namespace Booking.Persistence.EntityTypeConfigurations.Addres
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.AddressId);

            builder.Property(e => e.Street).HasMaxLength(50).IsRequired();
            builder.Property(e => e.HouseNumber).HasMaxLength(10).IsRequired();
            builder.Property(e => e.Latitude).IsRequired();
            builder.Property(e => e.Longitude).IsRequired();

            builder.Navigation(r => r.City).AutoInclude();
        }
    }
}
