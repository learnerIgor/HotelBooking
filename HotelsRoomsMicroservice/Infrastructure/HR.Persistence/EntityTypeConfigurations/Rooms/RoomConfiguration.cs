using HR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.EntityTypeConfigurations.Rooms
{
    public class RoomConfiguration: IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.RoomId);
            builder.Property(e => e.RoomId).HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Floor).HasMaxLength(3).IsRequired();
            builder.Property(e => e.Number).HasMaxLength(4).IsRequired();

            builder.HasMany(e => e.Amenities)
                .WithOne(e => e.Room)
                .HasForeignKey(e => e.RoomId);

            builder.Navigation(e => e.Amenities).AutoInclude();
        }
    }
}
