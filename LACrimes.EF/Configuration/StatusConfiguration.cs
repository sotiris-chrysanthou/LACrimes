using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class StatusConfiguration : IEntityTypeConfiguration<Status> {
        public void Configure(EntityTypeBuilder<Status> builder) {
            // Set table name
            builder.ToTable("Status");

            // Set primary key
            builder.HasKey(s => s.ID);

            // Set properties
            builder.Property(s => s.ID).HasColumnName("ID").IsRequired();
            builder.Property(s => s.Code).HasColumnName("Code").IsRequired().HasMaxLength(2);
            builder.Property(s => s.Desc).HasColumnName("Desc").IsRequired().HasMaxLength(20);

            // Set navigation properties
            builder.HasMany(s => s.CrimeRecords)
                .WithOne(crmR => crmR.Status)
                .HasForeignKey(crmR => crmR.StatusID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
