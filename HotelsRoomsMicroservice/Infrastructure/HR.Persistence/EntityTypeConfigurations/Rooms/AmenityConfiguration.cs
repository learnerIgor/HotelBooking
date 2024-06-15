using HR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.EntityTypeConfigurations.Rooms
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.HasKey(e => e.AmenityId);

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(e => e.Rooms)
                .WithOne(e => e.Amenity)
                .HasForeignKey(e => e.AmenityId);
        }
    }
}