using Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityTypeConfigurations.Auth
{
    public class RefreshTokenTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(i => i.RefreshTokenId);

            builder.Property(e => e.ApplicationUserId).IsRequired();

            builder.HasOne(u => u.ApplicationUser).WithMany().HasForeignKey(u => u.ApplicationUserId);
        }
    }
}
