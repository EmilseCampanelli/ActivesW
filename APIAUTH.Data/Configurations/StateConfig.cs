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
    public class StateConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> b)
        {
            b.ToTable("Province");
            b.HasKey(x => x.Id);

            b.Property(x => x.Code).IsRequired().HasMaxLength(15); // "AR.24"
            b.Property(x => x.Name).IsRequired().HasMaxLength(150);

            b.HasIndex(x => new { x.CountryId, x.Code }).IsUnique();

            b.HasMany(x => x.Cities)
             .WithOne(c => c.Province!)
             .HasForeignKey(c => c.ProvinceId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
