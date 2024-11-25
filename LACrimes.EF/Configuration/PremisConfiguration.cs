using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class PremisConfiguration : IEntityTypeConfiguration<Premis> {
        public void Configure(EntityTypeBuilder<Premis> builder) {
            // Set table name
            //builder.ToTable("Premis");

            // Set primary key
            builder.HasKey(p => p.ID);

            // Set properties
            builder.Property(p => p.ID).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.Desc).HasMaxLength(100);


            // Set navigation properties
            builder.HasMany(p => p.CrimeRecords)
                .WithOne(crmR => crmR.Premis)
                .HasForeignKey(crmR => crmR.PremisID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
