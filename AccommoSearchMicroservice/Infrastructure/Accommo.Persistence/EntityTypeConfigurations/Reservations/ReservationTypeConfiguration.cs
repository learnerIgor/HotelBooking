using Accommo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accommo.Persistence.EntityTypeConfigurations.Reservations
{
    public class ReservationTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(e => e.ReservationId);

            builder.Property(r => r.CheckInDate).IsRequired();
            builder.Property(r => r.CheckOutDate).IsRequired();

            builder.Navigation(e => e.Room).AutoInclude();
        }
    }
}
