using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LACrimes.EF.Configuration {
    public class CrimeRecordConfiguration : IEntityTypeConfiguration<CrimeRecord> {
        public void Configure(EntityTypeBuilder<CrimeRecord> builder) {
            // Set table name
            builder.ToTable("CrimeRecord");

            // Set primary key
            builder.HasKey(crmR => crmR.ID);

            // Set properties
            builder.Property(crmR => crmR.ID).HasColumnName("ID").IsRequired();
            builder.Property(crmR => crmR.DrNo).HasColumnName("DrNo").IsRequired();
            builder.Property(crmR => crmR.DateRptd).HasColumnName("DateRptd").IsRequired();
            builder.Property(crmR => crmR.DateOcc).HasColumnName("DateOcc").IsRequired();

            builder.HasIndex(crmR => crmR.ID).IsUnique();
            builder.HasIndex(crmR => crmR.DrNo).IsUnique();

            // Set navigation properties
            builder.HasOne(crmR => crmR.SubArea)
                .WithMany(sa => sa.CrimeRecords)
                .HasForeignKey(crmR => crmR.SubAreaID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Victim)
                .WithMany(v => v.CrimeRecords)
                .HasForeignKey(crmR => crmR.VictimID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Premis)
                .WithMany(p => p.CrimeRecords)
                .HasForeignKey(crmR => crmR.PremisID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Status)
                .WithMany(s => s.CrimeRecords)
                .HasForeignKey(crmR => crmR.StatusID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Weapon)
                .WithMany(w => w.CrimeRecords)
                .HasForeignKey(crmR => crmR.WeaponID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Street)
                .WithMany(s => s.CrimeRecordsStreet)
                .HasForeignKey(crmR => crmR.StreetID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.CrossStreet)
                .WithMany(cs => cs.CrimeRecordsCrossStreet)
                .HasForeignKey(crmR => crmR.CrossStreetID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Coordinates)
                .WithMany(c => c.CrimeRecords)
                .HasForeignKey(crmR => crmR.CoordinatesID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Crime1)
                .WithMany(crm => crm.CrimeRecordsCrime1)
                .HasForeignKey(crmR => crmR.Crime1ID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Crime2)
                .WithMany(crm => crm.CrimeRecordsCrime2)
                .HasForeignKey(crmR => crmR.Crime2ID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(crmR => crmR.Crime3)
                .WithMany(crm => crm.CrimeRecordsCrime3)
                .HasForeignKey(crmR => crmR.Crime3ID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
