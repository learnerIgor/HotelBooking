﻿using HR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.EntityTypeConfigurations.Addres
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId);
            builder.Property(e => e.CityId).HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            builder.Navigation(r => r.Country).AutoInclude();
        }
    }
}
