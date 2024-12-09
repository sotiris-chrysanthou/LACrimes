using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.EF.Configuration {
    public class AccountConfiguration : IEntityTypeConfiguration<Account> {
        public void Configure(EntityTypeBuilder<Account> builder) {
            // Set table name
            builder.ToTable("Accounts");

            // Set primary key
            builder.HasKey(a => a.ID);

            // Set properties
            builder.Property(a => a.ID).HasColumnName("Id").IsRequired();
            builder.Property(a => a.Username).HasColumnName("Userame").IsRequired().HasMaxLength(20);
            builder.Property(a => a.Password).HasColumnName("Password").IsRequired().HasMaxLength(20);
            builder.Property(a => a.Role).HasColumnName("Role").IsRequired().HasConversion<string>();
        }
    }
}
