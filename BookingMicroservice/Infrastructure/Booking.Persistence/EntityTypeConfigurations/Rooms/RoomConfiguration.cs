using Booking.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Persistence.EntityTypeConfigurations.Rooms
{
    public class RoomConfiguration: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.RoomId);

            builder.Property(e => e.Floor).HasMaxLength(3).IsRequired();
            builder.Property(e => e.Number).HasMaxLength(4).IsRequired();

            builder.Navigation(e => e.RoomType).AutoInclude();
            builder.Navigation(e => e.Hotel).AutoInclude();
        }
    }
}
