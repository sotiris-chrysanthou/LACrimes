using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LACrimes.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coordinates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lat = table.Column<double>(type: "double precision", precision: 7, scale: 4, nullable: false),
                    lon = table.Column<double>(type: "double precision", precision: 7, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coordinates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "crimes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crimes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "premis",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_premis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    desc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "streets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_streets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "victims",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    sex = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    descent = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_victims", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "weapons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    desc = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_weapons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subareas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rpddistno = table.Column<string>(type: "text", nullable: false),
                    areaid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subareas", x => x.id);
                    table.ForeignKey(
                        name: "fk_subareas_areas_areaid",
                        column: x => x.areaid,
                        principalTable: "areas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crimesrecords",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    drno = table.Column<string>(type: "text", nullable: false),
                    daterptd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dateocc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    timeocc = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    subareaid = table.Column<Guid>(type: "uuid", nullable: true),
                    victimid = table.Column<Guid>(type: "uuid", nullable: true),
                    premisid = table.Column<Guid>(type: "uuid", nullable: true),
                    statusid = table.Column<Guid>(type: "uuid", nullable: true),
                    weaponid = table.Column<Guid>(type: "uuid", nullable: true),
                    streetid = table.Column<Guid>(type: "uuid", nullable: true),
                    crossstreetid = table.Column<Guid>(type: "uuid", nullable: true),
                    coordinatesid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crimesrecords", x => x.id);
                    table.ForeignKey(
                        name: "fk_crimesrecords_coordinates_coordinatesid",
                        column: x => x.coordinatesid,
                        principalTable: "coordinates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_premis_premisid",
                        column: x => x.premisid,
                        principalTable: "premis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_statuses_statusid",
                        column: x => x.statusid,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_streets_crossstreetid",
                        column: x => x.crossstreetid,
                        principalTable: "streets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_streets_streetid",
                        column: x => x.streetid,
                        principalTable: "streets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_subareas_subareaid",
                        column: x => x.subareaid,
                        principalTable: "subareas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_victims_victimid",
                        column: x => x.victimid,
                        principalTable: "victims",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimesrecords_weapons_weaponid",
                        column: x => x.weaponid,
                        principalTable: "weapons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "crimeseverities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    severity = table.Column<int>(type: "integer", nullable: false),
                    crimeid = table.Column<Guid>(type: "uuid", nullable: false),
                    crimerecordid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crimeseverities", x => x.id);
                    table.ForeignKey(
                        name: "fk_crimeseverities_crimes_crimeid",
                        column: x => x.crimeid,
                        principalTable: "crimes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crimeseverities_crimesrecords_crimerecordid",
                        column: x => x.crimerecordid,
                        principalTable: "crimesrecords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_crimeseverities_crimeid",
                table: "crimeseverities",
                column: "crimeid");

            migrationBuilder.CreateIndex(
                name: "ix_crimeseverities_crimerecordid",
                table: "crimeseverities",
                column: "crimerecordid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_coordinatesid",
                table: "crimesrecords",
                column: "coordinatesid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_crossstreetid",
                table: "crimesrecords",
                column: "crossstreetid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_drno",
                table: "crimesrecords",
                column: "drno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_id",
                table: "crimesrecords",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_premisid",
                table: "crimesrecords",
                column: "premisid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_statusid",
                table: "crimesrecords",
                column: "statusid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_streetid",
                table: "crimesrecords",
                column: "streetid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_subareaid",
                table: "crimesrecords",
                column: "subareaid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_victimid",
                table: "crimesrecords",
                column: "victimid");

            migrationBuilder.CreateIndex(
                name: "ix_crimesrecords_weaponid",
                table: "crimesrecords",
                column: "weaponid");

            migrationBuilder.CreateIndex(
                name: "ix_subareas_areaid",
                table: "subareas",
                column: "areaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crimeseverities");

            migrationBuilder.DropTable(
                name: "crimes");

            migrationBuilder.DropTable(
                name: "crimesrecords");

            migrationBuilder.DropTable(
                name: "coordinates");

            migrationBuilder.DropTable(
                name: "premis");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "streets");

            migrationBuilder.DropTable(
                name: "subareas");

            migrationBuilder.DropTable(
                name: "victims");

            migrationBuilder.DropTable(
                name: "weapons");

            migrationBuilder.DropTable(
                name: "areas");
        }
    }
}
