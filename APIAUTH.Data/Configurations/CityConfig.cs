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
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> b)
        {
            b.ToTable("Cities");
            b.HasKey(x => x.Id); // geonameid

            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Lat).HasColumnType("numeric(9,6)");
            b.Property(x => x.Lng).HasColumnType("numeric(9,6)");

            b.HasIndex(x => new { x.CountryId, x.ProvinceId, x.Name });

            b.HasOne(x => x.Country)
             .WithMany()
             .HasForeignKey(x => x.CountryId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Province)
             .WithMany(s => s.Cities)
             .HasForeignKey(x => x.ProvinceId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
