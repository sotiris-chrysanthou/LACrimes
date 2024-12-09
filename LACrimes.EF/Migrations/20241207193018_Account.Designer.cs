﻿// <auto-generated />
using System;
using LACrimes.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LACrimes.EF.Migrations
{
    [DbContext(typeof(LACrimeDbContext))]
    [Migration("20241207193018_Account")]
    partial class Account
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LACrimes.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("userame");

                    b.HasKey("Id")
                        .HasName("pk_accounts");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("LACrimes.Model.Area", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_areas");

                    b.ToTable("areas");
                });

            modelBuilder.Entity("LACrimes.Model.Coordinates", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<double>("Lat")
                        .HasPrecision(7, 4)
                        .HasColumnType("double precision")
                        .HasColumnName("lat");

                    b.Property<double>("Lon")
                        .HasPrecision(7, 4)
                        .HasColumnType("double precision")
                        .HasColumnName("lon");

                    b.HasKey("ID")
                        .HasName("pk_coordinates");

                    b.ToTable("coordinates");
                });

            modelBuilder.Entity("LACrimes.Model.Crime", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("desc");

                    b.HasKey("ID")
                        .HasName("pk_crimes");

                    b.ToTable("crimes");
                });

            modelBuilder.Entity("LACrimes.Model.CrimeRecord", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CoordinatesID")
                        .HasColumnType("uuid")
                        .HasColumnName("coordinatesid");

                    b.Property<Guid?>("CrossStreetID")
                        .HasColumnType("uuid")
                        .HasColumnName("crossstreetid");

                    b.Property<DateTime>("DateOcc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dateocc");

                    b.Property<DateTime>("DateRptd")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("daterptd");

                    b.Property<string>("DrNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("drno");

                    b.Property<Guid?>("PremisID")
                        .HasColumnType("uuid")
                        .HasColumnName("premisid");

                    b.Property<Guid?>("StatusID")
                        .HasColumnType("uuid")
                        .HasColumnName("statusid");

                    b.Property<Guid?>("StreetID")
                        .HasColumnType("uuid")
                        .HasColumnName("streetid");

                    b.Property<Guid?>("SubAreaID")
                        .HasColumnType("uuid")
                        .HasColumnName("subareaid");

                    b.Property<TimeOnly>("TimeOcc")
                        .HasColumnType("time without time zone")
                        .HasColumnName("timeocc");

                    b.Property<Guid?>("VictimID")
                        .HasColumnType("uuid")
                        .HasColumnName("victimid");

                    b.Property<Guid?>("WeaponID")
                        .HasColumnType("uuid")
                        .HasColumnName("weaponid");

                    b.HasKey("ID")
                        .HasName("pk_crimesrecords");

                    b.HasIndex("CoordinatesID")
                        .HasDatabaseName("ix_crimesrecords_coordinatesid");

                    b.HasIndex("CrossStreetID")
                        .HasDatabaseName("ix_crimesrecords_crossstreetid");

                    b.HasIndex("DrNo")
                        .IsUnique()
                        .HasDatabaseName("ix_crimesrecords_drno");

                    b.HasIndex("ID")
                        .IsUnique()
                        .HasDatabaseName("ix_crimesrecords_id");

                    b.HasIndex("PremisID")
                        .HasDatabaseName("ix_crimesrecords_premisid");

                    b.HasIndex("StatusID")
                        .HasDatabaseName("ix_crimesrecords_statusid");

                    b.HasIndex("StreetID")
                        .HasDatabaseName("ix_crimesrecords_streetid");

                    b.HasIndex("SubAreaID")
                        .HasDatabaseName("ix_crimesrecords_subareaid");

                    b.HasIndex("VictimID")
                        .HasDatabaseName("ix_crimesrecords_victimid");

                    b.HasIndex("WeaponID")
                        .HasDatabaseName("ix_crimesrecords_weaponid");

                    b.ToTable("crimesrecords");
                });

            modelBuilder.Entity("LACrimes.Model.CrimeSeverity", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CrimeID")
                        .HasColumnType("uuid")
                        .HasColumnName("crimeid");

                    b.Property<Guid>("CrimeRecordID")
                        .HasColumnType("uuid")
                        .HasColumnName("crimerecordid");

                    b.Property<int>("Severity")
                        .HasColumnType("integer")
                        .HasColumnName("severity");

                    b.HasKey("ID")
                        .HasName("pk_crimeseverities");

                    b.HasIndex("CrimeID")
                        .HasDatabaseName("ix_crimeseverities_crimeid");

                    b.HasIndex("CrimeRecordID")
                        .HasDatabaseName("ix_crimeseverities_crimerecordid");

                    b.ToTable("crimeseverities");
                });

            modelBuilder.Entity("LACrimes.Model.Premis", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    b.Property<string>("Desc")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("desc");

                    b.HasKey("ID")
                        .HasName("pk_premis");

                    b.ToTable("premis");
                });

            modelBuilder.Entity("LACrimes.Model.Status", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("code");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("desc");

                    b.HasKey("ID")
                        .HasName("pk_statuses");

                    b.ToTable("statuses");
                });

            modelBuilder.Entity("LACrimes.Model.Street", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_streets");

                    b.ToTable("streets");
                });

            modelBuilder.Entity("LACrimes.Model.SubArea", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AreaID")
                        .HasColumnType("uuid")
                        .HasColumnName("areaid");

                    b.Property<string>("RpdDistNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rpddistno");

                    b.HasKey("ID")
                        .HasName("pk_subareas");

                    b.HasIndex("AreaID")
                        .HasDatabaseName("ix_subareas_areaid");

                    b.ToTable("subareas");
                });

            modelBuilder.Entity("LACrimes.Model.Victim", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("Descent")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("descent");

                    b.Property<string>("Sex")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("sex");

                    b.HasKey("ID")
                        .HasName("pk_victims");

                    b.ToTable("victims");
                });

            modelBuilder.Entity("LACrimes.Model.Weapon", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("desc");

                    b.HasKey("ID")
                        .HasName("pk_weapons");

                    b.ToTable("weapons");
                });

            modelBuilder.Entity("LACrimes.Model.CrimeRecord", b =>
                {
                    b.HasOne("LACrimes.Model.Coordinates", "Coordinates")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("CoordinatesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_crimesrecords_coordinates_coordinatesid");

                    b.HasOne("LACrimes.Model.Street", "CrossStreet")
                        .WithMany("CrimeRecordsCrossStreet")
                        .HasForeignKey("CrossStreetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_streets_crossstreetid");

                    b.HasOne("LACrimes.Model.Premis", "Premis")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("PremisID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_premis_premisid");

                    b.HasOne("LACrimes.Model.Status", "Status")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_statuses_statusid");

                    b.HasOne("LACrimes.Model.Street", "Street")
                        .WithMany("CrimeRecordsStreet")
                        .HasForeignKey("StreetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_streets_streetid");

                    b.HasOne("LACrimes.Model.SubArea", "SubArea")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("SubAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_subareas_subareaid");

                    b.HasOne("LACrimes.Model.Victim", "Victim")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("VictimID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_victims_victimid");

                    b.HasOne("LACrimes.Model.Weapon", "Weapon")
                        .WithMany("CrimeRecords")
                        .HasForeignKey("WeaponID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_crimesrecords_weapons_weaponid");

                    b.Navigation("Coordinates");

                    b.Navigation("CrossStreet");

                    b.Navigation("Premis");

                    b.Navigation("Status");

                    b.Navigation("Street");

                    b.Navigation("SubArea");

                    b.Navigation("Victim");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("LACrimes.Model.CrimeSeverity", b =>
                {
                    b.HasOne("LACrimes.Model.Crime", "Crime")
                        .WithMany("CrimeSeverities")
                        .HasForeignKey("CrimeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_crimeseverities_crimes_crimeid");

                    b.HasOne("LACrimes.Model.CrimeRecord", "CrimeRecord")
                        .WithMany("CrimeSeverities")
                        .HasForeignKey("CrimeRecordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_crimeseverities_crimesrecords_crimerecordid");

                    b.Navigation("Crime");

                    b.Navigation("CrimeRecord");
                });

            modelBuilder.Entity("LACrimes.Model.SubArea", b =>
                {
                    b.HasOne("LACrimes.Model.Area", "Area")
                        .WithMany("SubAreas")
                        .HasForeignKey("AreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_subareas_areas_areaid");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("LACrimes.Model.Area", b =>
                {
                    b.Navigation("SubAreas");
                });

            modelBuilder.Entity("LACrimes.Model.Coordinates", b =>
                {
                    b.Navigation("CrimeRecords");
                });

            modelBuilder.Entity("LACrimes.Model.Crime", b =>
                {
                    b.Navigation("CrimeSeverities");
                });

            modelBuilder.Entity("LACrimes.Model.CrimeRecord", b =>
                {
                    b.Navigation("CrimeSeverities");
                });

            modelBuilder.Entity("LACrimes.Model.Premis", b =>
                {
                    b.Navigation("CrimeRecords");
                });

            modelBuilder.Entity("LACrimes.Model.Status", b =>
                {
                    b.Navigation("CrimeRecords");
                });

            modelBuilder.Entity("LACrimes.Model.Street", b =>
                {
                    b.Navigation("CrimeRecordsCrossStreet");

                    b.Navigation("CrimeRecordsStreet");
                });

            modelBuilder.Entity("LACrimes.Model.SubArea", b =>
                {
                    b.Navigation("CrimeRecords");
                });

            modelBuilder.Entity("LACrimes.Model.Victim", b =>
                {
                    b.Navigation("CrimeRecords");
                });

            modelBuilder.Entity("LACrimes.Model.Weapon", b =>
                {
                    b.Navigation("CrimeRecords");
                });
#pragma warning restore 612, 618
        }
    }
}