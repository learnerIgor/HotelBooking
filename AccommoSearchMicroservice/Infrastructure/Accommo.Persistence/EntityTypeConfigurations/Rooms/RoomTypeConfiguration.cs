using Accommo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommo.Persistence.EntityTypeConfigurations.Rooms
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(e => e.RoomTypeId);


            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.BaseCost).IsRequired();

            builder
                .HasMany(e => e.Rooms)
                .WithOne(e => e.RoomType)
                .HasForeignKey(e => e.RoomTypeId);
        }
    }
}