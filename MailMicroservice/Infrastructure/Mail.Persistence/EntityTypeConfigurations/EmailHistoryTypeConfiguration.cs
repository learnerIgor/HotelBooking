using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mail.Domain;

namespace Mail.Persistence.EntityTypeConfigurations
{
    public class EmailHistoryTypeConfiguration : IEntityTypeConfiguration<EmailHistory>
    {
        public void Configure(EntityTypeBuilder<EmailHistory> builder)
        {
            builder.HasKey(e => e.EmailHistoryId);
            builder.Property(e => e.EmailHistoryId).HasDefaultValueSql("NEWID()");

            builder.Property(e => e.ApplicationUserId).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(40).IsRequired();
            builder.Property(e => e.SendDate).IsRequired();
        }
    }
}
