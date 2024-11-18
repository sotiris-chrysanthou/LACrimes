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
            builder.ToTable("Victim");

            // Set primary key
            builder.HasKey(v => v.ID);

            // Set properties
            builder.Property(v => v.ID).HasColumnName("ID").IsRequired();
            builder.Property(v => v.Sex).HasColumnName("Sex").IsRequired();
            builder.Property(v => v.Age).HasColumnName("Age").IsRequired();
            builder.Property(v => v.Descent).HasColumnName("Descent").IsRequired();

            // Set navigation properties
            builder.HasMany(v => v.CrimeRecords)
                .WithOne(crmR => crmR.Victim)
                .HasForeignKey(crmR => crmR.VictimID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
