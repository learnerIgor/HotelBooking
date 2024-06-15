using Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityTypeConfigurations.Users
{
    public class ApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasKey(r => r.ApplicationUserRoleId);

            builder.Property(n => n.Name).HasMaxLength(20).IsRequired();

            builder.HasMany(u => u.Users);
        }
    }
}
