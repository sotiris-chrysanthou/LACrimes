using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class SubAreaConfiguration : IEntityTypeConfiguration<SubArea> {
        public void Configure(EntityTypeBuilder<SubArea> builder) {
            // Set table name
            builder.ToTable("SubArea");

            // Set primary key
            builder.HasKey(sa => sa.ID);

            // Set properties
            builder.Property(sa => sa.ID).HasColumnName("ID").IsRequired();
            builder.Property(sa => sa.RpdDistNo).HasColumnName("RpdDistNo").IsRequired();

            //Set navigation properties
            builder.HasMany(sa => sa.CrimeRecords)
                .WithOne(crmR => crmR.SubArea)
                .HasForeignKey(crmR => crmR.SubAreaID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
