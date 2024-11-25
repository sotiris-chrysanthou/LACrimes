using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.EF.Configuration;
using LACrimes.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace LACrimes.EF.Context {
    public class LACrimeDbContext : DbContext {

        private readonly bool _onlyForTest;

        public LACrimeDbContext(bool onlyForTest = false) : base() {
            _onlyForTest = onlyForTest;
        }

        public DbSet<Area> Areas { get; set; } = null!;
        public DbSet<Coordinates> Coordinates { get; set; } = null!;
        public DbSet<Crime> Crimes { get; set; } = null!;
        public DbSet<CrimeRecord> CrimesRecords { get; set; } = null!;
        public DbSet<Premis> Premis { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Street> Streets { get; set; } = null!;
        public DbSet<SubArea> SubAreas { get; set; } = null!;
        public DbSet<Victim> Victims { get; set; } = null!;
        public DbSet<Weapon> Weapons { get; set; } = null!;
        public DbSet<CrimeSeverity> CrimeSeverities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new CoordinatesConfiguration());
            modelBuilder.ApplyConfiguration(new CrimeConfiguration());
            modelBuilder.ApplyConfiguration(new CrimeRecordConfiguration());
            modelBuilder.ApplyConfiguration(new PremisConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new StreetConfiguration());
            modelBuilder.ApplyConfiguration(new SubAreaConfiguration());
            modelBuilder.ApplyConfiguration(new VictimConfiguration());
            modelBuilder.ApplyConfiguration(new WeaponConfiguration());
            modelBuilder.ApplyConfiguration(new CrimeSeverityConfiguration());
            foreach(var entity in modelBuilder.Model.GetEntityTypes()) {
                // Set table name to lowercase
                entity.SetTableName(entity.GetTableName()?.ToLower());

                // Set column names to lowercase
                foreach(var property in entity.GetProperties()) {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }

                // Set key names to lowercase
                foreach(var key in entity.GetKeys()) {
                    key.SetName(key.GetName()?.ToLower());
                }

                // Set foreign key names to lowercase
                foreach(var foreignKey in entity.GetForeignKeys()) {
                    foreignKey.SetConstraintName(foreignKey.GetConstraintName()?.ToLower());
                }

                // Set index names to lowercase
                foreach(var index in entity.GetIndexes()) {
                    index.SetDatabaseName(index.GetDatabaseName()?.ToLower());
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            if(_onlyForTest) {
                TestConfiguring(optionsBuilder);
                return;
            }
            Configuring(optionsBuilder);
        }

        private static void Configuring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("Host=localhost;Database=LACrimeDb;Username=postgres;Password=1997");
            }
        }

        private static void TestConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(optionsBuilder.IsConfigured) {
                return;
            }
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");

        }
    }
}
