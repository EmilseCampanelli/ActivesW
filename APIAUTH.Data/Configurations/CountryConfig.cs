using APIAUTH.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Data.Configurations
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> b)
        {
            b.ToTable("Countries");
            b.HasKey(x => x.Id);

            b.Property(x => x.Iso2).IsRequired().HasMaxLength(2);
            b.Property(x => x.Iso3).IsRequired().HasMaxLength(3);
            b.Property(x => x.Name).IsRequired().HasMaxLength(150);
            b.Property(x => x.Region).IsRequired().HasMaxLength(50);

            b.HasIndex(x => x.Iso2).IsUnique();
            b.HasIndex(x => x.Iso3).IsUnique();

            b.HasMany(x => x.Province)
             .WithOne(s => s.Country)
             .HasForeignKey(s => s.CountryId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
