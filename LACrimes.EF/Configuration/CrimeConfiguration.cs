using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class CrimeConfiguration : IEntityTypeConfiguration<Crime> {
        public void Configure(EntityTypeBuilder<Crime> builder) {
            // Set table name
            builder.ToTable("Crime");

            // Set primary key
            builder.HasKey(c => c.ID);

            // Set properties
            builder.Property(c => c.ID).HasColumnName("Id").IsRequired();
            builder.Property(c => c.Code).HasColumnName("Code").IsRequired();
            builder.Property(c => c.Desc).HasColumnName("Desc").IsRequired().HasMaxLength(100);

            // Set navigation properties
            builder.HasMany(c => c.CrimeRecordsCrime1)
                .WithOne(crmR => crmR.Crime1)
                .HasForeignKey(crmR => crmR.Crime1ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Set navigation properties
            builder.HasMany(c => c.CrimeRecordsCrime2)
                .WithOne(crmR => crmR.Crime2)
                .HasForeignKey(crmR => crmR.Crime1ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Set navigation properties
            builder.HasMany(c => c.CrimeRecordsCrime3)
                .WithOne(crmR => crmR.Crime3)
                .HasForeignKey(crmR => crmR.Crime1ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
