using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class WeaponConfiguration : IEntityTypeConfiguration<Weapon> {
        public void Configure(EntityTypeBuilder<Weapon> builder) {
            // Set table name
            //builder.ToTable("Weapon");

            // Set primary key
            builder.HasKey(a => a.ID);

            // Set properties
            builder.Property(a => a.ID).IsRequired();
            builder.Property(a => a.Code).IsRequired();
            builder.Property(a => a.Desc).IsRequired().HasMaxLength(100);

            // Set navigation properties
            builder.HasMany(a => a.CrimeRecords)
                .WithOne(crmR => crmR.Weapon)
                .HasForeignKey(crmR => crmR.WeaponID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
