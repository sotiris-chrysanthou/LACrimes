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
            //builder.ToTable("CrimeRecord");

            // Set primary key
            builder.HasKey(crmR => crmR.ID);

            // Set properties
            builder.Property(crmR => crmR.ID).IsRequired();
            builder.Property(crmR => crmR.DrNo).IsRequired();
            builder.Property(crmR => crmR.DateRptd).IsRequired();
            builder.Property(crmR => crmR.DateOcc).IsRequired();
            builder.Property(crmR => crmR.TimeOcc).IsRequired();

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

            builder.HasMany(crmR => crmR.CrimeSeverities)
                .WithOne(crmS => crmS.CrimeRecord)
                .HasForeignKey(crmS => crmS.CrimeRecordID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
