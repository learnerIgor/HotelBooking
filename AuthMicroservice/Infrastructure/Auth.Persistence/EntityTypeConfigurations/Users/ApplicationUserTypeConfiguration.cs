using Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityTypeConfigurations.Users
{
    public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.ApplicationUserId);

            builder.Property(u => u.Login).HasMaxLength(50).IsRequired();

            builder.HasMany(e => e.Roles);

            builder.Navigation(e => e.Roles).AutoInclude();
        }
    }
}
