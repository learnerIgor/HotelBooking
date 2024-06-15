using HR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.EntityTypeConfigurations.Rooms
{
    public class AmenityRoomConfiguration : IEntityTypeConfiguration<AmenityRoom>
    {
        public void Configure(EntityTypeBuilder<AmenityRoom> builder)
        {
            builder.HasKey(e => new { e.AmenityId, e.RoomId });

            builder.HasOne(e => e.Room)
                .WithMany(e => e.Amenities)
                .HasForeignKey(e => e.RoomId);

            builder.HasOne(e => e.Amenity)
                .WithMany(e => e.Rooms)
                .HasForeignKey(e => e.AmenityId);
        }
    }
}