using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class CoordinatesConfiguration : IEntityTypeConfiguration<Coordinates> {
        public void Configure(EntityTypeBuilder<Coordinates> builder) {
            // Set table name
            //builder.ToTable("Coordinates");

            // Set primary key
            builder.HasKey(c => c.ID);

            // Set properties
            builder.Property(c => c.ID).IsRequired();
            builder.Property(c => c.Lat).IsRequired().HasPrecision(7, 4);
            builder.Property(c => c.Lon).IsRequired().HasPrecision(7, 4);

            // Set navigation properties
            builder.HasMany(c => c.CrimeRecords)
                .WithOne(crmR => crmR.Coordinates)
                .HasForeignKey(crmR => crmR.CoordinatesID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
