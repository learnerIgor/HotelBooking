using Microsoft.EntityFrameworkCore;
using Booking.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Persistence.EntityTypeConfigurations.Payments
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.PaymentId);
            builder.Property(e => e.PaymentId).HasDefaultValueSql("NEWID()");

            builder.Property(e => e.PaymentDate).IsRequired();
            builder.Property(e => e.Amount).IsRequired();
        }
    }
}
