using Accommo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommo.Persistence.EntityTypeConfigurations.Hotels
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(e => e.HotelId);

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(200).IsRequired();

            builder
                .HasMany(e => e.Rooms)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId);

            builder.Navigation(r => r.Rooms).AutoInclude();
            builder.Navigation(r => r.Address).AutoInclude();
        }
    }
}
