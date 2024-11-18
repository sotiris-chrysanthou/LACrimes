using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class StreetConfiguration : IEntityTypeConfiguration<Street> {
        public void Configure(EntityTypeBuilder<Street> builder) {
            // Set table name
            builder.ToTable("Street");

            // Set primary key
            builder.HasKey(s => s.ID);

            // Set properties
            builder.Property(s => s.ID).HasColumnName("ID").IsRequired();
            builder.Property(s => s.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);

            // Set navigation properties
            builder.HasMany(s => s.CrimeRecordsStreet)
                .WithOne(crmR => crmR.Street)
                .HasForeignKey(crmR => crmR.StreetID)
                .OnDelete(DeleteBehavior.Cascade);  

            builder.HasMany(cs => cs.CrimeRecordsCrossStreet)
                .WithOne(crmR => crmR.CrossStreet)
                .HasForeignKey(crmR => crmR.CrossStreetID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
