using HR.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.EntityTypeConfigurations.Addres
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.CountryId);
            builder.Property(e => e.CountryId).HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        }
    }
}
