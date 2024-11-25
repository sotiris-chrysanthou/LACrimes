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
            //builder.ToTable("Crime");

            // Set primary key
            builder.HasKey(c => c.ID);

            // Set properties
            builder.Property(c => c.ID).IsRequired();
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.Desc).IsRequired().HasMaxLength(100);

            // Set navigation properties
            builder.HasMany(c => c.CrimeSeverities)
                .WithOne(crmS => crmS.Crime)
                .HasForeignKey(crmS => crmS.CrimeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
