using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Configuration {
    public class CrimeSeverityConfiguration : IEntityTypeConfiguration<CrimeSeverity> {

        public void Configure(EntityTypeBuilder<CrimeSeverity> builder) {
            // Set table name
            //builder.ToTable("CrimeSeverity");

            // Set primary key
            builder.HasKey(a => a.ID);

            // Set properties
            builder.Property(cs => cs.ID).IsRequired();
            builder.Property(cs => cs.Severity).IsRequired();

            // Set navigation properties
            builder.HasOne(cs => cs.Crime)
                .WithMany(crm => crm.CrimeSeverities)
                .HasForeignKey(cs => cs.CrimeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Set navigation properties
            builder.HasOne(cs => cs.CrimeRecord)
                .WithMany(crmR => crmR.CrimeSeverities)
                .HasForeignKey(cs => cs.CrimeRecordID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
