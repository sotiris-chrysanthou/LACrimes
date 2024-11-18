using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class AreaConfiguration : IEntityTypeConfiguration<Area> {
        public void Configure(EntityTypeBuilder<Area> builder) {
            // Set table name
            builder.ToTable("Area");

            // Set primary key
            builder.HasKey(a => a.ID);

            // Set properties
            builder.Property(a => a.ID).HasColumnName("ID").IsRequired();
            builder.Property(a => a.Code).HasColumnName("Code").IsRequired();
            builder.Property(a => a.Name).HasColumnName("Name").IsRequired().HasMaxLength(40);

            // Set navigation properties
            builder.HasMany(a => a.SubAreas)
                .WithOne(sa => sa.Area)
                .HasForeignKey(sa => sa.AreaID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
