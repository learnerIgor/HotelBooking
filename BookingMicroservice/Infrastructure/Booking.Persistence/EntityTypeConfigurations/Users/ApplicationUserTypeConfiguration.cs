using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain;

namespace Booking.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.ApplicationUserId);

        builder.Property(e => e.Login).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired();

        builder.Navigation(e => e.Reservations).AutoInclude();
    }
}