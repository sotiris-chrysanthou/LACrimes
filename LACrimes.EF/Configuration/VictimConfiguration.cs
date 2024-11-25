using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class VictimConfiguration : IEntityTypeConfiguration<Victim> {
        public void Configure(EntityTypeBuilder<Victim> builder) {
            // Set table name
            //builder.ToTable("Victim");

            // Set primary key
            builder.HasKey(v => v.ID);

            // Set properties
            builder.Property(v => v.ID).IsRequired();
            builder.Property(v => v.Sex).HasMaxLength(1);
            builder.Property(v => v.Age).IsRequired();
            builder.Property(v => v.Descent).HasMaxLength(1);

            // Set navigation properties
            builder.HasMany(v => v.CrimeRecords)
                .WithOne(crmR => crmR.Victim)
                .HasForeignKey(crmR => crmR.VictimID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
